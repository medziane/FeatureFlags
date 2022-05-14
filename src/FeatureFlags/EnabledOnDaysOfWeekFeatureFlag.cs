namespace Med.FeatureFlags;

/// <summary>
/// A feature flag that is enabled on the specified days of the week.
/// </summary>
public class EnabledOnDaysOfWeekFeatureFlag :
    FeatureFlag
{
    /// <summary>
    /// Creates a new instance of the <see cref="EnabledOnDaysOfWeekFeatureFlag"/> class.
    /// </summary>
    /// <param name="daysOfWeek">The days of the week on which this feature flag is enabled.</param>
    public EnabledOnDaysOfWeekFeatureFlag(params DayOfWeek[] daysOfWeek)
        : base(() => EvaluateEnabled(daysOfWeek))
    {
        EvaluateEnabledAndScheduleNextCheck(daysOfWeek, CancellationToken.None);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="EnabledOnDaysOfWeekFeatureFlag"/> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    /// <param name="daysOfWeek">The days of the week on which this feature flag is enabled.</param>
    public EnabledOnDaysOfWeekFeatureFlag(string id, params DayOfWeek[] daysOfWeek)
        : base(id, () => EvaluateEnabled(daysOfWeek))
    {
        EvaluateEnabledAndScheduleNextCheck(daysOfWeek, CancellationToken.None);
    }

    /// <summary>
    /// Evaluates whether or not this feature flag is enabled.
    /// </summary>
    /// <param name="daysOfWeek">The days of the week on which this feature flag is enabled.</param>
    /// <returns><c>true</c> if this feature flag is enabled today. Otherwise, <c>false</c>.</returns>
    protected static bool EvaluateEnabled(IEnumerable<DayOfWeek> daysOfWeek)
    {
        return EvaluateEnabledOnCertainDay(daysOfWeek, DateTime.Now.DayOfWeek);
    }

    /// <summary>
    /// Evaluates whether or not this feature flag is enabled on the specified day of the week.
    /// </summary>
    /// <param name="daysOfWeek">The days of the week on which this feature flag is enabled.</param>
    /// <param name="targetDay">The day being checked.</param>
    /// <returns><c>true</c> if this feature flag is enabled on <paramref name="targetDay"/>. Otherwise, <c>false</c>.</returns>
    protected static bool EvaluateEnabledOnCertainDay(IEnumerable<DayOfWeek> daysOfWeek, DayOfWeek targetDay)
    {
        return daysOfWeek.Contains(targetDay);
    }

    /// <summary>
    /// Evaluates whether or not this feature flag is enabled and schedules the next check.
    /// </summary>
    /// <param name="daysOfWeek">The days of the week on which this feature flag is enabled.</param>
    /// <param name="cancellationToken">A component for notification that operations should be canceled.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected virtual Task EvaluateEnabledAndScheduleNextCheck(
        DayOfWeek[] daysOfWeek,
        CancellationToken cancellationToken)
    {
        Enabled = EvaluateEnabled(daysOfWeek);

        if (daysOfWeek.Length == 0)
            return Task.CompletedTask;

        try
        {
            var timeUntilNextPredicate = TimeUntilEvaluationChange(daysOfWeek);
            return Task.Delay(timeUntilNextPredicate, cancellationToken)
                .ContinueWith(
                    _ => EvaluateEnabledAndScheduleNextCheck(daysOfWeek, cancellationToken),
                    cancellationToken);
        }
        catch (Exception)
        {
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// Determines the time until midnight, when the day change occurs.
    /// </summary>
    /// <returns>The time until midnight.</returns>
    protected virtual TimeSpan TimeUntilDayOfWeekChange()
    {
        var now = DateTime.Now;
        var tomorrow = DateTime.Today.AddDays(1);
        return tomorrow - now;
    }

    /// <summary>
    /// Determines the time until the next day of the week, when the state of the feature flag will be different than today's.
    /// </summary>
    /// <param name="daysOfWeek">The days of the week on which this feature flag is enabled.</param>
    /// <returns>The time until the next evaluation returns a different result than today.</returns>
    /// <exception cref="InvalidOperationException">Thrown when unable to determine the next evaluation change.</exception>
    protected virtual TimeSpan TimeUntilEvaluationChange(DayOfWeek[] daysOfWeek)
    {
        var today = DateTime.Today.DayOfWeek;
        var stateOfToday = EvaluateEnabledOnCertainDay(daysOfWeek, today);

        for (var day = 1; day < 7; day++)
        {
            var nextDay = DateTime.Today.AddDays(day).DayOfWeek;
            var stateOfNextDay = daysOfWeek.Contains(nextDay);
            if (stateOfToday != stateOfNextDay) return DateTime.Today.AddDays(day) - DateTime.Now;
        }

        throw new InvalidOperationException("Could not determine time until evaluation change.");
    }
}

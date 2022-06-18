namespace Med.FeatureFlags;

/// <summary>
///     A feature flag that is enabled every day during the specified period of time.
/// </summary>
public class EnabledDailyFeatureFlag :
    FeatureFlag
{
    /// <summary>
    ///     Creates a new instance of the <see cref="EnabledDailyFeatureFlag" /> class.
    /// </summary>
    /// <param name="timePeriod">The time period during which this feature flag is enabled.</param>
    public EnabledDailyFeatureFlag(IRange<TimeOnly> timePeriod)
        : base(() => EvaluateEnabled(timePeriod))
    {
        EvaluateEnabledAndScheduleNextCheck(timePeriod, CancellationToken.None);
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="EnabledDailyFeatureFlag" /> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    /// <param name="timePeriod">The time period during which this feature flag is enabled.</param>
    public EnabledDailyFeatureFlag(string id, IRange<TimeOnly> timePeriod)
        : base(id, () => EvaluateEnabled(timePeriod))
    {
        EvaluateEnabledAndScheduleNextCheck(timePeriod, CancellationToken.None);
    }

    /// <summary>
    ///     Evaluates whether or not this feature flag is enabled.
    /// </summary>
    /// <param name="timePeriod">The time period during which this feature flag is enabled.</param>
    /// <returns><c>true</c> if this feature flag is enabled right now. Otherwise, <c>false</c>.</returns>
    protected static bool EvaluateEnabled(IRange<TimeOnly> timePeriod)
    {
        return timePeriod.Contains(TimeOnly.FromDateTime(DateTime.Now));
    }

    /// <summary>
    ///     Evaluates whether or not this feature flag is enabled and schedules the next check.
    /// </summary>
    /// <param name="timePeriod">The time period during which this feature flag is enabled.</param>
    /// <param name="cancellationToken">A component for notification that operations should be canceled.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected virtual Task EvaluateEnabledAndScheduleNextCheck(
        IRange<TimeOnly> timePeriod,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(timePeriod);

        var now = DateTime.Now;
        var timeOnlyNow = TimeOnly.FromDateTime(now);
        var tomorrow = DateOnly.FromDateTime(now.AddDays(1));

        Enabled = EvaluateEnabled(timePeriod);

        var timeUntilNextEvaluation = timePeriod.Maximum < timeOnlyNow
            ? tomorrow.ToDateTime(timePeriod.Minimum) - now
            : timeOnlyNow < timePeriod.Minimum
                ? timePeriod.Minimum - timeOnlyNow
                : timePeriod.Maximum - timeOnlyNow;

        return Task.Delay(timeUntilNextEvaluation, cancellationToken).ContinueWith(
            _ => EvaluateEnabledAndScheduleNextCheck(timePeriod, cancellationToken), cancellationToken);
    }
}

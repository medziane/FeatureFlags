namespace Med.FeatureFlags;

/// <summary>
///     A feature flag that is enabled outside a given date period.
/// </summary>
public class EnabledOutsideDatePeriodFeatureFlag :
    FeatureFlag
{
    /// <summary>
    ///     Create a new instance of the <see cref="EnabledOutsideDatePeriodFeatureFlag" /> class.
    /// </summary>
    /// <param name="datePeriod">The date range representing the period in which this feature flag is disabled.</param>
    public EnabledOutsideDatePeriodFeatureFlag(IRange<DateOnly> datePeriod)
        : base(() => EvaluateEnabled(datePeriod))
    {
        ArgumentNullException.ThrowIfNull(datePeriod);
        EvaluateEnabledAndScheduleNextCheck(datePeriod, CancellationToken.None);
    }

    /// <summary>
    ///     Create a new instance of the <see cref="EnabledOutsideDatePeriodFeatureFlag" /> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    /// <param name="datePeriod">The date range representing the period in which this feature flag is disabled.</param>
    public EnabledOutsideDatePeriodFeatureFlag(string id, IRange<DateOnly> datePeriod)
        : base(id, () => EvaluateEnabled(datePeriod))
    {
        ArgumentNullException.ThrowIfNull(datePeriod);
        EvaluateEnabledAndScheduleNextCheck(datePeriod, CancellationToken.None);
    }

    /// <summary>
    ///     Evaluates whether or not this feature flag is enabled.
    /// </summary>
    /// <param name="datePeriod">The date range representing the period in which this feature flag is disabled.</param>
    /// <returns><c>true</c> if this feature flag is enabled today. Otherwise, <c>false</c>.</returns>
    protected static bool EvaluateEnabled(IRange<DateOnly> datePeriod)
    {
        ArgumentNullException.ThrowIfNull(datePeriod);
        return !datePeriod.Contains(DateOnly.FromDateTime(DateTime.Now));
    }

    /// <summary>
    ///     Evaluates whether or not this feature flag is enabled and schedules the next check.
    /// </summary>
    /// <param name="datePeriod">The date range representing the period in which this feature flag is disabled.</param>
    /// <param name="cancellationToken">A component for notification that operations should be canceled.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected virtual Task EvaluateEnabledAndScheduleNextCheck(
        IRange<DateOnly> datePeriod,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(datePeriod);

        var now = DateTime.Now;
        var today = DateOnly.FromDateTime(now);

        Enabled = EvaluateEnabled(datePeriod);

        if (datePeriod.Maximum < today)
            return Task.CompletedTask;

        if (today < datePeriod.Minimum)
        {
            var timeUntilNextEvaluation = datePeriod.Minimum.ToDateTime() - now;
            return Task.Delay(timeUntilNextEvaluation, cancellationToken).ContinueWith(
                _ => EvaluateEnabledAndScheduleNextCheck(datePeriod, cancellationToken), cancellationToken);
        }
        else
        {
            var timeUntilNextEvaluation = datePeriod.Maximum.ToDateTime() - now;
            return Task.Delay(timeUntilNextEvaluation, cancellationToken).ContinueWith(
                _ => EvaluateEnabledAndScheduleNextCheck(datePeriod, cancellationToken), cancellationToken);
        }
    }
}

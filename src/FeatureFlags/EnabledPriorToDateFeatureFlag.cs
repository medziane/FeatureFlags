namespace Med.FeatureFlags;

/// <summary>
///     A feature flag that is enabled prior to a given date.
/// </summary>
public class EnabledPriorToDateFeatureFlag :
    FeatureFlag
{
    /// <summary>
    ///     Creates a new instance of the <see cref="EnabledPriorToDateFeatureFlag" /> class.
    /// </summary>
    /// <param name="cutoffDate">The day on which the feature flag is no longer enabled.</param>
    public EnabledPriorToDateFeatureFlag(DateOnly cutoffDate)
        : base(() => EvaluateEnabled(cutoffDate))
    {
        EvaluateEnabledAndScheduleNextCheck(cutoffDate, CancellationToken.None);
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="EnabledPriorToDateFeatureFlag" /> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    /// <param name="cutoffDate">The day on which the feature flag is no longer enabled.</param>
    public EnabledPriorToDateFeatureFlag(string id, DateOnly cutoffDate)
        : base(id, () => EvaluateEnabled(cutoffDate))
    {
        EvaluateEnabledAndScheduleNextCheck(cutoffDate, CancellationToken.None);
    }

    /// <summary>
    ///     Evaluates whether or not this feature flag is enabled.
    /// </summary>
    /// <param name="cutoffDate">The day on which the feature flag is no longer enabled.</param>
    /// <returns><c>true</c> if the <paramref name="cutoffDate" /> is in the future. Otherwise, <c>false</c>.</returns>
    protected static bool EvaluateEnabled(DateOnly cutoffDate)
    {
        return DateOnly.FromDateTime(DateTime.Now) < cutoffDate;
    }

    /// <summary>
    ///     Evaluates whether or not this feature flag is enabled and schedules the next check.
    /// </summary>
    /// <param name="cutoffDate">The day on which the feature flag is no longer enabled.</param>
    /// <param name="cancellationToken">A component for notification that operations should be canceled.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected virtual Task EvaluateEnabledAndScheduleNextCheck(
        DateOnly cutoffDate,
        CancellationToken cancellationToken)
    {
        var timeToCutoff = cutoffDate.ToDateTime(TimeOnly.FromTimeSpan(TimeSpan.Zero)) - DateTime.Now;
        Enabled = TimeSpan.Zero < timeToCutoff;

        return Enabled
            ? Task.Delay(timeToCutoff, cancellationToken)
                .ContinueWith(_ => EvaluateEnabledAndScheduleNextCheck(cutoffDate, cancellationToken),
                    cancellationToken)
            : Task.CompletedTask;
    }
}

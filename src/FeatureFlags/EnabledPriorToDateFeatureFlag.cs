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
        EvaluateEnabledAndScheduleNextCheck(cutoffDate.ToDateTime(new TimeOnly(0)), CancellationToken.None);
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="EnabledPriorToDateFeatureFlag" /> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    /// <param name="cutoffDate">The day on which the feature flag is no longer enabled.</param>
    public EnabledPriorToDateFeatureFlag(string id, DateOnly cutoffDate)
        : base(id, () => EvaluateEnabled(cutoffDate))
    {
        EvaluateEnabledAndScheduleNextCheck(cutoffDate.ToDateTime(new TimeOnly(0)), CancellationToken.None);
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="EnabledPriorToDateFeatureFlag" /> class.
    /// </summary>
    /// <param name="cutoffTime">The point in time in which the feature flag is no longer enabled.</param>
    public EnabledPriorToDateFeatureFlag(DateTime cutoffTime)
        : base(() => EvaluateEnabled(cutoffTime))
    {
        EvaluateEnabledAndScheduleNextCheck(cutoffTime, CancellationToken.None);
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="EnabledPriorToDateFeatureFlag" /> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    /// <param name="cutoffTime">The point in time in which the feature flag is no longer enabled.</param>
    public EnabledPriorToDateFeatureFlag(string id, DateTime cutoffTime)
        : base(id, () => EvaluateEnabled(cutoffTime))
    {
        EvaluateEnabledAndScheduleNextCheck(cutoffTime, CancellationToken.None);
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="EnabledPriorToDateFeatureFlag" /> class.
    /// </summary>
    /// <param name="cutoffTime">The point in time in which the feature flag is no longer enabled.</param>
    public EnabledPriorToDateFeatureFlag(DateTimeOffset cutoffTime)
        : base(() => EvaluateEnabled(cutoffTime))
    {
        EvaluateEnabledAndScheduleNextCheck(cutoffTime, CancellationToken.None);
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="EnabledPriorToDateFeatureFlag" /> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    /// <param name="cutoffTime">The point in time in which the feature flag is no longer enabled.</param>
    public EnabledPriorToDateFeatureFlag(string id, DateTimeOffset cutoffTime)
        : base(id, () => EvaluateEnabled(cutoffTime))
    {
        EvaluateEnabledAndScheduleNextCheck(cutoffTime, CancellationToken.None);
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
    ///     Evaluates whether or not this feature flag is enabled.
    /// </summary>
    /// <param name="cutoffTime">The point in time in which the feature flag is no longer enabled.</param>
    /// <returns><c>true</c> if the <paramref name="cutoffTime" /> is in the future. Otherwise, <c>false</c>.</returns>
    protected static bool EvaluateEnabled(DateTimeOffset cutoffTime)
    {
        return DateTimeOffset.Now < cutoffTime;
    }

    /// <summary>
    ///     Evaluates whether or not this feature flag is enabled and schedules the next check.
    /// </summary>
    /// <param name="cutoffTime">The point in time in which the feature flag is no longer enabled.</param>
    /// <param name="cancellationToken">A component for notification that operations should be canceled.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected virtual Task EvaluateEnabledAndScheduleNextCheck(
        DateTimeOffset cutoffTime,
        CancellationToken cancellationToken)
    {
        var timeToCutoff = cutoffTime - DateTimeOffset.Now;
        Enabled = TimeSpan.Zero < timeToCutoff;

        return Enabled
            ? Task.Delay(timeToCutoff, cancellationToken).ContinueWith(_ => EvaluateEnabledAndScheduleNextCheck(cutoffTime, cancellationToken), cancellationToken)
            : Task.CompletedTask;
    }
}

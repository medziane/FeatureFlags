namespace Med.FeatureFlags;

/// <summary>
///     A feature flag that is enabled starting from a given date.
/// </summary>
public class EnabledStartingFromMomentInTimeFeatureFlag :
    FeatureFlag
{
    /// <summary>
    ///     Creates a new instance of the <see cref="EnabledStartingFromDateFeatureFlag" /> class.
    /// </summary>
    /// <param name="startTime">The point in time in which the feature flag becomes enabled.</param>
    public EnabledStartingFromMomentInTimeFeatureFlag(DateTime startTime)
        : base(() => EvaluateEnabled(startTime))
    {
        EvaluateEnabledAndScheduleNextCheck(startTime, CancellationToken.None);
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="EnabledStartingFromDateFeatureFlag" /> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    /// <param name="startTime">The point in time in which the feature flag becomes enabled.</param>
    public EnabledStartingFromMomentInTimeFeatureFlag(string id, DateTime startTime)
        : base(id, () => EvaluateEnabled(startTime))
    {
        EvaluateEnabledAndScheduleNextCheck(startTime, CancellationToken.None);
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="EnabledStartingFromDateFeatureFlag" /> class.
    /// </summary>
    /// <param name="startTime">The point in time in which the feature flag becomes enabled.</param>
    public EnabledStartingFromMomentInTimeFeatureFlag(DateTimeOffset startTime)
        : base(() => EvaluateEnabled(startTime))
    {
        EvaluateEnabledAndScheduleNextCheck(startTime, CancellationToken.None);
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="EnabledStartingFromDateFeatureFlag" /> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    /// <param name="startTime">The point in time in which the feature flag becomes enabled.</param>
    public EnabledStartingFromMomentInTimeFeatureFlag(string id, DateTimeOffset startTime)
        : base(id, () => EvaluateEnabled(startTime))
    {
        EvaluateEnabledAndScheduleNextCheck(startTime, CancellationToken.None);
    }

    /// <summary>
    ///     Evaluates whether or not this feature flag is enabled.
    /// </summary>
    /// <param name="startTime">The point in time in which the feature flag becomes enabled.</param>
    /// <returns><c>true</c> if the <paramref name="startTime" /> is in the past. Otherwise, <c>false</c>.</returns>
    protected static bool EvaluateEnabled(DateTimeOffset startTime)
    {
        return startTime <= DateTimeOffset.Now;
    }

    /// <summary>
    ///     Evaluates whether or not this feature flag is enabled and schedules the next check.
    /// </summary>
    /// <param name="startTime">The point in time in which the feature flag becomes enabled.</param>
    /// <param name="cancellationToken">A component for notification that operations should be canceled.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected virtual Task EvaluateEnabledAndScheduleNextCheck(
        DateTimeOffset startTime,
        CancellationToken cancellationToken)
    {
        var timeUntilStart = startTime - DateTimeOffset.Now;
        Enabled = timeUntilStart <= TimeSpan.Zero;

        return Enabled
            ? Task.Delay(timeUntilStart, cancellationToken)
                .ContinueWith(_ => EvaluateEnabledAndScheduleNextCheck(startTime, cancellationToken), cancellationToken)
            : Task.CompletedTask;
    }
}
namespace Med.FeatureFlags;

/// <summary>
///     A feature flag that is enabled prior to a given date.
/// </summary>
public class EnabledPriorToDateFeatureFlag :
    EnabledPriorToMomentInTimeFeatureFlag
{
    /// <summary>
    ///     Creates a new instance of the <see cref="EnabledPriorToDateFeatureFlag" /> class.
    /// </summary>
    /// <param name="cutoffDate">The day on which the feature flag is no longer enabled.</param>
    public EnabledPriorToDateFeatureFlag(DateOnly cutoffDate)
        : base(cutoffDate.ToDateTime(TimeOnly.FromTimeSpan(TimeSpan.Zero)))
    {
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="EnabledPriorToDateFeatureFlag" /> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    /// <param name="cutoffDate">The day on which the feature flag is no longer enabled.</param>
    public EnabledPriorToDateFeatureFlag(string id, DateOnly cutoffDate)
        : base(id, cutoffDate.ToDateTime(TimeOnly.FromTimeSpan(TimeSpan.Zero)))
    {
    }
}

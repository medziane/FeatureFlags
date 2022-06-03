namespace Med.FeatureFlags;

/// <summary>
///     A feature flag that is enabled starting from a given date.
/// </summary>
/// <seealso cref="EnabledStartingFromMomentInTimeFeatureFlag" />
public class EnabledStartingFromDateFeatureFlag :
    EnabledStartingFromMomentInTimeFeatureFlag
{
    /// <summary>
    ///     Creates a new instance of the <see cref="EnabledStartingFromDateFeatureFlag" /> class.
    /// </summary>
    /// <param name="startDate">The day on which the feature flag becomes enabled.</param>
    public EnabledStartingFromDateFeatureFlag(DateOnly startDate)
        : base(startDate.ToDateTime(TimeOnly.FromTimeSpan(TimeSpan.Zero)))
    {
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="EnabledStartingFromDateFeatureFlag" /> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    /// <param name="startDate">The day on which the feature flag becomes enabled.</param>
    public EnabledStartingFromDateFeatureFlag(string id, DateOnly startDate)
        : base(id, startDate.ToDateTime(TimeOnly.FromTimeSpan(TimeSpan.Zero)))
    {
    }
}

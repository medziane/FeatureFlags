namespace Med.FeatureFlags;

/// <summary>
///     A feature flag that is enabled when the given feature flag is also enabled, and vice versa.
/// </summary>
public class EntanglementFeatureFlag :
    EnabledWhenAllEnabledFeatureFlag
{
    /// <summary>
    ///     Creates a new instance of the <see cref="EntanglementFeatureFlag" /> class.
    /// </summary>
    /// <param name="featureFlag">The feature flag, the state of which is to be shared by this one.</param>
    public EntanglementFeatureFlag(IFeatureFlag featureFlag)
        : base(featureFlag)
    {
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="EntanglementFeatureFlag" /> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    /// <param name="featureFlag">The feature flag, the state of which is to be shared by this one.</param>
    public EntanglementFeatureFlag(string id, IFeatureFlag featureFlag)
        : base(id, featureFlag)
    {
    }
}
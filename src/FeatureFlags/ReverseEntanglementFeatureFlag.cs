namespace Med.FeatureFlags;

/// <summary>
///     A feature flag that is enabled when the given feature flag is disabled, and vice versa.
/// </summary>
public class ReverseEntanglementFeatureFlag :
    EnabledWhenAllDisabledFeatureFlag
{
    /// <summary>
    ///     Creates a new instance of the <see cref="ReverseEntanglementFeatureFlag" /> class.
    /// </summary>
    /// <param name="featureFlag">The feature flag to be opposed by this one.</param>
    public ReverseEntanglementFeatureFlag(IFeatureFlag featureFlag)
        : base(featureFlag)
    {
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="ReverseEntanglementFeatureFlag" /> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    /// <param name="featureFlag">The feature flag to be opposed by this one.</param>
    public ReverseEntanglementFeatureFlag(string id, IFeatureFlag featureFlag)
        : base(id, featureFlag)
    {
    }
}
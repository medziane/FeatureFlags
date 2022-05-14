namespace Med.FeatureFlags;

/// <summary>
/// A feature flag that is enabled when the given feature flag is disabled, and vice versa.
/// </summary>
public class ReverseEnabledFeatureFlag :
    EnabledWhenAllDisabledFeatureFlag
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReverseEnabledFeatureFlag"/> class.
    /// </summary>
    /// <param name="featureFlag">The feature flag to be opposed by this one.</param>
    public ReverseEnabledFeatureFlag(IFeatureFlag featureFlag)
        : base(featureFlag)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReverseEnabledFeatureFlag"/> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    /// <param name="featureFlag">The feature flag to be opposed by this one.</param>
    public ReverseEnabledFeatureFlag(string id, IFeatureFlag featureFlag)
        : base(id, featureFlag)
    {
    }
}

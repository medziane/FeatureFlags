namespace Med.FeatureFlags;

/// <summary>
///     Exposes a property that indicates whether the feature is enabled.
/// </summary>
public interface IFeatureFlaggable
{
    /// <summary>
    ///     Gets a value indicating whether the feature is enabled.
    /// </summary>
    IFeatureFlag FeatureFlag { get; }
}

namespace Med.FeatureFlags;

/// <summary>
///     A feature flag that is always enabled.
/// </summary>
public class AlwaysEnabledFeatureFlag :
    FeatureFlag
{
    /// <summary>
    ///     Creates a new instance of the <see cref="AlwaysEnabledFeatureFlag" /> class.
    /// </summary>
    public AlwaysEnabledFeatureFlag()
        : base(true)
    {
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="AlwaysEnabledFeatureFlag" /> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    public AlwaysEnabledFeatureFlag(string id)
        : base(id, true)
    {
    }
}

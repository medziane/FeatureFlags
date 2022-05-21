namespace Med.FeatureFlags;

/// <summary>
///     A feature flag that is always disabled.
/// </summary>
public class AlwaysDisabledFeatureFlag :
    FeatureFlag
{
    /// <summary>
    ///     Creates a new instance of the <see cref="AlwaysDisabledFeatureFlag" /> class.
    /// </summary>
    public AlwaysDisabledFeatureFlag()
        : base(false)
    {
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="AlwaysDisabledFeatureFlag" /> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    public AlwaysDisabledFeatureFlag(string id)
        : base(id, false)
    {
    }
}

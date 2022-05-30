namespace Med.FeatureFlags;

/// <summary>
///     A feature flag that is enabled starting from a certain assembly version.
/// </summary>
public class EnabledStartingFromVersionFeatureFlag :
    FeatureFlag
{
    /// <summary>
    ///     Creates a new instance of the <see cref="EnabledStartingFromVersionFeatureFlag" /> class.
    /// </summary>
    /// <param name="startVersion">The minimum version from which this feature flag is enabled.</param>
    /// <param name="fallbackEnabled">The fallback state of this feature flag if the assembly version is unattainable.</param>
    public EnabledStartingFromVersionFeatureFlag(Version startVersion, bool fallbackEnabled = false)
        : base(() => EvaluateEnabled(startVersion, fallbackEnabled))
    {
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="EnabledStartingFromVersionFeatureFlag" /> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    /// <param name="startVersion">The minimum version from which this feature flag is enabled.</param>
    /// <param name="fallbackEnabled">The fallback state of this feature flag if the assembly version is unattainable.</param>
    public EnabledStartingFromVersionFeatureFlag(string id, Version startVersion, bool fallbackEnabled = false)
        : base(id, () => EvaluateEnabled(startVersion, fallbackEnabled))
    {
    }

    /// <summary>
    ///     Evaluates whether or not this feature flag is enabled.
    /// </summary>
    /// <param name="startVersion">The minimum version from which this feature flag is enabled.</param>
    /// <param name="fallbackEnabled">The fallback state of this feature flag if the assembly version is unattainable.</param>
    /// <returns><c>true</c> if this feature flag is enabled. Otherwise, <c>false</c>.</returns>
    protected static bool EvaluateEnabled(Version startVersion, bool fallbackEnabled)
    {
        try
        {
            var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
            return startVersion <= assemblyVersion;
        }
        catch (Exception)
        {
            return fallbackEnabled;
        }
    }
}

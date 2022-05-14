namespace Med.FeatureFlags;

/// <summary>
/// A feature flag that is enabled starting from a certain assembly version.
/// </summary>
public class EnabledPriorToVersionFeatureFlag :
    FeatureFlag
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnabledPriorToVersionFeatureFlag"/> class.
    /// </summary>
    /// <param name="cutoffVersion">The minimum version from which this feature flag is disabled.</param>
    /// <param name="fallbackEnabled">The fallback state of this feature flag if the assembly version is unattainable.</param>
    public EnabledPriorToVersionFeatureFlag(Version cutoffVersion, bool fallbackEnabled = false)
        : base(() => EvaluateEnabled(cutoffVersion, fallbackEnabled))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EnabledPriorToVersionFeatureFlag"/> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    /// <param name="startVersion">The minimum version from which this feature flag is disabled.</param>
    /// <param name="fallbackEnabled">The fallback state of this feature flag if the assembly version is unattainable.</param>
    public EnabledPriorToVersionFeatureFlag(string id, Version startVersion, bool fallbackEnabled = false)
        : base(id, () => EvaluateEnabled(startVersion, fallbackEnabled))
    {
    }

    /// <summary>
    /// Evaluates whether or not this feature flag is enabled.
    /// </summary>
    /// <param name="cutoffVersion">The minimum version from which this feature flag is disabled.</param>
    /// <param name="fallbackEnabled">The fallback state of this feature flag if the assembly version is unattainable.</param>
    /// <returns><c>true</c> if this feature flag is enabled. Otherwise, <c>false</c>.</returns>
    protected static bool EvaluateEnabled(Version cutoffVersion, bool fallbackEnabled)
    {
        try
        {
            var assemblyVersion = typeof(EnabledPriorToVersionFeatureFlag).Assembly.GetName().Version;
            return assemblyVersion < cutoffVersion;
        }
        catch (Exception)
        {
            return fallbackEnabled;
        }
    }
}

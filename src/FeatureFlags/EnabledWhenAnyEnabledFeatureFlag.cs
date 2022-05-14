namespace Med.FeatureFlags;

/// <summary>
/// A feature flag that is enabled when any of the given feature flags are enabled.
/// </summary>
public class EnabledWhenAnyEnabledFeatureFlag :
    FeatureFlag
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnabledWhenAnyEnabledFeatureFlag"/> class.
    /// </summary>
    /// <param name="featureFlags">A set of feature flags.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="featureFlags"/> is <c>null</c>.</exception>
    public EnabledWhenAnyEnabledFeatureFlag(params IFeatureFlag[] featureFlags)
        : base(() => EvaluateEnabled(featureFlags))
    {
        FeatureFlags = featureFlags ?? throw new ArgumentNullException(nameof(featureFlags));

        foreach (var featureFlag in FeatureFlags)
            featureFlag.EnabledChanged += OnFeatureFlagEnabledChanged;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EnabledWhenAnyEnabledFeatureFlag"/> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    /// <param name="featureFlags">A set of feature flags.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="featureFlags"/> is <c>null</c>.</exception>
    public EnabledWhenAnyEnabledFeatureFlag(string id, params IFeatureFlag[] featureFlags)
        : base(id, () => EvaluateEnabled(featureFlags))
    {
        FeatureFlags = featureFlags ?? throw new ArgumentNullException(nameof(featureFlags));

        foreach (var featureFlag in FeatureFlags)
            featureFlag.EnabledChanged += OnFeatureFlagEnabledChanged;
    }

    /// <summary>
    /// Gets the set of feature flags.
    /// </summary>
    protected virtual IFeatureFlag[] FeatureFlags { get; }

    /// <summary>
    /// Evaluates whether or not this feature flag is enabled.
    /// </summary>
    /// <param name="featureFlags">The set of feature flags that contribute to the state of this feature flag.</param>
    /// <returns><c>true</c> if any of the <paramref name="featureFlags"/> are enabled. Otherwise, <c>false</c>.</returns>
    protected static bool EvaluateEnabled(IEnumerable<IFeatureFlag> featureFlags)
    {
        return featureFlags.Any(x => x.Enabled);
    }

    /// <summary>
    /// Called when one of the feature flags has changed its enabled state.
    /// </summary>
    /// <param name="sender">The originator of this event.</param>
    /// <param name="enabled">The new state of this feature flag.</param>
    protected virtual void OnFeatureFlagEnabledChanged(object sender, bool enabled)
    {
        Enabled = EvaluateEnabled(FeatureFlags);
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        foreach (var featureFlag in FeatureFlags)
            featureFlag.EnabledChanged -= OnFeatureFlagEnabledChanged;

        base.Dispose(disposing);
    }
}

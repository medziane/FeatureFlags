namespace Med.FeatureFlags;

/// <summary>
///     A feature flag that is enabled when any of the given feature flags is disabled.
/// </summary>
public class EnabledWhenAnyDisabledFeatureFlag :
    FeatureFlag
{
    /// <summary>
    ///     Creates a new instance of the <see cref="EnabledWhenAnyDisabledFeatureFlag" /> class.
    /// </summary>
    /// <param name="featureFlags">A set of feature flags.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="featureFlags" /> is <c>null</c>.</exception>
    public EnabledWhenAnyDisabledFeatureFlag(params IFeatureFlag[] featureFlags)
        : base(() => EvaluateEnabled(featureFlags))
    {
        ArgumentNullException.ThrowIfNull(featureFlags);
        FeatureFlags = featureFlags;

        foreach (var featureFlag in FeatureFlags)
            featureFlag.EnabledChanged += OnFeatureFlagEnabledChanged;
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="EnabledWhenAnyDisabledFeatureFlag" /> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    /// <param name="featureFlags">A set of feature flags.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="featureFlags" /> is <c>null</c>.</exception>
    public EnabledWhenAnyDisabledFeatureFlag(string id, params IFeatureFlag[] featureFlags)
        : base(id, () => EvaluateEnabled(featureFlags))
    {
        ArgumentNullException.ThrowIfNull(featureFlags);
        FeatureFlags = featureFlags;

        foreach (var featureFlag in FeatureFlags)
            featureFlag.EnabledChanged += OnFeatureFlagEnabledChanged;
    }

    /// <summary>
    ///     Gets the set of feature flags.
    /// </summary>
    protected virtual IFeatureFlag[] FeatureFlags { get; }

    /// <summary>
    ///     Evaluates whether or not this feature flag is enabled.
    /// </summary>
    /// <param name="featureFlags">The set of feature flags that contribute to the state of this feature flag.</param>
    /// <returns><c>true</c> if any of the <paramref name="featureFlags" /> is disabled. Otherwise, <c>false</c>.</returns>
    protected static bool EvaluateEnabled(IEnumerable<IFeatureFlag> featureFlags)
    {
        return featureFlags.Any(x => !x.Enabled);
    }

    /// <summary>
    ///     Called when one of the feature flags has changed its enabled state.
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

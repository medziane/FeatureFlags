namespace Med.FeatureFlags;

/// <summary>
///     A feature flag that is enabled or disabled based on a pre-defined <c>true</c> or <c>false</c> input, or calculated
///     using a provided function.
/// </summary>
public class FeatureFlag :
    BaseFeatureFlag
{
    /// <summary>
    ///     Creates a new instance of the <see cref="FeatureFlag" /> class with a pre-determined state.
    /// </summary>
    /// <param name="enabled">The pre-determined state of the feature.</param>
    public FeatureFlag(bool enabled)
        : this(Guid.NewGuid().ToString(), () => enabled)
    {
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="FeatureFlag" /> class with an identifier and a pre-determined state.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    /// <param name="enabled">The pre-determined state of the feature.</param>
    public FeatureFlag(string id, bool enabled)
        : this(id, () => enabled)
    {
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="FeatureFlag" /> class with a function to determine the feature state.
    /// </summary>
    /// <param name="predicate">A function to determine whether this feature is enabled or disabled.</param>
    public FeatureFlag(Func<bool> predicate)
        : this(Guid.NewGuid().ToString(), predicate)
    {
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="FeatureFlag" /> class with an identifier and a function to determine the
    ///     feature state.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    /// <param name="predicate">A function to determine whether this feature is enabled or disabled.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="predicate" /> is <c>null</c>.</exception>
    public FeatureFlag(string id, Func<bool> predicate)
        : base(id)
    {
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        Enabled = predicate.Invoke();
    }
}

namespace Med.FeatureFlags;

/// <summary>
///     Represents a toggle that can be enabled or disabled.
/// </summary>
/// <seealso cref="IIdentifiable{TIdentity}" />

public interface IFeatureFlag :
    IIdentifiable<string>
{
    /// <summary>
    ///     Gets a value indicating whether this <see cref="IFeatureFlag" /> is enabled.
    /// </summary>
    bool Enabled { get; }

    /// <summary>
    ///     Notifies clients that the <see cref="Enabled"/> property value has changed.
    /// </summary>
    event EventHandler<bool> EnabledChanged;
}

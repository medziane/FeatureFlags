namespace Med.FeatureFlags;

/// <summary>
///     The API for components that are identifiable.
/// </summary>
/// <typeparam name="TIdentity">The type of the value defining the identity of an instance.</typeparam>
public interface IIdentifiable<TIdentity> :
    IEquatable<TIdentity>
{
    /// <summary>
    ///     Gets the id of the subject.
    /// </summary>
    TIdentity Id { get; }
}

namespace Med.FeatureFlags.Enablers;

/// <summary>
///     A representation of a continuous set of values with a minimum and maximum value.
/// </summary>
/// <typeparam name="T">The type of the values in this range.</typeparam>
public interface IRange<out T>
    where T : IComparable<T>
{
    /// <summary>
    ///     The lowest value contained in this range.
    /// </summary>
    T Minimum { get; }

    /// <summary>
    ///     The highest value contained in this range.
    /// </summary>
    T Maximum { get; }
}

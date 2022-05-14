namespace Med.FeatureFlags;

/// <inheritdoc />
/// <seealso cref="IComparable{T}" />
public class Range<T> :
    IRange<T>
    where T : IComparable<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Range{T}" /> class.
    /// </summary>
    /// <param name="minimum">The lowest value of this range.</param>
    /// <param name="maximum">The highest value of this range.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the <paramref name="minimum" /> and/or <paramref name="maximum" /> are <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown if the <paramref name="minimum" /> is strictly greater than the <paramref name="maximum" />.
    /// </exception>
    public Range(T minimum, T maximum)
    {
        if (minimum == null)
            throw new ArgumentNullException(nameof(minimum));

        if (maximum == null)
            throw new ArgumentNullException(nameof(maximum));

        if (minimum.CompareTo(maximum) > 0)
            throw new ArgumentException(@"the maximum cannot be bigger than the minimum.", nameof(maximum));

        Minimum = minimum;
        Maximum = maximum;
    }

    /// <inheritdoc />
    public virtual T Minimum { get; }

    /// <inheritdoc />
    public virtual T Maximum { get; }
}

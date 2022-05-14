namespace Med.FeatureFlags;

/// <summary>
/// A collection of extension methods for the <see cref="IRange{T}" /> interface.
/// </summary>
public static class RangeExtensions
{
    /// <summary>
    /// Determines whether two ranges intersect with one another.
    /// </summary>
    /// <param name="range">The first range.</param>
    /// <param name="otherRange">The second range.</param>
    /// <typeparam name="T">The type of the values of the two ranges.</typeparam>
    /// <returns><c>true</c> if the two ranges overlap at any point; Otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if any of the ranges is <c>null</c>.</exception>
    public static bool Intersects<T>(this IRange<T> range, IRange<T> otherRange) where T : IComparable<T>
    {
        if (range == null)
            throw new ArgumentNullException(nameof(range));

        if (otherRange == null)
            throw new ArgumentNullException(nameof(otherRange));

        return range.Minimum.CompareTo(otherRange.Maximum) < 0 && otherRange.Minimum.CompareTo(range.Maximum) < 0;
    }

    /// <summary>
    /// Determines whether one range is contained within another range.
    /// </summary>
    /// <param name="range">The first range.</param>
    /// <param name="otherRange">The second range.</param>
    /// <typeparam name="T">The type of the values of the two ranges.</typeparam>
    /// <returns><c>true</c> if the second range in within the first range; Otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if any of the ranges is <c>null</c>.</exception>
    public static bool Contains<T>(this IRange<T> range, IRange<T> otherRange) where T : IComparable<T>
    {
        if (range == null)
            throw new ArgumentNullException(nameof(range));

        if (otherRange == null)
            throw new ArgumentNullException(nameof(otherRange));

        return range.Minimum.CompareTo(otherRange.Minimum) <= 0 && otherRange.Maximum.CompareTo(range.Maximum) <= 0;
    }

    /// <summary>
    /// Determines whether a value exists within a range.
    /// </summary>
    /// <param name="range">The range.</param>
    /// <param name="element">The value to check.</param>
    /// <typeparam name="T">The type of the values at hand.</typeparam>
    /// <returns><c>true</c> if the element is within the range; Otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if any of the range and/or the element is <c>null</c>.</exception>
    public static bool Contains<T>(this IRange<T> range, T element) where T : IComparable<T>
    {
        if (range == null)
            throw new ArgumentNullException(nameof(range));

        if (element == null)
            throw new ArgumentNullException(nameof(element));

        return range.Minimum.CompareTo(element) <= 0 && element.CompareTo(range.Maximum) <= 0;
    }
}

namespace Med.FeatureFlags;

/// <summary>
/// A feature flag that is enabled during the weekend.
/// </summary>
public class EnabledDuringWeekendsFeatureFlag :
    EnabledOnDaysOfWeekFeatureFlag
{
    /// <summary>
    /// Creates a new instance of the <see cref="EnabledDuringWeekendsFeatureFlag"/> class.
    /// </summary>
    public EnabledDuringWeekendsFeatureFlag()
        : base(DayOfWeek.Saturday, DayOfWeek.Sunday)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="EnabledDuringWeekendsFeatureFlag"/> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    public EnabledDuringWeekendsFeatureFlag(string id)
        : base(id, DayOfWeek.Saturday, DayOfWeek.Sunday)
    {
    }
}

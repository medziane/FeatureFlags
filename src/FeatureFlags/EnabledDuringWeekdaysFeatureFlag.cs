namespace Med.FeatureFlags;

/// <summary>
/// A feature flag that is enabled during the weekdays.
/// </summary>
public class EnabledDuringWeekdaysFeatureFlag :
    EnabledOnDaysOfWeekFeatureFlag
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnabledDuringWeekdaysFeatureFlag"/> class.
    /// </summary>
    public EnabledDuringWeekdaysFeatureFlag()
        : base(DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EnabledDuringWeekdaysFeatureFlag"/> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    public EnabledDuringWeekdaysFeatureFlag(string id)
        : base(id, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday)
    {
    }
}

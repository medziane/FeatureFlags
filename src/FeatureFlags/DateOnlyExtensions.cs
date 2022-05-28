namespace Med.FeatureFlags;

public static class DateOnlyExtensions
{
    public static DateTime ToDateTime(this DateOnly dateOnly)
    {
        return dateOnly.ToDateTime(TimeOnly.FromTimeSpan(TimeSpan.Zero));
    }
}
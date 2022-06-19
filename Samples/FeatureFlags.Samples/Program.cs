using Med.FeatureFlags;

IFeatureFlag alwaysEnabledFeatureFlag = new AlwaysEnabledFeatureFlag();
IFeatureFlag weekendFeatureFlag = new EnabledDuringWeekendsFeatureFlag();

Console.WriteLine(alwaysEnabledFeatureFlag.Enabled
    ? "The always-enabled feature flag is enabled"
    : "The always-enabled feature flag is disabled (Houston, we have a problem!)");

Console.Write(DateTime.Today.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday
    ? "It is weekend; "
    : "It is not weekend; ");
Console.WriteLine(weekendFeatureFlag.Enabled
    ? "weekend feature flag is enabled"
    : "weekend feature flag is disabled");

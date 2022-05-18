namespace Med.FeatureFlags.UnitTests;

public class FeatureFlagTests
{
    [Theory]
    [InlineData(null, true)]
    [InlineData(null, false)]
    public void Constructor_IdIsNull_ArgumentNullExceptionThrown(string id, bool enabled)
    {
        Assert.Throws<ArgumentNullException>(() => { _ = new FeatureFlag(id, enabled); });
    }

    [Theory]
    [InlineData("", true)]
    [InlineData("   ", false)]
    public void Constructor_IdIsEmptyOrWhiteSpace_ArgumentExceptionThrown(string id, bool enabled)
    {
        Assert.Throws<ArgumentException>(() => { _ = new FeatureFlag(id, enabled); });
    }

    [Theory]
    [InlineData("123", true)]
    [InlineData("456", false)]
    public void Constructor_IdIsValid_EnabledMatchesInput(string id, bool enabled)
    {
        var featureFlag = new FeatureFlag(id, enabled);
        Assert.Equal(id, featureFlag.Id);
        Assert.Equal(enabled, featureFlag.Enabled);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Constructor_NoId_EnabledMatchesInput(bool enabled)
    {
        var featureFlag = new FeatureFlag(enabled);
        Assert.Equal(enabled, featureFlag.Enabled);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Constructor_NoIdAndValidPredicate_EnabledMatchesInput(bool enabled)
    {
        var featureFlag = new FeatureFlag(() => enabled);
        Assert.Equal(enabled, featureFlag.Enabled);
    }

    [Theory]
    [InlineData(null)]
    public void Constructor_NoIdAndPredicateIsNull_EnabledMatchesInput(Func<bool> predicate)
    {
        Assert.Throws<ArgumentNullException>(() => { _ = new FeatureFlag(predicate); });
    }

    [Theory]
    [InlineData("123", null)]
    public void Constructor_PredicateIsNull_EnabledMatchesInput(string id, Func<bool> predicate)
    {
        Assert.Throws<ArgumentNullException>(() => { _ = new FeatureFlag(id, predicate); });
    }
}

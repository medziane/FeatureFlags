namespace Med.FeatureFlags.UnitTests;

public class AlwaysDisabledFeatureFlagTests
{
    [Theory]
    [InlineData(null)]
    public void Constructor_IdIsNull_ArgumentNullExceptionThrown(string id)
    {
        Assert.Throws<ArgumentNullException>(() => new AlwaysDisabledFeatureFlag(id));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_IdIsEmptyOrWhiteSpace_ArgumentExceptionThrown(string id)
    {
        Assert.Throws<ArgumentException>(() => { _ = new AlwaysDisabledFeatureFlag(id); });
    }

    [Theory]
    [InlineData("123")]
    public void Constructor_IdIsValid_EnabledIsFalse(string id)
    {
        var featureFlag = new AlwaysDisabledFeatureFlag(id);

        Assert.False(featureFlag.Enabled);
    }

    [Fact]
    public void Constructor_NoId_EnabledIsFalse()
    {
        var featureFlag = new AlwaysDisabledFeatureFlag();

        Assert.False(featureFlag.Enabled);
    }
}

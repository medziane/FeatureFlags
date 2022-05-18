namespace Med.FeatureFlags.UnitTests;

public class AlwaysEnabledFeatureFlagTests
{
    [Theory]
    [InlineData(null)]
    public void Constructor_IdIsNull_ArgumentNullExceptionThrown(string id)
    {
        Assert.Throws<ArgumentNullException>(() => new AlwaysEnabledFeatureFlag(id));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_IdIsEmptyOrWhiteSpace_ArgumentExceptionThrown(string id)
    {
        Assert.Throws<ArgumentException>(() => { _ = new AlwaysEnabledFeatureFlag(id); });
    }

    [Theory]
    [InlineData("123")]
    public void Constructor_IdIsValid_EnabledIsTrue(string id)
    {
        var featureFlag = new AlwaysEnabledFeatureFlag(id);

        Assert.True(featureFlag.Enabled);
    }

    [Fact]
    public void Constructor_NoId_EnabledIsTrue()
    {
        var featureFlag = new AlwaysEnabledFeatureFlag();

        Assert.True(featureFlag.Enabled);
    }
}
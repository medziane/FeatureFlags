using Med.FeatureFlags.Enablers;

namespace Med.FeatureFlags.UnitTests;

public class BaseFeatureFlagTests
{
    [Theory]
    [InlineData("123", true, "123", false)]
    [InlineData("123", true, "456", true)]
    public void Constructor_CompareTwoFeatureFlags_EqualIfIdsAreEqual(
        string id,
        bool enabled,
        string otherId,
        bool otherEnabled)
    {
        var baseFeatureFlag = new FeatureFlag(id, enabled) as BaseFeatureFlag;
        var otherBaseFeatureFlag = new FeatureFlag(otherId, otherEnabled) as BaseFeatureFlag;
        var expected = id.Equals(otherId);
        var result = baseFeatureFlag.Equals(otherBaseFeatureFlag);
        Assert.Equal(id, baseFeatureFlag.Id);
        Assert.Equal(otherId, otherBaseFeatureFlag.Id);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("123", true, "123", false)]
    [InlineData("123", true, "456", true)]
    public void Constructor_CompareTwoFeatureFlagsUsingOperator_EqualIfIdsAreEqual(
        string id,
        bool enabled,
        string otherId,
        bool otherEnabled)
    {
        var baseFeatureFlag = new FeatureFlag(id, enabled) as BaseFeatureFlag;
        var otherBaseFeatureFlag = new FeatureFlag(otherId, otherEnabled) as BaseFeatureFlag;
        var expected = id.Equals(otherId);
        var result = baseFeatureFlag == otherBaseFeatureFlag;
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("123", true, "123", false)]
    [InlineData("123", true, "456", true)]
    public void Constructor_CompareTwoFeatureFlagsUsingOperator_NotEqualIfIdsAreNotEqual(
        string id,
        bool enabled,
        string otherId,
        bool otherEnabled)
    {
        var baseFeatureFlag = new FeatureFlag(id, enabled) as BaseFeatureFlag;
        var otherBaseFeatureFlag = new FeatureFlag(otherId, otherEnabled) as BaseFeatureFlag;
        var expected = !id.Equals(otherId);
        var result = baseFeatureFlag != otherBaseFeatureFlag;
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("123", true, "123", false)]
    [InlineData("123", true, "456", true)]
    public void Constructor_CompareTwoIdentifiableFeatureFlags_EqualIfIdsAreEqual(string id, bool enabled, string otherId,
        bool otherEnabled)
    {
        var identifiable = new FeatureFlag(id, enabled) as IIdentifiable<string>;
        var otherIdentifiable = new FeatureFlag(otherId, otherEnabled) as IIdentifiable<string>;
        var expected = id.Equals(otherId);
        var result = identifiable.Equals(otherIdentifiable);
        Assert.Equal(expected, result);
    }
}

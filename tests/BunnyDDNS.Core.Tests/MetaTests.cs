namespace BunnyDDNS.Core.Tests;

[TestClass]
public class MetaTests
{
    [TestMethod]
    public void UserAgent_Is_Not_Null()
    {
        // Arrange
        var userAgent = Meta.UserAgent;

        // Assert
        Assert.IsNotNull(userAgent);
        Assert.AreEqual(Meta.Name, userAgent.Product.Name);
        Assert.AreEqual(Meta.Version, userAgent.Product.Version);
    }
}

using BunnyDDNS.Core.Configuration;

namespace BunnyDDNS.Core.Tests.Entities.Configuration;

[TestClass]
public class BunnyTests
{
    [TestMethod]
    public void Bunny_Tests()
    {
        // Arrange
        var expectedAccessToken = "token123456";
        var bunnyObj = new Bunny
        {
            AccessToken = expectedAccessToken
        };

        // Assert
        Assert.IsNotNull(bunnyObj);
        Assert.AreEqual(expectedAccessToken, bunnyObj.AccessToken);
    }
}

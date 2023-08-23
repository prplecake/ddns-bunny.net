namespace BunnyDDNS.Core.Tests.Entities.Configuration;

[TestClass]
public class ConfigTests
{
    [TestMethod]
    public void Config_Tests()
    {
        // Arrange
        var expectedAccessToken = "token123456";
        var expectedRecordName = "ddns";
        var expectedZoneName = "int.example.com";
        var configObj = new Config
        {
            Bunny = new Bunny
            {
                AccessToken = expectedAccessToken
            },
            RecordName = expectedRecordName,
            ZoneName = expectedZoneName
        };

        // Assert
        Assert.IsNotNull(configObj);
        Assert.IsNotNull(configObj.Bunny);
        Assert.AreEqual(expectedAccessToken, configObj.Bunny.AccessToken);
        Assert.AreEqual(expectedRecordName, configObj.RecordName);
        Assert.AreEqual(expectedZoneName, configObj.ZoneName);
    }
}

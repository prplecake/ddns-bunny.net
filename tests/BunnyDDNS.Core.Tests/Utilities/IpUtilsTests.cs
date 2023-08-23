using System.Net;
using System.Net.Mime;
using System.Text;
using BunnyDDNS.Core.Utilities;
using Moq;
using Moq.Protected;

namespace BunnyDDNS.Core.Tests.Utilities;

[TestClass]
public class IpUtilsTests
{
    [TestMethod]
    public void GetCurrentIp_Success()
    {
        // Arrange
        var httpResponse = new HttpResponseMessage();
        httpResponse.StatusCode = HttpStatusCode.OK;
        var expectedIp = "1.2.3.4";
        httpResponse.Content = new StringContent(expectedIp, Encoding.UTF8, MediaTypeNames.Text.Plain);

        Mock<HttpMessageHandler> mockHandler = new();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(r =>
                    r.Method == HttpMethod.Get && r.RequestUri.ToString().StartsWith("https://text.myip.wtf")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponse);

        var httpClient = new HttpClient(mockHandler.Object);
        var ipUtils = new IpUtils(httpClient);

        // Act
        var result = ipUtils.GetCurrentIp();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedIp, result);
    }
}

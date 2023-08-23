using System.Net.Http.Headers;
using System.Net.Mime;

namespace BunnyDDNS.Core.Utilities;

public class IpUtils
{
    private static HttpClient Client = new();
    public IpUtils(HttpClient client)
    {
        // Setup HttpClient
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        client.DefaultRequestHeaders.UserAgent.Add(Meta.UserAgent);
        Client = client;
    }
    public string GetCurrentIp()
    {
        var url = "https://text.myip.wtf";
        var response = Client.GetAsync(url).Result;
        response.EnsureSuccessStatusCode();
        return response.Content.ReadAsStringAsync().Result.Trim(Environment.NewLine.ToCharArray());
    }
}

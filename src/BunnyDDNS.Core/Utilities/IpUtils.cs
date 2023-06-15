using System.Net.Http.Headers;
using System.Net.Mime;

namespace BunnyDDNS.Core.Utilities;

public class IpUtils
{
    private static readonly HttpClient Client = new();
    public IpUtils()
    {
        // Setup HttpClient
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        Client.DefaultRequestHeaders.UserAgent.Add(Meta.UserAgent);
    }
    public string GetCurrentIp()
    {
        var url = "https://text.myip.wtf";
        var response = Client.GetAsync(url).Result;
        response.EnsureSuccessStatusCode();
        return response.Content.ReadAsStringAsync().Result.Trim(Environment.NewLine.ToCharArray());
    }
}

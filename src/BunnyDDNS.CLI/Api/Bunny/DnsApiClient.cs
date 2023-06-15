using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using BunnyDDNS.Core;
using BunnyDDNS.Core.Configuration;
using BunnyDDNS.Core.Entities.Bunny;
using Newtonsoft.Json;
using Serilog;

namespace BunnyDDNS.CLI.Api.Bunny;

public class DnsApiClient
{
    private static readonly ILogger _logger = Log.ForContext<DnsApiClient>();
    private readonly string _baseUrl = "https://api.bunny.net";
    private readonly HttpClient _client = new();
    public DnsApiClient(Config config)
    {
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        _client.DefaultRequestHeaders.UserAgent.Add(Meta.UserAgent);
        _client.DefaultRequestHeaders.Add("AccessKey", config.Bunny.AccessToken);
    }
    public async Task<DnsZone> GetDnsZoneByName(string zoneName)
    {
        var zones = await GetDnsZones();
        return zones.First(z => z.Domain.ToLower() == zoneName);
    }
    public async Task<List<DnsZone>> GetDnsZones()
    {
        _logger.Debug("Running {Method}", nameof(GetDnsZones));
        var requestUri = $"{_baseUrl}/dnszone";
        var response = await _client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        string responseContent = await response.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<ListDnsZoneApiResponse>(responseContent);
        return obj.Items;
    }
    public async void UpdateDnsRecord(int zoneId, DnsRecord record, string newValue)
    {
        _logger.Debug("Running {Method}", nameof(UpdateDnsRecord));
        // Prep payload
        Dictionary<string, object?> payload = new()
        {
            {
                "Id", record.Id
            },
            {
                "Value", newValue
            }
        };
        var stringContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.Default,
            MediaTypeNames.Application.Json);
        var requestUri = $"{_baseUrl}/dnszone/{zoneId}/records/{record.Id}";
        var response = await _client.PostAsync(requestUri, stringContent);
        response.EnsureSuccessStatusCode();
    }
}

using BunnyDDNS.CLI.Api.Bunny;
using BunnyDDNS.Core.Configuration;
using BunnyDDNS.Core.Utilities;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace BunnyDDNS.CLI;

public static class Program
{
    public static int Main()
    {
        // Read config
        var config = SetupConfiguration();

        // Get current IP address
        var _ipUtils = new IpUtils();
        string currentIp = _ipUtils.GetCurrentIp();

        // Find record in Bunny DNS
        var dnsClient = new DnsApiClient(config);
        var zone = dnsClient.GetDnsZoneByName(config.ZoneName).Result;

        // Update record in Bunny DNS
        dnsClient.UpdateDnsRecord(
            zone.Id,
            zone.Records.First(r => r.Name.ToLower() == config.RecordName),
            currentIp
        );
        return 0;
    }
    private static Config SetupConfiguration()
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(PascalCaseNamingConvention.Instance)
            .Build();
        var config = deserializer.Deserialize<Config>(File.ReadAllText("bunny-ddns.yml"));
        return config;
    }
}

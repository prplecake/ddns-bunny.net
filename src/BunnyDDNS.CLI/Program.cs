using Bunny.API.NET.Client;
using BunnyDDNS.Core.Configuration;
using BunnyDDNS.Core.Utilities;
using Serilog;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace BunnyDDNS.CLI;

public static class Program
{
    private static bool _debug;
    private static ILogger? _logger;
    public static int Main(string[] args)
    {
        _debug = args.Contains("--debug");
        // Setup logging
        var logOutputTemplate =
            "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] [{SourceContext}] {Message}{NewLine}{Exception}";
        var logConfig = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Console(
                outputTemplate: logOutputTemplate
            );
        if (_debug) logConfig.MinimumLevel.Debug();
        Log.Logger = logConfig.CreateLogger();
        _logger = Log.ForContext(typeof(Program));

        // Read config
        _logger.Debug("Getting configuration");
        var config = SetupConfiguration();

        // Create HttpClient
        var httpClient = new HttpClient();

        // Get current IP address
        var _ipUtils = new IpUtils(httpClient);
        string currentIp = _ipUtils.GetCurrentIp();
        _logger.Information("Current IP: {Ip}", currentIp);

        // Find record in Bunny DNS
        var dnsClient = new BunnyClient();
        dnsClient.SetApiKey(config.Bunny.AccessToken);
        var zone = dnsClient.GetZoneByName(config.ZoneName).Result;

        // Update record in Bunny DNS
        var record = zone.Records.First(r => r.Name.ToLower() == config.RecordName);
        if (record.Value.Equals(currentIp))
        {
            _logger.Information("Current IP matches DNS Record value. Not updating");
            return 0;
        }
        _logger.Information("Updating record: {Record}", $"{record}.{zone}");
        record.Value = currentIp;
        dnsClient.UpdateRecord(zone.Id, record);
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

using System.Net.Http.Headers;

namespace BunnyDDNS.Core;

public static class Meta
{
    public const string
        Name = "bunny-ddns.net",
        Version = "1.0";
    public static readonly ProductInfoHeaderValue UserAgent = new(Name, Version);
}

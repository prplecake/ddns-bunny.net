namespace BunnyDDNS.Core.Entities.Bunny;

public class DnsZone
{
    public string Domain { get; set; }
    public int Id { get; set; }
    public List<DnsRecord> Records { get; set; }
    /// <inheritdoc />
    public override string ToString() => Domain;
}
public class DnsRecord
{
    public int Id { get; set; }
    public string LinkName { get; set; }
    public string Name { get; set; }
    public int Ttl { get; set; }
    public int Type { get; set; }
    public string Value { get; set; }
    /// <inheritdoc />
    public override string ToString() => Name;
}
public class ListDnsZoneApiResponse
{
    public int CurrentPage { get; set; }
    public bool HasMoreItems { get; set; }
    public List<DnsZone> Items { get; set; }
    public int TotalItems { get; set; }
}

namespace EventUnion.Api.Features.Common;

// ReSharper disable once ClassNeverInstantiated.Global
public record AddressPayload
{
    public string? ZipCode { get; set; }
    public string? Street { get; set; }
    public string? Neighbourhood { get; set; }
    public string? Number { get; set; }
    public string? AddInfo { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
}
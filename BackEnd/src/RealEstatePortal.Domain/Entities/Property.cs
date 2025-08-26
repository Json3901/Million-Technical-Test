using RealEstatePortal.Domain.Attributes;

namespace RealEstatePortal.Domain.Entities;

[CollectionName("properties")]
public sealed class Property: BaseEntity
{
    public string OwnerId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string InternalCode { get; set; } = string.Empty;
    public string Year { get; set; }
}
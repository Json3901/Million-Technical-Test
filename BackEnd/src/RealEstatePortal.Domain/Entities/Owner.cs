using RealEstatePortal.Domain.Attributes;

namespace RealEstatePortal.Domain.Entities;

[CollectionName("owners")]
public sealed class Owner : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
}
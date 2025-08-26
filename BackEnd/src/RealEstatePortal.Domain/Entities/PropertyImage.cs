using RealEstatePortal.Domain.Attributes;

namespace RealEstatePortal.Domain.Entities;

[CollectionName("property-images")]
public class PropertyImage : BaseEntity
{
    public string PropertyId { get; set; }
    public string UrlFile { get; set; }
    public bool Enabled { get; set; } = true;
}
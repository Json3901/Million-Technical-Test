using RealEstatePortal.Domain.Attributes;

namespace RealEstatePortal.Domain.Entities;

[CollectionName("property-traces")]
public class PropertyTrace : BaseEntity
{
    public string PropertyId { get; set; }
    public DateTime DateSale { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
    public decimal Tax { get; set; }
    public bool Enabled { get; set; } = true;
}
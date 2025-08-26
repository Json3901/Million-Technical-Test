namespace RealEstatePortal.Domain.Entities;

public abstract class BaseEntity
{
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
}
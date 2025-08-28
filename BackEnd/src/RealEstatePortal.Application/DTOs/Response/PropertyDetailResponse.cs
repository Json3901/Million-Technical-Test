using RealEstatePortal.Domain.Entities;

namespace RealStatePortal.Application.DTOs.Response;

public class PropertyDetailResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Owner { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public string InternalCode { get; set; } = string.Empty;
    public List<PropertyImage> Images { get; set; } = [];
    public List<PropertyTrace> Traces { get; set; } = [];
}
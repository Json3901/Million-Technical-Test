namespace RealStatePortal.Application.DTOs.Response;

public class SearchPropertyResponse
{
    public string Id { get; set; } = string.Empty;
    public string OwnerName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string InternalCode { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
}
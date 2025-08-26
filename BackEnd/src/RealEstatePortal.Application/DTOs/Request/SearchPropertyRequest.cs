namespace RealStatePortal.Application.DTOs.Request;

public class SearchPropertyRequest
{
    public string[] Names { get; set; } = [];
    public string[] OwnerNames { get; set; } = [];
    public string[] Addresses { get; set; } = [];
    public decimal MinPrice { get; set; } = decimal.MinValue;
    public decimal MaxPrice { get; set; } = decimal.MaxValue;
    public string[] InternalCodes { get; set; } = [];
    public string[] Years { get; set; } = [];
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
    public string SortBy { get; set; } = "CreatedAt";
    public bool OrderDesc { get; set; } = true;
}
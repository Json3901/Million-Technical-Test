namespace RealStatePortal.Application.DTOs;

public class PaginationDto<T> where T : class
{
    public long Count { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public IReadOnlyList<T> Items { get; set; }
    public long PageCount { get; set; }
    public int ResultByPage { get; set; }

    public PaginationDto(int pageNumber, int pageSize, long count, IReadOnlyList<T> items)
    {
        var num = (long)Math.Ceiling((double)count / (double)pageSize);
        PageNumber = pageNumber;
        PageSize = pageSize;
        Count = count;
        Items = items;
        PageCount = num;
        ResultByPage = items != null ? items.Count : 0;
    }
}
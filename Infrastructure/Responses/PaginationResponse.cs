using Domain.Filters;

namespace Infrastructure.Responses;

public record PaginationResponse<T> : BaseFilter
{
    public int TotalPages { get; init; }
    public int TotalRecords { get; init; }
    public T Data { get; set; }

    private PaginationResponse(int pageNumber, int pageSize, int totalRecords, T data) : base(pageSize, pageNumber)
    {
        Data = data;
        TotalRecords = totalRecords;
        TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
    }

    public static PaginationResponse<T> Create(int pageNumber, int pageSize, int totalRecords, T data)
        => new PaginationResponse<T>(pageNumber, pageSize, totalRecords, data);
}
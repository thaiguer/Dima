namespace Dima.Core.Requests;

internal abstract class PagedRequest : BaseRequest
{
    public int PageNumber { get; set; } = Configuration.DefaultPageNumber;
    public int PageSize { get; set; } = Configuration.DefaultPageSize;
}
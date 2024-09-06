namespace Dima.Core.Requests.Transactions;

public class GetTransactionsByPeriodRequest : PagedRequest
{
    public DateTime? Start { get; set; }
    public DateTime? Finish { get; set; }
}
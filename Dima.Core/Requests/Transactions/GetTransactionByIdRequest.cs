using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Transactions;

public class GetTransactionByIdRequest : BaseRequest
{
    [Required]
    public long Id { get; set; }
}
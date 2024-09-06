using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Transactions;

public class DeleteTransactionRequest : BaseRequest
{
    [Required]
    public long Id { get; set; }
}
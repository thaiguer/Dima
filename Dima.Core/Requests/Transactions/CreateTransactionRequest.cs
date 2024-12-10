using Dima.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Transactions;

public class CreateTransactionRequest : BaseRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public ETransactionType Type { get; set; } = ETransactionType.Withdraw;

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public long CategoryId { get; set; }

    [Required]
    public DateTime? PaidOrReceivedAt { get; set; } = DateTime.Now;
}
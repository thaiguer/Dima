﻿using Dima.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Transactions;

public class CreateTransactionRequest : BaseRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public ETransactionType Type { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public long CategoryId { get; set; }

    [Required]
    public DateTime? PaidOrReceivedAt { get; set; }
}
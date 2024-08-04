﻿namespace Dima.Core.Models;

internal class Transaction
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? PaidOrReceivedAt { get; set; }

    public int Type { get; set; }
    public decimal Ammount { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public string UserId { get; set; } = string.Empty;
}

using Dima.Api.Data;
using Dima.Core.Enums;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class TransactionHandler (AppDbContext appDbContext): ITransactionHandler
{
    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
        try
        {
            var transaction = new Transaction();
            transaction.Title = request.Title;
            transaction.Type = request.Type;
            transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
            transaction.Amount = request.Amount;
            transaction.CreatedAt = DateTime.UtcNow;
            transaction.CategoryId = request.CategoryId;
            transaction.UserId = request.UserId;

            await appDbContext.Transactions.AddAsync(transaction);
            await appDbContext.SaveChangesAsync();

            return new Response<Transaction?>(transaction, 201, "Transaction created with success");
        }
        catch
        {
            return new Response<Transaction?>(null, 500, "Impossible to create the transaction");
        }
    }

    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        try
        {
            var transaction = await appDbContext
                .Transactions
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (transaction == null) return new Response<Transaction?>(null, 404, "Transaction not found");

            transaction.CategoryId = request.CategoryId;
            transaction.Amount = request.Amount;
            transaction.Title = request.Title;
            transaction.Type = request.Type;
            transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;

            appDbContext.Transactions.Update(transaction);
            await appDbContext.SaveChangesAsync();

            return new Response<Transaction?>(transaction, 200, "Transaction updated with success");
        }
        catch
        {
            return new Response<Transaction?>(null, 500, "Impossible to update the transaction");
        }
    }

    public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
    {
        try
        {
            var transaction = await appDbContext.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            if (transaction == null) return new Response<Transaction?>(null, 404, "Transaction not found");

            appDbContext.Transactions.Remove(transaction);
            await appDbContext.SaveChangesAsync();

            return new Response<Transaction?>(transaction, message: "Transaction deleted with success");
        }
        catch
        {
            return new Response<Transaction?>(null, 500, "Impossible to update the transaction");
        }
    }

    public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        try
        {
            var transaction = await appDbContext
                .Transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (transaction == null) return new Response<Transaction?>(null, 404, "Transaction not found");

            return new Response<Transaction?>(transaction, message: "Transaction found with success");
        }
        catch
        {
            return new Response<Transaction?>(null, 500, "Impossible to get the transaction");
        }
    }

    Task<PagedResponse<List<Transaction>>> GetAllAsync(GetTransactionsByPeriodRequest request);
}

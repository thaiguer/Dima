using Dima.Core.Common.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using System.Net.Http.Json;

namespace Dima.Web.Handlers;

public class TransactionHandler (IHttpClientFactory httpClientFactory): ITransactionHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    
    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/transactions", request);
        return
            await result.Content.ReadFromJsonAsync<Response<Transaction?>>()
            ?? new Response<Transaction?>(null, 400, "Failure on creating the transaction.");
    }

    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        var result = await _httpClient.PutAsJsonAsync($"v1/transactions/{request.Id}", request);
        return
            await result.Content.ReadFromJsonAsync<Response<Transaction?>>()
            ?? new Response<Transaction?>(null, 400, "Failure on updating the transaction.");
    }

    public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
    {
        var result = await _httpClient.DeleteAsync($"v1/transactions/{request.Id}");
        return
            await result.Content.ReadFromJsonAsync<Response<Transaction?>>()
            ?? new Response<Transaction?>(null, 400, "Failure on deleting the transaction.");
    }

    public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        var result = await _httpClient.GetAsync($"v1/transactions/{request.Id}");
        return
            await result.Content.ReadFromJsonAsync<Response<Transaction?>>()
            ?? new Response<Transaction?>(null, 400, "Failure on getting the transaction.");
    }

    public async Task<PagedResponse<List<Transaction>>> GetAllAsync(GetTransactionsByPeriodRequest request)
    {
        const string format = "yyyy-MM-dd";
        var start = request.Start is not null
            ? request.Start.Value.ToString(format)
            : DateTime.Now.GetFirstDay().ToString(format);

        var finish = request.Finish is not null
            ? request.Finish.Value.ToString(format)
            : DateTime.Now.GetLastDay().ToString(format);

        string url = $"v1/transactions?start={start}&finish={finish}";

        return
            await _httpClient.GetFromJsonAsync<PagedResponse<List<Transaction>>>(url)
            ?? new PagedResponse<List<Transaction>>(null, 400, "Failure on getting the transactions.");
    }    
}
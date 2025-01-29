using Dima.Core.Common.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Transactions;

public partial class ListTransactionsPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public List<Transaction> Transactions { get; set; } = [];
    public string SearchTerm { get; set; } = string.Empty;
    public int CurrentYear { get; set; } = DateTime.Now.Year;
    public int CurrentMonth { get; set; } = DateTime.Now.Month;

    public int[] Years { get; set; } =
    {
        DateTime.Now.Year,
        DateTime.Now.AddYears(-1).Year,
        DateTime.Now.AddYears(-2).Year,
        DateTime.Now.AddYears(-3).Year,
        DateTime.Now.AddYears(-4).Year,
        DateTime.Now.AddYears(-5).Year,
    };

    #endregion
    
    #region Services

    [Inject] public ISnackbar SnackBar { get; set; } = null!;

    [Inject] public IDialogService DialogService { get; set; } = null!;
    
    [Inject]
    public ITransactionHandler Handler { get; set; } = null!;
    
    #endregion
    
    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        await GetTransactionsAsync();
    }

    #endregion
    
    #region Public Methods

    public Func<Transaction, bool> Filter => transaction =>
    {
        if (string.IsNullOrEmpty(SearchTerm)) return true;

        return transaction.Id.ToString().Contains(SearchTerm, StringComparison.CurrentCultureIgnoreCase)
            || transaction.Title.Contains(SearchTerm, StringComparison.CurrentCultureIgnoreCase);
    };
    
    #endregion
    
    #region Private Methods

    private async Task GetTransactionsAsync()
    {
        IsBusy = true;
        try
        {
            var request = new GetTransactionsByPeriodRequest
            {
                Start = DateTime.Now.GetFirstDay(CurrentYear, CurrentMonth),
                Finish = DateTime.Now.GetLastDay(CurrentYear, CurrentMonth),
                PageNumber = 1,
                PageSize = 1000
            };
            
            var result = await Handler.GetAllAsync(request);

            if (result.IsSuccess)
            {
                Transactions = result.Data ?? [];
            }
        }
        catch (Exception e)
        {
            SnackBar.Add(e.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion
}
using AppControleFinanceiro.Controller.Enums;
using AppControleFinanceiro.Controller.Repositories.Interfaces;
using CommunityToolkit.Mvvm.Messaging;

namespace AppControleFinanceiro.Views;

public partial class TransactionList : ContentPage
{
	private ITransactionRepository _repository;
	public TransactionList(ITransactionRepository repository)
	{
		this._repository = repository;

		InitializeComponent();

        RefreshTransactions();

        WeakReferenceMessenger.Default.Register<string>(this, (e, msg) => 
		{
            RefreshTransactions();
        }
        );
	}

	private void RefreshTransactions()
	{
        var items = _repository.GetAll();
        CollectionViewTransactions.ItemsSource = items;

        double income = items.Where(a => a.Type == TransactionType.Income).Sum(a => a.Value);
        double expense = items.Where(a => a.Type == TransactionType.Expenses).Sum(a => a.Value);

        var balance = income - expense;

        LabelIncome.Text = income.ToString("C");
        LabelExpense.Text = expense.ToString("C");
        LabelBalance.Text = balance.ToString("C");
    }

    private void OnButtonClicked_To_TransactionAdd(object sender, EventArgs eventArgs)
	{
        var transactionAdd = Handler.MauiContext.Services.GetService<TransactionAdd>();
		Navigation.PushModalAsync(transactionAdd);
	}

    private void OnButtonClicked_To_TransactionEdit(object sender, EventArgs e)
    {
        var transactionEdit = Handler.MauiContext.Services.GetService<TransactionEdit>();
        Navigation.PushModalAsync(transactionEdit);
    }
}
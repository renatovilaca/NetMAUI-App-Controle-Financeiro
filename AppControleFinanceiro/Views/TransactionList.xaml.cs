using AppControleFinanceiro.Controller.Enums;
using AppControleFinanceiro.Controller.Models;
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

    private void TapGestureRecognizer_To_TransactionEdit(object sender, TappedEventArgs e)
    {
        var grid = (Grid)sender;
        var gesture = (TapGestureRecognizer)grid.GestureRecognizers[0];

        Transaction transaction = (Transaction)gesture.CommandParameter;

        var transactionEdit = Handler.MauiContext.Services.GetService<TransactionEdit>();
        transactionEdit.SetTransactionToEdit(transaction);
        Navigation.PushModalAsync(transactionEdit);
    }

    private async void TapGestureRecognizer_To_TransactionDelete(object sender, TappedEventArgs e)
    {
        bool result = await App.Current.MainPage.DisplayAlert("Excluir", "Tem certeza que deseja excluir?", "Sim", "Não");

        if (result)
        {
            Transaction transaction = (Transaction)e.Parameter;
            _repository.Delete(transaction);

            RefreshTransactions();
        }
    }


}
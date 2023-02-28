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
        CollectionViewTransactions.ItemsSource = _repository.GetAll();
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
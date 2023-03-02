using AppControleFinanceiro.Controller.Enums;
using AppControleFinanceiro.Controller.Models;
using AppControleFinanceiro.Controller.Repositories.Interfaces;
using CommunityToolkit.Mvvm.Messaging;
using System.Text;

namespace AppControleFinanceiro.Views;

public partial class TransactionAdd : ContentPage
{
    private ITransactionRepository _repository;
	public TransactionAdd(ITransactionRepository repository)
	{
		InitializeComponent();
        _repository = repository;
	}

    private void TapGestureRecognizer_Tapped_ToClose(object sender, TappedEventArgs e)
    {
		Navigation.PopModalAsync();
    }

    private void OnButtonClicked_Save(object sender, EventArgs e)
    {
        if (!IsValidData())
            return;

        SaveTransactionInDatabase();

        Navigation.PopModalAsync();

        WeakReferenceMessenger.Default.Send<string>(string.Empty);

        //var countRecords = _repository.GetAll().Count;
        //App.Current.MainPage.DisplayAlert("Notificação!", $"Há {countRecords} registro(s) no BD!", "OK");

    }

    private void SaveTransactionInDatabase()
    {
        Transaction transaction = new()
        {
            Type = RadioIncome.IsChecked ? TransactionType.Income : TransactionType.Expenses,
            Name = EntryName.Text,
            Date = DatePickerDate.Date,
            Value = double.Parse(EntryValue.Text)
        };

        //add directly DI ITransactionRepository example
        //var repository = this.Handler.MauiContext.Services.GetService<ITransactionRepository>();
        //repository.Add(transaction);

        _repository.Add(transaction);
    }

    private bool IsValidData()
    {
        bool isValid = true;
        StringBuilder sb = new();

        if (string.IsNullOrEmpty(EntryName.Text) || string.IsNullOrWhiteSpace(EntryName.Text))
        {
            sb.AppendLine("O campo 'Nome' deve ser preenchido!");
            isValid = false;
        }

        if (string.IsNullOrEmpty(EntryValue.Text) || string.IsNullOrWhiteSpace(EntryValue.Text))
        {
            sb.AppendLine("O campo 'Valor' deve ser preenchido!");
            isValid = false;
        }

        if (!string.IsNullOrEmpty(EntryValue.Text) && !double.TryParse(EntryValue.Text, out double result))
        {
            isValid = false;
            sb.AppendLine("O campo 'Valor' é inválido!");
        }

        if (!isValid)
        {
            LabelError.IsVisible = true;
            LabelError.Text = sb.ToString();
        }

        return isValid;

    }
}
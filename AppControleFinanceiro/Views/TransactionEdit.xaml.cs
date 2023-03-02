using AppControleFinanceiro.Controller.Enums;
using AppControleFinanceiro.Controller.Models;
using AppControleFinanceiro.Controller.Repositories.Interfaces;
using CommunityToolkit.Mvvm.Messaging;
using System.Text;

namespace AppControleFinanceiro.Views;

public partial class TransactionEdit : ContentPage
{
    private ITransactionRepository _repository;
    private Transaction _transaction;
	public TransactionEdit(ITransactionRepository repository)
	{
		InitializeComponent();
        _repository = repository;
    }

    private void TapGestureRecognizer_Tapped_ToClose(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }

    public void SetTransactionToEdit(Transaction transaction)
    {
        _transaction = transaction;

        if (transaction.Type == TransactionType.Income)
           RadioIncome.IsChecked = true;
        else
            RadioExpense.IsChecked = true;

        EntryName.Text = transaction.Name;
        EntryValue.Text = transaction.Value.ToString();
        DatePickerDate.Date = transaction.Date.Date;

    }

    private void OnButtonClicked_Save(object sender, EventArgs e)
    {
        if (!IsValidData())
            return;

        SaveTransactionInDatabase();

        Navigation.PopModalAsync();

        WeakReferenceMessenger.Default.Send<string>(string.Empty);
    }

    private void SaveTransactionInDatabase()
    {
        Transaction transaction = new()
        {
            Id = _transaction.Id,
            Type = RadioIncome.IsChecked ? TransactionType.Income : TransactionType.Expenses,
            Name = EntryName.Text,
            Date = DatePickerDate.Date,
            Value = double.Parse(EntryValue.Text)
        };

        _repository.Update(transaction);
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
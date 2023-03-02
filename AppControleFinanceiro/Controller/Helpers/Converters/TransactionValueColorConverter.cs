using AppControleFinanceiro.Controller.Enums;
using AppControleFinanceiro.Controller.Models;
using System.Globalization;

namespace AppControleFinanceiro.Controller.Helpers.Converters
{
    public class TransactionValueColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Transaction transaction = (Transaction)value;

            if (transaction == null)  return null;

            if (transaction.Type == TransactionType.Income)
                return Color.FromArgb("#FF939E5A");
            else
                return Colors.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

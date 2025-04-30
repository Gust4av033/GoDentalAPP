using GoDentalAPP.ViewModels;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GoDentalAPP.src.GoDentalAPP.APP.Views.Login
{
    public partial class LoginView : Window
    {
        public LoginView(ViewModels.LoginViewModel? loginViewModel)
        {
            InitializeComponent();
            DataContext = loginViewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.Password = PasswordBox.Password;
            }
        }
    }


    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                bool invert = parameter is string paramStr && paramStr.ToLower() == "invert";
                return invert ?
                    (boolValue ? Visibility.Collapsed : Visibility.Visible) :
                    (boolValue ? Visibility.Visible : Visibility.Collapsed);
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                bool invert = parameter is string paramStr && paramStr.ToLower() == "invert";
                return invert ?
                    (visibility != Visibility.Visible) :
                    (visibility == Visibility.Visible);
            }
            return false;
        }
    }
}
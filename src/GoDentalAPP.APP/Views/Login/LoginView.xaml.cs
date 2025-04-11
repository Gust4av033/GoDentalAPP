using GoDentalAPP.ViewModels;
using System.Windows;

namespace GoDentalAPP.Views.Login
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }
    }
}
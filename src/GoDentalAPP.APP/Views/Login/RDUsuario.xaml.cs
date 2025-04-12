using GoDentalAPP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GoDentalAPP.INFRAESTRUCTURE.Repositorios;
using Microsoft.Extensions.DependencyInjection;



namespace GoDentalAPP.src.GoDentalAPP.APP.Views.Login
{
    /// <summary>
    /// Lógica de interacción para RDUsuario.xaml
    /// </summary>
    public partial class RDUsuario : Window
    {
        public RDUsuario(IUserRepository userRepository)
        {
            InitializeComponent();

            // Crear ViewModel con inyección de dependencia
            this.DataContext = new RegisterUserViewModel(userRepository);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterUserViewModel vm)
            {
                vm.Contrasena = ((PasswordBox)sender).Password;
            }
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterUserViewModel vm)
            {
                vm.ConfirmarContrasena = ((PasswordBox)sender).Password;
            }
        }
    }

}

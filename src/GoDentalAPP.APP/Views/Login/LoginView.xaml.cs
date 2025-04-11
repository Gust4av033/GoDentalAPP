using System.Windows;

namespace GoDentalAPP.Views.Login
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private bool ValidarCredenciales(string usuario, string contraseña)
        {
            // Simulación de validación (puedes reemplazar esto con una consulta a la base de datos)
            return usuario == "admin" && contraseña == "1234";
        }

        private void OnLoginButtonClick(object sender, RoutedEventArgs e)
        {
            string usuario = UsernameTextBox.Text;
            string contraseña = PasswordBox.Password;

            if (ValidarCredenciales(usuario, contraseña))
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
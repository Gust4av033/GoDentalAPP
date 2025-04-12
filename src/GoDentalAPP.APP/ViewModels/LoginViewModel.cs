using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using GoDentalAPP.Helpers;
using GoDentalAPP.INFRAESTRUCTURE.Repositorios;
using GoDentalAPP.Infrastructure.Persistence;
using GoDentalAPP.src.GoDentalAPP.APP.Views.Login;
using GoDentalAPP.Core.Entities;

namespace GoDentalAPP.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _errorMessage;
        private bool _isErrorVisible;

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public bool IsErrorVisible
        {
            get => _isErrorVisible;
            set => SetProperty(ref _isErrorVisible, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand OpenRegisterUserCommand { get; }
        public ICommand OpenRecoverPasswordCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLogin);
            OpenRegisterUserCommand = new RelayCommand(OpenRegister);
            //OpenRecoverPasswordCommand = new RelayCommand(OpenRecoverPassword);
            IsErrorVisible = false;
        }

        private void ExecuteLogin(object parameter)
        {
            if (ValidarCredenciales(Username, Password))
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
                Application.Current.MainWindow?.Close();
            }
            else
            {
                ErrorMessage = "Usuario o contraseña incorrectos";
                IsErrorVisible = true;
            }
        }

        private void OpenRegister(object parameter)
        {
            using (var context = new AppDbContext())
            {
                var userRepository = new UserRepository(context);
                var registerWindow = new RDUsuario(userRepository);
                registerWindow.ShowDialog();
            }
        }

        /*
        private void OpenRecoverPassword(object parameter)
        {
            // Implementa la lógica para recuperar contraseña
            var recoverWindow = new RecuperarContraseña();
            recoverWindow.Show();
            Application.Current.MainWindow?.Close();
        }*/

        private bool ValidarCredenciales(string usuario, string contraseña)
        {
            using (var context = new AppDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.NombreUsuario == usuario);
                if (user == null) return false;

                var storedHash = user.Contrasena;

                try
                {
                    // Intenta verificar con BCrypt (usuarios nuevos)
                    if (BCrypt.Net.BCrypt.Verify(contraseña, storedHash))
                        return true;
                }
                catch (BCrypt.Net.SaltParseException)
                {
                    // Si lanza error, asumimos que es un hash antiguo (SHA256)
                    using (var sha256 = System.Security.Cryptography.SHA256.Create())
                    {
                        var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(contraseña));
                        var hashSha256 = BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
                        return hashSha256 == storedHash.ToLowerInvariant();
                    }
                }

                return false;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
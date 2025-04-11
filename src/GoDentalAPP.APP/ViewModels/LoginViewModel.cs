using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using GoDentalAPP.Helpers;
using GoDentalAPP.src.GoDentalAPP.APP.Views.Login;

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
            var registerWindow = new RDUsuario();
            registerWindow.Show();
            Application.Current.MainWindow?.Close();
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
            // Implementa tu lógica real de validación aquí
            return usuario == "admin" && contraseña == "1234";
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
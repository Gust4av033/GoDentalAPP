using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using GoDentalAPP.Helpers;
using GoDentalAPP.INFRAESTRUCTURE.Repositorios;
using GoDentalAPP.src.GoDentalAPP.APP.Views.Login;
using GoDentalAPP.Views.Login;
using Microsoft.Extensions.DependencyInjection;

namespace GoDentalAPP.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IUserRepository _userRepository;
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

        public LoginViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            LoginCommand = new RelayCommand(ExecuteLogin);
            OpenRegisterUserCommand = new RelayCommand(OpenRegister);
            IsErrorVisible = false;
        }

        private void ExecuteLogin(object parameter)
        {
            if (ValidarCredenciales(Username, Password))
            {
                var mainWindow = App.ServiceProvider.GetService<MainWindow>();
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
            var registerWindow = App.ServiceProvider.GetService<RDUsuario>();
            registerWindow.Show();

            Application.Current.MainWindow?.Close();
        }

        private bool ValidarCredenciales(string usuario, string contraseña)
        {
            return _userRepository.VerifyCredentials(usuario, contraseña);
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
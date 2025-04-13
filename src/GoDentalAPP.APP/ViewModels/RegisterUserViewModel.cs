using GoDentalAPP.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;
using GoDentalAPP.Helpers;
using System.Text.RegularExpressions;
using System.Linq;
using GoDentalAPP.INFRAESTRUCTURE.Repositorios;  // Para el repositorio
using GoDentalAPP.Views.Login;
using GoDentalAPP.src.GoDentalAPP.APP.Views.Login;

namespace GoDentalAPP.ViewModels
{
    public class RegisterUserViewModel : INotifyPropertyChanged
    {
        private readonly IUserRepository _userRepository;
        private string _nombreUsuario = string.Empty;
        private string _correoElectronico = string.Empty;
        private string _contrasena = string.Empty;
        private string _confirmarContrasena = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isErrorVisible;

        public string NombreUsuario
        {
            get => _nombreUsuario;
            set { _nombreUsuario = value; OnPropertyChanged(); }
        }

        public string CorreoElectronico
        {
            get => _correoElectronico;
            set { _correoElectronico = value; OnPropertyChanged(); }
        }

        public string Contrasena
        {
            get => _contrasena;
            set { _contrasena = value; OnPropertyChanged(); }
        }

        public string ConfirmarContrasena
        {
            get => _confirmarContrasena;
            set { _confirmarContrasena = value; OnPropertyChanged(); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public bool IsErrorVisible
        {
            get => _isErrorVisible;
            set { _isErrorVisible = value; OnPropertyChanged(); }
        }

        public ICommand RegisterUserCommand { get; }
        public ICommand NavigateToLoginCommand { get; } // Nuevo comando


        public RegisterUserViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            RegisterUserCommand = new RelayCommand(RegisterUser, CanRegisterUser);
            NavigateToLoginCommand = new RelayCommand(NavigateToLogin); // Inicializar el nuevo comando

        }

        private bool CanRegisterUser(object parameter)
        {
            return !string.IsNullOrWhiteSpace(NombreUsuario) &&
                   !string.IsNullOrWhiteSpace(CorreoElectronico) &&
                   !string.IsNullOrWhiteSpace(Contrasena) &&
                   !string.IsNullOrWhiteSpace(ConfirmarContrasena);
        }

        private void RegisterUser(object parameter)
        {
            if (!ValidateInputs())
                return;

            try
            {
                // Usar el repositorio en lugar del contexto directamente
                var result = _userRepository.RegisterUser(
                    NombreUsuario,
                    CorreoElectronico,
                    Contrasena // El repositorio debería encargarse del hashing
                );

                if (result != 0)
                {
                    MessageBox.Show("Usuario registrado con éxito.", "Éxito",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetForm();
                }
                else
                {
                    ErrorMessage = "No se pudo registrar el usuario. Intente nuevamente.";
                    IsErrorVisible = true;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al registrar: {ex.Message}";
                IsErrorVisible = true;
            }
        }

        private bool ValidateInputs()
        {
            // Validación de campos vacíos
            if (string.IsNullOrWhiteSpace(NombreUsuario) ||
                string.IsNullOrWhiteSpace(CorreoElectronico) ||
                string.IsNullOrWhiteSpace(Contrasena) ||
                string.IsNullOrWhiteSpace(ConfirmarContrasena))
            {
                ErrorMessage = "Todos los campos son obligatorios.";
                IsErrorVisible = true;
                return false;
            }

            // Validación de formato de email
            if (!IsValidEmail(CorreoElectronico))
            {
                ErrorMessage = "Por favor ingrese un correo electrónico válido.";
                IsErrorVisible = true;
                return false;
            }

            // Validación de contraseña
            if (Contrasena.Length < 8)
            {
                ErrorMessage = "La contraseña debe tener al menos 8 caracteres.";
                IsErrorVisible = true;
                return false;
            }

            // Confirmación de contraseña
            if (Contrasena != ConfirmarContrasena)
            {
                ErrorMessage = "Las contraseñas no coinciden.";
                IsErrorVisible = true;
                return false;
            }

            IsErrorVisible = false;
            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        private void ResetForm()
        {
            NombreUsuario = string.Empty;
            CorreoElectronico = string.Empty;
            Contrasena = string.Empty;
            ConfirmarContrasena = string.Empty;
            IsErrorVisible = false;
        }

        private void NavigateToLogin(object parameter)
        {
            var loginWindow = new LoginView();
            loginWindow.Show();

            // Cerrar la ventana de registro actual (RDUsuario)
            if (Application.Current.Windows.OfType<RDUsuario>().FirstOrDefault() is RDUsuario currentRegisterWindow)
            {
                currentRegisterWindow.Close();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
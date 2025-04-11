using GoDentalAPP.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;
using GoDentalAPP.Helpers;

namespace GoDentalAPP.ViewModels
{
    public class RegisterUserViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _dbContext; // Inyectar el contexto

        private string _nombreUsuario = string.Empty;
        private string _correoElectronico = string.Empty;
        private string _contrasena = string.Empty;

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

        public ICommand RegisterUserCommand { get; }

        public RegisterUserViewModel(AppDbContext dbContext)
        {
            _dbContext = dbContext; // Asignar el contexto inyectado
            RegisterUserCommand = new RelayCommand(RegisterUser);
        }

        private void RegisterUser(object parameter)
        {
            var usuario = new User
            {
                NombreUsuario = NombreUsuario,
                CorreoElectronico = CorreoElectronico,
                Contrasena = Contrasena, // Asegúrate de encriptar la contraseña
                FechaRegistro = DateTime.Now,
                Estado = true
            };

            _dbContext.Users.Add(usuario);
            _dbContext.SaveChanges();

            MessageBox.Show("Usuario registrado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

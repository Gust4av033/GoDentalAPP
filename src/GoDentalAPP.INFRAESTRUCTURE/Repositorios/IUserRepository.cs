using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using GoDentalAPP.Infrastructure.Persistence;
using BCrypt.Net;
using GoDentalAPP.Core.Entities;

namespace GoDentalAPP.INFRAESTRUCTURE.Repositorios
{
    public interface IUserRepository
    {
        int RegisterUser(string nombreUsuario, string correo, string contrasena);
        User GetUserByUsername(string username);
        bool VerifyCredentials(string username, string password);
    }

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int RegisterUser(string nombreUsuario, string correo, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new ArgumentException("El nombre de usuario no puede estar vacío", nameof(nombreUsuario));

            if (string.IsNullOrWhiteSpace(correo))
                throw new ArgumentException("El correo electrónico no puede estar vacío", nameof(correo));

            if (string.IsNullOrWhiteSpace(contrasena))
                throw new ArgumentException("La contraseña no puede estar vacía", nameof(contrasena));

            var contrasenaHash = HashPassword(contrasena);

            var parameters = new[] {
                new SqlParameter("@NombreUsuario", nombreUsuario),
                new SqlParameter("@Correo", correo),
                new SqlParameter("@ContrasenaHash", contrasenaHash)
            };

            try
            {
                return _context.Database.ExecuteSqlRaw(
                    "EXEC InsertarUsuario @NombreUsuario, @Correo, @ContrasenaHash",
                    parameters);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al registrar el usuario", ex);
            }
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users
                .AsNoTracking()
                .FirstOrDefault(u => u.NombreUsuario == username);
        }

        public bool VerifyCredentials(string username, string password)
        {
            var user = GetUserByUsername(username);
            if (user == null) return false;

            return VerifyPassword(password, user.Contrasena);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(inputPassword, storedHash);
            }
            catch
            {
                // Fallback para contraseñas antiguas si es necesario
                using var sha256 = System.Security.Cryptography.SHA256.Create();
                var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputPassword));
                var hashSha256 = BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
                return hashSha256 == storedHash.ToLowerInvariant();
            }
        }
    }
}
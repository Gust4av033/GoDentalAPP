using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using GoDentalAPP.Infrastructure.Persistence;
using BCrypt.Net;

namespace GoDentalAPP.INFRAESTRUCTURE.Repositorios
{
    public interface IUserRepository
    {
        int RegisterUser(string nombreUsuario, string correo, string contrasena);
        // Otros métodos que necesites...
    }

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public int RegisterUser(string nombreUsuario, string correo, string contrasena)
        {
            var contrasenaHash = HashPassword(contrasena);

            var parameters = new[] {
                new SqlParameter("@NombreUsuario", nombreUsuario),
                new SqlParameter("@Correo", correo),
                new SqlParameter("@ContrasenaHash", contrasenaHash)
            };

            return _context.Database.ExecuteSqlRaw("EXEC InsertarUsuario @NombreUsuario, @Correo, @ContrasenaHash", parameters);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}

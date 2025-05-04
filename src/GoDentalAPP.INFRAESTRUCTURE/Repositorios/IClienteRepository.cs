using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GoDentalAPP.Infrastructure.Persistence;
using GoDentalAPP.src.GoDentalAPP.CORE.Entities;

namespace GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Repositorios
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> GetClientesAsync();
        Task<List<Cliente>> GetClientesActivosAsync();
        Task<List<Cliente>> GetClientesInactivosAsync();
        Task<Cliente> GetClienteDetallesAsync(int id);
        Task CreateClienteAsync(Cliente cliente);
        Task UpdateClienteAsync(int clienteId, Cliente cliente);
        Task DeleteClienteAsync(int id);
        Task DesactivarClienteAsync(int clienteId, string motivoInactivacion);
        Task EliminarClienteLogicoAsync(int clienteId);
        
    }

    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetClientesAsync()
        {
            return await _context.Clientes
                .FromSqlRaw("EXEC [dbo].[MostrarClientes]")
                .ToListAsync();
        }

        public async Task<List<Cliente>> GetClientesActivosAsync()
        {
            return await _context.Clientes
                .FromSqlRaw("EXEC [dbo].[MostrarClientesActivos]")
                .ToListAsync();
        }

        public async Task<List<Cliente>> GetClientesInactivosAsync()
        {
            return await _context.Clientes
                .FromSqlRaw("EXEC [dbo].[MostrarClientesInactivos]")
                .ToListAsync();
        }

        public async Task<Cliente> GetClienteDetallesAsync(int id)
        {
            return await _context.Clientes
                .Include(c => c.Estado)
                .Include(c => c.TipoDocumento)
                .FirstOrDefaultAsync(c => c.ClienteID == id);
        }

        public async Task<Cliente> GetClienteAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task CreateClienteAsync(Cliente cliente)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[InsertarCliente] " +
                "@NombreCompleto = {0}, " +
                "@Telefono = {1}, " +
                "@CorreoElectronico = {2}, " +
                "@Direccion = {3}, " +
                "@LinkDireccion = {4}, " +
                "@NIT = {5}, " +
                "@NRC = {6}, " +
                "@TipoContribuyente = {7}, " +
                "@Giro = {8}, " +
                "@TipoDocumentoID = {9}, " +
                "@NumeroDocumento = {10}",
                cliente.NombreCompleto,
                cliente.Telefono,
                cliente.CorreoElectronico,
                cliente.Direccion,
                cliente.LinkDireccion,
                cliente.NIT,
                cliente.NRC,
                cliente.TipoContribuyente,
                cliente.Giro,
                cliente.TiposDocumentoID ?? (object)DBNull.Value,
                cliente.NumeroDocumento
            );
        }

        public async Task UpdateClienteAsync(int clienteId, Cliente cliente)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[EditarCliente] @ClienteID = {0}, @NombreCompleto = {1}, @Telefono = {2}, @CorreoElectronico = {3}, @Direccion = {4}, @LinkDireccion = {5}",
                clienteId,
                cliente.NombreCompleto,
                cliente.Telefono,
                cliente.CorreoElectronico,
                cliente.Direccion,
                cliente.LinkDireccion
            );
        }

        public async Task DeleteClienteAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[EliminarCliente] @ClienteID = {0}",
                id
            );
        }

        public async Task DesactivarClienteAsync(int clienteId, string motivoInactivacion)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[DesactivarCliente] @ClienteID = {0}, @MotivoInactivacion = {1}",
                clienteId,
                motivoInactivacion
            );
        }

        public async Task EliminarClienteLogicoAsync(int clienteId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[EliminarClienteLogico] @ClienteID = {0}",
                clienteId
            );
        }

        
    }
}

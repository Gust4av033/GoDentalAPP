using GoDentalAPP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GoDentalAPP.Infrastructure.Persistence;

namespace GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Repositorios
{
    public interface IClienteRepository
    {
        Task<Cliente> Crear(Cliente cliente);
        Task<IEnumerable<Cliente>> ObtenerTodos();
        Task<Cliente> ObtenerPorId(int id);
    }

    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;
        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Cliente> Crear(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }
        public async Task<IEnumerable<Cliente>> ObtenerTodos()
        {
            return await _context.Clientes.ToListAsync();
        }
        public async Task<Cliente> ObtenerPorId(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }
    }
}

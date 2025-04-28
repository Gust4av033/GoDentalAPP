using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoDentalAPP.Infrastructure.Persistence;
using GoDentalAPP.src.GoDentalAPP.CORE.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Repositorios
{
    public interface IProveedorRepository
    {
        Task<IEnumerable<Proveedor>> GetAllAsync();
    }

    // ProveedorRepository.cs
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly AppDbContext _context;

        public ProveedorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Proveedor>> GetAllAsync()
        {
            return await _context.Proveedores.ToListAsync();
        }
    }
}

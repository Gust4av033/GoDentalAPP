using GoDentalAPP.Infrastructure.Persistence;
using GoDentalAPP.src.GoDentalAPP.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Repositorios
{
    public interface IEstadoRepository
    {
        Task<List<Estado>> GetAllAsync();
        Task<Estado> GetByIdAsync(int id);
    }

    public class EstadoRepository : IEstadoRepository
    {
        private readonly AppDbContext _context;

        public EstadoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Estado>> GetAllAsync()
        {
            return await _context.Estados.ToListAsync();
        }

        public async Task<Estado> GetByIdAsync(int id)
        {
            return await _context.Estados.FindAsync(id);
        }
    }
}

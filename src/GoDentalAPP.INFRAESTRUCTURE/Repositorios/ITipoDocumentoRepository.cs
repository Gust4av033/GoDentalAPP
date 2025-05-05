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
    public interface ITipoDocumentoRepository
    {
        Task<List<TipoDocumento>> GetAllAsync();
        Task<TipoDocumento> GetByIdAsync(int id);
    }

    public class TipoDocumentoRepository : ITipoDocumentoRepository
    {
        private readonly AppDbContext _context;

        public TipoDocumentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TipoDocumento>> GetAllAsync()
        {
            return await _context.TiposDocumento.ToListAsync();
        }

        public async Task<TipoDocumento> GetByIdAsync(int id)
        {
            return await _context.TiposDocumento.FindAsync(id);
        }
    }
}

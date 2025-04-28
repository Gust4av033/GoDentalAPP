using GoDentalAPP.src.GoDentalAPP.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoDentalAPP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Repositorios
{

    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetAllAsync();
    }

    // CategoriaRepository.cs
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            return await _context.Categorias.ToListAsync();
        }
    }
}

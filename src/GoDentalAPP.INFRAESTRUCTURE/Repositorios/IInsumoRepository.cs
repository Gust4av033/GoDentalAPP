using System.Collections.Generic;
using System.Threading.Tasks;
using GoDentalAPP.Core.Entities;
using GoDentalAPP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Repositorios
{
    public interface IInsumoRepository
    {
        Task<List<InsumoDental>> GetInsumosDentalesAsync();
        Task<InsumoDental> GetInsumoDentalAsync(int id);
        Task CreateInsumoDentalAsync(InsumoDental insumoDental);
        Task UpdateInsumoDentalAsync(InsumoDental insumoDental);
        Task DeleteInsumoDentalAsync(int id);
    }

    public class InsumoRepository : IInsumoRepository
    {
        private readonly AppDbContext _context;

        public InsumoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<InsumoDental>> GetInsumosDentalesAsync()
        {
            return await _context.InsumosDentales.ToListAsync();
        }

        public async Task<InsumoDental> GetInsumoDentalAsync(int id)
        {
            return await _context.InsumosDentales.FindAsync(id);
        }

        public async Task CreateInsumoDentalAsync(InsumoDental insumoDental)
        {
            _context.InsumosDentales.Add(insumoDental);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInsumoDentalAsync(InsumoDental insumoDental)
        {
            _context.InsumosDentales.Update(insumoDental);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInsumoDentalAsync(int id)
        {
            var insumoDental = await GetInsumoDentalAsync(id);
            if (insumoDental != null)
            {
                _context.InsumosDentales.Remove(insumoDental);
                await _context.SaveChangesAsync();
            }
        }
    }
}
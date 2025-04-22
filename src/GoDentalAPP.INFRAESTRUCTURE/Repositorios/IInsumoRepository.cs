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
        Task UpdateInsumoDentalAsync(int insumoId, InsumoDental insumoDental);
        Task DeleteInsumoDentalAsync(int id);
        Task ActualizarStockInsumoAsync(int insumoId, int cantidadDelta);
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

        public async Task UpdateInsumoDentalAsync(int insumoId, InsumoDental insumoDental)
        {
            var existingInsumo = await _context.InsumosDentales.FindAsync(insumoId);
            if (existingInsumo != null)
            {
                // Actualizar propiedades relevantes (evitando sobrescribir campos como ID)
                existingInsumo.NombreInsumo = insumoDental.NombreInsumo;
                existingInsumo.Descripcion = insumoDental.Descripcion;
                existingInsumo.PrecioUnitario = insumoDental.PrecioUnitario;
                existingInsumo.CantidadEnStock = insumoDental.CantidadEnStock;
                // ... otras propiedades

                await _context.SaveChangesAsync();
            }
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

        // Implementación en InsumoRepository
        public async Task ActualizarStockInsumoAsync(int insumoId, int cantidadDelta)
        {
            var existinginsumo = await _context.InsumosDentales.FindAsync(insumoId);
            if (existinginsumo != null)
            {
                existinginsumo.CantidadEnStock += cantidadDelta;
                await _context.SaveChangesAsync();
            }
        }
    }
}
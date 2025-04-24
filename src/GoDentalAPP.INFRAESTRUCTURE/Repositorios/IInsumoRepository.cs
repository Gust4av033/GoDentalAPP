using System.Collections.Generic;
using System.Threading.Tasks;
using GoDentalAPP.src.GoDentalAPP.CORE.Entities;
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
            return await _context.InsumosDentales
         .Include(i => i.Categoria)
         .Include(i => i.Proveedor)
         .AsNoTracking()
         .Select(i => new InsumoDental
         {
             InsumoID = i.InsumoID,
             NombreInsumo = i.NombreInsumo,
             Descripcion = i.Descripcion ?? string.Empty, // Manejar nulos
             PrecioUnitario = i.PrecioUnitario,
             CantidadEnStock = i.CantidadEnStock,
             FechaRegistro = i.FechaRegistro,
             FechaVencimiento = i.FechaVencimiento,
             ProveedorID = i.ProveedorID,
             CategoriaID = i.CategoriaID,
             CodigoBarras = i.CodigoBarras ?? string.Empty, // Manejar nulos
             TieneImpuesto = i.TieneImpuesto,
             Categoria = i.Categoria, // Puede ser null
             Proveedor = i.Proveedor // Puede ser null
         })
         .ToListAsync();
        }

        public async Task<InsumoDental> GetInsumoDentalAsync(int id)
        {
            return await _context.InsumosDentales
                .Include(i => i.Categoria)
                .Include(i => i.Proveedor)
                .FirstOrDefaultAsync(i => i.InsumoID == id);
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
                _context.Entry(existingInsumo).CurrentValues.SetValues(insumoDental);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteInsumoDentalAsync(int id)
        {
            var insumo = await _context.InsumosDentales.FindAsync(id);
            if (insumo != null)
            {
                _context.InsumosDentales.Remove(insumo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ActualizarStockInsumoAsync(int insumoId, int cantidadDelta)
        {
            var insumo = await _context.InsumosDentales.FindAsync(insumoId);
            if (insumo != null)
            {
                insumo.CantidadEnStock += cantidadDelta;
                await _context.SaveChangesAsync();
            }
        }
    }
}
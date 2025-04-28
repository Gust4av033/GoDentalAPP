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
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[AgregarProducto] @NombreInsumo = {0}, @Descripcion = {1}, @PrecioUnitario = {2}, @CantidadEnStock = {3}, @FechaVencimiento = {4}, @ProveedorID = {5}, @CategoriaID = {6}",
                insumoDental.NombreInsumo,
                insumoDental.Descripcion,
                insumoDental.PrecioUnitario,
                insumoDental.CantidadEnStock,
                insumoDental.FechaVencimiento,
                insumoDental.ProveedorID,
                insumoDental.CategoriaID ?? (object)DBNull.Value // Manejar nulos
            );
        }

        public async Task UpdateInsumoDentalAsync(int insumoId, InsumoDental insumoDental)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[EditarProducto] @InsumoID = {0}, @NombreInsumo = {1}, @Descripcion = {2}, @PrecioUnitario = {3}, @CantidadEnStock = {4}, @FechaVencimiento = {5}, @ProveedorID = {6}, @CategoriaID = {7}",
                insumoId,
                insumoDental.NombreInsumo,
                insumoDental.Descripcion,
                insumoDental.PrecioUnitario,
                insumoDental.CantidadEnStock,
                insumoDental.FechaVencimiento,
                insumoDental.ProveedorID,
                insumoDental.CategoriaID ?? (object)DBNull.Value
            );
        }

        public async Task DeleteInsumoDentalAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[EliminarProducto] @InsumoID = {0}",
                id
            );
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
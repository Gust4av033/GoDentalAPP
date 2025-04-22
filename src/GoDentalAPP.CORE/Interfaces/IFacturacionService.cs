
using GoDentalAPP.Core.Entities;
using GoDentalAPP.src.GoDentalAPP.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Interfaces
{
    public interface IFacturacionService
    {
        Task<Factura> CrearFacturaNormal(FacturaDto facturaDto);
        Task<Factura> CrearCreditoFiscal(FacturaDto facturaDto);
    }
}

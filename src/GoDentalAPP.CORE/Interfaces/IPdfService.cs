using GoDentalAPP.src.GoDentalAPP.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Interfaces
{
    public interface IPdfService
    {
        Task<string> GenerarPdfFactura(Factura factura);
    }
}

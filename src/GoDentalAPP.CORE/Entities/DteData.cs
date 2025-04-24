using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Entities
{
    public class DteData
    {
        public Identificacion Identificacion { get; set; }
        public Emisor Emisor { get; set; }
        public Receptor Receptor { get; set; }
        public List<CuerpoDocumento> CuerpoDocumento { get; set; }
        public Resumen Resumen { get; set; }
        public Extension Extension { get; set; }
    }

    public class Identificacion
    {
        public int Version { get; set; } = 1;
        public string Ambiente { get; set; } = "PRODUCCION";
        public string TipoDte { get; set; } // 01 o 03
        public string NumeroControl { get; set; }
        public string CodigoGeneracion { get; set; }
        public string FechaEmision { get; set; } // Formato: yyyy-MM-ddTHH:mm:ss
    }

    public class Emisor
    {
        public string Nit { get; set; }
        public string Nrc { get; set; }
        public string Nombre { get; set; }
        public string CodigoActividad { get; set; }
        public string DescripcionActividad { get; set; }
        public Direction Direccion { get; set; }
    }

    public class Receptor
    {
        public string Nit { get; set; }
        public string Nombre { get; set; }
        public Direction Direccion { get; set; }
    }

    public class CuerpoDocumento
    {
        public int NumItem { get; set; }
        public decimal Cantidad { get; set; }
        public string Codigo { get; set; } // Código de barras
        public string UniMedida { get; set; } = "UNI";
        public string Descripcion { get; set; }
        public decimal PrecioUni { get; set; }
        public decimal MontoDescu { get; set; }
        public decimal VentaGravada { get; set; }
        public string Tributos { get; set; } // "20" para IVA
        public decimal IvaItem { get; set; }
    }

    public class Resumen
    {
        public decimal TotalNoSuj { get; set; }
        public decimal TotalExenta { get; set; }
        public decimal TotalGravada { get; set; }
        public decimal SubTotalVentas { get; set; }
        public decimal TotalDescu { get; set; }
        public decimal IvaRete1 { get; set; }
        public decimal MontoTotalOperacion { get; set; }
        public string TotalLetras { get; set; }
        public List<Pago> Pagos { get; set; }
    }

    public class Pago
    {
        public int Codigo { get; set; } // 1=Contado, 2=Crédito
        public decimal MontoPago { get; set; }
    }
}

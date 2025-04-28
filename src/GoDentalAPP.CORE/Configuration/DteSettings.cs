using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Configuration
{
    public class DteSettings
    {
        public string ApiUrl { get; set; }
        public string Nit { get; set; }
        public string Nrc { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string NombreEmpresa { get; set; }
        public string CodigoActividad { get; set; }
    }
    public class DteResponse
    {
        public string SelloRecibido { get; set; }
        public string Estado { get; set; }
        public string CodigoGeneracion { get; set; }
        public string NumeroControl { get; set; }
        public DateTime FechaHoraCertificacion { get; set; }
    }

    public class AuthResponse
    {
        public string Token { get; set; }
        public DateTime Expira { get; set; }
        public string Mensaje { get; set; }
    }
}

namespace GoDentalAPP.Core.Entities
{
    
    public class Cliente
    {
        public int ClienteID { get; set; }
        public string NombreCompleto { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string Direccion { get; set; }
        public string LinkDireccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int EstadoID { get; set; }

        // Relación con Estado
        public Estado Estado { get; set; }
    }
}
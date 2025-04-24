namespace GoDentalAPP.src.GoDentalAPP.CORE.Entities
{
    public class Estado
    {
        public int EstadoID { get; set; }
        public string NombreEstado { get; set; }

        // Relación inversa con Cliente
        public ICollection<Cliente> Clientes { get; set; }
        public ICollection<User> Usuarios { get; set; }
    }
}
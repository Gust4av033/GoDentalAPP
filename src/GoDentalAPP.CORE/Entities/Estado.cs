﻿using GoDentalAPP.src.GoDentalAPP.APP.Views.Login;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Entities
{
    public class Estado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EstadoID { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreEstado { get; set; }

        // Navigation properties
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<PedidoProveedor> PedidosProveedores { get; set; }
        public virtual ICollection<Proveedor> Proveedores { get; set; }
        public virtual ICollection<User> Usuarios { get; set; }
    }
}
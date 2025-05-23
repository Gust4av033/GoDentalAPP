﻿using GoDentalAPP.src.GoDentalAPP.APP.Views.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Entities
{
    public class PedidoProveedor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PedidoID { get; set; }

        [ForeignKey("Proveedor")]
        public int? ProveedorID { get; set; }

        public DateTime FechaPedido { get; set; } = DateTime.Now;

        public DateTime? FechaEntrega { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalPedido { get; set; }

        [ForeignKey("Usuario")]
        public int? UsuarioID { get; set; }

        [ForeignKey("Estado")]
        public int? EstadoID { get; set; }

        // Navigation properties
        public virtual Proveedor Proveedor { get; set; }
        public virtual User Usuario { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual ICollection<DetallePedidoProveedor> Detalles { get; set; }
    }
}

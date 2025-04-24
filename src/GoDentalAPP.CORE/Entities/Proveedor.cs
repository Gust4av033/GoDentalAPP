using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Entities
{
    public class Proveedor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProveedorID { get; set; }

        [Required(ErrorMessage = "El nombre del proveedor es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string NombreProveedor { get; set; }

        [StringLength(100, ErrorMessage = "El contacto no puede exceder 100 caracteres")]
        public string Contacto { get; set; }

        [StringLength(20, ErrorMessage = "El teléfono no puede exceder 20 caracteres")]
        public string Telefono { get; set; }

        [StringLength(100, ErrorMessage = "El correo electrónico no puede exceder 100 caracteres")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido")]
        public string CorreoElectronico { get; set; }

        [StringLength(255, ErrorMessage = "La dirección no puede exceder 255 caracteres")]
        public string Direccion { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [ForeignKey("Estado")]
        public int EstadoID { get; set; } = 1; // Valor por defecto para estado activo

        // Propiedad de navegación
        public virtual Estado Estado { get; set; }

        // Relación inversa con InsumosDentales
        public virtual ICollection<InsumoDental> Insumos { get; set; }

        // Relación inversa con PedidosProveedores
        //public virtual ICollection<PedidoProveedor> Pedidos { get; set; }
    }
}

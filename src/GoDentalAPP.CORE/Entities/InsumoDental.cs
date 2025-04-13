using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoDentalAPP.Core.Entities
{
    public class InsumoDental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InsumoID { get; set; }

        [Required(ErrorMessage = "El nombre del insumo es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        [Column("NombreInsumo")]
        public string NombreInsumo { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }  // Cambiado a no-nullable

        [Required(ErrorMessage = "La cantidad en stock es obligatoria")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad no puede ser negativa")]
        public int CantidadEnStock { get; set; }  // Cambiado a no-nullable

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;  // Valor por defecto

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaVencimiento { get; set; }  // Nullable porque puede no tener

        [ForeignKey("Proveedor")]
        public int? ProveedorID { get; set; }

        [ForeignKey("Categoria")]
        public int? CategoriaID { get; set; }

        /* Propiedades de navegación (opcionales pero recomendadas)
        public virtual Proveedor Proveedor { get; set; }
        public virtual Categoria Categoria { get; set; }*/
    }
}
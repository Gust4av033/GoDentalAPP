using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Entities
{
    public class ControlVentasMensuales
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ControlID { get; set; }

        [Required]
        public int Mes { get; set; }

        [Required]
        public int Anio { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalVentas { get; set; }
    }
}

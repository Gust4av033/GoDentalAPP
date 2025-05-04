using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Entities
{
    public class SeriesFacturacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SerieId { get; set; }

        [Required]
        [StringLength(2)]
        public string TipoDocumento { get; set; }

        [Required]
        [StringLength(10)]
        public string Serie { get; set; }

        [Required]
        public int NumeroActual { get; set; }

        [Required]
        public bool Activa { get; set; } = true;
    }
}

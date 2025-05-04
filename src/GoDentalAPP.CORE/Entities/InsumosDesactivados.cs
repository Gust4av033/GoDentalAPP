using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Entities
{
    public class InsumosDesactivados
    {
        [Key]
        [ForeignKey("InsumoDental")]
        public int InsumoID { get; set; }

        [StringLength(255)]
        public string? MotivoDesactivacion { get; set; }

        public DateTime FechaDesactivacion { get; set; } = DateTime.Now;

        // Navigation property
        public virtual InsumoDental InsumoDental { get; set; }
    }
}

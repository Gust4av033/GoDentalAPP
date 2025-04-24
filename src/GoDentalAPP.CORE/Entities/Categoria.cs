
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Entities
{
    public class Categoria
    {
        [Key]
        public int CategoriaID { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreCategoria { get; set; }

        // Opcional: relación inversa
        public virtual ICollection<InsumoDental> Insumos { get; set; }
    }
}

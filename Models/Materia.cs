using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Models
{
    public class Materia
    {
        [Required]
        public int MateriaId { get; set; }
        [Required]
        public string Nombre { get; set; }
        // Otras propiedades
        [Required]
        public int ProfesorId { get; set; }

        //[NotMapped]
       // public string PropiedadNoDeseada { get; set; }
        public Profesor Profesor { get; set; }
      
    }
}

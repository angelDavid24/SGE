namespace SGE.Models
{
    public class Calificacion
    {
        public int CalificacionId { get; set; }
        public int MateriaId { get; set; }
        public Materia Materia { get; set; }
        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; }
        public int Valor { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SGE.Models;

namespace SGE.Data
{
    public class SGEContext : DbContext
    {
        public SGEContext (DbContextOptions<SGEContext> options)
            : base(options)
        {
        }

        public DbSet<SGE.Models.Alumno> Alumno { get; set; } = default!;
        public DbSet<SGE.Models.Profesor> Profesor { get; set; } = default!;
        public DbSet<SGE.Models.Materia> Materia { get; set; } = default!;
        public DbSet<SGE.Models.Calificacion> Calificacion { get; set; } = default!;
    }
}

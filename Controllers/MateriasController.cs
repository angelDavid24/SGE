using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGE.Data;
using SGE.Models;

namespace SGE.Controllers
{
    public class MateriasController : Controller
    {
        private readonly SGEContext _context;

        public MateriasController(SGEContext context)
        {
            _context = context;
        }

        //GET: Materias
        public async Task<IActionResult> Index()
        {
            var sGEContext = _context.Materia.Include(m => m.Profesor);
            return View(await sGEContext.ToListAsync());
        }
        //public async Task<IActionResult> Index()
        //{
        //    var materias = await _context.Materia
        //        .Include(m => m.Profesor) // Incluir la propiedad de navegación Profesor
        //        .ToListAsync();

        //    // Ahora, puedes ajustar los resultados según tus necesidades
        //    var materiasConNombreProfesor = materias.Select(m => new Materia
        //    {
        //        MateriaId = m.MateriaId,
        //        Nombre = m.Nombre,
        //        Profesor = new Profesor
        //        {
        //            Nombre = m.Profesor.Nombre // Incluir solo el nombre del profesor en lugar del objeto completo
        //                                       // Otros campos del profesor si es necesario
        //        }
        //    });

        //    return View(materiasConNombreProfesor);
        //}

        // GET: Materias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materia = await _context.Materia
                .Include(m => m.Profesor)
                .FirstOrDefaultAsync(m => m.MateriaId == id);
            if (materia == null)
            {
                return NotFound();
            }

            return View(materia);
        }

        // GET: Materias/Create
        public IActionResult Create()
        {
            ViewData["ProfesorId"] = new SelectList(_context.Profesor, "ProfesorId", "Nombre");
            return View();
        }

        // POST: Materias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MateriaId,Nombre,ProfesorId")] Materia materia)
        {
           
                //if (ModelState.IsValid)
              //  {
                    _context.Add(materia);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
             //   }
          

            ViewData["ProfesorId"] = new SelectList(_context.Profesor, "ProfesorId", "Nombre", materia.Nombre);
                return View(materia);

         }

            // GET: Materias/Edit/5
            public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materia = await _context.Materia.FindAsync(id);
            if (materia == null)
            {
                return NotFound();
            }
            ViewData["ProfesorId"] = new SelectList(_context.Profesor, "ProfesorId", "Nombre", materia.ProfesorId);
            return View(materia);
        }

        // POST: Materias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MateriaId,Nombre,ProfesorId")] Materia materia)
        {
            if (id != materia.MateriaId)
            {
                return NotFound();
            }

           // if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(materia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MateriaExists(materia.MateriaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            ViewData["ProfesorId"] = new SelectList(_context.Profesor, "ProfesorId", "Nombre", materia.Nombre);
            return View(materia);
        }

        // GET: Materias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materia = await _context.Materia
                .Include(m => m.Profesor)
                .FirstOrDefaultAsync(m => m.MateriaId == id);
            if (materia == null)
            {
                return NotFound();
            }

            return View(materia);
        }

        // POST: Materias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materia = await _context.Materia.FindAsync(id);
            if (materia != null)
            {
                _context.Materia.Remove(materia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MateriaExists(int id)
        {
            return _context.Materia.Any(e => e.MateriaId == id);
        }
    }
}

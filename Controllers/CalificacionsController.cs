﻿using System;
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
    public class CalificacionsController : Controller
    {
        private readonly SGEContext _context;

        public CalificacionsController(SGEContext context)
        {
            _context = context;
        }

        // GET: Calificacions
        public async Task<IActionResult> Index()
        {
            var sGEContext = _context.Calificacion.Include(c => c.Alumno).Include(c => c.Materia);
            return View(await sGEContext.ToListAsync());
        }

        // GET: Calificacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificacion
                .Include(c => c.Alumno)
                .Include(c => c.Materia)
                .FirstOrDefaultAsync(m => m.CalificacionId == id);
            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }

        // GET: Calificacions/Create
        public IActionResult Create()
        {
            ViewData["AlumnoId"] = new SelectList(_context.Alumno, "AlumnoId", "Nombre");
            ViewData["MateriaId"] = new SelectList(_context.Materia, "MateriaId", "Nombre");
            return View();
        }

        // POST: Calificacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CalificacionId,MateriaId,AlumnoId,Valor")] Calificacion calificacion)
        {
          //  if (ModelState.IsValid)
        //   {
                _context.Add(calificacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
       //     }
            ViewData["AlumnoId"] = new SelectList(_context.Alumno, "AlumnoId", "Nombre", calificacion.AlumnoId);
            ViewData["MateriaId"] = new SelectList(_context.Materia, "MateriaId", "Nombre", calificacion.MateriaId);
            return View(calificacion);
        }

        // GET: Calificacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificacion.FindAsync(id);
            if (calificacion == null)
            {
                return NotFound();
            }
            ViewData["AlumnoId"] = new SelectList(_context.Alumno, "AlumnoId", "Nombre", calificacion.AlumnoId);
            ViewData["MateriaId"] = new SelectList(_context.Materia, "MateriaId", "Nombre", calificacion.MateriaId);
            return View(calificacion);
        }

        // POST: Calificacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CalificacionId,MateriaId,AlumnoId,Valor")] Calificacion calificacion)
        {
            if (id != calificacion.CalificacionId)
            {
                return NotFound();
            }

           
                try
                {
                    _context.Update(calificacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalificacionExists(calificacion.CalificacionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["AlumnoId"] = new SelectList(_context.Alumno, "AlumnoId", "Nombre", calificacion.AlumnoId);
            ViewData["MateriaId"] = new SelectList(_context.Materia, "MateriaId", "Nombre", calificacion.MateriaId);
            return View(calificacion);
        }

        // GET: Calificacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificacion
                .Include(c => c.Alumno)
                .Include(c => c.Materia)
                .FirstOrDefaultAsync(m => m.CalificacionId == id);
            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }

        // POST: Calificacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calificacion = await _context.Calificacion.FindAsync(id);
            if (calificacion != null)
            {
                _context.Calificacion.Remove(calificacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalificacionExists(int id)
        {
            return _context.Calificacion.Any(e => e.CalificacionId == id);
        }
    }
}

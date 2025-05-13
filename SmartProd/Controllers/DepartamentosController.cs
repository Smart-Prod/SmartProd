using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartProd.Data;
using SmartProd.Models;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authorization;   

namespace SmartProd.Controllers
{
    [Authorize]
    public class DepartamentosController : Controller
    {
        private readonly ContextMongodb _context = new ContextMongodb();
        private readonly UserManager<ApplicationUser> _userManager;

        public DepartamentosController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: Departamentos
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            return View(await _context.Departamento.Find(d => d.IdUsuario == userId).ToListAsync());
        }

        // GET: Departamentos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = _userManager.GetUserId(User);
            var departamento = await _context.Departamento
                .Find(m => m.IdUsuario == userId).FirstOrDefaultAsync();
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // GET: Departamentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,IdUsuario")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                departamento.Id = Guid.NewGuid();
                var userId = _userManager.GetUserId(User);
                departamento.IdUsuario = userId;

                await _context.Departamento.InsertOneAsync(departamento);
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        // GET: Departamentos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamento.Find(p => p.Id == id).FirstOrDefaultAsync();
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }

        // POST: Departamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Descricao,IdUsuario")] Departamento departamento)
        {
            if (id != departamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                departamento.IdUsuario = userId;
                await _context.Departamento.ReplaceOneAsync(d => d.Id == departamento.Id, departamento);
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        // GET: Departamentos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamento
                .Find(p => p.Id == id).FirstOrDefaultAsync();
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // POST: Departamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var departamento = await _context.Departamento.DeleteOneAsync(p => p.Id == id);
            
            return RedirectToAction(nameof(Index));
        }

        private bool DepartamentoExists(Guid id)
        {
            return _context.Departamento.Find(e => e.Id == id).Any();
        }
    }
}

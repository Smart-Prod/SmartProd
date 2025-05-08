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
using SmartProd.Services;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authorization;


namespace SmartProd.Controllers
{
    [Authorize]
    public class FuncionariosController : Controller
    {
        private readonly ContextMongodb _context = new ContextMongodb();
        private readonly ViaCepService _viaCep;
        private UserManager<ApplicationUser> _userManager;


        public FuncionariosController(UserManager<ApplicationUser> userManager, ViaCepService viaCep)
        {
            this._userManager = userManager;
            this._viaCep = viaCep;
        }

        // GET: Funcionario
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            return View(await _context.Funcionario.Find(f => f.IdUsuario == userId).ToListAsync());
        }

        // GET: Funcionario/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var funcionario = await _context.Funcionario.Find(f => f.Id == id).FirstOrDefaultAsync();
            if (funcionario == null) return NotFound();

            return View(funcionario);
        }

        // GET: Funcionario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                funcionario.Id = Guid.NewGuid();
                var userId = _userManager.GetUserId(User);
                funcionario.IdUsuario = userId;
                await _context.Funcionario.InsertOneAsync(funcionario);
                return RedirectToAction(nameof(Index));
            }
            return View(funcionario);
        }

        // GET: Funcionario/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var funcionario = await _context.Funcionario.Find(f => f.Id == id).FirstOrDefaultAsync();
            if (funcionario == null) return NotFound();

            return View(funcionario);
        }

        // POST: Funcionario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Funcionario funcionario)
        {
            if (id != funcionario.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                funcionario.IdUsuario = userId;
                await _context.Funcionario.ReplaceOneAsync(f => f.Id == id, funcionario);
                return RedirectToAction(nameof(Index));
            }
            return View(funcionario);
        }

        // GET: Funcionario/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var funcionario = await _context.Funcionario.Find(f => f.Id == id).FirstOrDefaultAsync();
            if (funcionario == null) return NotFound();

            return View(funcionario);
        }

        // POST: Funcionario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _context.Funcionario.DeleteOneAsync(f => f.Id == id);
            return RedirectToAction(nameof(Index));
        }
    }
}

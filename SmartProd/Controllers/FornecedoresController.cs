using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using SmartProd.Data;
using SmartProd.Models;
using SmartProd.Services;
using Microsoft.AspNetCore.Authorization;


namespace SmartProd.Controllers
{
    [Authorize]
    public class FornecedoresController : Controller
    {
        private readonly ContextMongodb _context = new ContextMongodb();
        private readonly ViaCepService _viaCep;
        private UserManager<ApplicationUser> _userManager;
        

        public FornecedoresController(UserManager<ApplicationUser> userManager, ViaCepService viaCep)
        {
            this._userManager = userManager;
            this._viaCep = viaCep;
        }

        // GET: Fornecedor
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            return View(await _context.Fornecedor.Find(f => f.IdUsuario == userId).ToListAsync());
        }

        // GET: Fornecedor/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var userId = _userManager.GetUserId(User);
            var fornecedor = await _context.Fornecedor.Find(f => f.IdUsuario == userId).FirstOrDefaultAsync();
            if (fornecedor == null) return NotFound();

            return View(fornecedor);
        }

        // GET: Fornecedor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fornecedor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                fornecedor.Id = Guid.NewGuid();
                var userId = _userManager.GetUserId(User);
                fornecedor.IdUsuario = userId;
                await _context.Fornecedor.InsertOneAsync(fornecedor);
                return RedirectToAction(nameof(Index));
            }
            return View(fornecedor);
        }

        // GET: Fornecedor/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            
            var fornecedor = await _context.Fornecedor.Find(f => f.Id == id).FirstOrDefaultAsync();
            if (fornecedor == null) return NotFound();

            return View(fornecedor);
        }

        // POST: Fornecedor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Fornecedor fornecedor)
        {
            if (id != fornecedor.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                fornecedor.IdUsuario = userId;
                await _context.Fornecedor.ReplaceOneAsync(f => f.Id == id, fornecedor);
                return RedirectToAction(nameof(Index));
            }
            return View(fornecedor);
        }

        // GET: Fornecedor/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var fornecedor = await _context.Fornecedor.Find(f => f.Id == id).FirstOrDefaultAsync();
            if (fornecedor == null) return NotFound();

            return View(fornecedor);
        }

        // POST: Fornecedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _context.Fornecedor.DeleteOneAsync(f => f.Id == id);
            return RedirectToAction(nameof(Index));
        }
    }
}

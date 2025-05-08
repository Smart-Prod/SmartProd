using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartProd.Data;
using SmartProd.Models;
using MongoDB.Driver;
using SmartProd.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace SmartProd.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly ContextMongodb _context = new ContextMongodb();
        private readonly ViaCepService _viaCep;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClientesController(UserManager<ApplicationUser> userManager, ViaCepService viaCep)
        {
            this._userManager = userManager;
            this._viaCep = viaCep;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
           
            return View(await _context.Cliente.Find(c => c.IdUsuario == userId).ToListAsync());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.Id = Guid.NewGuid();
                var userId = _userManager.GetUserId(User);
                cliente.IdUsuario = userId;

                if (!string.IsNullOrWhiteSpace(cliente.Endereco?.Cep))
                {
                    var endereco = await _viaCep.ConsultarCepAsync(cliente.Endereco.Cep);
                    if (endereco != null)
                    {
                        cliente.Endereco = endereco;
                    }
                }

                await _context.Cliente.InsertOneAsync(cliente);
                return RedirectToAction(nameof(Index));
            }

            return View(cliente);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var cliente = await _context.Cliente.Find(c => c.Id == id).FirstOrDefaultAsync();
            if (cliente == null) return NotFound();

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Cliente cliente)
        {
            if (id != cliente.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(cliente.Endereco?.Cep))
                {
                    var endereco = await _viaCep.ConsultarCepAsync(cliente.Endereco.Cep);
                    if (endereco != null)
                    {
                        cliente.Endereco = endereco;
                    }
                }
                var userId = _userManager.GetUserId(User);
                cliente.IdUsuario = userId;
                await _context.Cliente.ReplaceOneAsync(c => c.Id == id, cliente);
                return RedirectToAction(nameof(Index));
            }

            return View(cliente);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var cliente = await _context.Cliente.Find(c => c.Id == id).FirstOrDefaultAsync();
            if (cliente == null) return NotFound();

            return View(cliente);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var cliente = await _context.Cliente.Find(c => c.Id == id).FirstOrDefaultAsync();
            if (cliente == null) return NotFound();

            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _context.Cliente.DeleteOneAsync(c => c.Id == id);
            return RedirectToAction(nameof(Index));
        }
    }
}

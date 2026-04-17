using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using agenda.Data;
using agenda.Models;

namespace agenda.Controllers
{
    public class ItemVendasController : Controller
    {
        private readonly BDContext _context;

        public ItemVendasController(BDContext context)
        {
            _context = context;
        }

        // GET: ItemVendas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Itens_Vendas.ToListAsync());
        }

        // GET: ItemVendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var itemVenda = await _context.Itens_Vendas.FirstOrDefaultAsync(m => m.Id == id);
            if (itemVenda == null) return NotFound();

            return View(itemVenda);
        }

        // GET: ItemVendas/Create
        public IActionResult Create()
        {
            ViewBag.Produtos = new SelectList(_context.Produtos, "Id", "Nome");
            ViewBag.Clientes = new SelectList(_context.Agenda, "Id", "Nome");
            return View();
        }

        // POST: ItemVendas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_Agenda,Id_Produto,Quantidade,NomeCliente,NomeProduto,Data")] ItemVenda itemVenda)
        {
            // Preenche os nomes automaticamente a partir dos IDs
            var produto = await _context.Produtos.FindAsync(itemVenda.Id_Produto);
            var cliente = await _context.Agenda.FindAsync(itemVenda.Id_Agenda);

            if (produto != null) itemVenda.NomeProduto = produto.Nome;
            if (cliente != null) itemVenda.NomeCliente = cliente.Nome;

            ModelState.Remove("NomeProduto");
            ModelState.Remove("NomeCliente");

            if (ModelState.IsValid)
            {
                _context.Add(itemVenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Produtos = new SelectList(_context.Produtos, "Id", "Nome");
            ViewBag.Clientes = new SelectList(_context.Agenda, "Id", "Nome");
            return View(itemVenda);
        }

        // GET: ItemVendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var itemVenda = await _context.Itens_Vendas.FindAsync(id);
            if (itemVenda == null) return NotFound();

            ViewBag.Produtos = new SelectList(_context.Produtos, "Id", "Nome", itemVenda.Id_Produto);
            ViewBag.Clientes = new SelectList(_context.Agenda, "Id", "Nome", itemVenda.Id_Agenda);
            return View(itemVenda);
        }

        // POST: ItemVendas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Id_Agenda,Id_Produto,Quantidade,NomeCliente,NomeProduto,Data")] ItemVenda itemVenda)
        {
            if (id != itemVenda.Id) return NotFound();

            // Preenche os nomes automaticamente a partir dos IDs
            var produto = await _context.Produtos.FindAsync(itemVenda.Id_Produto);
            var cliente = await _context.Agenda.FindAsync(itemVenda.Id_Agenda);

            if (produto != null) itemVenda.NomeProduto = produto.Nome;
            if (cliente != null) itemVenda.NomeCliente = cliente.Nome;

            ModelState.Remove("NomeProduto");
            ModelState.Remove("NomeCliente");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemVenda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemVendaExists(itemVenda.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Produtos = new SelectList(_context.Produtos, "Id", "Nome", itemVenda.Id_Produto);
            ViewBag.Clientes = new SelectList(_context.Agenda, "Id", "Nome", itemVenda.Id_Agenda);
            return View(itemVenda);
        }

        // GET: ItemVendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var itemVenda = await _context.Itens_Vendas.FirstOrDefaultAsync(m => m.Id == id);
            if (itemVenda == null) return NotFound();

            return View(itemVenda);
        }

        // POST: ItemVendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemVenda = await _context.Itens_Vendas.FindAsync(id);
            if (itemVenda != null)
            {
                _context.Itens_Vendas.Remove(itemVenda);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // API: retorna nome do produto por ID (usada pelo JS)
        [HttpGet]
        public async Task<IActionResult> GetProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return NotFound();
            return Json(new { nome = produto.Nome });
        }

        private bool ItemVendaExists(int id)
        {
            return _context.Itens_Vendas.Any(e => e.Id == id);
        }
    }
}
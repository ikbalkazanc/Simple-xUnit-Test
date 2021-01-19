using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UnitTestIntegration.Web.Model;
using UnitTestIntegration.Web.Repository;

namespace UnitTestIntegration.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IRepository<Table> _productRepository;

        public ProductsController(IRepository<Table> productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            return View(await _productRepository.GetAll());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var table = await _productRepository.GetById(id);
            if (table == null)
            {
                return NotFound();
            }

            return View(table);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Stock")] Table table)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.Create(table);
                return RedirectToAction(nameof(Index));
            }
            return View(table);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var product = await _productRepository.GetById((int)id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Price,Stock")] Table table)
        {
            if (id != table.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _productRepository.Update(table);

                return RedirectToAction(nameof(Index));
            }
            return View(table);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var table = await _productRepository.GetById(id);
            if (table == null)
            {
                return NotFound();
            }

            return View(table);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var table = await _productRepository.GetById(id);
            _productRepository.Delete(table);
            return RedirectToAction(nameof(Index));
        }

        private bool TableExists(int id)
        {
            var product = _productRepository.GetById(id).Result;

            if (product == null)
                return false;
            else
                return true;
        }
    }
}

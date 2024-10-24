using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbContextB;
using ModelsB.Product_B;
using ApplicationB.Services_B.Product;

namespace DTOsB.Controllers
{
    public class ProductBsController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IProductService productService;

        public ProductBsController(IProductService _productService, IWebHostEnvironment _webHostEnvironment)
        {
           productService= _productService;
            webHostEnvironment = _webHostEnvironment;
        }

        // GET: ProductBs
        public async Task<IActionResult> Index()
        {
            return View(await productService.GetAllProductsAsync());
        }

        // GET: ProductBs/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
    //    {
        //        return NotFound();
        //    }

        //    var productB = await _context.Products
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (productB == null)
        //    {
        //        return NotFound();
    //    }

        //    return View(productB);
        //}

    //    // GET: ProductBs/Create
    //    public IActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: ProductBs/Create
    //    // To protect from overposting attacks, enable the specific properties you want to bind to.
    //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Create([Bind("Price,StockQuantity,Id,CreatedBy,Created,UpdatedBy,Updated,IsDeleted")] ProductB productB)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            _context.Add(productB);
    //            await _context.SaveChangesAsync();
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(productB);
    //    }

    //    // GET: ProductBs/Edit/5
    //    public async Task<IActionResult> Edit(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var productB = await _context.Products.FindAsync(id);
    //        if (productB == null)
    //        {
    //            return NotFound();
    //        }
    //        return View(productB);
    //    }

    //    // POST: ProductBs/Edit/5
    //    // To protect from overposting attacks, enable the specific properties you want to bind to.
    //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Edit(int id, [Bind("Price,StockQuantity,Id,CreatedBy,Created,UpdatedBy,Updated,IsDeleted")] ProductB productB)
    //    {
    //        if (id != productB.Id)
    //        {
    //            return NotFound();
    //        }

    //        if (ModelState.IsValid)
    //        {
    //            try
    //            {
    //                _context.Update(productB);
    //                await _context.SaveChangesAsync();
    //            }
    //            catch (DbUpdateConcurrencyException)
    //            {
    //                if (!ProductBExists(productB.Id))
    //                {
    //                    return NotFound();
    //                }
    //                else
    //                {
    //                    throw;
    //                }
    //            }
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(productB);
    //    }

    //    // GET: ProductBs/Delete/5
    //    public async Task<IActionResult> Delete(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var productB = await _context.Products
    //            .FirstOrDefaultAsync(m => m.Id == id);
    //        if (productB == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(productB);
    //    }

    //    // POST: ProductBs/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> DeleteConfirmed(int id)
    //    {
    //        var productB = await _context.Products.FindAsync(id);
    //        if (productB != null)
    //        {
    //            _context.Products.Remove(productB);
    //        }

    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }

    //    private bool ProductBExists(int id)
    //    {
    //        return _context.Products.Any(e => e.Id == id);
    //    }
    }
}

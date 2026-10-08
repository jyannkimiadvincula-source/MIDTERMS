using Microsoft.AspNetCore.Mvc;
using MIDTERMS.Models;

namespace MIDTERMS.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // =========================
        // PRODUCTS
        // =========================
        public IActionResult Index()
        {
            var products = _db.Products.ToList();

            return View(products);
        }

        // =========================
        // CREATE PRODUCT - GET
        // =========================
        public IActionResult Create()
        {
            return View();
        }

        // =========================
        // CREATE PRODUCT - POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // =========================
        // EDIT PRODUCT - GET
        // =========================
        public IActionResult Edit(int id)
        {
            var product = _db.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // =========================
        // EDIT PRODUCT - POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Products.Update(product);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // =========================
        // DELETE PRODUCT
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            _db.Products.Remove(product);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
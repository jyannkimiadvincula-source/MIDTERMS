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

public IActionResult Index(string searchString)
{
    var products = _db.Products.AsQueryable();

    if (!string.IsNullOrEmpty(searchString))
    {
        products = products.Where(p => p.Name.Contains(searchString));
    }

    ViewData["searchString"] = searchString;

    return View(products.ToList());
}
    }
}
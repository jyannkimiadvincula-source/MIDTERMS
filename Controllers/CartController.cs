using Microsoft.AspNetCore.Mvc;
using MIDTERMS.Models;

namespace MIDTERMS.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        // CART PAGE
        public IActionResult Index()
        {
            var cartIds = HttpContext.Session.GetString("Cart");

            if (string.IsNullOrEmpty(cartIds))
            {
                return View(new List<Product>());
            }

            var ids = cartIds
                .Split(',')
                .Where(x => int.TryParse(x, out _))
                .Select(int.Parse)
                .ToList();

            var products = _db.Products
                .Where(p => ids.Contains(p.Id))
                .ToList();

            return View(products);
        }

        // ADD TO CART
        public IActionResult Add(int id)
        {
            var product = _db.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            var cart = HttpContext.Session.GetString("Cart");

            if (string.IsNullOrEmpty(cart))
            {
                cart = id.ToString();
            }
            else
            {
                cart += "," + id;
            }

            HttpContext.Session.SetString("Cart", cart);

            return RedirectToAction("Index", "Products");
        }

        // REMOVE FROM CART
        public IActionResult Remove(int id)
        {
            var cart = HttpContext.Session.GetString("Cart");

            if (!string.IsNullOrEmpty(cart))
            {
                var ids = cart
                    .Split(',')
                    .Where(x => x != id.ToString())
                    .ToList();

                HttpContext.Session.SetString(
                    "Cart",
                    string.Join(",", ids)
                );
            }

            return RedirectToAction("Index");
        }
    }
}
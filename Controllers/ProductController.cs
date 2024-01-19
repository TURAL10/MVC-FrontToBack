using HomeTaskkMVC4.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeTaskkMVC4.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            ViewBag.ProductCount = _appDbContext.Products.Count();
            var query = _appDbContext.Products.AsQueryable();
            var products = query.
                 Include(p => p.Category).
                 Include(p => p.ProductImages).
                 Take(4).
                 ToList();
            return View(products);
        }
        public IActionResult LoadMore(int skip)
        {
            var products = _appDbContext.Products.
                Include(p => p.ProductImages)
                .Include(P => P.Category)
                .Skip(skip)
                .Take(4)
                .ToList();
            skip += 4;

            return PartialView("_LoadMorePartial", products);
        }

        public IActionResult Search(string search)
        {
            var products = _appDbContext.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .Where(p => p.Name.ToLower().Contains(search.ToLower()))
                .OrderByDescending(p=>p.Id)
                .Take(10)
                .ToList();

            return PartialView("_SearchPartial", products);
                
        }
    }
}

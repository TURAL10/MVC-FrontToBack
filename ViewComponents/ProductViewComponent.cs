using HomeTaskkMVC4.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeTaskkMVC4.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly AppDbContext _appDbContext;
        public ProductViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async  Task<IViewComponentResult>InvokeAsync()
        {
            var products=_appDbContext.Products
                .Include(p=>p.Category)
                .Include(p=>p.ProductImages)
                .ToList();
            return View(await Task.FromResult(products));
        }
    }
}

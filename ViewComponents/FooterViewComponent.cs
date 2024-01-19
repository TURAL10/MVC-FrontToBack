using HomeTaskkMVC4.DAL;
using Microsoft.AspNetCore.Mvc;

namespace HomeTaskkMVC4.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        
            private readonly AppDbContext _appDbContext;
            public FooterViewComponent(AppDbContext appDbContext)
            {
                _appDbContext = appDbContext;
            }
            public async Task<IViewComponentResult> InvokeAsync()
            {
                var bio = _appDbContext.Bios.FirstOrDefault();
                return View(await Task.FromResult(bio));
            }
        }
    }

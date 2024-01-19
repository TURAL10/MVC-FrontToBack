 using HomeTaskkMVC4.DAL;
using HomeTaskkMVC4.Models;
using HomeTaskkMVC4.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HomeTaskkMVC4.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {

        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _usermanager;
 
    public HeaderViewComponent(AppDbContext appDbContext,UserManager<AppUser> userManager)
       {
            _appDbContext = appDbContext;
            _usermanager = userManager;
            
        } 
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.UserFullName = null;
            if(User.Identity.IsAuthenticated)
            {
                var user=await _usermanager.FindByNameAsync(User.Identity.Name);
                ViewBag.UserFullName = user.FullName;
            }

            ViewBag.Count = 0;
            string basket = Request.Cookies["basket"];
            if(basket != null)
            {
                var products=JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                ViewBag.Count = products.Sum(p=>p.BasketCount);
            }
          
         
             var bio =_appDbContext.Bios.FirstOrDefault();
            return View(await Task.FromResult(bio));
        }
    }
}

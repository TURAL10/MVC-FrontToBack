using HomeTaskkMVC4.DAL;
using HomeTaskkMVC4.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeTaskkMVC4.Controllers
{
    public class HomeController : Controller
    {
        public readonly AppDbContext _appDbContext;

        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            HomeVM vm = new();
            vm.Sliders = _appDbContext.Sliders.ToList();
            vm.SliderContents = _appDbContext.SliderContents.FirstOrDefault();
             ////Product sildikki viewcomponentegore
            vm.Categories = _appDbContext.Categories.ToList();
            vm.AboutContent = _appDbContext.AboutContents.FirstOrDefault();
            vm.AboutListContent = _appDbContext.AboutListContents.ToList();
            vm.ExpertContent=_appDbContext.ExpertContents.FirstOrDefault();
            vm.Experts=_appDbContext.Experts.Include(e=>e.ExpertImages).ToList();
            vm.BlogContent=_appDbContext.BlogContents.FirstOrDefault();
            vm.BlogImages = _appDbContext.BlogImages.ToList();
            vm.SaySliderContents=_appDbContext.SaySliderContents.Include(s=>s.SaySliderImages).ToList();
            vm.InstagramSliders = _appDbContext.instagramSliders.ToList();
            return View(vm);
        }
    }
}

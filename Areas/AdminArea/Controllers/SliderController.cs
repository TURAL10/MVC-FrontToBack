using HomeTaskkMVC4.DAL;
using HomeTaskkMVC4.Extension;
using HomeTaskkMVC4.Models;
using HomeTaskkMVC4.ViewModels.AdminCategory;
using HomeTaskkMVC4.ViewModels.AdminSlider;
using Microsoft.AspNetCore.Mvc;

namespace HomeTaskkMVC4.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        

        public SliderController(AppDbContext appDbContext,IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
         {
            return View(_appDbContext.Sliders.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(CreateSliderVM createSliderVM)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("Photo", "Bos Qoyma");
                return View();
            }
            if(!createSliderVM.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (createSliderVM.Photo.Length/1024> 1000)
            {
                ModelState.AddModelError("Photo", "Olchu boyukdur");
                return View();
            }
            
            Slider slider = new Slider();
            slider.ImageUrl = createSliderVM.Photo.SaveImage("img",_webHostEnvironment);
            _appDbContext.Sliders.Add(slider);
            _appDbContext.SaveChanges();


            return View();
        }

        public IActionResult Delete(int?id)
        {
            if(id==null) return NotFound();
            var existslider= _appDbContext.Sliders.FirstOrDefault(s => s.Id==id);
            if(existslider==null) return NotFound();
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", existslider.ImageUrl);
   //         if(System.IO.//File.Exists(path))
 //           {                Her Iki Terefli Silmek Ucundur
     //           System.IO.File.Delete(path);
     //       }
            _appDbContext.Sliders.Remove(existslider);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int?id)
        {
            if (id == null) return NotFound();
            var existslider = _appDbContext.Sliders.FirstOrDefault(s => s.Id == id);
            if (existslider == null) return NotFound();
            var updatesliderVM = new UpdateSliderVM { Id = existslider.Id,ImageUrl=existslider.ImageUrl };
            return View(updatesliderVM);
        }
        [HttpPost]
        public IActionResult Update(UpdateSliderVM updatesliderVM)
        {
            if (!ModelState.IsValid) return NotFound();
            
                var existslider = _appDbContext.Sliders.FirstOrDefault(c => c.Id == updatesliderVM.Id);
           
            if (!updatesliderVM.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (updatesliderVM.Photo.Length / 1024 > 1000)
            {
                ModelState.AddModelError("Photo", "Olchu boyukdur");
                return View();
            }

            existslider.Id = updatesliderVM.Id;
            existslider.ImageUrl = updatesliderVM.Photo.SaveImage("img", _webHostEnvironment);
            
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}

using HomeTaskkMVC4.DAL;
using HomeTaskkMVC4.Models;
using HomeTaskkMVC4.ViewModels.AdminCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeTaskkMVC4.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_appDbContext.Categories.ToList());
        }
        public IActionResult Detail(int? Id)
        {
            if (Id == null) return NotFound();
            var existcategory = _appDbContext.Categories.FirstOrDefault(c => c.Id == Id);
            if (existcategory == null) return NotFound();
            return View(existcategory);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public IActionResult Create(CreateCategoryVM createCategoryVM)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            if (_appDbContext.Categories.Any(c => c.Name.ToLower() == createCategoryVM.Name.ToLower())) 
            {
                ModelState.AddModelError("Name", "Bu Adli Melumat Movcuddur");
                return View();
            }
            Category category = new();
            category.Name=createCategoryVM.Name;
            category.Desc=createCategoryVM.Desc;
            _appDbContext.Categories.Add(category);
            _appDbContext.SaveChanges();


            return RedirectToAction("Index");
        }

        public IActionResult Update(int ? id)
        {
            if(id==null) return NotFound();
            var existcategory=_appDbContext.Categories.FirstOrDefault(c=>c.Id == id);
            if (existcategory == null) return NotFound();
            var updateCategoryVM=new UpdateCategoryVM {Id=existcategory.Id, Name=existcategory.Name, Desc=existcategory.Desc };
            return View(updateCategoryVM );
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public IActionResult Update(UpdateCategoryVM updateCategoryVM )
        {
            if (!ModelState.IsValid) return View();
            var existcategory=_appDbContext.Categories.FirstOrDefault(c=>c.Id==updateCategoryVM.Id);

            if(_appDbContext.Categories.Any(c=>c.Name==updateCategoryVM.Name&&c.Id!=updateCategoryVM.Id))
            {
                ModelState.AddModelError("Name", "Artiq Movcuddur");
                return View();
            }
            existcategory.Name=updateCategoryVM.Name;
            existcategory.Desc=updateCategoryVM.Desc;
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var deletedcategory = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (deletedcategory == null) return NotFound();
            
            _appDbContext.Categories.Remove(deletedcategory);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}

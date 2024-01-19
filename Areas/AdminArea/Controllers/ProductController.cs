using HomeTaskkMVC4.DAL;
using HomeTaskkMVC4.Extension;
using HomeTaskkMVC4.Helpers;
using HomeTaskkMVC4.Models;
using HomeTaskkMVC4.ViewModels.AdminCategory;
using HomeTaskkMVC4.ViewModels.AdminProduct;
using HomeTaskkMVC4.ViewModels.AdminSlider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeTaskkMVC4.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(int page=1,int take=4)
        {
            var query = _context.Products.AsQueryable();
            var products=query.Include(p => p.Category)
            .Include(p => p.ProductImages)
            .AsNoTracking()
            .Skip((page - 1) * take)
            .Take(4)
             .ToList();

            
            
            Pagination<Product>pagination=new Pagination<Product>(products,CalculatePage(query.Count(),take), page);    

                 

            return View(pagination);
        }

        public int CalculatePage(int count,int take)
        {
            return (int)Math.Ceiling((decimal)(count) / take);
        }



        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(CreateProductVM createProduct)
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            if (!ModelState.IsValid) return View();
            Product newProduct = new();
            newProduct.Name = createProduct.Name;
            newProduct.Count = createProduct.Count;
            newProduct.Price = createProduct.Price;
            newProduct.CategoryId = createProduct.CategoryId;
            newProduct.ProductImages = new();

            foreach (var photo in createProduct.Photos)
            {
                if (!photo.CheckIMage())
                {
                    ModelState.AddModelError("Photos", "Only Image");
                    return View();
                }
                if (photo.CheckIMageSize(1000))
                {
                    ModelState.AddModelError("Photos", "oversize");
                    return View();
                }

                ProductImage productImage = new();
                if (photo == createProduct.Photos[0])
                {
                    productImage.IsMain = true;
                }
                productImage.ImageUrl = photo.SaveImage("img", _webHostEnvironment);
                newProduct.ProductImages.Add(productImage);
            }
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var existproduct = _context.Products.FirstOrDefault(p => p.Id == id);
            if (existproduct == null) return NotFound();
            _context.Products.Remove(existproduct);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Update(int? id)
        {

            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            if (id == null) return NotFound();
            var existproduct = _context.Products.Include(p=>p.ProductImages).FirstOrDefault(c => c.Id == id);
            
            if (existproduct == null) return NotFound();
            var updateProductVM = new UpdateProductVM
            {
                Id = existproduct.Id,
                Name = existproduct.Name,
                Price = existproduct.Price,
                CategoryId = existproduct.CategoryId,
                Count = existproduct.Count,
                ImageUrl = existproduct.ProductImages[0].ImageUrl
                
                
            };
            return View(updateProductVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(UpdateProductVM updateProductVM)
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            if (!ModelState.IsValid) return View();
            var existproduct = _context.Products.Include(p=>p.ProductImages).FirstOrDefault(c => c.Id == updateProductVM.Id);
       

            if (_context.Products.Any(c => c.Name == updateProductVM.Name && c.Id != updateProductVM.Id))
            {
                ModelState.AddModelError("Name", "Artiq Movcuddur");
                return View();
            }
            existproduct.Name = updateProductVM.Name;
            existproduct.Count = updateProductVM.Count;
            existproduct.Price = updateProductVM.Price;
            existproduct.CategoryId = updateProductVM.CategoryId;
            existproduct.Id=updateProductVM.Id;

            if(updateProductVM.Photos==null)
            {

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            foreach (var photo in updateProductVM.Photos)
            {

                    if (!photo.CheckIMage())
                    {
                        ModelState.AddModelError("Photos", "Only Image");
                        return View();
                    }
                    if (photo.CheckIMageSize(1000))
                    {
                        ModelState.AddModelError("Photos", "oversize");
                        return View();
                    }

                    ProductImage productImage = new ProductImage();
                    if (photo == updateProductVM.Photos[0])
                    {
                        productImage.IsMain = true;
                        productImage.Id = existproduct.Id;
                    }

                    existproduct.ProductImages[0].ImageUrl = photo.SaveImage("img", _webHostEnvironment);
                }
            
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
        }

    }


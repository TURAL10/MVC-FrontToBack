using HomeTaskkMVC4.DAL;
using HomeTaskkMVC4.Models;
using HomeTaskkMVC4.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomeTaskkMVC4.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public BasketController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddBasket(int ?id)
        {
            if(id==null) return NotFound();
            var existproduct=_appDbContext.Products
                .Include(p=>p.ProductImages)
                .FirstOrDefault(p=>p.Id==id);
            if (existproduct == null) return NotFound();
            List<BasketVM> list = CheckBasket();
            CheckBasketItemCount(list, existproduct.Id);

            Response.Cookies.Append("basket",JsonConvert.SerializeObject(list),
                new CookieOptions { MaxAge=TimeSpan.FromMinutes(20)});
            return RedirectToAction("index", "home");
        }

        public IActionResult ShowBasket()
        {
            string basket = Request.Cookies["basket"];
            List<BasketVM> products = new();
            if (basket != null)
            {
                 products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
              products=UpdateBasket(products);
            }
            return View(products);
        }
        private List<BasketVM> UpdateBasket(List<BasketVM> products)
        {
            foreach (var basketproduct in products)
            {
                var existproduct = _appDbContext.Products.
                    Include(p => p.ProductImages)
                    .FirstOrDefault(p => p.Id == basketproduct.Id);
                basketproduct.Name = existproduct.Name;
                basketproduct.Price = existproduct.Price;
                basketproduct.ImageUrl = existproduct.ProductImages.FirstOrDefault(p => p.IsMain).ImageUrl;

            }
            return (products);
        }

        private List<BasketVM> CheckBasket()
        {
            List<BasketVM> list;
            string basket = Request.Cookies["basket"];
            if (basket == null)
            {
                list = new();
            }
            else
            {

                list = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            }
            return list;
        }

        private void CheckBasketItemCount(List<BasketVM>list, int id)
        {

            var existProductInBasket = list.FirstOrDefault(p => p.Id == id);
            if (existProductInBasket == null)
            {
                BasketVM basketVM = new();
                basketVM.Id = id;
                basketVM.BasketCount = 1;
               
                list.Add(basketVM);
            }
            else
            {
                existProductInBasket.BasketCount++;
            }

        }

        public IActionResult Remove(int?id)
        {
           
            string basket = Request.Cookies["basket"];
            var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            var basketitem=products.FirstOrDefault(p => p.Id == id);
            if(basketitem!=null)
            {
                products.Remove(basketitem);
            }
            
            
                Response.Cookies.Append("basket", JsonConvert.SerializeObject(products),
                       new CookieOptions { MaxAge = TimeSpan.FromMinutes(20) });

                return RedirectToAction("Showbasket");
            
        }

        public IActionResult Reduce(int?id)
        {
            string basket = Request.Cookies["basket"];
            var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            var basketitem = products.FirstOrDefault(p => p.Id == id);
            basketitem.BasketCount --;
            if(basketitem.BasketCount==0)
                {
                    products.Remove(basketitem);
                }
         
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products),
                    new CookieOptions { MaxAge = TimeSpan.FromMinutes(20) });

            return RedirectToAction("Showbasket");
        }

        public IActionResult Increase(int? id)
        {
            var dataproduct = _appDbContext.Products.FirstOrDefault(p => p.Id == id);
            string basket = Request.Cookies["basket"];
            var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            var basketitem = products.FirstOrDefault(p => p.Id == id);
          
            if (basketitem.BasketCount<dataproduct.Count)
            {
                basketitem.BasketCount++;
            }
         

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products),
                    new CookieOptions { MaxAge = TimeSpan.FromMinutes(20) });

            return RedirectToAction("Showbasket");
        }


    } 
}

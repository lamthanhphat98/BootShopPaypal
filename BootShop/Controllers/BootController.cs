using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BootShop.Models;
using BootShop.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BootShop.Controllers
{
    public class BootController : Controller
    {
        BootShopContext db = new BootShopContext();

        public IActionResult ViewList()
        {
            HttpContext.Session.SetString("CURRENTUSER", "user1");
            var Color = db.TblColor.ToList();
            var SelectColor = new SelectList(Color, "ColorId", "ColorName");
            var Size = db.TblSize.ToList();
            var SelectSize = new SelectList(Size, "SizeId", "SizeName");
            ViewBag.SelectSize = SelectSize;
            ViewBag.SelectColor = SelectColor;
            var NewListBoot = db.TblBoots.OrderByDescending(x => x.Id).Take(4).Select(x => new BootViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                Price = x.Price,
                Sex = x.Sex,
                Sale = x.Sale
            });

            var SaleList = db.TblBoots.Where(b => b.Sale == true).OrderByDescending(x => x.Id).Take(4).Select(x => new BootViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                Price = x.Price,
                Sex = x.Sex,
                Sale = x.Sale
            });
            
                
            ViewBag.NewList = NewListBoot.ToList();
            ViewBag.SaleList = SaleList.ToList();
            return View();
        }
        public IActionResult Detail(int id)
        {
            
          
           
            //ViewBag.SelectColor = SelectColor;
            var list = db.TblBoots.Where(x => x.Id == id).Select(x => new BootViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                Price = x.Price,
                Sex = x.Sex,
                Sale = x.Sale,             
            });
            var Size = db.TblSize.Where(s=>s.SizeId==db.TblBoots.Where(b=>b.Id==id).FirstOrDefault().SizeId).ToList();
            var SelectSize = new SelectList(Size, "SizeId", "SizeName");
            ViewBag.SelectSize = SelectSize;
            return View(list);
        }
        public IActionResult Find(FindViewModel find)
        {
           
            var list = db.TblBoots.Where(b=>b.ColorId==find.ColorId || b.SizeId==find.SizeId).Select(x => new BootViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                Price = x.Price,
                Sex = x.Sex,
                Sale = x.Sale,
            });
            return View(list);
        }
        public JsonResult AddCart(int id)
        {
            int currentId = 0;
            return Json(new { success = true,currentId=id});
        }
        public JsonResult AddToCart(int Id,String Name,Double Price,String Picture,String SizeName)
        {
            String username = HttpContext.Session.GetString("CURRENTUSER");
            if (SizeName.Equals("Search by Size"))
            {
                return Json(new { success = false });
            }
            else
            {
                var checkExist = db.TblCart.Where(c => c.ProductId == Id && c.UserId == username).FirstOrDefault();
                if(checkExist!=null)
                {
                    checkExist.Quantity = checkExist.Quantity + 1;
                    db.SaveChanges();
                }
                else
                {
                    TblCart cart = new TblCart();
                    cart.ProductId = Id;
                    cart.ProductName = Name;
                    cart.Price = Price;
                    cart.Picture = Picture;
                    cart.SizeName = SizeName;
                    cart.Quantity = 1;
                    cart.UserId = username;
                    db.TblCart.Add(cart);
                    db.SaveChanges();

                }
            
         
            return Json(new { success = true });
            }
        }

public IActionResult ListCart()
        {
            String username = HttpContext.Session.GetString("CURRENTUSER");
            var listCart = db.TblCart.Where(u => u.UserId == username);
            ViewBag.Count = 0;
            
            if(listCart.Count()!=0)
            {
               if(HttpContext.Session.GetInt32("CURRENTORDER").ToString().Length==0)
                {
                    TblOrder order = new TblOrder();
                    order.OrderDate = DateTime.UtcNow;
                    order.UserId = username;
                    db.TblOrder.Add(order);
                    db.SaveChanges();
                    var currentOrder = db.TblOrder.Where(o => o.UserId == username).OrderByDescending(o => o.OrderId).FirstOrDefault();
                    HttpContext.Session.SetInt32("CURRENTORDER", currentOrder.OrderId);
                }
            }
            ViewBag.ListCart = listCart.ToList();
            return View();
           
        }
        public JsonResult GetAllCart()
        {
            String username = HttpContext.Session.GetString("CURRENTUSER");
            var listCart = db.TblCart.Where(u => u.UserId == username).ToList();
            //ViewBag.ListCart = listCart.ToList();
            return Json(listCart);
        }
        public JsonResult UpdatePlusCart(int id)
        {
            String username = HttpContext.Session.GetString("CURRENTUSER");
            var currentCart = db.TblCart.Where(c => c.CartId == id && c.UserId == username).ToList().FirstOrDefault();
            if(currentCart.Quantity==0)
            {
                return Json(new { success = false });
            }
            else
            {
                currentCart.Quantity = currentCart.Quantity + 1;
                db.SaveChanges();
                return Json(new { success = true });
            }
           // return Json(new { success = true });
        }
        public JsonResult UpdateMinusCart(int id)
        {
            String username = HttpContext.Session.GetString("CURRENTUSER");
            var currentCart = db.TblCart.Where(c => c.CartId == id && c.UserId == username).ToList().FirstOrDefault();
            if (currentCart.Quantity == 1)
            {
                return Json(new { success = false });
            }
            else
            {
                currentCart.Quantity = currentCart.Quantity - 1;
                db.SaveChanges();
                return Json(new { success = true });
            }
            // return Json(new { success = true });
        }
        public JsonResult DeleteCart(int id)
        {
            String username = HttpContext.Session.GetString("CURRENTUSER");
            var currentCart = db.TblCart.Where(c => c.CartId == id && c.UserId == username).ToList().FirstOrDefault();


            db.TblCart.Remove(currentCart);
                db.SaveChanges();
                return Json(new { success = true });
            
            // return Json(new { success = true });
        }
        public IActionResult Success()
        {
            String username = HttpContext.Session.GetString("CURRENTUSER");
            var oldList = db.TblCart.Where(c => c.UserId == username).ToList();
            db.TblCart.RemoveRange(oldList);
            db.SaveChanges();
            return View();
        }

    }
}
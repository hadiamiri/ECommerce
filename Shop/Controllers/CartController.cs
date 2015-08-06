using System;
using System.Linq;
using System.Web.Mvc;
using Shop.Models;
using Shop.ViewModels;

namespace Shop.Controllers
{
    public class CartController : Controller
    {


        public ActionResult Index(string returnUrl)
        {
            var model = new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        public PartialViewResult Summary()
        {
            var cart = GetCart();
            return PartialView(cart);
        }

        public ActionResult AddToCart(int id, string returnUrl)
        {
            using (var db = new Context())
            {
                Product p = db.Products.FirstOrDefault(x => x.Id == id);

                if (p != null)
                {
                    GetCart().AddToCart(p, 1);
                }
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public ActionResult RemoveFromCart(int id, string returnUrl)
        {
            using (var _db = new Context())
            {
                Product p = _db.Products.FirstOrDefault(x => x.Id == id);

                if (p != null)
                {
                    GetCart().RemoveLine(p);
                }
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            var cart = (Cart)Session["Cart"];
            if (cart != null) return cart;
            cart = new Cart();
            Session["Cart"] = cart;

            return cart;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Checkout()
        {
            using (var db = new Context())
            {
                var firstOrDefault = db.Users.FirstOrDefault(x => x.UserName.Equals(User.Identity.Name));
                if (firstOrDefault == null) return View();
                var model = new CheckoutViewModel
                {
                    Lines = GetCart().CartLines,
                    Address = firstOrDefault.Address
                };
                return View(model);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Checkout(CheckoutViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                using (var db = new Context())
                {
                    var user = db.Users.FirstOrDefault(x => x.UserName.Equals(User.Identity.Name));

                    var order = new Order
                    {
                        CreateDate = DateTime.Now,
                        IsPaid = true,
                        User = user,
                        Address = model.Address
                    };
                    db.Orders.Add(order);

                    foreach (var item in GetCart().Lines)
                    {
                        var detail = new OrderDetail
                        {
                            Count = item.Count,
                            Order = order,
                            ProductName = item.Product.Name
                        };
                        item.Product.SellCount += item.Count;
                        db.OrderDetails.Add(detail);

                    }
                    order.Price = GetCart().Lines.Sum(x => x.Count * x.Product.Price);
                    GetCart().CartLines.Clear();
                    db.SaveChanges();
                    return View("Success", order.Id);
                }
            }
            return View();
        }

    }
}
using System;
using System.Linq;
using System.Web.Mvc;
using Shop.Models;
using Shop.ViewModels;

namespace Shop.Controllers
{
    public class ViewProductController : Controller
    {
        readonly Context _db = new Context();
        public ActionResult Category(int id)
        {
            var products = _db.Products.Where(x => x.CategoryId == id);

            //var model = c.Select(x => new ProductHomeView
            //    {
            //        Id = x.Id,
            //        Description = x.Description.Length > 50 ? x.Description.Substring(0, 50) : x.Description,
            //        Name = x.Name,
            //        Price = x.Price,
            //        ReviewsCount = x.Reviews.Count,
            //        Rating = (int?)x.Reviews.Sum(t => t.Rating) / 5 ?? 0
            //    });
            var model = products.Select(x => new ProductHomeView
            {
                Id = x.Id,
                Description = x.Description.Length > 50 ? x.Description.Substring(0, 50) : x.Description,
                Name = x.Name,
                Price = x.Price,
                ReviewsCount = x.Reviews.Count,
                Rating = (int?)x.Reviews.Sum(t => t.Rating) / (x.Reviews.Count == 0 ? 1 : x.Reviews.Count()) ?? 0
            }).ToList();
            return View(model);

        }


        public ActionResult ProductDetail(int id)
        {
            var p = _db.Products.FirstOrDefault(x => x.Id == id);
            if (p == null)
            {
                return HttpNotFound("Not found");
            }
            var productDetail = new ProductDetailViewModel
            {
                Id = p.Id,
                Description = p.Description,
                Name = p.Name,
                Price = p.Price,
                Rating = !p.Reviews.Any() ? 0 : p.Reviews.Sum(x => x.Rating) / p.Reviews.Count(),
                Reviews = p.Reviews,
                ReviewsCount = p.Reviews.Count()
            };

            return View("_ItemDetail", productDetail);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddComment(int id)
        {

            var user = _db.Users.FirstOrDefault(x => x.UserName.Equals(User.Identity.Name));
            if (user != null && user.Reviews.Any(x => x.Product.Id == id))
            {
                return RedirectToAction("ProductDetail", "ViewProduct", new { id });
            }


            var firstOrDefault = _db.Products.FirstOrDefault(x => x.Id == id);

            var productName = firstOrDefault.Name;

            var model = new CommentViewModel
            {
                ProductName = productName,
                Id = firstOrDefault.Id
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddComment(CommentViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var product = _db.Products.FirstOrDefault(x => x.Id == model.Id);
                var user = _db.Users.FirstOrDefault(x => x.UserName.Equals(User.Identity.Name));

                var rating = new Review
                {
                    AddDate = DateTime.Now,
                    Rating = model.Rating,
                    Comment = model.Comment,
                    Product = product,
                    User = user
                };

                _db.Reviews.Add(rating);
                _db.SaveChanges();
                return RedirectToAction("ProductDetail", "ViewProduct", new { model.Id });
            }
            return View();
        }
    }


}
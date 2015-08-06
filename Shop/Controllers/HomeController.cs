using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;
using Shop.ViewModels;

namespace Shop.Controllers
{
    
    public class HomeController : Controller
    {
        readonly Context _db = new Context();

        public ActionResult About()
        {
            var model = _db.Contents.FirstOrDefault(x => x.Name.Equals("درباره ما"));
            if (model == null)
                throw new HttpException((int) System.Net.HttpStatusCode.NotFound, "پیدا نشد");

            return View(model);
        }

        public ActionResult ContactUs()
        {
            var model = _db.Contents.FirstOrDefault(x => x.Name.Equals("ارتباط با ما"));
            if (model == null)
                throw new HttpException((int)System.Net.HttpStatusCode.NotFound, "پیدا نشد");

            return View(model);
        }


        public ActionResult Guide()
        {
            var model = _db.Contents.FirstOrDefault(x => x.Name.Equals("راهنمای خرید"));
            if (model == null)
                throw new HttpException((int)System.Net.HttpStatusCode.NotFound, "پیدا نشد");

            return View(model);
        }


        public ActionResult Index()
        {
            var products = _db.Products.ToList();
            var model = products.Select(x => new ProductHomeView
            {
                Id = x.Id,
                Description = x.Description.Length > 50 ? x.Description.Substring(0, 50) : x.Description,
                Name = x.Name,
                Price = x.Price,
                ReviewsCount = x.Reviews.Count,
                Rating = (int)x.Reviews.Sum(t => t.Rating) / (x.Reviews.Count == 0 ? 1 : x.Reviews.Count())
            }).ToList();
            return View(model);
        }

        public ActionResult TopSell()
        {
            var model = _db.Products.OrderBy(x => x.SellCount).Select(x => new ProductHomeView
            {
                Id = x.Id,
                Description = x.Description.Length > 50 ? x.Description.Substring(0, 50) : x.Description,
                Name = x.Name,
                Price = x.Price,
                ReviewsCount = x.Reviews.Count,
                Rating = (int)x.Reviews.Sum(t => t.Rating) / (x.Reviews.Count == 0 ? 1 : x.Reviews.Count())
            }).Take(3);
            return PartialView("ItemHolder",model);
        }


        public ActionResult Hottest()
        {
            var model = _db.Products.OrderBy(x => x.AddDate).Select(x => new ProductHomeView
            {
                Id = x.Id,
                Description = x.Description.Length > 50 ? x.Description.Substring(0, 50) : x.Description,
                Name = x.Name,
                Price = x.Price,
                ReviewsCount = x.Reviews.Count,
                Rating = (int)x.Reviews.Sum(t => t.Rating) / (x.Reviews.Count == 0 ? 1 : x.Reviews.Count())
            }).Take(3);
            return PartialView("ItemHolder", model);
        }
        public ActionResult CategoryMenue()
        {
            var cat = _db.Categories.ToList();
            return PartialView("_CategoryList", cat);
        }

    }
}
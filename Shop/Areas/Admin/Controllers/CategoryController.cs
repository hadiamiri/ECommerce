using System.Linq;
using System.Web.Mvc;
using Shop.Infrastructure;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
   [SiteAuthorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        readonly Context _db = new Context();
        public ActionResult Index()
        {
            var catList = _db.Categories.ToList();
            return View(catList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (this.ModelState.IsValid)
            {
                var cat = new Category { Name = category.Name };
                _db.Categories.Add(cat);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var cat = _db.Categories.FirstOrDefault(x => x.Id == id);
            return View(cat);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (this.ModelState.IsValid)
            {
                var cat = _db.Categories.FirstOrDefault(x => x.Id == category.Id);
                if (cat != null)
                {
                    cat.Name = category.Name;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

       [HttpPost]
        public ActionResult Delete(int id)
        {
            var cat = _db.Categories.FirstOrDefault(x => x.Id == id);
            if (cat != null)
            {
                _db.Categories.Remove(cat);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
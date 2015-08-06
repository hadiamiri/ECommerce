using System;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Shop.Areas.Admin.ViewModels;
using Shop.Infrastructure;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
    [SiteAuthorize(Roles = "admin")]
    public class ProductController : Controller
    {
        readonly Context _db = new Context();
        public ActionResult Index()
        {
            var productList = _db.Products.ToList();
            return View(productList);
        }

        [HttpGet]
        public ActionResult Create()
        {

            var selectList = new SelectList(_db.Categories, "Id", "Name");
            ViewBag.Category = selectList;
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductCreate product, HttpPostedFileBase image)
        {
            if (this.ModelState.IsValid)
            {

                var p = new Product
                {
                    CategoryId = product.CategoryId,
                    Description = product.Description,
                    Price = product.Price,
                    Name = product.Name,
                    AddDate = DateTime.Now,
                    ImageMimeType = image.ContentType,
                };

                var temp = new byte[image.ContentLength];
                image.InputStream.Read(temp, 0, image.ContentLength);
                var webImageDetailImage = new WebImage(temp);
                var webImagethumImage = new WebImage(temp);

                byte[] detail = webImageDetailImage.Resize(400, 300, false, false).GetBytes();
                byte[] thumb = webImagethumImage.Resize(320, 150,false, false).GetBytes();

                p.ImageData = new byte[detail.Length];
                p.ThumbnailImage = new byte[thumb.Length];

                Array.Copy(detail, p.ImageData, detail.Length);
                Array.Copy(thumb, p.ThumbnailImage, thumb.Length);

                // image.InputStream.Read(p.ImageData, 0, image.ContentLength);
                _db.Products.Add(p);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            var selectList = new SelectList(_db.Categories, "Id", "Name");
            ViewBag.Category = selectList;
            return View();
        }
        
        [AllowAnonymous]
        public FileContentResult GetImage(int id)
        {
            var p = _db.Products.FirstOrDefault(x => x.Id == id);
            if (p != null)
            {
                return File(p.ImageData, p.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        [AllowAnonymous]
        public FileContentResult GetThumb(int id)
        {
            var p = _db.Products.FirstOrDefault(x => x.Id == id);
            if (p != null)
            {
                return File(p.ThumbnailImage, p.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var p = _db.Products.FirstOrDefault(x => x.Id == id);
            _db.Products.Remove(p);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var p = _db.Products.FirstOrDefault(x => x.Id == id);
            var selectList = new SelectList(_db.Categories, "Id", "Name", p.CategoryId);
            ViewBag.Category = selectList;
            var model = new ProductCreate
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                Description = p.Description,
                Name = p.Name,
                Price = p.Price
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProductCreate product, HttpPostedFileBase image)
        {
            if (this.ModelState.IsValid)
            {
                var p = _db.Products.FirstOrDefault(x => x.Id == product.Id);
                if (p != null)
                {
                    p.CategoryId = product.CategoryId;
                    p.Description = product.Description;
                    p.Name = product.Name;
                    p.Price = product.Price;

                    if (image != null)
                    {
                        var temp = new byte[image.ContentLength];
                        image.InputStream.Read(temp, 0, image.ContentLength);
                        var webImageDetailImage = new WebImage(temp);
                        var webImagethumImage = new WebImage(temp);

                        byte[] detail = webImageDetailImage.Resize(400, 300, false, true).GetBytes();
                        byte[] thumb = webImagethumImage.Resize(320, 150, false, true).GetBytes();


                        p.ImageData = new byte[detail.Length];
                        p.ThumbnailImage = new byte[thumb.Length];

                        Array.Copy(detail, p.ImageData, detail.Length);
                        Array.Copy(thumb, p.ThumbnailImage, thumb.Length);

                    }
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
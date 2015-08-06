using System.Linq;
using System.Web.Mvc;
using Shop.Areas.Admin.ViewModels;
using Shop.Infrastructure;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
    [SiteAuthorize(Roles = "admin")]
    public class ContentController : Controller
    {

        public ActionResult Index()
        {
            using (var ctx = new Context())
            {
                var contents = ctx.Contents.ToList();
                var model = contents.Select(x => new ContentCreateModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Text = x.Text.Substring(0, x.Text.Length / 5)
                });
                return View(model);
            }
        }

        public ActionResult Detail(int id)
        {
            using (var ctx = new Context())
            {
                var contents = ctx.Contents.FirstOrDefault(x => x.Id == id);


                var model = new ContentCreateModel
                {
                    Id = contents.Id,
                    Name = contents.Name,
                    Text = contents.Text
                };

                return View(model);

            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")]ContentCreateModel model)
        {
            if (this.ModelState.IsValid)
            {
                using (var ctx = new Context())
                {
                    ctx.Contents.Add(new Content
                    {
                        Name = model.Name,
                        Text = model.Text
                    });
                    ctx.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var ctx = new Context())
            {
                var t = ctx.Contents.FirstOrDefault(x => x.Id == id);
                ctx.Contents.Remove(t);
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var ctx = new Context())
            {
                var c = ctx.Contents.FirstOrDefault(x => x.Id == id);
                var model = new ContentCreateModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Text = c.Text
                };
                return View(model);
            }

        }

        [HttpPost]
        public ActionResult Edit(ContentCreateModel post)
        {
            if (this.ModelState.IsValid)
            {
                using (var ctx = new Context())
                {
                    var c = ctx.Contents.FirstOrDefault(x => x.Id == post.Id);

                    c.Text = post.Text;
                    c.Name = post.Name;
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }

}
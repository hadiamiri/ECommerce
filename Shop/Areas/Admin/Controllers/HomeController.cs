using System.Web.Mvc;
using Shop.Infrastructure;

namespace Shop.Areas.Admin.Controllers
{
    [SiteAuthorize(Roles = "admin")]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}
using System.Linq;
using System.Web.Mvc;
using Shop.Areas.Admin.ViewModels;
using Shop.Infrastructure;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
    [SiteAuthorize(Roles = "admin")]
    public class OrderController : Controller
    {

        public ActionResult Index()
        {
            using (var ctx = new Context())
            {
                var orders = ctx.Orders;


                var model = orders.Select(x => new OrderIndexViewModel
                {
                    Id = x.Id,
                    Date = x.CreateDate,
                    Price = x.Price,
                    Status = x.IsPaid ? "پرداخت شده" : "پرداخت نشده",
                    UserName = ctx.Users.FirstOrDefault(m => m.Id == x.User.Id).UserName
                });


                return View(model.ToList());
            }
        }

        public ActionResult OrderDetail(int id)
        {
            using (var ctx = new Context())
            {
                var detail = ctx.Orders.FirstOrDefault(x => x.Id == id).OrderDetails;

                return View(detail);
            }
        }

    }
}
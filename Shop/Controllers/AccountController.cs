using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Shop.Infrastructure;
using Shop.Models;
using Shop.ViewModels;

namespace Shop.Controllers
{
    public class AccountController : Controller
    {
        private readonly Context _db = new Context();
        [HttpGet]
        public ActionResult LogOn(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ShouldRedirect(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return Redirect(FormsAuthentication.DefaultUrl);
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private bool ShouldRedirect(string returnUrl)
        {
            // it's a security check
            return !string.IsNullOrWhiteSpace(returnUrl) &&
                                Url.IsLocalUrl(returnUrl) &&
                                returnUrl.Length > 1 &&
                                returnUrl.StartsWith("/") &&
                                !returnUrl.StartsWith("//") &&
                                !returnUrl.StartsWith("/\\");
        }

        [HttpPost]
        public ActionResult LogOn(Account loginInfo, string returnUrl)
        {
            if (this.ModelState.IsValid)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserName == loginInfo.UserName);
                if (user == null)
                {
                    this.ModelState.AddModelError("", "نام کاربری یا رمز معتبر نمیباشد");
                    return View();
                }
                else
                {
                    if (Encryption.VerifyPassword(loginInfo.Password, user.Password))
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, loginInfo.RememberMe);
                        if (returnUrl != null)
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        this.ModelState.AddModelError("", "نام کاربری یا رمز معتبر نمیباشد");
                        return View();
                    }
                }
            }
            return View();
        }



        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Register register)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            var username = _db.Users.FirstOrDefault(x => x.UserName == register.UserName);
            if (username != null)
            {
                this.ModelState.AddModelError("UserName", "نام کاربری قبلا استفاده شده");
            }
            var email = _db.Users.FirstOrDefault(x => x.Email == register.Email);
            if (email != null)
            {
                this.ModelState.AddModelError("Email", "ایمیل تکراری میباشد");
            }

            var role = _db.Roles.FirstOrDefault(x => x.Name == "customer");

            if (this.ModelState.IsValid)
            {
                var user = new User
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Password = Encryption.EncryptingPassword(register.Password),
                    UserName = register.UserName,
                    Email = register.Email,
                    Mobile = register.Mobile,
                    Address = register.Address,
                    CreateDate = DateTime.Now,
                    Orders = new Collection<Order>(),
                    Role = role
                };
                _db.Users.Add(user);
                _db.SaveChanges();
                return RedirectToAction("LogOn");
            }
            return View();
        }
    }
}
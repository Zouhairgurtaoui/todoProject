using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using todoproject.Models;

namespace todoproject.Controllers
{
    public class AccountController : Controller
    {
        private ContextDb _context;
        public AccountController()
        {
            _context = new ContextDb();
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel accModel)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.SingleOrDefault(u => u.Email == accModel.Email && u.Password == accModel.Password);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "Email or Password incorrect");
                    return View(accModel);
                }

                Session["UserId"] = user.UserId;
                
                ClaimsIdentity identity = new ClaimsIdentity(new[]
                {
                    new Claim("UserId",user.UserId.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Role,"User")
                }, DefaultAuthenticationTypes.ApplicationCookie);

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);


                return RedirectToAction("Index", "Tasks");
            }
            ModelState.AddModelError("Email", "Email or Password incorrect");

            return View(accModel);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserViewModel accModel)
        {
            if (ModelState.IsValid)
            {
                var emailExits = _context.Users.Any(u => u.Email == accModel.User.Email);
                if (emailExits)
                {
                    ModelState.AddModelError("Email", "Email Already in use");
                    return View(accModel);
                }
                if (accModel.User.Password.Length < 8)
                {
                    ModelState.AddModelError("Password", "Password is too small");
                    return View(accModel);
                }
                else if (accModel.User.Password.Length > 16)
                {
                    ModelState.AddModelError("Password", "Password is too large");
                    return View(accModel);
                }
                User user = new User()
                {
                    UserName = accModel.User.UserName,
                    Email = accModel.User.Email,
                    Password = accModel.User.Password
                };

                
                    _context.Users.Add(user);
                    _context.SaveChanges();
                

                return RedirectToAction("Login", "Account");
            }
            var errors = ModelState
                                   .Where(x => x.Value.Errors.Count > 0)
                                   .Select(x => new { x.Key, x.Value.Errors })
                                   .ToArray();

            foreach (var error in errors)
            {
                System.Console.WriteLine($"Key: {error.Key}, Errors: {string.Join(",", error.Errors.Select(e => e.ErrorMessage))}");
            }
            return View(accModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;

            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Login", "Account");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using todoproject.Models;

namespace todoproject.Controllers
{
    public class HomeController : Controller
    {
        private ContextDb _context;
        public HomeController()
        {
            _context = new ContextDb();
        }
        
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
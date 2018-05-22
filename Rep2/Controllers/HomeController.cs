using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rep2.Models.EntityModel;

namespace Rep2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        bGomlaDBreportEntities db = new bGomlaDBreportEntities();
        public ActionResult Index()
        {

            var sl = db.DataBaseServers.ToList();
            return View(sl);
        }
    }
}
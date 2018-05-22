using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rep2.Models.EntityModel;

namespace Rep2.Controllers
{
    public class ArticalsController : Controller
    {

        bGomlaDBreportEntities db = new bGomlaDBreportEntities();

        // GET: Articals
        public ActionResult Index(int id)
        {

            var p = db.Publications.Where(x => x.PublicationID == id).FirstOrDefault();
            return View(p);
        }

        public ActionResult Create(int id)
        {
            Artical a = new Artical();
            var p =  db.Publications.Find(id);
            a.Publication = p;
            a.PublicationID = p.PublicationID;
            return View(a);
        }

        [HttpPost]
        public ActionResult Create(Artical a)
        {

            if (ModelState.IsValid)
            {
                db.Articals.Add(a);
                db.SaveChanges();
                return RedirectToAction("index", new { id = a.PublicationID } );
            }
            return View(a);
        }


        public ActionResult Edit(int id)
        {
            var a = db.Articals.Find(id);
            return View(a);
        }

        [HttpPost]
        public ActionResult Edit(Artical a)
        {

            if (ModelState.IsValid)
            {
                var old = db.Articals.Find(a.ArticallD);
                old.ArticalName = a.ArticalName;
                old.Fillters = a.Fillters;
                db.Entry(old).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index", new { id = a.PublicationID });
            }
            return View(a);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rep2.Models.EntityModel;

namespace Rep2.Controllers
{
    public class PublicationsController : Controller
    {

        bGomlaDBreportEntities db = new bGomlaDBreportEntities();

        //GET: Publications


        private void IntDD(int selected)
        {

            ViewBag.BranchTypeID = new SelectList(db.BranchTypes , "TypeID", "TypeName", selected);
        }

        public ActionResult Index()
        {
            return View(db.Publications);
        }


        public ActionResult Create()
        {
            IntDD(0);
            Publication p = new Publication();

            return View(p);
        }

        [HttpPost]
        public ActionResult Create(Publication p)
        {

            if (ModelState.IsValid)
            {
                db.Publications.Add(p);
                db.SaveChanges();
                return RedirectToAction("index");
            }



            IntDD(0);
            return View(p);
        }

        public ActionResult Edit(int id)
        {

            var p = db.Publications.Find(id);
            IntDD(p.BranchTypeID);
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(Publication p)
        {

            if (ModelState.IsValid)
            {

                var old = db.Publications.Find(p.PublicationID);
                old.PublicationName = p.PublicationName;
                old.BranchTypeID = p.BranchTypeID;
                db.Entry(old).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");

            }

            IntDD(p.BranchTypeID);
            return View(p);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rep2.Models.EntityModel;

namespace Rep2.Controllers
{
    public class ServerBackupController : Controller
    {
        // GET: ServerBackup

        bGomlaDBreportEntities db = new bGomlaDBreportEntities();
        public ActionResult AddPlan(int id)
        {



            var srv = db.ServerBackups.Where(x=> x.DbServerID == id);

            // var btl = db.BackupTypes.Where(x => !srv.ServerBackups.Any(y => x.typeID == y.BackupTypeID)).ToList();
            //var result = peopleList2.Where(p => !peopleList1.Any(p2 => p2.ID == p.ID));
            var btl = db.BackupTypes.Where(x => !srv.Any(y => x.typeID == y.BackupTypeID )).ToList();
          //  var btl = (from bt in db.BackupTypes where(!srv.ServerBackups.Any(x=> x.BackupTypeID == bt.typeID) )  select new { bt.typeID , bt.typeName }).ToList() ;


            if (btl.Count > 0)
            {
                ViewBag.BackupTypeID = new SelectList(btl,
                "typeID", "typeName");
                ServerBackup bk = new ServerBackup();
                bk.DataBaseServer = db.DataBaseServers.Find(id) ;
                bk.DbServerID = id;
                bk.intervalMin = 1440;
                
                return View(bk);
            }
           


            return View();
        }

        [HttpPost]
        public ActionResult AddPlan(ServerBackup backup)
        {


            if (ModelState.IsValid)
            {

                backup.DataBaseServer = null;
                db.ServerBackups.Add(backup);
                db.SaveChanges();
                _app.Backups bk = new _app.Backups();
               
                bk.RefreshBackupLog(backup.DbServerID);
                return RedirectToAction("index", new { id = backup.DbServerID });

            }

            var srv = db.DataBaseServers.Find(backup.DbServerID);

            var btl = db.BackupTypes.Where(x => !srv.ServerBackups.Any(y => x.typeID == y.BackupTypeID)).ToList();


            if (btl.Count > 0)
            {
                ViewBag.BackupTypeID = new SelectList(btl,
                "typeID", "typeName");
                ServerBackup bk = new ServerBackup();
                bk.DataBaseServer = srv;
                bk.DbServerID = srv.ServerID;
                bk.intervalMin = 1440;

               
            }


            return View(backup);
        }


      


        public ActionResult index(int id)
        {

            //_app.Backups bk = new _app.Backups();

            //bk.RefreshBackupLog(db.DataBaseServers.Find(id), db);

            var l = db.ServerBackups.Where(x => x.DbServerID == id).ToList();
            return View(l);
        }


        public ActionResult Edit(int id)
        {

            var bk = db.ServerBackups.Find(id);

          

            return View(bk);
        }

        [HttpPost]
        public ActionResult Edit(ServerBackup backup)
        {


            if (ModelState.IsValid)
            {


                var old = db.ServerBackups.Find(backup.SrvBkID);

                old.Location = backup.Location;
                old.intervalMin = backup.intervalMin;

                backup.DataBaseServer = null;

                db.Entry(old).State = System.Data.Entity.EntityState.Modified;
               // db.ServerBackups.Add(backup);
                db.SaveChanges();
                return RedirectToAction("index", new { id = backup.DbServerID });

            }

           


            return View(backup);
        }


        public ActionResult Details(int id)
        {


            var bk = db.ServerBackups.Find(id);

            //_app.Backups bkc = new _app.Backups();
            //bkc.RefreshBackupLog(bk.DbServerID);
            return View(bk);
        }

    }
}
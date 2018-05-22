using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Rep2.Models.EntityModel;

namespace Rep2.Controllers
{
    public class DataBaseServersController : Controller
    {
        private bGomlaDBreportEntities db = new bGomlaDBreportEntities();
        _app.Networking nt = new _app.Networking();
        _app.Files file = new _app.Files();




        // GET: DataBaseServers
        public ActionResult Index()
        {
            var dataBaseServers = db.DataBaseServers.ToList();
            return View(dataBaseServers.ToList());
        }

        // GET: DataBaseServers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DataBaseServer dataBaseServer = db.DataBaseServers.Find(id);
            if (dataBaseServer == null)
            {
                return HttpNotFound();
            }
            return View(dataBaseServer);
        }

        // GET: DataBaseServers/Create
        public ActionResult Create()
        {
            ViewBag.DataBaseAccountID = new SelectList(db.AccessAccounts.Where(x => x.TypeID == _app.Auth.SQL), "AccountID", "AccountName");
            ViewBag.BranchID = new SelectList(db.Branches, "BranchID", "BranchName");
            ViewBag.NetworkAccountID = new SelectList(db.AccessAccounts.Where(x => x.TypeID == _app.Auth.Windows), "AccountID", "AccountName");

            // ViewBag.ServerID = new SelectList(db.Machines, "MachineID", "MachineName");
            DataBaseServer srv = new DataBaseServer(db);
            //  srv.DataBaseFiles.Add( new DataBaseFile { DataBaseFilesName =  })

            return View(srv);
        }

        // POST: DataBaseServers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DataBaseServer dataBaseServer, FormCollection form)
        {

            SetFilePaths(dataBaseServer, form);
            nt.TestSqlConnection(dataBaseServer);
            if (ModelState.IsValid && nt.Errors.Count == 0)
            {


                db.DataBaseServers.Add(dataBaseServer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DataBaseAccountID = new SelectList(db.AccessAccounts.Where(x => x.TypeID == _app.Auth.SQL), "AccountID", "AccountName", dataBaseServer.DataBaseAccountID);
            ViewBag.BranchID = new SelectList(db.Branches, "BranchID", "BranchName", dataBaseServer.BranchID);
            ViewBag.NetworkAccountID = new SelectList(db.AccessAccounts.Where(x => x.TypeID == _app.Auth.Windows), "AccountID", "AccountName", dataBaseServer.NetworkAccountID);

            // ViewBag.ServerID = new SelectList(db.Machines, "MachineID", "MachineName", dataBaseServer.ServerID);
            return View(dataBaseServer);
        }

        // GET: DataBaseServers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DataBaseServer dataBaseServer = db.DataBaseServers.Find(id);
            if (dataBaseServer == null)
            {
                return HttpNotFound();
            }
            ViewBag.DataBaseAccountID = new SelectList(db.AccessAccounts.Where(x => x.TypeID == _app.Auth.SQL).ToList(), "AccountID", "AccountName", dataBaseServer.DataBaseAccountID);
            ViewBag.BranchID = new SelectList(db.Branches, "BranchID", "BranchName", dataBaseServer.BranchID);
            ViewBag.NetworkAccountID = new SelectList(db.AccessAccounts.Where(x => x.TypeID == _app.Auth.Windows), "AccountID", "AccountName", dataBaseServer.NetworkAccountID);

            return View(dataBaseServer);
        }

        // POST: DataBaseServers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DataBaseServer dataBaseServer, FormCollection form)
        {


            var oldserver = db.DataBaseServers.Find(dataBaseServer.ServerID);

            SetFilePaths(oldserver, form);
            nt.TestSqlConnection(dataBaseServer);
            if (ModelState.IsValid && nt.Errors.Count == 0)
            {
                oldserver.BranchID = dataBaseServer.BranchID;
                oldserver.DataBaseAccountID = dataBaseServer.DataBaseAccountID;
                oldserver.DataBaseName = dataBaseServer.DataBaseName;
                oldserver.Machine.MachineIP = dataBaseServer.Machine.MachineIP;
                oldserver.Machine.MachineName = dataBaseServer.Machine.MachineName;
                oldserver.NetworkAccountID = dataBaseServer.NetworkAccountID;
                


                db.Entry(oldserver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DataBaseAccountID = new SelectList(db.AccessAccounts.Where(x => x.TypeID == _app.Auth.SQL), "AccountID", "AccountName", dataBaseServer.DataBaseAccountID);
            ViewBag.BranchID = new SelectList(db.Branches, "BranchID", "BranchName", dataBaseServer.BranchID);
            ViewBag.NetworkAccountID = new SelectList(db.AccessAccounts.Where(x => x.TypeID == _app.Auth.Windows), "AccountID", "AccountName", dataBaseServer.NetworkAccountID);


            return View(dataBaseServer);
        }

        // GET: DataBaseServers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DataBaseServer dataBaseServer = db.DataBaseServers.Find(id);
            if (dataBaseServer == null)
            {
                return HttpNotFound();
            }
            return View(dataBaseServer);
        }

        // POST: DataBaseServers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DataBaseServer dataBaseServer = db.DataBaseServers.Find(id);
            db.DataBaseServers.Remove(dataBaseServer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        private void SetFilePaths(DataBaseServer srv, FormCollection form)
        {

            if (srv.DataBaseFiles.Count == 0)
            {
                DataBaseServer s = new DataBaseServer(db);
                srv.DataBaseFiles = s.DataBaseFiles;
            }

            DataBaseFile dbf = new DataBaseFile();
            foreach (var item in _app.DB.DataBaseFilesName)
            {

                dbf = srv.DataBaseFiles.FirstOrDefault(x => x.DataBaseFilesName.FileID == item.FileID);

                dbf.FilePath = form["filepath_" + item.FileName].ToString();

                if (dbf.DataBaseFilesSizes.Count > 0)
                {

                    if (dbf.DataBaseFilesSizes.OrderByDescending(x => x.DateTaken).FirstOrDefault().DateTaken.Date < DateTime.Now.Date)
                    {
                        dbf.DataBaseFilesSizes.Add(new DataBaseFilesSize { DateTaken = DateTime.Now, Size = file.FileSizeMB(dbf, srv) });

                    }


                }
                else
                {
                    dbf.DataBaseFilesSizes.Add(new DataBaseFilesSize { DateTaken = DateTime.Now, Size = file.FileSizeMB(dbf, srv) });
                }



                


            }



        }

    }
}

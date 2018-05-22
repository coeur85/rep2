using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rep2.Models.EntityModel;

namespace Rep2.Controllers
{
    public class DistributersController : Controller
    {

        bGomlaDBreportEntities db = new bGomlaDBreportEntities();

        // GET: Distributers
        public ActionResult Index(int id)
        {

            List<ClassCollection.ServerSelect> sl = new List<ClassCollection.ServerSelect>();

            var p = db.Publications.Find(id);

            var ds = db.DataBaseServers;


            foreach (var item in ds)
            {

                sl.Add(new ClassCollection.ServerSelect { DataBaseServer = item , IsSubscriped =
                    p.Distributers.Any(x=> x.ServerID == item.ServerID ) });

            }


            _app.UI.Pgaes.DistributerPage pg = new _app.UI.Pgaes.DistributerPage();
            pg.Distributers = sl;
            pg.Publication = p;
            return View(pg);
        }
    }
}
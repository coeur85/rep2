using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rep2.Models.EntityModel;

namespace Rep2.Controllers.API
{
    public class SubscriptionsController : Controller
    {
        // GET: Subscriptions


        bGomlaDBreportEntities db = new bGomlaDBreportEntities();
        //public ActionResult Index(int id)
        //{

        //    var p = db.Publications.Find(id);
        //    var sl = p.SubscriptionServers;
        //    var dbs = db.DataBaseServers.Where(x=> x.ServerID != p.PublicationServerID);
        //    _app.UI.Pgaes.SubscriptionsPage subServers = new _app.UI.Pgaes.SubscriptionsPage();



        //    foreach (var s in dbs)
        //    {

        //        subServers.Subscriptions.Add( new ClassCollection.Subscription
        //        {
        //            DataBaseServer = s,
        //            IsSubscriped = sl.Any(x=> x.ServerID == s.ServerID)
        //        });

        //    }

        //    subServers.Publication = p;
        //    return View(subServers);
        //}
    }
}
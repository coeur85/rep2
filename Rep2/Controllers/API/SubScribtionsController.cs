using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Rep2.Models.EntityModel;

namespace Rep2.Controllers.API
{
    public class SubScribtionsController : ApiController
    {



        bGomlaDBreportEntities db = new bGomlaDBreportEntities();


        //[HttpPost]
        //public IHttpActionResult UpdateSupscriptions(_app.jsonObjects.updateReplicationServers model)
        //{

        //    //var p = db.Publications.Find(model.PublicationID);
        //    //if (model.IsSubscriped)
        //    //{

        //    //    p.SubscriptionServers.Add(db.DataBaseServers.Find(model.ServerID));

        //    //}
        //    //else
        //    //{
        //    //    p.SubscriptionServers.Remove(db.DataBaseServers.Find(model.ServerID));

        //    //}
        //    //db.SaveChanges();
        //    return Ok();
        //}



    }
}

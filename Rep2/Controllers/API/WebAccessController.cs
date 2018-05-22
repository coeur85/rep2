using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Rep2.Models.EntityModel;
namespace Rep2.Controllers.API
{
    public class WebAccessController : ApiController
    {

        bGomlaDBreportEntities db = new bGomlaDBreportEntities();




        [System.Web.Http.HttpGet]
        public IHttpActionResult Refresh()
        {

            int i = 0;

            _app.Backups bk = new _app.Backups();
            _app.Files fe = new _app.Files();
            foreach (var s in db.DataBaseServers)
            {
               // if (s.Online)
               //{
                i += bk.RefreshBackupLog(s.ServerID);
                i += fe.RefreshDatabaseFileSize(s.ServerID);
               //}
               
                

            }

            return Ok(i);

        }




        [System.Web.Http.HttpPost]
        public IHttpActionResult UpdateDistributers(_app.jsonObjects.Distributers model)
        {

            int i = 0;
            var p = db.Publications.Find(model.PublicationID);


            if (model.IsSubscriped)
            {


                p.Distributers.Add(new Distributer
                {
                    DistributerServer = db.DataBaseServers.Find(model.ServerID),
                    Publication = p,
                    ServerID = model.ServerID,
                    PublicationID = p.PublicationID
                });

            }


            else
            {

                //   p.Distributers.Remove();
                db.Distributers.Remove(db.Distributers.FirstOrDefault(x => x.ServerID == model.ServerID && x.PublicationID == p.PublicationID));


            }
            i = db.SaveChanges();

            return Ok(i);

        }

    }
}

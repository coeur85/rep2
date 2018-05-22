using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rep2.Models.EntityModel;
using System.Web.Helpers;

namespace Rep2
{


    namespace _app
    {
 public class DB
    {

        private static bGomlaDBreportEntities db = new bGomlaDBreportEntities();


        public static List<DataBaseFilesName> DataBaseFilesName { get { return db.DataBaseFilesNames.ToList(); } }

    }

 public class errors
    {

        public  List<error> Errors { get; set; }

        public  void addError(string message) {Errors.Add(new error(message)); }


        public errors() { Errors = new List<error>(); }

    }




     public  class jsonObjects
        {


            public class Backuploags
            {
            public static string dates(List<BackupLog> logs)
            {


                    //var q = from b in logs select new { b.DateCreated.Day, b.FileSize };
                    //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    // return serializer.Serialize(q);


                    string v = "";

                    logs = logs.Where(x => x.FileExists == true).ToList();

                    if (logs.Count > 0)
                    {

                        var bk = logs.FirstOrDefault().ServerBackup.BackupTypeID;






                        if (bk == 1)
                        {

                            foreach (var item in logs)
                            {
                                v += ("'" + item.DateCreated.ToShortDateString() + "',"); //+ " at " + item.DateCreated.ToShortTimeString() )+ "',";
                            }

                        }
                        else if (bk == 2 || bk == 3)
                        {

                            foreach (var item in logs)
                            {
                                v += ("'" + item.DateCreated.Day.ToString() +"/" + item.DateCreated.Month.ToString()
                                 + " " + item.DateCreated.ToShortTimeString()   + "',"); //+ " at " + item.DateCreated.ToShortTimeString() )+ "',";
                            }


                        }


                        

                        


                    v = v.Remove(v.Length - 1);

                    v = "[" + v + "]";

                    }

                  

                    return v;



                    //string v = "";


                    //foreach (var item in logs)
                    //{
                    //    v += "[" + item.DateCreated.Ticks.ToString() + "," + item.FileSize.ToString() + "],";
                    //}


                    //v = v.Remove(v.Length - 1);

                    //v = "[" + v + "]";

                    //return v;
                }

                public static string FileSize(List<BackupLog> logs)
                {


                    //var q = from b in logs select new { b.DateCreated.Day, b.FileSize };
                    //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    // return serializer.Serialize(q);


                    string v = "";
                    if (logs.Count > 0)
                    {

                        foreach (var item in logs)
                        {
                            v += item.FileSize.ToString() + ",";
                        }


                        v = v.Remove(v.Length - 1);

                        v = "[" + v + "]";

                    }



                    return v;



                    //string v = "";


                    //foreach (var item in logs)
                    //{
                    //    v += "[" + item.DateCreated.Ticks.ToString() + "," + item.FileSize.ToString() + "],";
                    //}


                    //v = v.Remove(v.Length - 1);

                    //v = "[" + v + "]";

                    //return v;
                }

            }


            public class updateReplicationServers
            {

                public int PublicationID { get; set; }
                public int ServerID { get; set; }
                public bool IsSubscriped { get; set; }

            }



            public class getPublications
            {
                public int id { get; set; }

                public int PublicationID { get; set; }
                public string PublicationName { get; set; }
            }


            public class Distributers
            {

                public int PublicationID { get; set; }
                public int ServerID { get; set; }
                public bool IsSubscriped { get; set; }


            }

        }

    }


   

    public class error
    {

        public static string messages { get; set; }
        public error(string msg) { messages = msg; }

    }





   
}
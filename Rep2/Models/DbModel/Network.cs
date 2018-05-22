using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rep2.Models.EntityModel;
using System.Data.SqlClient;
using System.IO;




namespace Rep2
{
    namespace _app
    {

        public class UI
        {

            public static string Ago(DateTime d)
            {

                var t = DateTime.Now - d;



                if (t.Hours > 0)
                {

                    return string.Format("{0:D2}h:{1:D2}m",
                    t.Hours,
                    t.Minutes);

                }



                else if (t.Minutes > 0)
                {
                    return string.Format("{0:D2}m",
                   t.Minutes);

                }
                else
                {

                    return string.Format("{0:D2}s",
                    t.Seconds);

                }

               
              

            }


            public class Pgaes
            {

                public class DistributerPage
                {

                public List<ClassCollection.ServerSelect> Distributers { get; set; }

                public Publication Publication { get; set; }

                    public DistributerPage() { Distributers = new List<ClassCollection.ServerSelect>(); }
               


                }



            }

        }

        public class Networking : _app.errors
        {
            public void TestSqlConnection(DataBaseServer server)
            {
                string str = @"data source={servername};initial catalog={dabasename};Integrated Security=false; User ID={userid}; Password={password};";

                str = str.Replace("{servername}", server.Machine.MachineName);
                str = str.Replace("{dabasename}", server.DataBaseName);

                AccessAccount acc = new AccessAccount();

                if (server.DBAccount == null)
                {
                    bGomlaDBreportEntities db = new bGomlaDBreportEntities();
                    acc = db.AccessAccounts.FirstOrDefault(x => x.AccountID == server.DataBaseAccountID);
                }
                else { acc = server.DBAccount; }
               

                str = str.Replace("{userid}", acc.AccountName);
                str = str.Replace("{password}", acc.AccountPassword);

               

                SqlConnection con = new SqlConnection(str);

                try
                {
                    con.Open();
                }
                catch (Exception e)
                {

                    addError(e.Message);

                    
                }







            }

            public string ResloveMachineName(DataBaseServer srv)
            {

                return @"\\" + srv.Machine.MachineName;

            }
            public string ResloveAdminSharePath(string path)
            {

                return path.Replace(":", "$");

            }


            public string RelosveFullPath(DataBaseServer srv, string filepath)
            {

                return ResloveMachineName(srv) + @"\" + ResloveAdminSharePath(filepath);

            }



        }



        public class Files : _app.errors
        {

            public decimal FileSizeMB(FileInfo file)
            {

                return (file.Length / 1024) / 1024;

            }
            public decimal FileSizeMB(string filePath)
            {

                FileInfo file = new FileInfo(filePath);

                return FileSizeMB(file);

            }
            public decimal FileSizeMB(DataBaseFile dbf)
            {

               
                return FileSizeMB(dbf, dbf.DataBaseServer);

            }
            public decimal FileSizeMB(DataBaseFile dbf, DataBaseServer srv)
            {

                _app.Networking nt = new Networking();

                string filePath = nt.RelosveFullPath(srv,dbf.FilePath) ; // access admin share to file path

                return FileSizeMB(filePath);

            }

            public DateTime FileCreationTime(FileInfo file)
            {

                return file.CreationTime ;

            }

            public FileInfo[] FilesInFolder(string folderPath, string fileExtention)
            {
                DirectoryInfo d = new DirectoryInfo(folderPath);
                return d.GetFiles("*" + fileExtention);
            }



            public int RefreshDatabaseFileSize(DataBaseServer srv, bGomlaDBreportEntities db)
            {

                var files = srv.DataBaseFiles;
                if (!srv.Online) { return 0; }

                foreach (var file in files)
                {

                    var fs = file.DataBaseFilesSizes.OrderByDescending(x => x.DateTaken).FirstOrDefault();


                   
                    if(fs.DateTaken.Date != DateTime.Now.Date)

                    { var curent = FileSizeMB(file); file.DataBaseFilesSizes.Add(new DataBaseFilesSize { DateTaken = DateTime.Now, Size = curent }); }


                }

                return db.SaveChanges();
            }
            public int RefreshDatabaseFileSize(int srvid)
            {
                bGomlaDBreportEntities db = new bGomlaDBreportEntities();
                return RefreshDatabaseFileSize(db.DataBaseServers.Find(srvid), db);

            }


        }


        public class Backups : _app.errors
        {

           
            public int RefreshBackupLog(DataBaseServer  srv, bGomlaDBreportEntities db)
            {

                _app.Files fe = new Files();
                _app.Networking nt = new Networking();
                int i = 0;
                if (!srv.Online) { return i; }
                foreach (var bkt in srv.ServerBackups)
                {
                    // mark all files as do not exitis


                    if (bkt.LastFile.DateCreated.AddMinutes(bkt.intervalMin) < DateTime.Now)
                    {

                        
                    


                    foreach (var item in bkt.BackupLogs)
                    {
                        item.FileExists = false;
                    }

                    BackupLog log = new BackupLog();

                        var fls = fe.FilesInFolder(nt.RelosveFullPath(bkt.DataBaseServer, bkt.Location), bkt.BackupType.FilesExtintion);


                        foreach (var f in fls)
                        {
                            log = bkt.BackupLogs.FirstOrDefault(x => x.FileName == f.Name);
                            if (log == null)
                            {
                                log = new BackupLog
                                {
                                    FileName = f.Name,
                                    DateCreated = f.CreationTime,
                                    SrvBkId = bkt.DbServerID,
                                    FilePath = bkt.Location,
                                    FileSize = fe.FileSizeMB(f),

                                };
                                bkt.BackupLogs.Add(log);
                            }
                            log.FileExists = true;

                        }

                        i += db.SaveChanges();
                    }
                    

                }


                return i;
            }

            public int RefreshBackupLog(int serverid)
            {

                bGomlaDBreportEntities db = new bGomlaDBreportEntities();

             return   RefreshBackupLog(db.DataBaseServers.Find(serverid), db);

            }


          //  private FileInfo GetBackupFile(BackupLog)

        }




        public static class Auth
        {
            public const int Windows = 1;
            public const int SQL = 2;
          
        }





    }
}


namespace ClassCollection
{

    public class ServerSelect
    {

        public DataBaseServer DataBaseServer { get; set; }
        public bool IsSubscriped { get; set; }


    }

}
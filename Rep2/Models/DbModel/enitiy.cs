using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rep2.Controllers;
using System.Net.NetworkInformation;

namespace Rep2.Models.EntityModel
{


    public partial class DataBaseServer
    {


        public DataBaseServer(bGomlaDBreportEntities defalutValues)
        {
            this.DataBaseFiles = new HashSet<DataBaseFile>();



            Machine = new Machine { MachineID = 0 , MachineIP= "127.0.0.1" };
            DataBaseName = "Retail";
            foreach (var item in defalutValues.DataBaseFilesNames.ToList() )
            {

                DataBaseFiles.Add(new DataBaseFile
                {
                    DbServerID = ServerID,
                    FilePath = null,
                    DataBaseFilesName = item
                } );

            }



            


        }



        public decimal DabaseSizeMB { get
            {

                decimal total = 0;
                DataBaseFilesSize fs = new DataBaseFilesSize();

                foreach (var item in DataBaseFiles)
                {

                    fs = item.DataBaseFilesSizes.OrderByDescending(x => x.DateTaken).FirstOrDefault();
                    if (fs != null) { total += fs.Size; }
                    
                }

                return total;
            }
        }

        private bool? _online = null;
        public bool Online { get
            {
                if (_online == null)
                {
                Ping p = new Ping();
                var r = p.Send(Machine.MachineName);
                if (r.Status == IPStatus.Success) { _online = true; }
                else { _online = false; }
                }
                return _online.Value;
            }
        }


    }


    public partial class ServerBackup
    {


        public BackupLog LastFile { get
            {

                return BackupLogs.OrderBy(x => x.DateCreated).Where(x => x.FileExists == true).LastOrDefault();


            } }
        public BackupLog FirstFile
        {
            get
            {

                return BackupLogs.OrderBy(x => x.DateCreated).Where(x=> x.FileExists == true).FirstOrDefault();


            }
        }


        public decimal AvgFileSize { get
            {


                decimal size = 0;

                if (BackupLogs.Count > 0)
                {
                    foreach (var item in BackupLogs)
                    {
                        size += item.FileSize;
                    }


                    return (size / BackupLogs.Count);


                }

                else {

                    return size;
                }

            } }

    }
};
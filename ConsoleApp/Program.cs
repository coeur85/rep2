using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("initializing methods");
            Console.WriteLine("Accessing web service");
            var lb = RefreshBakups();
            Console.WriteLine("updated recorders :" + lb.ToString());
            Console.WriteLine("update is over");

        }


        public static string RefreshBakups()
        {

           #pragma warning disable CS0618 // Type or member is obsolete
            var domin = ConfigurationSettings.AppSettings.Get("Domain");
            Console.WriteLine("found domian :" + domin);
#pragma warning restore CS0618 // Type or member is obsolete
            Console.WriteLine("sending request.. please wait !");
            return new   System.Net.WebClient().DownloadString("http://"+domin+"/Api/WebAccess/Refresh");

        }
    }
}

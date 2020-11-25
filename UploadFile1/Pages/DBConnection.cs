using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadFile1.Pages
{
    public class DBConnection
    {
        public string DbString ()
        {
            string dbString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\zairu\source\repos\Week9CUploadFileSolution\UploadFile1\Data\UploadFile1.mdf;Integrated Security=True;Connect Timeout=30";
            return dbString;
        }
    }
}

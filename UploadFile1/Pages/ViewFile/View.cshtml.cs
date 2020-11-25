using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UploadFile1.Models;

namespace UploadFile1.Pages.ViewFile
{
    public class ViewModel : PageModel
    {
        public List<StudentFile> FileRec { get; set; }
        public void OnGet()
        {
            DBConnection DBCon = new DBConnection();
            string DbString = DBCon.DbString();
            SqlConnection conn = new SqlConnection(DbString);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM StudentFile";

                var reader = command.ExecuteReader();

                FileRec = new List<StudentFile>();

                while (reader.Read())
                {
                    StudentFile rec = new StudentFile();
                    rec.Id = reader.GetInt32(0); // we need this to send the Id to Delete page for another enquiry
                    rec.StudentName = reader.GetString(1);
                    rec.FileName = reader.GetString(2);
                    FileRec.Add(rec);
                }
            }

        }
    }
}

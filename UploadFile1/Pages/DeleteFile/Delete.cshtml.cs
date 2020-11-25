using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UploadFile1.Models;

namespace UploadFile1.Pages.DeleteFile
{
    public class DeleteModel : PageModel
    {

      

        [BindProperty]
        public StudentFile StdFileRec { get; set; }

        public readonly IWebHostEnvironment _env;

        //a constructor for the class
        public DeleteModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult OnGet(int? Id)//we receive this Id from View.cs
        {
            DBConnection DBCon = new DBConnection();
            string DbString = DBCon.DbString();
            SqlConnection conn = new SqlConnection(DbString);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM StudentFile WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", Id);

                var reader = command.ExecuteReader();

                StdFileRec = new StudentFile();
                while (reader.Read())
                {
                    StdFileRec.Id = reader.GetInt32(0);
                    StdFileRec.StudentName = reader.GetString(1); //to display on the html page
                    StdFileRec.FileName = reader.GetString(2); //to display on the html page
                }

                Console.WriteLine("File name : "+StdFileRec.FileName);


            }

            return Page();
        }


        public IActionResult OnPost()
        {

            deletePicture(StdFileRec.Id, StdFileRec.FileName);
            return RedirectToPage("/ViewFile/View");
        }



        public void deletePicture(int Id, string FileName)
        {
            Console.WriteLine("Record Id : "+Id);
            Console.WriteLine("File Name : "+FileName);

            DBConnection DBCon = new DBConnection();
            string DbString = DBCon.DbString();
            SqlConnection conn = new SqlConnection(DbString);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"DELETE FROM StudentFile WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", Id);

                command.ExecuteNonQuery();
            }
            Console.WriteLine(FileName);
            string RetrieveImage = Path.Combine(_env.WebRootPath, "Files", FileName);
            System.IO.File.Delete(RetrieveImage);
            Console.WriteLine("File has been deleted");

           
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UploadFile1.Models;

namespace UploadFile1.Pages.UploadFile
{
    public class UploadFileModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public IFormFile StdFile { get; set; }

        [BindProperty(SupportsGet = true)]
        public StudentFile StudFileRec { get; set; }

        public readonly IWebHostEnvironment _env;
        
        //a constructor for the class
        public UploadFileModel (IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnGet()
        {
           
        }

        public IActionResult OnPost()
        {
           
            var FileToUpload = Path.Combine(_env.WebRootPath, "Files", StdFile.FileName);//this variable consists of file path
            Console.WriteLine("File Name : " + FileToUpload);
            


            using (var FStream = new FileStream(FileToUpload, FileMode.Create))
            {
                 StdFile.CopyTo(FStream);//copy the file into FStream variable
            }

            DBConnection DBCon = new DBConnection();
            string DbString = DBCon.DbString();
            SqlConnection conn = new SqlConnection(DbString);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"INSERT StudentFile (StudentName, FileName) VALUES (@StdName, @FName)";
                command.Parameters.AddWithValue("@StdName", StudFileRec.StudentName);
                command.Parameters.AddWithValue("@FName", StdFile.FileName);
                Console.WriteLine("File name : " + StudFileRec.StudentName);
                Console.WriteLine("File name : "+ StdFile.FileName);
                command.ExecuteNonQuery();
            }



                return RedirectToPage("/index");
        }
    }
}

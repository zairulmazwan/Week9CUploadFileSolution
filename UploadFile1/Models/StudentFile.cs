using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UploadFile1.Models
{
    public class StudentFile
    {

        public int Id { get; set; }

        [Display(Name = "Student Name")]
        public string StudentName { get; set; }

        public string FileName { get; set; }
    }
}

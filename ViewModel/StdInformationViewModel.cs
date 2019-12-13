using StudentAttendance.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentAttendance.ViewModel
{
    public class StdInformationViewModel
    {
        public int Id { get; set; }
        [Required]
        public string StdName { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public long Mobile { get; set; }
        public string Email { get; set; }
        //radiobtn(M/F/O)
        public string Gender { get; set; }
        //checkbox(yes/No)
        public bool Food { get; set; }
        public string Description { get; set; }
        public int CourseId { get; set; }
        public string Course { get; set; }

       
    }
}
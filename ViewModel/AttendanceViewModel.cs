using StudentAttendance.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentAttendance.ViewModel
{
    public class AttendanceViewModel
    {
        public int Id { get; set; }
        
       public int StdId { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime InTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime OutTime { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Information { get; set; }


    }
}
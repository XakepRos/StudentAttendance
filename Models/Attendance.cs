using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentAttendance.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:HH:mm}")]
        public DateTime InTime { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}" )]
        public DateTime OutTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public int StdId { get; set; }
        [ForeignKey("StdId")]
        public virtual Information StdInformations { get; set; }

    }
}
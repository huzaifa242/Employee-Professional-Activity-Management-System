using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Training_Details
    {
        [Key]
        public int Training_DetailsId { get; set; }
        [ForeignKey("employee")]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public DateTime? Begin_Date { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public DateTime? End_Date { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string Subject { get; set; }
        public string Description { get; set; }
        public virtual Employee employee { get; set; }
    }
}
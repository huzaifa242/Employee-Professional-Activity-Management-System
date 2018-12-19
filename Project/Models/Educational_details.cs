using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Educational_details
    {
        [Key]
        public int Educational_detailsId { get; set; }
        [ForeignKey("employee")]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public DateTime? Start_Date { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public DateTime? End_Date { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string Course_Name { get; set; }
        public string Description { get; set; }
        public virtual Employee employee { get; set; }
    }
}
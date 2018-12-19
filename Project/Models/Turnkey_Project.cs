using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Turnkey_Project
    {
        [Key]
        public int Turnkey_ProjectId { get; set; }
        [ForeignKey("employee")]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string Project_Name { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public DateTime? Begin_Date { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public DateTime? End_Date { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string Role { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public int Team_Size { get; set; }
        public virtual Employee employee { get; set; }
    }
}
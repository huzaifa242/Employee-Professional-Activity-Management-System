using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Confirmation
    {
        [Key]
        public int ConfirmationId { get; set; }
        [ForeignKey("employee")]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string fname { get; set; }
        public string ftype { get; set; }
        public string status { get; set; }
        public virtual Employee employee { get; set; }
    }
}
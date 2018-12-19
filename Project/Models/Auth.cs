using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Auth
    {
        [Key]
        [ForeignKey("employee")]
        public int AuthId { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This Field is Required")]
        public string password { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string Auth_Role { get; set; }
        public virtual Employee employee { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage ="This Field is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public DateTime? Dob { get; set; }
        public string Permanent_Addr { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string Current_Addr { get; set; }
        public string Marital_Status { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string Contact_No { get; set; }
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        [Required(ErrorMessage = "This Field is Required")]
        public string Email { get; set; }
        public string Blood_Group { get; set; }
        public virtual Auth auth { get; set; }
        public virtual ICollection<Educational_details> Educational_Details { get; set; }
        public virtual ICollection<Workshop_Details> Workshop_Details { get; set; }
        public virtual ICollection<Training_Details> Training_Details { get; set; }
        public virtual ICollection<Turnkey_Project> Turnkey_Projects { get; set; }
        public virtual ICollection<Confirmation> Confirmations { get; set; }
    }
}
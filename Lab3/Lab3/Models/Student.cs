using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Lab3.Models
{
    public class Student
    {
        public String firstName { get; set; }
        public String lastName { get; set; }
        public int studentId { get; set; }
        [StringLength(255)]
        [EmailAddress]
        public String EmailAddress { get; set; }
        [StringLength(100)]
        [BindProperty(Name = "pass")]
        public String Password { get; set; }
        public String description { get; set; }

    }
}

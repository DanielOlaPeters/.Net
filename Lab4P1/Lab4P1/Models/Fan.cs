using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Lab4P1.Models
{
    public class Fan
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => LastName + ", " + FirstName;

        public IEnumerable<Subscription> Subscriptions { get; set; }
    }
}
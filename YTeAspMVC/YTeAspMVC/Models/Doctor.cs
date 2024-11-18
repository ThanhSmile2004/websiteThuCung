using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YTeAspMVC.Models
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDoctor { get; set; }

        [StringLength(255)]
        [Required]
        public string Email { get; set; }

        [StringLength(255)]
        [Required]
        public string FullName { get; set; }

        [StringLength(255)]
        [Required]
        public string Password { get; set; }

        [StringLength(255)]
        public string Specialist { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        public string Describe { get; set; }

        public int Status { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
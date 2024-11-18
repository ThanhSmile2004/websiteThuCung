using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YTeAspMVC.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }

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
        public string PhoneNumber { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        public string Gender { get; set; }

        public int Status { get; set; }

        public int IdRole { get; set; }

        public string Created { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Number> Numbers { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
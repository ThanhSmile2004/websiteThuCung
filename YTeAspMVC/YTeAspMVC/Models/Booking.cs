using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YTeAspMVC.Models
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdBooking { get; set; }

        [StringLength(255)]
        public string Day { get; set; }

        public int Time { get; set; }

        [StringLength(255)]
        public string Reason { get; set; }
        public string Dichvu { get; set; }

        public int Status { get; set; }

        public int IdUser { get; set; }
        public int IdDoctor { get; set; }

        public virtual User User { get; set; }
        public virtual Doctor Doctor { get; set; }

        

    }
}
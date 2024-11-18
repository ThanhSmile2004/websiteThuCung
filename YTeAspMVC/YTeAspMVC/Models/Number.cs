using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YTeAspMVC.Models
{
    public class Number
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdNumber { get; set; }

        public int NumberInt { get; set; }

        public string Day { get; set;  }

        public int IdUser { get; set; }

        public virtual User User { get; set; }
    }
}
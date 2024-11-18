using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YTeAspMVC.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPost { get; set; }

        [StringLength(255)]
        [Required]
        public string Title { get; set; }

        [StringLength(255)]
        [Required]
        public string Image { get; set; }

        public string Description { get; set; }

        public string Created { get; set; }

        public int Status { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace todoproject.Models
{
    public class PopulateTaskViewModel
    {
        public int TaskId { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Title")]
        public string TaskTitle { get; set; }
        [Display(Name ="Description")]
        public string TaskDescription { get; set; }

        [Required]
        [Display(Name ="Time")]
        public DateTime TaskTime { get; set; }

        [Required]
        public string Priority { get; set; }
    }
}
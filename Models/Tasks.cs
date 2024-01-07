using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace todoproject.Models
{
    public class Tasks
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name ="Title")]
        public string TaskTitle { get; set; }
        [Display(Name = "Description")]
        public string TaskDescription { get; set; }
        [Display(Name = "Time")]
        [Required]
        public DateTime TaskTime { get; set; }
        
        [Required]
        public string Priority { get; set; }
        [Display(Name = "Status")]
        public bool Status { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
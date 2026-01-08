using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskTracker.Models
{
    public class TaskItem
    {
        public int TaskId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public int UserId { get; set; }
    }
}
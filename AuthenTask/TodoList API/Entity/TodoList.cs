using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList_API.Entity
{
    public class TodoList
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("AspNetUsers")]
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public string LongNote { get; set; } 
        public string DueDate {get; set; }
        public TasksUpdate TasksUpdate { get; set; } 
    }
}

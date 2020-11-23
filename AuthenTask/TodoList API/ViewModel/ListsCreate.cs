using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList_API.ViewModel
{
    public class ListsCreate
    {
        [Required(ErrorMessage ="Enter Task")]
        public string Name { get; set; }
        public string LongNote { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public string TasksUpdate { get; set; }
    }
}

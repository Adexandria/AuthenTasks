using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList_API.ViewModel
{
    public class ListsView
    {
        public string Name { get; set; }
        public string LongNote { get; set; }
        public string DueDate { get; set; } = "0-00-0000 00:00:00";
        public string TasksUpdate { get; set; } = "Uncompleted";
    }
}

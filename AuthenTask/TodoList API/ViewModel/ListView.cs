using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList_API.ViewModel
{
    public class ListView
    {
        public string Name { get; set; }
        public string LongNote { get; set; }
        public string DueDate { get; set; } = null;
        public string TasksUpdate { get; set; } = "Uncompleted";
    }
}

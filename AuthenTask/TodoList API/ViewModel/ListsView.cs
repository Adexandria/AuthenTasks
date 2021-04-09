using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList_API.ViewModel
{
    public class ListsView :ListView
    {
        public Guid Id { get; set; }
       
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList_API.Entity;

namespace TodoList_API.Services
{
    public class ListDbContext : DbContext
    {
        public ListDbContext(DbContextOptions<ListDbContext> options):base(options)
        {
                
        }
        public DbSet<TodoList> TodoLists { get; set; }
    }
}

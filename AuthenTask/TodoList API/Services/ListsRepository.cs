using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList_API.Entity;

namespace TodoList_API.Services
{
    public class ListsRepository : ILists
    {
        private readonly ListDbContext db;
        public ListsRepository(ListDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<TodoList> GetTodo 
        {
            get
            {
                return db.TodoLists;
            }
        }



        public async Task<TodoList> Add(TodoList list)
        {
            if(list == null)
            {
                throw new NullReferenceException(nameof(list));
            }
            list.Id = new Guid();
            await db.TodoLists.AddAsync(list);
            return list;
        }

        public async Task<int> Delete(Guid id)
        {
            if (id == null)
            {
                throw new NullReferenceException(nameof(id));
            }
            var query = await GetTodoById(id);
            if(query == null)
            {
                throw new NullReferenceException(nameof(query));
            }
            db.TodoLists.Remove(query);
            return await Saves();
        }

        public async Task<TodoList> GetTodoById(Guid id)
        {
           if(id== null)
            {
                throw new NullReferenceException(nameof(id));
            }
            return await db.TodoLists.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> Saves()
        {
            return await db.SaveChangesAsync();
        }

        public async Task<TodoList> SearchByDate(string date)
        {
            return await db.TodoLists.Where(r => r.DueDate.ToString() == date).FirstOrDefaultAsync();
        }

        public async Task<TodoList> SearchByName(string name)
        {
            return await db.TodoLists.Where(r => r.Name == name).FirstOrDefaultAsync();
        }

        public TodoList Update(TodoList list)
        {
            var query = db.TodoLists.Attach(list);
            query.State = EntityState.Modified;
            return list;
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList_API.Entity;
using TodoList_API.Login_Entity;

namespace TodoList_API.Services
{
    public class ListsRepository : ILists
    {
        private readonly IdentityDbcontext db;
        private readonly UserManager<SignUp> sign;
        public ListsRepository(IdentityDbcontext db,UserManager<SignUp> sign)
        {
            this.db = db;
            this.sign = sign;
        }
        public IEnumerable<TodoList> GetTodo (string id)
        {
            return db.TodoLists.Where(s => s.OwnerId == id).AsNoTracking().OrderBy(s=>s.Id);
        }

        public async Task<TodoList> Add(TodoList list,string username)
        {
            if(list == null)
            {
                throw new NullReferenceException(nameof(list));
            }
            var user = sign.FindByNameAsync(username).Result;
            list.OwnerId = user.Id;
            list.Id = new Guid();
            await db.TodoLists.AddAsync(list);
            return list;
        }

        public async Task<int> Delete(Guid id)
        {
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
            return await db.TodoLists.Where(r => r.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<int> Saves()
        {
            return await db.SaveChangesAsync();
        }

        public IEnumerable<TodoList> SearchByDate(string date)
        {
            return  db.TodoLists.Where(r => r.DueDate.ToString() == date).AsNoTracking();
        }

        public async Task<TodoList> SearchByName(string name)
        {
            return await db.TodoLists.Where(r => r.Name == name).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<TodoList> Update(TodoList list,Guid id)
        {
            var query = await GetTodoById(id);
            list.OwnerId = query.OwnerId;
            db.Entry(query).State =EntityState.Detached;
            db.Entry(list).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return await GetTodoById(list.Id);
        }
    }
}

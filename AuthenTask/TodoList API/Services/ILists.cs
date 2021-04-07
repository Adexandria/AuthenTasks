using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList_API.Entity;

namespace TodoList_API.Services
{
    public interface ILists
    {
        IEnumerable<TodoList> GetTodo(string id);
        Task<TodoList> GetTodoById(Guid id);
        Task<TodoList> Add(TodoList list,string username);
        Task<TodoList> SearchByName(string name);
        IEnumerable<TodoList> SearchByDate(string date);
        Task<int> Delete(Guid id);
        Task<int> Saves();
        Task<TodoList> Update(TodoList list,Guid id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList_API.Entity;

namespace TodoList_API.Services
{
    public interface ILists
    {
        IEnumerable<TodoList> GetTodo { get; }
        Task<TodoList> GetTodoById(Guid id);
        Task<TodoList> Add(TodoList list);
        Task<TodoList> SearchByName(string name);
        Task<TodoList> SearchByDate(string date);
        Task<int> Delete(Guid id);
        Task<int> Saves();
        TodoList Update(TodoList list);
    }
}

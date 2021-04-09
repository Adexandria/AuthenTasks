using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoList_API.Entity;
using TodoList_API.Login_Entity;
using TodoList_API.Services;
using TodoList_API.ViewModel;

namespace TodoList_API.Controllers
{
    [ApiController]
    [Route("api/user/{username}/tasks")]
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ILists lists;
        private readonly UserManager<SignUp> sign;
        private readonly IMapper mapper;

        public TasksController(ILists lists, IMapper mapper, UserManager<SignUp> sign)
        {
            this.lists = lists;
            this.mapper = mapper;
            this.sign = sign;
        }
        [HttpGet()]
        public ActionResult<IEnumerable<ListsView>> GetTasks(string username)
        {
            var user = sign.FindByNameAsync(username).Result;
            var tasks = lists.GetTodo(user.Id);
            var newTasks = mapper.Map<IEnumerable<ListsView>>(tasks);
            return Ok(newTasks);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ListView>> GetTasksById(Guid id)
        {
            var list = await lists.GetTodoById(id);
            if (list == null)
            {
                return NotFound("The task was not found");
            }
            var newlist = mapper.Map<ListView>(list);
            return Ok(newlist);
        }
       /* [HttpGet("{date}")]
        public ActionResult<IEnumerable<ListsView>> SearchTasks(string date)
        {
            var task = lists.SearchByDate(date);
            if (task != null)
            {
                var newTask = mapper.Map<IEnumerable<ListsView>>(task);
                return Ok(newTask);
            }
            return NotFound();
        }*/

        [HttpPost()]
        public async Task<ActionResult<ListsView>> PostTask(ListsCreate create, string username)
        {
            var newTask = mapper.Map<TodoList>(create);
            var task = await lists.Add(newTask, username);
            await lists.Saves();
            var query = mapper.Map<ListsView>(task);
            return Ok(query);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ListsView>> UpdateTask(ListUpdate updatedtask,Guid id) 
        {
            var query = await lists.GetTodoById(id);
            if(query != null) 
            {
                var updatetask = mapper.Map<TodoList>(updatedtask);
                var task = await lists.Update(updatetask, id);
                await lists.Saves();
                var newtask = mapper.Map<ListsView>(task);
                return Ok(newtask);
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id) 
        {
            var query = await lists.GetTodoById(id);
            if( query != null) 
            {
                await lists.Delete(id);
                await lists.Saves();
                return NoContent();
            }
            return NotFound();
        }
    }
}

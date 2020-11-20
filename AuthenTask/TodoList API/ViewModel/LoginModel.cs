using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TodoList_API.Login_Entity;

namespace TodoList_API.ViewModel
{
  
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter User Name")]
        public string Username { get; set; }
       [Required(ErrorMessage = "Enter Password Name")]
        public string Password { get; set; }
    }
}

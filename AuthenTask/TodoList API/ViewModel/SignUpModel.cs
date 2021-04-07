using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList_API.ViewModel
{
    public class SignUpModel
    {
        [Required]
        public string Id { get; set; }
        [Required(ErrorMessage ="Enter First Name"),StringLength(20)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter LastName"), StringLength(20)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter UserName"), StringLength(20),MinLength(3)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter Password"), StringLength(12),MinLength(4)]
        public string Password { get; set; }
    }
}

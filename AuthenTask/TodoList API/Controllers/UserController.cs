using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoList_API.Login_Entity;
using TodoList_API.ViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoList_API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private UserManager<SignUp> user;
        private readonly IMapper mapper;
        private SignInManager<SignUp> sign;
        public UserController(UserManager<SignUp> user, IMapper mapper,SignInManager<SignUp> sign)
        {
            this.user = user;
            this.mapper = mapper;
            this.sign = sign;
            
        }
       

        
        [HttpPost("signup")]
        public async Task<ActionResult<string>> SignUp(SignUpModel signUp)
        {
            var signups = mapper.Map<SignUp>(signUp);
            await user.AddClaimAsync(signups, new Claim(ClaimTypes.Role, "User"));
            IdentityResult identity = await user.CreateAsync(signups, signups.Password);
            if (identity.Succeeded)
            {
                var result = await sign.PasswordSignInAsync(signups.UserName, signups.Password, false, false);
                return this.StatusCode(StatusCodes.Status201Created, $"Welcome,{signups.UserName} Your account has been created");
            }
            return BadRequest(identity.Errors);
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginModel model)
        {
            var logins = mapper.Map<SignUp>(model);
           var result = await sign.PasswordSignInAsync(logins.UserName, logins.Password, false,false);
            if (result.Succeeded)
            {
                return Ok($"Welcome, {logins.UserName} it's a good day to create a task");
            }
            return BadRequest(result);
        }
    }
}

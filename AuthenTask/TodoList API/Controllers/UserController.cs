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
using Microsoft.AspNetCore.Razor.TagHelpers;
using TodoList_API.Login_Entity;
using TodoList_API.ViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoList_API.Controllers
{
    [ApiController]
    [Route("api/user")]
    [AllowAnonymous]
    public class UserController : Controller
    {
        private UserManager<SignUp> user;
        private readonly IMapper mapper;
        private SignInManager<SignUp> sign;
        private RoleManager<IdentityRole> roleManager;

        public UserController(UserManager<SignUp> user, IMapper mapper, SignInManager<SignUp> sign, RoleManager<IdentityRole> roleManager)
        {
            this.user = user;
            this.mapper = mapper;
            this.sign = sign;
            this.roleManager = roleManager;

        }

        [HttpPost("signup")]
        public async Task<ActionResult<string>> SignUp(SignUpModel newuser)
        {
            var signup = mapper.Map<SignUp>(newuser);
            var EmailisValid = await user.FindByEmailAsync(signup.Email);

            if (EmailisValid == null)
            {
                IdentityResult identity = await user.CreateAsync(signup, signup.Password);

                if (identity.Succeeded)
                {
                    await user.AddClaimAsync(signup, new Claim(ClaimTypes.Role, "User"));
                    await user.AddToRoleAsync(signup, "User");
                    var result = await sign.PasswordSignInAsync(signup.UserName, signup.Password, false, true);
                    if (result.Succeeded)
                    {
                        return this.StatusCode(StatusCodes.Status201Created, $"Welcome,{signup.UserName} Your account has been created");
                    }

                }
                else
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"Invalid password, follow the password requirements {identity.Errors} ");
                }
            }
            return BadRequest("This email is currently used by another user");
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginModel model)
        {
            var logindetails = mapper.Map<SignUp>(model);
            var result = await sign.PasswordSignInAsync(logindetails.UserName, logindetails.Password, false, true);
            await sign.CreateUserPrincipalAsync(logindetails);
            if (result.Succeeded)
            {
                return this.StatusCode(StatusCodes.Status200OK, $"Welcome,{logindetails.UserName} It's a good day to create a task");
            }
            return BadRequest("invalid username or password");
        }
        [HttpPost("{username}/signout")]
        public async Task<IActionResult> Signout(string username)
        {
            var currentuser = await user.FindByNameAsync(username);
            await user.UpdateSecurityStampAsync(currentuser);
            await sign.SignOutAsync();
            return Ok();
        }

        [Authorize(Roles ="Admin")]
        private async Task CreateRoles(string role)
        {
            bool x = await roleManager.RoleExistsAsync(role);
            if (!x)
            {
                var roles = new IdentityRole();
                roles.Name = role;
                await roleManager.CreateAsync(roles);
            }
        }
    }
}


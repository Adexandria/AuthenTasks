using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList_API.Login_Entity;

namespace TodoList_API.Services
{
    public class IdentityDbcontext:IdentityDbContext<SignUp> 
    {
        public IdentityDbcontext(DbContextOptions<IdentityDbcontext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

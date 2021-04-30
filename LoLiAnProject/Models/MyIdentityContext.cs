using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoLiAnProject.Models
{
    public class MyIdentityUser:IdentityUser
    {
        public string FirstName { get; set; }
    }

    //2b
    public class MyIdentityContext : IdentityDbContext<MyIdentityUser>
    {
        public MyIdentityContext(DbContextOptions<MyIdentityContext> options) : base(options)
        {
            //skapar DB schema (men endast om det är en tom databas! annars ignoreras den)
            var result = Database.EnsureCreated();
        }
    }

}


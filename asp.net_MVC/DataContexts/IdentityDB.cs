using asp.net_MVC.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asp.net_MVC.DataContexts
{
    public class IdentityDB : IdentityDbContext<ApplicationUser>
    {
        public IdentityDB()
            : base("DefaultConnection")
        {
        }
    }
}
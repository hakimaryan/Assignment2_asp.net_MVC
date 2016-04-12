using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using asp.net_MVC.Models;

namespace asp.net_MVC.DataContexts
{
    class PetDBContext : DbContext
    {
        public DbSet<Pet> Pets { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<FilePath> FilePaths { get; set; }

    }
}
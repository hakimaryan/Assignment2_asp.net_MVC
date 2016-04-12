using System.Data.Entity;
using asp.net_MVC.Models;

namespace asp.net_MVC.DataContexts
{
    class PetDBContext : DbContext
    {
        public PetDBContext()
           : base("DefaultConnection")
        { }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<FilePath> FilePaths { get; set; }
        public DbSet<Contact> Contacts { get; set; }

    }
}
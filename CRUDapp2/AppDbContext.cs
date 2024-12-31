using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CRUDapp2
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } //databaseset for Users table

        public DbSet<Phone> Phone { get; set; } //databaseset for Phone table

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TrialAndError;Integrated Security=True;"); //connection string for database
        }
    }

    public class User //class for users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }

    public class Phone //class for phones
    {
        public int Id { get; set; }
        public int Phonetype_Id { get; set; }
        public int User_Id { get; set; }
        public long Phonenumber { get; set; }

    }

}

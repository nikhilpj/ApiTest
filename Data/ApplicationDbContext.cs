using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{

    public class ApplicationDbContext : DbContext
    

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }




        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket()
                {
                    Id = 1,
                    UserId = 1,

                    Title = "not able to log in",
                    Description = "not able to log in account",
                    Status = "open"



                }
            );

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    UserName= "nikhilpj98",
                    Password = "password1"

                },
                 new User()
                 {
                     Id = 2,
                     UserName = "user",
                     Password = "123"

                 },
                  new User()
                  {
                      Id = 3,
                      UserName = "abc",
                      Password = "12"

                  }
            );
        }

    }

   

    
}



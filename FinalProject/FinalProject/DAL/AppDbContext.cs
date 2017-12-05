using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

//TODO: Change this using statement to match your project
using FinalProject.Models;


//TODO: Change this namespace to match your project
namespace FinalProject.DAL
{
    // NOTE: Here's your db context for the project.  All of your db sets should go in here
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        //TODO: Make sure that your connection string name is correct here.
        public AppDbContext()
            : base("MyDBConnection", throwIfV1Schema: false) { }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        // TODO: Add dbsets here. Remember, Identity adds a db set for users, 
        //so you shouldn't add that one - you will get an error
        //here's a sample for members
        //public DbSet<Member> Members { get; set; }

        //NOTE: This is a dbSet that you need to make roles work
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Duration> Durations { get; set; }

        public System.Data.Entity.DbSet<FinalProject.Models.Reservation> Reservations { get; set; }

        public System.Data.Entity.DbSet<FinalProject.Models.ReservationFlightDetail> ReservationFlightDetails { get; set; }
    }
}
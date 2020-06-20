using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TraigoApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Quotation> Quotation { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<UsAddress> UsAddress { get; set; }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Airline> Airline { get; set; }
        public DbSet<TransactionControl> TransactionControl { get; set; }
        public DbSet<QuotationStatus> QuotationStatus { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Command> Command { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<CommandStatus> CommandStatus { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {

            return new ApplicationDbContext();
        }
    
    }
}
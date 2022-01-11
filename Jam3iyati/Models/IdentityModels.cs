using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Jam3iyati.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
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
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Association> Associations { get; set; }
        public DbSet<AssociationMemeber> AssociationMemeber { get; set; }
        public DbSet<Memeber> Members { get; set; }
        public DbSet<Demande> Demandes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AdministrationDemande> AdministrationDemande { get; set; }
        public DbSet<Administration> Administrations { get; set; }
        public DbSet<Wilaya> Wilayas { get; set; }
        public DbSet<Daira> Dairas { get; set; }
        public DbSet<Apc> Apcs { get; set; }
       

    }
}
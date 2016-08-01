using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Transcode.Helpers;

namespace Transcode.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser, IEntity
    {
        public ApplicationUser()
        {
            this.Conversions = new List<Conversion>();
            this.Payments = new List<Payment>();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public DateTime CreatedOn { get; set; }
        public Nullable<DateTime> LastLogin { get; set; }
        public virtual IEnumerable<Conversion> Conversions { get; set; }
        public virtual IEnumerable<Payment> Payments { get; set; }
    }

    public class TranscodeContext : IdentityDbContext<ApplicationUser>
    {
        public TranscodeContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Conversion>().HasRequired<Media>(c => c.ProvidedMedia).WithMany().WillCascadeOnDelete(false);
            //modelBuilder.Entity<Conversion>().HasRequired<Media>(c => c.ConvertedMedia).WithMany().WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }

        public static TranscodeContext Create()
        {
            return new TranscodeContext();
        }

        public DbSet<Conversion> Conversions { get; set; }
    }
}
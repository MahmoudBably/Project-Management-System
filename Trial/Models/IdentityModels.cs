using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Trial.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(255)]
        public string Fname { get; set; }
        [Required]
        [StringLength(255)]
        public string Lname { get; set; }
        [Required]
        [StringLength(500)]
        public string Job_Description { get; set; }
        [Required]
        [StringLength(12)]
        public string PhoneNum { get; set; }

        public byte[] ProfilePicture { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Works_on> Works_on { get; set; }
        public virtual ICollection<Worked_on> Worked_on { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Submit> Submits { get; set; }
        public virtual ICollection<ProjectHistory> ProjectHistory { get; set; }


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
        public DbSet<Project> Projects { get; set; }
        public DbSet<Works_on> Works_on { get; set; }
        public DbSet<Worked_on> Worked_on { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Submit> Submits { get; set; }
        public DbSet<ProjectHistory> ProjectHistory { get; set; }
        public DbSet<Experience> Experience { get; set; }


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
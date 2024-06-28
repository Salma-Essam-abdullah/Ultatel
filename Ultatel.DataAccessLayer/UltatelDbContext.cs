using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ultatel.Models.Entities;
using Ultatel.Models.Entities.Identity;

namespace Ultatel.DataAccessLayer
{
    public class UltatelDbContext : IdentityDbContext<AppUser>
    {
        public UltatelDbContext(DbContextOptions<UltatelDbContext> options) : base(options) { }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<SuperAdmin> SuperAdmins { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentLogs> StudentLogs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
           .HasMany(s => s.Admins)
           .WithMany(c => c.Students)
           .UsingEntity<Dictionary<string, object>>(
               "AdminStudent",
               j => j.HasOne<Admin>().WithMany().HasForeignKey("AdminId"),
               j => j.HasOne<Student>().WithMany().HasForeignKey("StudentId")
           );

            base.OnModelCreating(modelBuilder); 
        }


    }



}

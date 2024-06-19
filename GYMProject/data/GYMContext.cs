using System.Collections.Generic;
using System.Data.Entity;
using GYMProject.Models;

namespace GYMProject.Data
{
    public class GYMContext : DbContext
    {
        public GYMContext() : base("name=GYMContext")
        {
            Database.SetInitializer(new GYMInitializer());
        }

        public DbSet<Staff> Staff { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Fluent API configurations if needed
            base.OnModelCreating(modelBuilder);
        }

        public class GYMInitializer : DropCreateDatabaseIfModelChanges<GYMContext>
        {
            protected override void Seed(GYMContext context)
            {
                var admins = new List<Admin>
                {
                    new Admin { Username = "admin", Password = "password" }
                };

                admins.ForEach(a => context.Admins.Add(a));
                context.SaveChanges();
            }
        }
    }
}

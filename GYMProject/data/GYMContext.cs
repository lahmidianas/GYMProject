using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using GYMProject.Models;
using GYMProject.Security;

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
            base.OnModelCreating(modelBuilder);
        }

        public class GYMInitializer : DropCreateDatabaseIfModelChanges<GYMContext>
        {
            protected override void Seed(GYMContext context)
            {
                var seedPassword = ConfigurationManager.AppSettings["SeedAdminPassword"];
                if (string.IsNullOrWhiteSpace(seedPassword))
                {
                    throw new InvalidOperationException("AppSetting 'SeedAdminPassword' must be set with a strong password.");
                }

                var passwordData = PasswordHasher.HashPassword(seedPassword);
                var admins = new List<Admin>
                {
                    new Admin
                    {
                        Username = "admin",
                        PasswordHash = passwordData.hash,
                        PasswordSalt = passwordData.salt,
                        PasswordIterations = passwordData.iterations
                    }
                };

                admins.ForEach(a => context.Admins.Add(a));
                context.SaveChanges();
            }
        }
    }
}

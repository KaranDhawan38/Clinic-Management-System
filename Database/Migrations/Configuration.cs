namespace Database.Migrations
{
    using ApteanClinic.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApteanClinic.Database.ApteanClinicContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApteanClinic.Database.ApteanClinicContext context)
        {
            //  This method will be called after migrating to the latest version.
            var Admin = new User
            {
                Name = "Admin",
                Contact = "9872231000",
                Gender = ApteanClinic.Models.Enum.Gender.Male,
                BloodGroup = ApteanClinic.Models.Enum.BloodGroup.A,
                Email = "admin@gmail.com",
                Password = "Bbmhp131",
                ConfirmPassword = "Bbmhp131",
                Role = ApteanClinic.Models.Enum.Role.Admin,
            };
            context.Users.Add(Admin);
            context.SaveChanges();
            
            
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}

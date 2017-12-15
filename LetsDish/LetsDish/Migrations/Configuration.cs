namespace LetsDish.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
	using LetsDish.Models;
	using Microsoft.AspNet.Identity.EntityFramework;

	internal sealed class Configuration : DbMigrationsConfiguration<LetsDish.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LetsDish.Models.ApplicationDbContext context)
        {
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//
			var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

			var user = new ApplicationUser
			{
				UserName = "TestUser",
				Email = "test@aol.com",
			};

			userManager.CreateAsync(user, "password").Wait();
		}
	}
}

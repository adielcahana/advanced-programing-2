namespace WebServer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using MazeGeneratorLib;

    internal sealed class Configuration : DbMigrationsConfiguration<WebServer.Models.UserContext>
    {
        DFSMazeGenerator mg = new DFSMazeGenerator();
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebServer.Models.UserContext context)
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
            User user2 = context.Users.Find("bbb");
            User user3 = context.Users.Find("ccc");
            context.Users.Remove(user2);
            context.Users.Remove(user3);
        }
    }
}

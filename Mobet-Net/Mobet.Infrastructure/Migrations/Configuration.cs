namespace Mobet.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Mobet.Infrastructure.ModelsContainer>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(Mobet.Infrastructure.ModelsContainer context)
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

            context.Users.AddOrUpdate(x => x.Subject, new Domain.Entities.User
            {
                Subject = "0F809FCE-D394-4907-8249-07BCA520A04A",
                Name = "ÐíËÉ³¬",
                NickName = "ÄÂÇáº®",
                Sex = 1,
                Email = "mobet_net@163.com",
                Telphone = "15618275259",
                Password = "mobet",
                Birthday = DateTime.Parse("1991-06-09")
            });
        }
    }
}

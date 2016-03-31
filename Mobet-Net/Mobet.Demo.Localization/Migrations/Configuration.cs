namespace Mobet.Demo.Localization.Migrations
{
    using Mobet.Localization.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Mobet.Demo.Localization.ModelsContainer>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Mobet.Demo.Localization.ModelsContainer context)
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

            context.Languages.AddOrUpdate(x => x.Name,
                new Language
                {
                    DisplayName = "简体中文",
                    Name = "zh-CN",
                },
                new Language
                {
                    DisplayName = "英文",
                    Name = "en",
                }
                );

            context.LanguageTexts.AddOrUpdate(x => x.Key,
                new LanguageText
                {
                    Key = "HelloWorld",
                    Value = "你好世界！-- 来自数据库",
                    LanguageName = "zh-CN",
                    Source = "Database"
                },
                new LanguageText
                {
                    Key = "HelloWorld",
                    Value = "Hello,World! -- from database",
                    LanguageName = "en",
                    Source = "Database"
                }
                );
        }
    }
}

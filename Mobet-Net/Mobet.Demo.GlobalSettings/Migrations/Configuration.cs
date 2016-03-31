namespace Mobet.Demo.GlobalSettings.Migrations
{
    using Mobet.GlobalSettings;
    using Mobet.GlobalSettings.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Linq;
    public sealed class Configuration : DbMigrationsConfiguration<Mobet.Demo.GlobalSettings.ModelsContainer>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Mobet.Demo.GlobalSettings.ModelsContainer context)
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


            context.GlobalSettings.AddOrUpdate(x => x.Name,

                new Mobet.GlobalSettings.Models.GlobalSetting
                {
                    DisplayName = "资源-根目录",
                    Name = Constants.GlobalSettingIds.Resources.Domain,
                    Value = "https://120.25.244.254:44300/",
                    Group = Constants.GlobalSettingGroup.Resources,
                    Scope = GlobalSettingScope.Application,
                    Description = "资源-根目录"
                },
                new Mobet.GlobalSettings.Models.GlobalSetting
                {
                    DisplayName = "资源-图片根目录",
                    Name = Constants.GlobalSettingIds.Resources.Images,
                    Value = "https://120.25.244.254:44300/resources/images",
                    Group = Constants.GlobalSettingGroup.Resources,
                    Description = "资源-图片根目录"
                },
                new Mobet.GlobalSettings.Models.GlobalSetting
                {
                    DisplayName = "资源-移动应用安装包根目录",
                    Name = Constants.GlobalSettingIds.Resources.ApplicationDownload,
                    Value = "https://120.25.244.254:44300/resources/application/download",
                    Group = Constants.GlobalSettingGroup.Resources,
                    Description = "资源-移动应用安装包根目录"
                }

            );


        }
    }
}

using Mobet.Configuration.Startup;
using Mobet.Dependency;
using Mobet.Domain.Entities;
using Mobet.Domain.Repositories;
using Mobet.Domain.Services;
using Mobet.EntityFramework;
using Mobet.GlobalSettings;
using Mobet.GlobalSettings.Models;
using Mobet.GlobalSettings.Provider;
using Mobet.GlobalSettings.Store;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;
using Autofac.Core;
using Mobet.EntityFramework.Startup;

namespace Mobet.Demo.GlobalSettings
{
    class Program
    {
        static void Main(string[] args)
        {
            StartupConfig.RegisterDependency(cfg =>
            {
                cfg.GlobalSettingsConfiguration.Providers.Add<ResourcesGlobalSettingProvider>();
               
                cfg.UseDataAccessEntityFramework(x =>
                {
                    x.DefaultNameOrConnectionString = "Mobet.Authorization";
                });
                cfg.RegisterConsoleApplication();
            });

            IGlobalSettingManager manager = IocManager.Instance.Resolve<IGlobalSettingManager>();


            var settings = manager.GetAllSettingsAsync().Result;

            foreach (var s in settings)
            {
                Console.WriteLine(JsonConvert.SerializeObject(s));
            }


            Console.ReadLine();
        }
    }

    public class ResourcesGlobalSettingProvider : GlobalSettingsProvider
    {
        public override IEnumerable<Mobet.GlobalSettings.Models.GlobalSetting> GetSettings(GlobalSettingsProviderContext context)
        {
            return new[]
            {
                new Mobet.GlobalSettings.Models.GlobalSetting{
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
                    Value = "https://120.25.244.254:44300/",
                    Group = Constants.GlobalSettingGroup.Resources,
                    Description = "资源-图片根目录"
                },
                new Mobet.GlobalSettings.Models.GlobalSetting
                {
                    DisplayName = "资源-移动应用安装包根目录",
                    Name = Constants.GlobalSettingIds.Resources.ApplicationDownload,
                    Value = "https://120.25.244.254:44300/",
                    Group = Constants.GlobalSettingGroup.Resources,
                    Description = "资源-移动应用安装包根目录"
                }
            };
        }
    }

    public class Constants
    {

        public static class GlobalSettingIds
        {
            public static class Resources
            {
                public const string Domain = "Resources.Domain";
                public const string Images = "Resources.Images";
                public const string ApplicationDownload = "Resources.ApplicationDownload";
            }
        }


        public static class GlobalSettingGroup
        {
            public const string Resources = "静态资源";
        }
    }

    public interface IGlobalSettingService : IApplicationService
    {
        IEnumerable<Mobet.GlobalSettings.Models.GlobalSetting> GetAllSettings();
        GlobalSetting GetSetting(string name);
        GlobalSetting DeleteSetting(string name);
        GlobalSetting AddOrUpdateSetting(Mobet.GlobalSettings.Models.GlobalSetting setting);
    }

    public class GlobalSettingService : IGlobalSettingService
    {
        public IGlobalSettingRepository GlobalSettingRepository { get; set; }

        public GlobalSettingService(IGlobalSettingRepository globalSettingRepository)
        {
            GlobalSettingRepository = globalSettingRepository;
        }

        public virtual IEnumerable<Mobet.GlobalSettings.Models.GlobalSetting> GetGlobalSettings()
        {
            Console.WriteLine("do get global settings ...");

            return GlobalSettingRepository.Models.AsEnumerable();
        }

        public IEnumerable<Mobet.GlobalSettings.Models.GlobalSetting> GetAllSettings()
        {
            var models = GlobalSettingRepository.Models.ToList();

            return models;
        }

        public GlobalSetting GetSetting(string name)
        {
            return GlobalSettingRepository.FirstOrDefault(x => x.Name == name);
        }

        public GlobalSetting DeleteSetting(string name)
        {
            return GlobalSettingRepository.Remove(GlobalSettingRepository.FirstOrDefault(x => x.Name == name));
        }

        public GlobalSetting AddOrUpdateSetting(Mobet.GlobalSettings.Models.GlobalSetting setting)
        {
            if (setting.Id == Guid.Empty)
            {
                return GlobalSettingRepository.Add(setting);
            }
            return GlobalSettingRepository.Update(setting);
        }
    }

    //public class GlobalSettingStore : IGlobalSettingStore, IDependency
    //{

    //    public IGlobalSettingService GlobalSettingService { get; set; }
    //    public GlobalSettingStore(IGlobalSettingService globalSettingService)
    //    {
    //        GlobalSettingService = globalSettingService;
    //    }

    //    public Task<GlobalSetting> AddOrUpdateSettingAsync(GlobalSetting setting)
    //    {
    //        return Task.FromResult(GlobalSettingService.AddOrUpdateSetting(setting));
    //    }

    //    public Task<GlobalSetting> DeleteSettingAsync(string name)
    //    {
    //        return Task.FromResult(GlobalSettingService.DeleteSetting(name));
    //    }

    //    public Task<List<GlobalSetting>> GetAllSettingsAsync()
    //    {
    //        return Task.FromResult(GlobalSettingService.GetAllSettings().ToList());
    //    }

    //    public Task<GlobalSetting> GetSettingAsync(string name)
    //    {
    //        return Task.FromResult(GlobalSettingService.GetSetting(name));
    //    }
    //}

    public class ModelsContainer : EntityFrameworkDbContext
    {
        public ModelsContainer() : base("Mobet.Authorization")
        {
        }
        public ModelsContainer(string nameOrConnectionString)
           : base(nameOrConnectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Mobet.GlobalSettings.Models.GlobalSetting> GlobalSettings { get; set; }

    }

    public class Repository<TEntity> : Repository<ModelsContainer, TEntity, Guid>
        where TEntity : class, IEntity<Guid>
    {
        public Repository(IEntityFrameworkDbContextProvider<ModelsContainer> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }

    public interface IRepository<TEntity> : IRepository<TEntity, Guid>
        where TEntity : class, IEntity<Guid>
    {


    }
    public interface IGlobalSettingRepository : IRepository<Mobet.GlobalSettings.Models.GlobalSetting>
    {

    }

    public class GlobalSettingRepository : Repository<Mobet.GlobalSettings.Models.GlobalSetting>, IGlobalSettingRepository
    {
        public GlobalSettingRepository(IEntityFrameworkDbContextProvider<ModelsContainer> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }


}
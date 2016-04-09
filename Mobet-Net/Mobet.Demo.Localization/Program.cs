using System;
using System.Globalization;
using System.Threading;
using System.Reflection;

using Mobet.Dependency;

using Mobet.Localization.Dictionaries.Xml;
using Mobet.Localization.Dictionaries;
using Mobet.Localization;
using Mobet.Localization.Sources;
using Mobet.Localization.Dictionaries.Json;
using Mobet.Localization.Sources.Resource;

using Mobet.Demo.Localization.Localization.ResourceSerouces;

using Mobet.Configuration.Startup;
using Mobet.Localization.Sources.Db;
using Mobet.Localization.Dictionaries.Db;
using Mobet.Localization.Store;
using Mobet.Localization.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mobet.EntityFramework.Startup;
using Mobet.EntityFramework;
using System.Data.Entity;
using Mobet.Domain.Entities;
using Mobet.Domain.Repositories;
using System.Linq.Expressions;
using Mobet.Domain.Services;
using System.Linq;

namespace Mobet.Demo.Localization
{
    class Program
    {

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");

            StartupConfig.RegisterDependency(cfg =>
            {
                cfg.UseLocalization(config =>
                {
                    config.Sources.Add(
                                new DictionaryBasedLocalizationSource(
                                    "Demo",
                                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                                        Assembly.GetExecutingAssembly(), "Mobet.Demo.Localization.Localization.XmlSources"
                            )));
                    config.Sources.Add(
                                new DictionaryBasedLocalizationSource(
                                    "Lang",
                                    new JsonEmbeddedFileLocalizationDictionaryProvider(
                                        Assembly.GetExecutingAssembly(),
                                         "Mobet.Demo.Localization.Localization.JsonSources"
                            )));

                    config.Sources.Add(new ResourceFileLocalizationSource("Res", Res.ResourceManager));

                    config.Sources.Add(new DbLocalizationSource("Database",new DbLocalizationDictionaryProvider()));
                });


                cfg.UseDataAccessEntityFramework(x =>
                {
                    x.DefaultNameOrConnectionString = "Mobet.Authorization";
                });


                cfg.RegisterConsoleApplication();
            });

            ILocalizationManager localizationManager = IocManager.Instance.Resolve<ILocalizationManager>();

            ILocalizationSource _localizationSource = localizationManager.GetSource("Demo");
            ILocalizationSource _localizationSource2 = localizationManager.GetSource("Lang");
            ILocalizationSource _localizationSource3 = localizationManager.GetSource("Res");
            ILocalizationSource _localizationSource4 = localizationManager.GetSource("Database");

            var helloWorld = _localizationSource.GetString(LocalizationNameConsts.HelloWorld);
            var helloWorld2 = _localizationSource2.GetString(LocalizationNameConsts.HelloWorld);
            var helloWorld3 = _localizationSource3.GetString(LocalizationNameConsts.HelloWorld);

            IocManager.Instance.Register<ILocalizationDictionary, DbLocalizationDictionary>(DependencyLifeStyle.Transient);

            var helloWorld4 = _localizationSource4.GetString(LocalizationNameConsts.HelloWorld);

            Console.WriteLine(helloWorld);
            Console.WriteLine(helloWorld2);
            Console.WriteLine(helloWorld3);
            Console.WriteLine(helloWorld4);

            Console.ReadKey();
        }
    }


    public class LocalizationNameConsts
    {

        public const string HelloWorld = "HelloWorld";
    }


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

        public virtual DbSet<Mobet.Localization.Models.Language> Languages { get; set; }
        public virtual DbSet<Mobet.Localization.Models.LanguageText> LanguageTexts { get; set; }


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
    public interface ILanguageTextRepository : IRepository<Mobet.Localization.Models.LanguageText>
    {

    }

    public class LanguageTextRepository : Repository<Mobet.Localization.Models.LanguageText>, ILanguageTextRepository
    {
        public LanguageTextRepository(IEntityFrameworkDbContextProvider<ModelsContainer> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }
    public interface ILanguageRepository : IRepository<Mobet.Localization.Models.Language>
    {

    }

    public class LanguageRepository : Repository<Mobet.Localization.Models.Language>, ILanguageRepository
    {
        public LanguageRepository(IEntityFrameworkDbContextProvider<ModelsContainer> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }
    public class LanguageStore : ILanguageStore, IApplicationService, IDependency
    {
        private ILanguageRepository _languageRepository { get; set; }

        public LanguageStore(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public Task<Language> AddOrUpdateLanguageAsync(Language language)
        {
            throw new NotImplementedException();
        }

        public Task<Language> DeleteLanguageAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Language>> GetAllLanguagesAsync()
        {
            var languages = _languageRepository.Get(x => true).ToList();
            return await Task.FromResult(languages);
        }

        public async Task<Language> GetLanguageAsync(string name)
        {
            return await Task.FromResult(_languageRepository.FirstOrDefault(x => x.Name == name));
        }
    }

    public class LanguageTextStore : ILanguageTextStore, IApplicationService, IDependency
    {
        private ILanguageTextRepository _languageTextRepository { get; set; }

        public LanguageTextStore(ILanguageTextRepository languageTextRepository)
        {
            _languageTextRepository = languageTextRepository;
        }
        public Task<LanguageText> AddOrUpdateLanguageTextAsync(LanguageText languageText)
        {
            throw new NotImplementedException();
        }

        public Task<LanguageText> DeleteLanguageTextAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LanguageText>> GetAllLanguageTextsAsync(Expression<Func<LanguageText, bool>> lambda)
        {
            return await _languageTextRepository.Get(lambda).ToListAsync();
        }

        public async Task<LanguageText> GetLanguageTextAsync(string name)
        {
            return await Task.FromResult(_languageTextRepository.FirstOrDefault(x => x.Key == name));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Domain.Services;
using Mobet.Dependency;
using Autofac;
using System.Reflection;
using Mobet.Caching;
using Mobet.Configuration.Startup;
using Autofac.Core;

namespace Mobet.Demo.Auditing
{
    class Program
    {
        static void Main(string[] args)
        {

            StartupConfig.RegisterDependency(cfg =>
            {
                cfg.RegisterConsoleApplication();
            });
            // castle windsor
            //using (var container = new WindsorContainer())
            //{
            //    container.Register(
            //            Classes.FromAssembly(Assembly.GetExecutingAssembly())
            //                .IncludeNonPublicTypes()
            //                .BasedOn<ISingletonDependency>()
            //                .WithService.Self()
            //                .WithService.DefaultInterfaces()
            //                .LifestyleSingleton()
            //     );
            //    var service = container.Resolve<IService>();
            //    service.Mothod();
            //}

            // aotufac

            var builder = new ContainerBuilder();
            builder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => typeof(ISingletonDependency).IsAssignableFrom(t) && t != typeof(ISingletonDependency) && !t.IsAbstract)
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .SingleInstance();


            List<NamedPropertyParameter> paramters = new List<NamedPropertyParameter>();
            paramters.Add(new NamedPropertyParameter("something", "Hello,World!"));
            paramters.Add(new NamedPropertyParameter("cacheManager", IocManager.Instance.Resolve<ICacheManager>()));

            builder.RegisterType<Service>()
                .AsImplementedInterfaces()
                .AsSelf()
                .PropertiesAutowired();
                //.WithParameters(paramters)
                //.Named<IService>("UsingConstructor");

            var container = builder.Build();

            //var service = container.Resolve<IService>();
            //service.Mothod();

            var service2 = container.Resolve<IService>(new NamedParameter("something", "Hello,World!"), new NamedParameter("cacheManager", IocManager.Instance.Resolve<ICacheManager>()));

            service2.Mothod2();

            Console.ReadKey();
        }
    }

    public interface ISingletonDependency
    {

    }

    public interface IService
    {
        void Mothod();
        void Mothod2();

    }
    public class Service : IService, ISingletonDependency
    {
        public ILogging Logging { get; set; }

        private string _something;
        private ICacheManager _cachemanager;

        public Service()
        {
            Logging = NullLogging.Instance;
        }

        public Service(string something, ICacheManager cacheManager)
        {
            _cachemanager = cacheManager;
            _something = something;
        }

        public void Mothod()
        {
            Logging.Log("service doing something...");
        }

        public void Mothod2()
        {
            Console.WriteLine(_something);
        }
    }

    public class NullLogging : ILogging
    {
        private static readonly NullLogging _logging = new NullLogging();
        public static NullLogging Instance { get { return _logging; } }


        public void Log(string message)
        {
            //nothing to do
        }

    }
    public class LoggingA : ILogging, ISingletonDependency
    {
        public void Log(string message)
        {
            Console.WriteLine(DateTime.Now.ToString() + "[-A-] " + message);
        }
    }
    public class LoggingB : ILogging, ISingletonDependency
    {
        public void Log(string message)
        {
            Console.WriteLine(DateTime.Now.ToString() + "[-B-] " + message);
        }
    }
    public interface ILogging
    {
        void Log(string message);
    }
}

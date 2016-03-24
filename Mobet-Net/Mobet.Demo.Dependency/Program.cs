using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Domain.Services;
using Mobet.Dependency;
using Autofac;
using System.Reflection;


namespace Mobet.Demo.Auditing
{
    class Program
    {
        static void Main(string[] args)
        {
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
            var container = builder.Build();

            var service = container.Resolve<IService>();
            service.Mothod();

            Console.ReadKey();
        }
    }

    public interface ISingletonDependency
    {

    }

    public interface IService
    {
        void Mothod();

    }
    public class Service : IService, ISingletonDependency
    {
        public ILogging Logging { get; set; }

        public Service()
        {
            Logging = NullLogging.Instance;
        }

        public void Mothod()
        {
            Logging.Log("service doing something...");
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

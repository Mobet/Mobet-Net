using Autofac;
using Autofac.Extras.DynamicProxy2;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Demo.Autofac.Interceptor
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                   .Where(t => typeof(IDenpendcy).IsAssignableFrom(t) && t != typeof(IDenpendcy) && !t.IsAbstract)
                   .AsImplementedInterfaces()
                   .InstancePerDependency();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                   .Where(t => typeof(IApplicationService).IsAssignableFrom(t) && t != typeof(IApplicationService) && !t.IsAbstract)
                   .AsImplementedInterfaces()
                   .EnableInterfaceInterceptors()
                   .InterceptedBy(typeof(ServiceInterceptor))
                   .InstancePerDependency();

            builder.RegisterType<ServiceInterceptor>();

            var container = builder.Build();
            var service = container.Resolve<IService>();
            service.inter();

            Console.ReadKey();
        }
    }

    public interface IService : IApplicationService
    {

        void inter();
    }

    public class Service : IService
    {
        public void inter()
        {
            Console.WriteLine("mothod inter run...");
        }
    }
    public class ServiceInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            BeforeServiceRunDoSomething();
            invocation.Proceed();
            AfterServiceRunDoSomething();
        }

        private void BeforeServiceRunDoSomething()
        {
            Console.WriteLine("before mothod inter run...");
        }

        private void AfterServiceRunDoSomething()
        {
            Console.WriteLine("after mothod inter run...");
        }
    }

    public interface IApplicationService : IDenpendcy
    {

    }

    public interface IDenpendcy
    {


    }
}

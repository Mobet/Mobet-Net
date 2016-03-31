using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Domain.Services;
using Mobet.Dependency;
using Mobet.Events;
using Mobet.Events.Handlers;
using Mobet.Configuration.Startup;

namespace Mobet.Demo.Events
{
    class Program
    {
        static void Main(string[] args)
        {
            StartupConfiguration boot = new StartupConfiguration();

            StartupConfig.RegisterDependency(cfg =>
            {
                cfg.RegisterConsoleApplication();
            });

            var data = new MySimpleEvent(200, "Simple");

            var eventBus = IocManager.Instance.Resolve<IEventBus>();
            try
            {
                throw new Exception("some exception throw");
            }
            catch (Exception e)
            {
                eventBus.Trigger(new ExceptionOccursEvent(e));
            }
            eventBus.Register<MySimpleEvent>(e =>
            {
                Console.WriteLine(string.Format("Envent data : Name:{0},value:{1}", e.Name, e.Value));
                Console.WriteLine("sending mail ...");
            });
            eventBus.Trigger(new MySimpleEvent(100, "simple2"));
            Console.ReadKey();
        }
    }

    public class MySimpleEvent : DomainEvent
    {
        public int Value { get; set; }

        public string Name { get; set; }

        public MySimpleEvent(int value, string Name)
        {
            Value = value;
        }
    }
    public class MySimpleEventHandler : IEventHandler<MySimpleEvent>
    {
        public void HandleEvent(MySimpleEvent domainEvent)
        {
            Console.WriteLine(string.Format("Envent data : Name:{0},value:{1}", domainEvent.Name, domainEvent.Value));
            Console.WriteLine("custom handler ...");
        }
    }
}

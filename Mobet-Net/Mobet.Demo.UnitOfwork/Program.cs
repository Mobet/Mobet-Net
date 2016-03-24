using Autofac;
using Autofac.Extras.DynamicProxy2;
using Mobet.Configuration.Startup;
using Mobet.Dependency;
using Mobet.Domain.Models;
using Mobet.Domain.Repositories;
using Mobet.Domain.Services;
using Mobet.Domain.UnitOfWork;
using Mobet.Domain.UnitOfWork.ConventionalRegistras;
using Mobet.EntityFramework;
using Mobet.EntityFramework.Startup;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Mobet.Demo.UnitOfwork
{
    class Program
    {
        static void Main(string[] args)
        {
            StartupConfig.RegisterDependency(config =>
            {
                config.UseDataAccessEntityFramework(cfg =>
                {
                    cfg.DefaultNameOrConnectionString = "Demo";
                });

                config.RegisterConsoleApplication();
            });



            //var service = IocManager.Instance.Resolve<IService>();

            //service.Add();

            var service2 = IocManager.Instance.Resolve<Service2>();

            var result = service2.MothodAsync();


            //IocManager.Instance.Register<IService, Service3>();

            //var service3 = IocManager.Instance.Resolve<IService>();

            //service3.Add();
        }
    }
    public class Service : IService, IApplicationService
    {
        private IDemoRepository _demoRepository { get; set; }
        public Service(IDemoRepository demoRepository)
        {
            _demoRepository = demoRepository;
        }

        public virtual void Add()
        {
            _demoRepository.Add(new Demo
            {
                Name = "Demo"
            });
        }
    }
    public class Service2 : ServiceBase
    {
        private IDemoRepository _demoRepository { get; set; }
        public Service2(IDemoRepository demoRepository)
        {
            _demoRepository = demoRepository;
        }

        [UnitOfWork]
        public virtual void Add()
        {
            _demoRepository.Add(new Demo
            {
                Name = "Demo2"
            });
        }

        public override Task MothodAsync()
        {
            using (var uow = IocManager.Instance.Resolve<IUnitOfWorkManager>().Begin())
            {
                _demoRepository.Add(new Demo
                {
                    Name = "Demo2 using and async"
                });

                return uow.CompleteAsync();
            }
        }
    }

    public abstract class ServiceBase
    {
        public virtual Task MothodAsync()
        {
            return Task.FromResult(0);
        }
    }

    public class Service3 : IService
    {
        private IDemoRepository _demoRepository { get; set; }

        private IUnitOfWorkManager _unitOfworkManager { get; set; }

        public Service3(IDemoRepository demoRepository, IUnitOfWorkManager unitOfworkManager)
        {
            _demoRepository = demoRepository;
            _unitOfworkManager = unitOfworkManager;
        }

        public void Add()
        {
            using (var uow = _unitOfworkManager.Begin())
            {
                _demoRepository.Add(new Demo
                {
                    Name = "Demo3"
                });

                uow.Complete();
            }
        }
    }

    public interface IService
    {
        void Add();
    }

    public interface IDemoRepository : IRepository<Demo>
    {

    }

    public class DemoRepository : Repository<ModelsContainer, Demo>, IDemoRepository
    {
        public DemoRepository(IEntityFrameworkDbContextProvider<ModelsContainer> _dbContextProvider)
            : base(_dbContextProvider)
        {

        }
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
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public virtual DbSet<Demo> Demo { get; set; }

    }

    public class Demo : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


}

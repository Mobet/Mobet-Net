﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

using Autofac;
using Autofac.Integration.WebApi;

namespace Mobet.Dependency.ConventionalRegistrars
{
    /// <summary>
    /// This class is used to register basic dependency implementations such as <see cref="IHttpController"/>.
    /// </summary>
    public class ApiControllerConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(context.Assembly);

            builder.Update(context.IocManager.IocContainer);
        }
    }
}

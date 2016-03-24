using System;
using System.Globalization;
using System.Threading;
using System.Reflection;
using Mobet.AutoMapper;
using Mobet.Extensions;
using Mobet.Configuration.Startup;
using Mobet.AutoMapper.Startup;

namespace Mobet.Demo.Localization
{
    class Program
    {

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");

            StartupConfig.RegisterDependency(cfg =>
            {
                cfg.UseAutoMapper();

                cfg.RegisterConsoleApplication();
            });

            UserDto dto = new UserDto
            {
                Name = "小明"
            };

            User u = dto.MapTo<User>();

            Console.WriteLine(u.Name);

            Console.ReadKey();
        }
    }


    public class User
    {
        public string Name { get; set; }
    }

    [AutoMapTo(typeof(User))]
    public class UserDto
    {
        public string Name { get; set; }
    }
}

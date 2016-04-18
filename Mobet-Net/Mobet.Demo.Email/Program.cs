using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Timers;
using System.Xml;
using Mobet.Configuration.Startup;
using Mobet.Dependency;
using Mobet.Net.Mail;

namespace MyEmailTest
{
    class Program
    {
        static void Main(string[] args)
        {

            StartupConfig.RegisterDependency(config =>
            {
                config.RegisterConsoleApplication();
            });

            var emailSender = IocManager.Instance.Resolve<IEmailSender>();

            try
            {
                emailSender.Send("392327013@qq.com", "账号绑定邮箱安全通知", @"
                    <p>亲爱的用户，您好</p>
                    <p>您的验证码是：176503</p>
                    <p>此验证码将用于验证身份，修改密码密保等。请勿将验证码透露给其他人。</p>
                    <p>本邮件由系统自动发送，请勿直接回复！</p>
                    <p>感谢您的访问，祝您使用愉快！</p>
                    <p>此致</p>
                    <p>IT应用支持</p>
                ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

}
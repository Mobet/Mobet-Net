using Castle.Core.Logging;
using Mobet.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mobet.Authorization.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ILogger Logger { get; set; }
        public IAppSession AppSession { get; set; }

        public HomeController() {
            Logger = NullLogger.Instance;
            AppSession = NullAppSession.Instance;
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}
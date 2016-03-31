using Mobet.Authorization.Controllers.Shared;
using Mobet.GlobalSettings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mobet.Authorization.Controllers
{
    public class GlobalSettingsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult Get(DataTablesParam<GlobalSetting> data)
        {
            var models = new List<GlobalSetting> {
                new GlobalSetting {Name = "资源站点地址",Value="资源站点地址" },
                new GlobalSetting {Name = "资源站点地址",Value="资源站点地址" },
                new GlobalSetting {Name = "资源站点地址",Value="资源站点地址" },
                new GlobalSetting {Name = "资源站点地址",Value="资源站点地址" },
                new GlobalSetting {Name = "资源站点地址",Value="资源站点地址" },
                new GlobalSetting {Name = "资源站点地址",Value="资源站点地址" },
                new GlobalSetting {Name = "资源站点地址",Value="资源站点地址" },
                new GlobalSetting {Name = "资源站点地址",Value="资源站点地址" },
                new GlobalSetting {Name = "资源站点地址",Value="资源站点地址" },
                new GlobalSetting {Name = "资源站点地址",Value="资源站点地址" },
                new GlobalSetting {Name = "资源站点地址",Value="资源站点地址" },
                new GlobalSetting {Name = "资源站点地址",Value="资源站点地址" },
                new GlobalSetting {Name = "资源站点地址",Value="资源站点地址" },
            };
            return Json(new { sEcho = Guid.NewGuid().ToString(), iTotalRecords = 1, iTotalDisplayRecords = 1, aaData = models }, JsonRequestBehavior.AllowGet);
        }
    }
}
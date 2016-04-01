using Mobet.Authorization.Controllers.Shared;
using Mobet.GlobalSettings;
using Mobet.GlobalSettings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mobet.Authorization.Controllers
{
    public class GlobalSettingsController : Controller
    {

        public IGlobalSettingManager GlobalSettingManager { get; set; }

        public GlobalSettingsController(IGlobalSettingManager globalSettingManager)
        {
            GlobalSettingManager = globalSettingManager;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail()
        {
            return View();
        }
        public async Task<JsonResult> Get(DataTablesParam<GlobalSetting> data)
        {
            var models = await GlobalSettingManager.GetAllSettingsAsync();

            return Json(new { sEcho = Guid.NewGuid().ToString(), iTotalRecords = 1, iTotalDisplayRecords = 1, aaData = models }, JsonRequestBehavior.AllowGet);
        }
    }
}
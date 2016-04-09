using Mobet.Authorization.Controllers.Shared;
using Mobet.GlobalSettings;
using Mobet.GlobalSettings.Models;
using Mobet.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mobet.Authorization.Controllers
{
    public class GlobalSettingsController : BaseController
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
            var total = models.Count;
            var datas = models.Skip(data.Start).Take(data.Length);
            return Json(new
            {
                sEcho = Guid.NewGuid().ToString(),
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = datas
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> AddOrUpdate(GlobalSetting data)
        {
            await GlobalSettingManager.AddOrUpdateSettingAsync(data);
            await GlobalSettingManager.ClearGlobalSettingCacheAsync(data.Name);

            return Json(new MvcAjaxResponse("保存成功"));
        }
        public async Task<JsonResult> ClearAllCache()
        {
            await GlobalSettingManager.ClearGlobalSettingCacheAsync();
            return Json(new MvcAjaxResponse("缓存清理完成"));
        }
    }
}
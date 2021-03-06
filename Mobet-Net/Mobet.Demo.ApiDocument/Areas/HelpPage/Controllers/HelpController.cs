using System;
using System.Web.Http;
using System.Web.Mvc;
using Mobet.Demo.ApiDocument.Areas.HelpPage.ModelDescriptions;
using Mobet.Demo.ApiDocument.Areas.HelpPage.Models;
using System.Web.Http.Description;
using System.Linq;
using System.Web.Http.Controllers;

namespace Mobet.Demo.ApiDocument.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";

        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; private set; }

        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        public ActionResult Category(string controllerName)
        {

            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();

            IGrouping<HttpControllerDescriptor, ApiDescription> grouping = null;
            foreach (var group in Configuration.Services.GetApiExplorer().ApiDescriptions.ToLookup(api => api.ActionDescriptor.ControllerDescriptor))
            {
                if (group.Key.ControllerName.ToLower() == controllerName)
                {
                    grouping = group;
                    break;
                }
            }
            return View(grouping);
        }

        public ActionResult Api(string apiId)
        {
            if (!String.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            return View(ErrorViewName);
        }

        public ActionResult ResourceModel(string modelName)
        {
            if (!String.IsNullOrEmpty(modelName))
            {
                ModelDescriptionGenerator modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
                ModelDescription modelDescription;
                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
                {
                    return View(modelDescription);
                }
            }

            return View(ErrorViewName);
        }
    }
}
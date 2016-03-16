using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Mobet.SoftwareDevelopmentKit;
using Mobet.SoftwareDevelopmentKit.UserAccount;
using Mobet.SoftwareDevelopmentKit.Core;
using Mobet.Application.Services;
using Castle.Core.Logging;
using Mobet.Logging;
using Newtonsoft.Json;

namespace Mobet.Open.Controllers
{
    public class UserAccountController : ApiController
    {
        public ILogger Logger { get; set; }
        public IUserAccountService UserAccountService { get; set; }
        public ILogService LogService { get; set; }
        public UserAccountController(IUserAccountService _UserAccountService, ILogService _LogService)
        {
            UserAccountService = _UserAccountService;
            LogService = _LogService;
            Logger = NullLogger.Instance;

        }
        [HttpGet]
        [Route("UserAccount/GetPaging",Name="GetPaging")]
        public ApiResponseWrapper GetPaging(UserAccountGetPagingRequest request)
        {
            Logger.DebugFormat("进入方法GetPaging...");
            var repsonse = LogService.Create(new SoftwareDevelopmentKit.Log.LogCreateRequest { 
                Client = "MAC OS",
                Duration = 0,
                Host = "127.0.01",
                Level = SoftwareDevelopmentKit.Log.AuditLevel.DEBUG,
                Route = "UserAccount/GetPaging",
                Time = DateTime.Now,
                Message = "进入方法GetPaging...",
                User = "Mobet"
            });
            Logger.DebugFormat("Logging {0}",JsonConvert.SerializeObject(repsonse));
            return ActionResponse<UserAccountGetPagingResponse>(() => UserAccountService.GetPaging(new UserAccountGetPagingRequest
            {
                PageIndex =1,
                PageSize = 10,
                OrderBy = "ID",
                OrderByDesc = false
            }));
        }

        
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
        public ApiResponseWrapper ActionResponse<T>(Func<T> action) where T : class ,IApiResponse
        {
            var input = default(T);
            string elapse = string.Empty;
            //TODO 请求时间监控
            Stopwatch st = new Stopwatch();
            st.Start();
            input = action();
            st.Stop();
            elapse = st.ElapsedMilliseconds.ToString();
            var wrapper = ApiResponseWrapper.ActionResponse<T>(input, elapse);
            return wrapper;
        }

    }
}
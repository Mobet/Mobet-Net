using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mobet.Demo.ApiDocument.Controllers
{
    /// <summary>
    /// 商铺服务|提供商铺管理、查询服务
    /// </summary>
    public class ShopController : ApiController
    {
        public string Get(int id)
        {
            return "value";
        }
    }
}

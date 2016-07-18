using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mobet.Demo.ApiDocument.Controllers
{
    /// <summary>
    /// 产品服务|提供产品查询、管理服务
    /// </summary>
    public class ProductController : ApiController
    {
        public string Get(int id)
        {
            return "value";
        }
    }
}

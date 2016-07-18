using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mobet.Demo.ApiDocument.Controllers
{
    /// <summary>
    /// 商品类目|提供商品类目管理、查询服务
    /// </summary>
    public class ItemCategoryController : ApiController
    {
        public string Get(int id)
        {
            return "value";
        }
    }
}

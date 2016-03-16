using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.SoftwareDevelopmentKit.Core
{
    public class ApiPagingRequest : ApiBaseRequest
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public string OrderBy { get; set; }

        public bool OrderByDesc { get; set; }
    }
}

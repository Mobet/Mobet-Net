using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.SoftwareDevelopmentKit.Models;
using Mobet.SoftwareDevelopmentKit.Core;

namespace Mobet.SoftwareDevelopmentKit.UserAccount
{
    public class UserAccountGetPagingRequest : ApiPagingRequest
    {
        public string Name { get; set; }
    }
    public class UserAccountGetPagingResponse : IApiResponse
    {

        public int Total { get; set; }

        public IEnumerable<Models.UserAccount> Models { get; set; }
    }
}

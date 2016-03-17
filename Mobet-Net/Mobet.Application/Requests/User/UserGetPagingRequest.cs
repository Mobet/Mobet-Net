using Mobet.Domain.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Application.Requests.User
{

    public class UserGetPagingRequest : PagingRequest
    {
        [StringLength(50)]
        public string Name { get; set; }
    }

    public class UserGetPagingResponse : IResponse
    {
        public int Total { get; set; }
        public IEnumerable<Models.User> Models { get; set; }
    }
}

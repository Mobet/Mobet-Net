using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Services.Requests
{
    public class BooleanResponse
    {
        public BooleanResponse(bool result, string message)
        {
            this.Result = result;
            this.Message = message;
        }
        public bool Result { get; set; }

        public string Message { get; set; }
    }
}

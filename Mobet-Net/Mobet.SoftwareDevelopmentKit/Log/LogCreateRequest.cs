using Mobet.SoftwareDevelopmentKit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.SoftwareDevelopmentKit.Log
{
    public class LogCreateRequest : ApiBaseRequest
    {
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public string Route { get; set; }

        public int Duration { get; set; }

        public AuditLevel Level { get; set; }

        public string Message { get; set; }

        public string Stack { get; set; }

        public string User { get; set; }

        public string Client { get; set; }

        public string Host { get; set; }
    }

    public class LogCreateResponse : ApiBaseRequest
    {
        public bool Result { get; set; }

        public string Message { get; set; }
    }

    public enum AuditLevel
    {
        DEBUG = 1,
        INFO,
        WARNING,
        ERROR,
    } 
}

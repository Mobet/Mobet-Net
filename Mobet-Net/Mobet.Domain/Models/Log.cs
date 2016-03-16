using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.AutoMapper;

namespace Mobet.Domain.Models
{
    [AutoMap(typeof(SoftwareDevelopmentKit.Models.Log),typeof(SoftwareDevelopmentKit.Log.LogCreateRequest))]
    public class Log : IAggregateRoot
    {
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public string Route { get; set; }

        public int Duration { get; set; }

        public int Level { get; set; }

        public string Message { get; set; }

        public string Stack { get; set; }

        public string User { get; set; }

        public string Client { get; set; }

        public string Host { get; set; }
    }
}

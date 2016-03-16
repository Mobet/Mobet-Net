using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Domain;
using Mobet.Domain.Models;
using Mobet.AutoMapper;

namespace Mobet.Domain.Models
{
    [AutoMapTo(typeof(SoftwareDevelopmentKit.Models.UserAccount))]
    public class UserAccount : IAggregateRoot
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Account { get; set; }
    }
}

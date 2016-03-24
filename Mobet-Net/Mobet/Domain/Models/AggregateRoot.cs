using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Domain.Models
{
    public abstract class AggregateRoot : Entity
    {
        public virtual byte[] Version { get; set; }
    }

    public abstract class AggregateRoot<TPrimaryKey> : Entity<TPrimaryKey>
    {
        public virtual byte[] Version { get; set; }
    }
}

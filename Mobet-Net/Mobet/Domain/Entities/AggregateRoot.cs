using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Domain.Entities
{
    public abstract class AggregateRoot : Entity
    {
        [Timestamp]
        public virtual byte[] Version { get; set; }
    }

    public abstract class AggregateRoot<TPrimaryKey> : Entity<TPrimaryKey>
    {
        [Timestamp]
        public virtual byte[] Version { get; set; }
    }
}

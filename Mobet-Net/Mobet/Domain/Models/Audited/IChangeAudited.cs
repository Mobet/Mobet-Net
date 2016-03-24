using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Domain.Models.Audited
{
    public interface IChangeAudited
    {
        /// <summary>
        /// The  modify user for this entity.
        /// </summary>
        string ChangeAccount { get; set; }
        /// <summary>
        /// The last modified time for this entity.
        /// </summary>
        DateTime? ChangeTime { get; set; }
    }
}

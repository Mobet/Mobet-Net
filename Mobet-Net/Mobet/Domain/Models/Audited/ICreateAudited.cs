using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Domain.Models.Audited
{
    public interface ICreateAudited
    {
        /// <summary>
        /// Creator user of this entity.
        /// </summary>
        string CreateAccount { get; set; }
        /// <summary>
        /// Creation time of this entity.
        /// </summary>
        DateTime? CreateTime { get; set; }
    }
}

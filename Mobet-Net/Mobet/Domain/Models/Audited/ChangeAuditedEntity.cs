﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Domain.Models.Audited
{
    public class ChangeAuditedEntity : Entity, IChangeAudited
    {
        /// <summary>
        /// The  modify user for this entity.
        /// </summary>
        [MaxLength(50)]
        public string ChangeAccount { get; set; }
        /// <summary>
        /// The last modified time for this entity.
        /// </summary>
        public DateTime? ChangeTime { get; set; }
    }

    public class ChangeAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, IChangeAudited
    {
        /// <summary>
        /// The  modify user for this entity.
        /// </summary>
        [MaxLength(50)]
        public string ChangeAccount { get; set; }
        /// <summary>
        /// The last modified time for this entity.
        /// </summary>
        public DateTime? ChangeTime { get; set; }
    }
}

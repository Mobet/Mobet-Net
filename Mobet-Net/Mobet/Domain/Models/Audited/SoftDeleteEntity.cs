using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Domain.Models.Audited
{
    public class SoftDeleteEntity : Entity, ISoftDelete, IAudited
    {
        /// <summary>
        /// Creation time of this entity.
        /// </summary>
        public virtual DateTime? CreateTime { get; set; }
        /// <summary>
        /// Creator of this entity.
        /// </summary>
        [MaxLength(50)]
        public virtual string CreateAccount { get; set; }
        /// <summary>
        /// The  modify user for this entity.
        /// </summary>
        [MaxLength(50)]
        public virtual string ChangeAccount { get; set; }
        /// <summary>
        /// The last modified time for this entity.
        /// </summary>
        public virtual DateTime? ChangeTime { get; set; }
        /// <summary>
        /// Used to mark an Entity as 'Deleted'. 
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// The deletion user for this entity.
        /// </summary>
        [MaxLength(50)]
        public virtual string DeleteAccount { get; set; }

        /// <summary>
        /// The last deleted time for this entity.
        /// </summary>
        public virtual DateTime? DeleteTime { get; set; }
    }

    public class SoftDeleteEntity<TPrimaryKey> : Entity<TPrimaryKey>, ISoftDelete, IAudited
    {
        /// <summary>
        /// Creation time of this entity.
        /// </summary>
        public virtual DateTime? CreateTime { get; set; }
        /// <summary>
        /// Creator of this entity.
        /// </summary>
        [MaxLength(50)]
        public virtual string CreateAccount { get; set; }
        /// <summary>
        /// The  modify user for this entity.
        /// </summary>
        [MaxLength(50)]
        public virtual string ChangeAccount { get; set; }
        /// <summary>
        /// The last modified time for this entity.
        /// </summary>
        public virtual DateTime? ChangeTime { get; set; }
        /// <summary>
        /// Used to mark an Entity as 'Deleted'. 
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// The deletion user for this entity.
        /// </summary>
        [MaxLength(50)]
        public virtual string DeleteAccount { get; set; }

        /// <summary>
        /// The last deleted time for this entity.
        /// </summary>
        public virtual DateTime? DeleteTime { get; set; }
    }
}

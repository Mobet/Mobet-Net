using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Domain.Entities
{
    public partial class Role : IAggregateRoot, IPassivable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Role()
        {
            this.Users = new HashSet<User>();
            this.Menus = new HashSet<Menu>();
        }

        /// <summary>
        /// 主键
        /// </summary>
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        [StringLength(50)]
        public virtual string Name { get; set; }
        /// <summary>
        /// 角色代码
        /// </summary>
        [StringLength(50)]
        public virtual string Code { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(250)]
        public virtual string Description { get; set; }
        /// <summary>
        /// 数据版本
        /// </summary>
        public virtual byte[] Version { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual bool IsDeleted { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 拥有的菜单
        /// </summary>
        public virtual ICollection<Menu> Menus { get; set; }

        /// <summary>
        /// 拥有的用户
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
    }
}

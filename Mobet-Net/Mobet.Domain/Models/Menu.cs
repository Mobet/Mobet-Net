using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Domain.Entities
{
    public partial class Menu : IAggregateRoot, IPassivable
    {
        public Menu()
        {
            this.Roles = new HashSet<Role>();
        }
        /// <summary>
        /// 主键
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        [StringLength(50)]
        public virtual string Name { get; set; }
        /// <summary>
        /// 路由
        /// </summary>
        [StringLength(250)]
        public virtual string Url { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Sort { get; set; }
        /// <summary>
        /// 父级菜单
        /// </summary>
        public virtual int? ParentId { get; set; }
        /// <summary>
        /// 图标样式
        /// </summary>
        [StringLength(50)]
        public virtual string Icon { get; set; }
        /// <summary>
        /// 标签样式
        /// </summary>
        [StringLength(50)]
        public virtual string LabelCss { get; set; }
        /// <summary>
        /// 标签文字
        /// </summary>
        [StringLength(50)]
        public virtual string LabelText { get; set; }
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
        /// 拥有的角色
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }
    }
}

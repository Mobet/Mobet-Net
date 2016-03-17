using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mobet.Domain.Models
{
    public class User : IAggregateRoot, IAuditable, ISoftDelete
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public User()
        {
            this.Roles = new HashSet<Role>();
        }
        /// <summary>
        /// 主键
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        /// <summary>
        /// 第三方登录提供程序Open Connect ID
        /// </summary>
        [StringLength(50)]
        public virtual string OpenId { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [StringLength(50)]
        public virtual string Name { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [StringLength(50)]
        public virtual string NickName { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 移动号码
        /// </summary>
        [StringLength(50)]
        public virtual string Mobilephone { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        [StringLength(50)]
        public virtual string Telphone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(50)]
        public virtual string Email { get; set; }
        /// <summary>
        /// 街道地址
        /// </summary>
        [StringLength(250)]
        public virtual string Street { get; set; }
        /// <summary>
        /// 所在城市
        /// </summary>
        [StringLength(50)]
        public virtual string City { get; set; }
        /// <summary>
        /// 所在省
        /// </summary>
        [StringLength(50)]
        public virtual string Province { get; set; }
        /// <summary>
        /// 所在国家
        /// </summary>
        [StringLength(50)]
        public virtual string Country { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual byte? Sex { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        [StringLength(2000)]
        public virtual string Headimageurl { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [StringLength(50)]
        public virtual string IdentityNo { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(250)]
        public virtual string Password { get; set; }
        /// <summary>
        /// 加密盐
        /// </summary>
        [StringLength(50)]
        public virtual string Salt { get; set; }

        /// <summary>
        /// 数据版本
        /// </summary>
        public virtual byte[] Version { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        [StringLength(50)]
        public virtual string CreateAccount { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? CreateTime { get; set; }
        /// <summary>
        /// 更改用户
        /// </summary>
        [StringLength(50)]
        public virtual string ChangeAccount { get; set; }
        /// <summary>
        /// 更改时间
        /// </summary>
        public virtual DateTime? ChangeTime { get; set; }
        /// <summary>
        /// 是否删除 
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 删除用户
        /// </summary>
        public virtual string DeleteAccount { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public virtual DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 拥有的角色
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.AutoMapper;

namespace Mobet.Application.Models
{
    [AutoMapTo(typeof(Domain.Models.User))]
    public class User
    {
        /// <summary>
        /// 第三方登录提供程序Open Connect ID
        /// </summary>
        public virtual string OpenId { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public virtual string NickName { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 移动号码
        /// </summary>
        public virtual string Mobilephone { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public virtual string Telphone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public virtual string Email { get; set; }
        /// <summary>
        /// 街道地址
        /// </summary>
        public virtual string Street { get; set; }
        /// <summary>
        /// 所在城市
        /// </summary>
        public virtual string City { get; set; }
        /// <summary>
        /// 所在省
        /// </summary>
        public virtual string Province { get; set; }
        /// <summary>
        /// 所在国家
        /// </summary>
        public virtual string Country { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual byte? Sex { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public virtual string Headimageurl { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public virtual string IdentityNo { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? CreateTime { get; set; }
    }
}

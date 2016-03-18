using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Domain.Services;
using System.ComponentModel.DataAnnotations;
using Mobet.AutoMapper;

namespace Mobet.Services.Requests.User
{
    [AutoMap(typeof(Domain.Models.User))]
    public class UserCreateRequest : IRequest
    {
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
    }

    public class UserCreateResponse : IResponse
    {
        public bool Result { get; set; }

        public string Message { get; set; }

    }
}

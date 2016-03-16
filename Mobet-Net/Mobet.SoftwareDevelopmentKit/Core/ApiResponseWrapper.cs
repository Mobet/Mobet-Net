using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Mobet.SoftwareDevelopmentKit.Core
{
    public sealed class ApiResponseWrapper
    {
        /// <summary>
        /// 业务实体内容
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 响应状态
        /// </summary>
        public string HttpStatus { get; set; }
        /// <summary>
        /// 消耗时间
        /// </summary>
        public string ElapseTime { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 响应内容包装
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static ApiResponseWrapper ActionResponse<T>(T input, string elapse) where T : class
        {
            JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();

            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            JsonSerializerSettings.Converters.Add(timeConverter);

            return new ApiResponseWrapper
            {
                Body = JsonConvert.SerializeObject(input, Formatting.None, JsonSerializerSettings),
                ElapseTime = elapse,
                HttpStatus = HttpStatusCode.OK.ToString()
            };
        }
    }
}

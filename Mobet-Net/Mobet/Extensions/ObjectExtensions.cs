using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Object"/>.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 转换为short
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <returns></returns>
        public static short TryShort(this object value)
        {
            return TryShort(value, short.MinValue);
        }
        /// <summary>
        /// 转换为short
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static short TryShort(this object value, short defValue)
        {
            short result = 0;
            return short.TryParse(value + "", out result) ? result : defValue;
        }
        /// <summary>
        /// 转换为short
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static short? TryShort(this object value, short? defValue)
        {
            short result = 0;
            return short.TryParse(value + "", out result) ? result : defValue;
        }
        /// <summary>
        /// 转换为Int，默认值：int.MinValue
        /// </summary>
        public static int TryInt(this object value)
        {
            return TryInt(value, int.MinValue);
        }
        /// <summary>
        /// 转换为Int
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static int TryInt(this object value, int defValue)
        {
            int temp = int.MinValue;
            return int.TryParse(value + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换为Int
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static int? TryInt(this object value, int? defValue)
        {
            int temp = int.MinValue;
            return int.TryParse(value + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换为Double，默认值：double.MinValue
        /// </summary>
        public static double TryDouble(this object value)
        {
            return TryDouble(value, double.MinValue);
        }
        /// <summary>
        /// 转换为Double
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static double TryDouble(this object value, double defValue)
        {
            double temp = double.MinValue;
            return double.TryParse(value + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换为Double
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static double? TryDouble(this object value, double? defValue)
        {
            double temp = double.MinValue;
            return double.TryParse(value + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换为Decimal，默认值：decimal.MinValue
        /// </summary>
        public static decimal TryDecimal(this object value)
        {
            return TryDecimal(value, decimal.MinValue);
        }
        /// <summary>
        /// 转换为Decimal
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static decimal TryDecimal(this object value, decimal defValue)
        {
            decimal temp = decimal.MinValue;
            return decimal.TryParse(value + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换为Decimal
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static decimal? TryDecimal(this object value, decimal? defValue)
        {
            decimal temp = decimal.MinValue;
            return decimal.TryParse(value + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换为long，默认值：long.MinValue
        /// </summary>
        public static long TryLong(this object value)
        {
            return TryLong(value, long.MinValue);
        }
        /// <summary>
        /// 转换为long
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static long TryLong(this object value, long defValue)
        {
            long temp = long.MinValue;
            return long.TryParse(value + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换为long
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static long? TryLong(this object value, long? defValue)
        {
            long temp = long.MinValue;
            return long.TryParse(value + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换为Boolean，默认值：false
        /// </summary>
        public static bool TryBool(this object value)
        {
            return TryBool(value, false);
        }
        /// <summary>
        /// 转换为Boolean
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static bool TryBool(this object value, bool defValue)
        {
            bool temp = false;
            return bool.TryParse(value + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换为Boolean
        /// </summary>
        /// <param name="value">输入的内容</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static bool? TryBool(this object value, bool? defValue)
        {
            bool temp = false;
            return bool.TryParse(value + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换byte[]
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] TryByte(this object s)
        {
            try
            {
                return Encoding.UTF8.GetBytes(s.TryString());
            }
            catch (Exception)
            {
                byte[] temp = null;
                return temp;
            }

        }
        /// <summary>
        /// 转换为Guid，默认值Guid.Empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid TryGuid(this object guid)
        {
            return TryGuid(guid, Guid.Empty);
        }
        /// <summary>
        /// 转换为Guid
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid TryGuid(this object guid, Guid defvalue)
        {
            return guid == null ? defvalue : Guid.Parse(guid.ToString());
        }
        /// <summary>
        /// 转换为String，默认值String.Empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TryString(this object s)
        {
            return TryString(s, "");
        }
        /// <summary>
        /// 转换为String
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TryString(this object s, string defvalue)
        {
            return s == null ? defvalue : s.ToString();
        }
        /// <summary>
        /// 转换为dynamic，主要是匿名对象
        /// </summary>
        public static dynamic TryDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();
            Type type = value.GetType();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
            foreach (PropertyDescriptor property in properties)
            {
                var val = property.GetValue(value);
                if (property.PropertyType.FullName.StartsWith("<>f__AnonymousType"))
                {
                    dynamic dval = val.TryDynamic();
                    expando.Add(property.Name, dval);
                }
                else
                {
                    expando.Add(property.Name, val);
                }
            }
            return expando as ExpandoObject;
        }
        /// <summary>
        /// 转换成枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T TryEmun<T>(this string s)
        {
            return (T)(Enum.Parse(typeof(T), s));
        }

        /// <summary>
        /// Converts given object to JSON string.
        /// </summary>
        /// <returns></returns>
        public static string ToJsonString(this object obj, bool camelCase = false, bool indented = false)
        {
            var options = new JsonSerializerSettings();

            if (camelCase)
            {
                options.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            if (indented)
            {
                options.Formatting = Formatting.Indented;
            }

            return JsonConvert.SerializeObject(obj, options);
        }
    }
}

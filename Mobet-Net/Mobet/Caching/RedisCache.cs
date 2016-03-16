using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;
using Mobet.Logging;

namespace Mobet.Caching
{
    public class RedisCache : ICache
    {
        private IDatabase client;
        private ConnectionMultiplexer Connection;
        public RedisCache(string configuration)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(configuration))
                {
                    throw new ArgumentNullException("Configuration can't be null.");
                }
                Connection = ConnectionMultiplexer.Connect(configuration);
                client = Connection.GetDatabase();
            }
            catch (Exception e)
            {
                LogHelper.Logger.ErrorFormat("Redis connection fail：" + e.Message);
            }
        }

        public object Get(string key)
        {
            return client.StringGet(key);
        }

        public T Get<T>(string key, Func<T> invoker = null)
        {
            if (client.KeyExists(key))
            {
                return JsonConvert.DeserializeObject<T>(client.StringGet(key));
            }
            return default(T);
        }

        public IDictionary<string, object> MultiGet(IEnumerable<string> keys)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            foreach (var key in keys)
            {
                var obj = Get(key);

                if (obj != null)
                {
                    dict.Add(key, obj);
                }
            }
            return dict;
        }

        public IEnumerable<T> MultiGet<T>(IEnumerable<string> keys)
        {
            return MultiGet(keys).Select(t => (T)t.Value);
        }

        public IEnumerable<T> MultiGet<T>(IEnumerable<string> keys, Func<IEnumerable<string>, IEnumerable<T>> invoker)
        {
            IDictionary<string, object> dict = MultiGet(keys.Distinct());
            IEnumerable<T> hitedT = dict.Select(t => (T)t.Value);

            int keyCount = keys.Count();
            int hitedTCount = hitedT.Count();

            if (keyCount == hitedTCount)
            {
                return hitedT;
            }
            else
            {
                IEnumerable<string> hitedKeys = dict.Select(t => t.Key);
                IEnumerable<string> missedKeys = keys.Where(t => !hitedKeys.Contains(t));

                if (missedKeys.Count() == 0)
                {
                    return hitedT;
                }
                else
                {
                    return hitedT.Concat(invoker(missedKeys));
                }
            }
        }

        public void Set(string key, object value)
        {
            client.StringSet(key, JsonConvert.SerializeObject(value));
        }

        public void Set(string key, object value, DateTime invalidatedTime)
        {
            client.StringSet(key, JsonConvert.SerializeObject(value), invalidatedTime - DateTime.Now);
        }

        public void Set(string key, object value, TimeSpan invalidatedSpan)
        {
            client.StringSet(key, JsonConvert.SerializeObject(value), invalidatedSpan);
        }

        public T Modify<T>(string key, Func<T, T> invoker)
        {
            if (key.Length == 0)
            {
                return default(T);
            }

            lock (key)
            {
                var get = Get(key);

                if (get == null)
                {
                    return default(T);
                }

                T value = invoker((T)get);

                Set(key, value);
                return value;
            }
        }

        public T Modify<T>(string key, Func<T, T> invoker, DateTime expireAt)
        {
            if (key.Length == 0)
            {
                return default(T);
            }

            lock (key)
            {
                var get = Get(key);

                if (get == null)
                {
                    return default(T);
                }

                T value = invoker((T)get);

                Set(key, value, expireAt);
                return value;
            }
        }

        public T Modify<T>(string key, Func<T, T> invoker, TimeSpan validFor)
        {
            if (key.Length == 0)
            {
                return default(T);
            }

            lock (key)
            {
                var get = Get(key);

                if (get == null)
                {
                    return default(T);
                }

                T value = invoker((T)get);

                Set(key, value, validFor);
                return value;
            }
        }

        public T Retrive<T>(string key, Func<T> invoker)
        {
            if (client.KeyExists(key))
            {
                return Get<T>(key);
            }
            else
            {
                T obj = invoker();
                Set(key, obj);
                return obj;
            }
        }

        public T Retrive<T>(string key, Func<T> invoker, DateTime invalidatedTime)
        {
            if (client.KeyExists(key))
            {
                return Get<T>(key);
            }
            else
            {
                T obj = invoker();
                Set(key, obj, invalidatedTime);
                return obj;
            }
        }

        public T Retrive<T>(string key, Func<T> invoker, TimeSpan invalidatedSpan)
        {
            if (client.KeyExists(key))
            {
                return Get<T>(key);
            }
            else
            {
                T obj = invoker();
                Set(key, obj, invalidatedSpan);
                return obj;
            }
        }

        public T Lengthen<T>(string key, Func<T, Tuple<T, bool>> lengthenInvoker, Func<T> initInvoker, DateTime expireAt)
        {
            throw new NotImplementedException();
        }

        public T Lengthen<T>(string key, Func<T, Tuple<T, bool>> lengthenInvoker, Func<T> initInvoker, TimeSpan validFor)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            client.KeyDelete(key);
        }

        public void FlushAll()
        {
            throw new NotImplementedException();
        }

        public void Increment(string key, int delta)
        {
            throw new NotImplementedException();
        }

        public void Increment(string key, int delta, DateTime expiresAt)
        {
            throw new NotImplementedException();
        }

        public void Increment(string key, int delta, TimeSpan validFor)
        {
            throw new NotImplementedException();
        }

        public void Increment(string key, int defaultValue, int delta)
        {
            throw new NotImplementedException();
        }

        public void Increment(string key, int defaultValue, int delta, DateTime expiresAt)
        {
            throw new NotImplementedException();
        }

        public void Increment(string key, int defaultValue, int delta, TimeSpan validFor)
        {
            throw new NotImplementedException();
        }

        public void Decrement(string key, int delta)
        {
            throw new NotImplementedException();
        }

        public void Decrement(string key, int delta, DateTime expiresAt)
        {
            throw new NotImplementedException();
        }

        public void Decrement(string key, int delta, TimeSpan validFor)
        {
            throw new NotImplementedException();
        }

        public void Decrement(string key, int defaultValue, int delta)
        {
            throw new NotImplementedException();
        }

        public void Decrement(string key, int defaultValue, int delta, DateTime expiresAt)
        {
            throw new NotImplementedException();
        }

        public void Decrement(string key, int defaultValue, int delta, TimeSpan validFor)
        {
            throw new NotImplementedException();
        }
    }
}

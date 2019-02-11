using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace IBSANBR.Extensions
{
    public static class SessionExtensions
    {
        public static void SetAsJson<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
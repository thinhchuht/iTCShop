using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Runtime.CompilerServices;

namespace iTCShop.Extensions
{
    public static class TempDataConverter
    {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value)
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>( this ITempDataDictionary tempData, string key) where T : class
        {
            tempData.TryGetValue(key, out var o);
            return o == null ? default : JsonConvert.DeserializeObject<T>((string)o);
        }

        public static T Peek<T>(this ITempDataDictionary tempData, string key)
        {
            var value = tempData.Peek(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>((string)value);
        }

        public static void PutResponse(this ITempDataDictionary tempData, ResponseModel rs)
        {
            tempData.Put("response", rs);
        }
    }
}

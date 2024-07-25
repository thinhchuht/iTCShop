using System.Text.Json;

namespace iTCShop.Extensions
{
    public static class SessionValueConverter
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
           
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        // Truy xuất đối tượng từ session
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}

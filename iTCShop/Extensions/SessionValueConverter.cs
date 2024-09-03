namespace iTCShop.Extensions
{
    public static class SessionValueConverter
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {

            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }

        public static Admin GetAdmin(this ISession session)
        {
           return session.GetObjectFromJson<Admin>("admin");
        }

        public static Customer GetCustomer(this ISession session)
        {
           return session.GetObjectFromJson<Customer>("user");
       
        }
    }
}

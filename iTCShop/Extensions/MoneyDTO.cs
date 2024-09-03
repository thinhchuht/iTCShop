namespace iTCShop.Extensions
{
    public static class MoneyDTO
    {
        public static string Convert(decimal? money) 
        {
            money ??= 0m;
            return money?.ToString("C0", new CultureInfo("vi-VN"));
        }
    }
}

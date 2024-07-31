namespace iTCShop.Extensions
{
    public static class MoneyDTO
    {
        public static string Convert(decimal? money) 
        {
            if (money == null) money = 0m;
            return money?.ToString("C0", new CultureInfo("vi-VN"));
        }
    }
}

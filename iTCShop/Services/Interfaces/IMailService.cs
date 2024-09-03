namespace iTCShop.Services.Interfaces
{
    public interface IMailService
    {
        void SendOrderCompletionEmail(Order order, Customer customer);
    }
}

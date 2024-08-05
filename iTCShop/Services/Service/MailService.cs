using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace iTCShop.Services.Service
{
    public class MailService : IMailService
    {
        public void SendOrderCompletionEmail(Order order, Customer customer)
        {

            var message = CreateMailMessage(order, customer);
                using (var client = new SmtpClient())
                {
                    client.CheckCertificateRevocation = false;
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.None);
                    client.Send(message);
                    client.Disconnect(true);
                };
        }

        private MimeMessage CreateMailMessage(Order order, Customer customer)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("iTCShop", "thinhld2249@gmail.com"));
            message.To.Add(new MailboxAddress(customer.Name, customer.Email));
            message.Subject = "Your Order is Completed";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = GenerateOrderCompletionHtml(order)
            };
            message.Body = bodyBuilder.ToMessageBody();

            return message;
        }

        private string GenerateOrderCompletionHtml(Order order)
        {
            string orderDetailsHtml = "";
            foreach (var detail in order.OrderDetails)
            {
                orderDetailsHtml += $@"
            <tr>
                <td>{detail.ProductID}</td>
                <td>{detail.Quantity}</td>
                <td>{detail.Price}Đ</td>
                <td>{detail.TotalAmount}Đ</td>
            </tr>";
            }

            return $@"
        <html>
        <body>
            <h1>Thank you for your purchase!</h1>
            <p>Your order with ID {order.ID} has been completed.</p>
            <p>Order Date: {order.OrderDate.ToString("MM/dd/yyyy")}</p>
            <p>Shipping Address: {order.ShipAddress}</p>
            <p>Total Pay: {order.TotalPay:C}</p>
            <h2>Order Details:</h2>
            <table border='1'>
                <thead>
                    <tr>
                        <th>Product ID</th>
                        <th>Quantity</th>
                        <th>Price</th>
                        <th>Total Amount</th>
                    </tr>
                </thead>
                <tbody>
                    {orderDetailsHtml}
                </tbody>
            </table>
        </body>
        </html>";
        }
    }

   
}
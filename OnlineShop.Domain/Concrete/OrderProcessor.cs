using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Mvc;
using OnlineShop.Domain.Abstract;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "sklep@cezaryluksza.pl";
        public string MailFromAddress = "sklep@cezaryluksza.pl";
        public bool UseSsl = true;
        public string Username = "cezaryluksza";
        public string Password = "1234";
        public string ServerName = "smtp.cezaryluksza.pl";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"c:\online_shop_emails";
    }

    public class OrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public OrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }


        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo, string userId = null)
        {
            SaveToDatabase(cart, shippingInfo, userId);
            SendEmail(cart, shippingInfo);
        }

        public void SendEmail(Cart cart, ShippingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                = new NetworkCredential(emailSettings.Username,
                emailSettings.Password);
                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod
                    = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }
                StringBuilder body = new StringBuilder()
                    .AppendLine("<html><head></head><body>")
                    .AppendLine("Nowe zamówienie")
                .AppendLine("---")
                .AppendLine("Produkty:");
                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (subtotal: {2:c}", line.Quantity, line.Product.Name, subtotal);
                }

                body.AppendFormat("Wartość całkowita: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Wysyłka dla:")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine(shippingInfo.Line1)
                    .AppendLine(shippingInfo.Line2 ?? "")
                    .AppendLine(shippingInfo.Line3 ?? "")
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.State ?? "")
                    .AppendLine(shippingInfo.Country)
                    .AppendLine(shippingInfo.Zip)
                    .AppendLine("---")
                    .AppendFormat("Pakowanie prezentu: {0}",
                        shippingInfo.GiftWrap ? "Tak" : "Nie")
                    .AppendLine()
                    .AppendLine("<img src='~/Content/Images/LogoMobile.png'>")
                    .AppendLine("</body></html>");
                MailMessage mailMessage = new MailMessage(
                emailSettings.MailFromAddress, // od
                emailSettings.MailToAddress, // do
                "Otrzymano nowe zamówienie!", // temat
                body.ToString()); // treść

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }
                smtpClient.Send(mailMessage);
            }
        }

        public void SaveToDatabase(Cart cart, ShippingDetails shippingInfo, string userId)
        {
            using (EFDbContext context = new EFDbContext())
            {
                ApplicationUser currentUser = null;
                if (userId != null)
                {
                    currentUser = context.Users.FirstOrDefault(x => x.Id == userId);
                }

                var order = new Order();
                order.ShippingDetails = shippingInfo;
                order.User = currentUser;
                context.Orders.Add(order);

                context.SaveChanges();

                foreach (var line in cart.Lines)
                {

                    var cartLine = new CartLine();
                    cartLine.Product = line.Product;
                    cartLine.Quantity = line.Quantity;
                    context.Cartlines.Add(cartLine);

                    var orderByCartLines = new OrderByCartLines();
                    orderByCartLines.CartLine = cartLine;
                    orderByCartLines.OrderId = order.OrderId;
                    context.OrderByCartLines.Add(orderByCartLines);
                }

                context.SaveChanges();
            }
        }
    }
}
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
        private readonly EmailSettings _emailSettings;

        public OrderProcessor(EmailSettings settings)
        {
            _emailSettings = settings;
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
                smtpClient.EnableSsl = _emailSettings.UseSsl;
                smtpClient.Host = _emailSettings.ServerName;
                smtpClient.Port = _emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_emailSettings.Username,
                _emailSettings.Password);
                if (_emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = _emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }
                
                StringBuilder body = new StringBuilder()
                    .AppendLine("<html><head></head><body>")
                    .AppendLine("Nowe zamówienie")
                .AppendLine("<br/>---<br/>")
                .AppendLine("Produkty:<br/>");
                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("{0} x {1}", line.Quantity, line.Product.Name);
                    body.AppendLine("<br/>");
                }
                body.AppendFormat("<br/>Wartość całkowita: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("<br/>---<br/>")
                    .AppendLine("Wysyłka dla:<br />")
                    .AppendLine(shippingInfo.Name + "<br/>")
                    .AppendLine(shippingInfo.Line1 + " ")
                    .AppendLine(shippingInfo.Line2 + " ")
                    .AppendLine(shippingInfo.Line3 + "<br />")
                    .AppendLine(shippingInfo.Zip + "<br />")
                    .AppendLine(shippingInfo.City + "<br />")
                    .AppendLine(shippingInfo.State + "<br />")
                    .AppendLine(shippingInfo.Country)
                    .AppendLine("<br />---<br />")
                    .AppendFormat("Pakowanie prezentu: {0}",
                        shippingInfo.GiftWrap ? "Tak <br />" : "Nie <br />")
                    .AppendLine()
                    .AppendLine("<br /><br /><img src='https://i.imgur.com/rtnePfb.png'>")
                    .AppendLine("</body></html>");
                MailMessage mailMessage = new MailMessage(_emailSettings.MailFromAddress, _emailSettings.MailToAddress,
                    "Otrzymano nowe zamówienie!", body.ToString());

                mailMessage.IsBodyHtml = true;
                if (_emailSettings.WriteAsFile)
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
                    cartLine.OrderId = order.OrderId;
                    context.CartLines.Add(cartLine);
                }

                context.SaveChanges();
            }
        }
    }
}
﻿using System.Net;
using System.Net.Mail;
using System.Text;
using OnlineShop.Domain.Abstract;
using OnlineShop.Domain.Entities;
namespace OnlineShop.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "orders@example.com";
        public string MailFromAddress = "onlineshop@example.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"c:\online_shop_emails";
    }
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;
        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
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
                .AppendLine("Nowe zamówienie")
                .AppendLine("---")
                .AppendLine("Produkty:");
                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (subtotal: {2:c}", line.Quantity,
                    line.Product.Name,
                    subtotal);
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
                shippingInfo.GiftWrap ? "Tak" : "Nie");
                MailMessage mailMessage = new MailMessage(
                emailSettings.MailFromAddress, // od
                emailSettings.MailToAddress, // do
                "Otrzymano nowe zamówienie!", // temat
                body.ToString()); // treść

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }
                smtpClient.Send(mailMessage);
            }
        }
    }
}
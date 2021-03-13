using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using ReserbizAPP.LIB.Helpers.Class;

namespace ReserbizAPP.LIB.Helpers.Services
{
    public class EmailService
    {
        private readonly string _smtp;
        private readonly string _smtpServerPassword;
        private readonly string _smtpServerAddress;

        public EmailService(string smtp, string smtpServerAddress, string smtpServerPassword)
        {
            _smtpServerAddress = smtpServerAddress;
            _smtpServerPassword = smtpServerPassword;
            _smtp = smtp;
        }

        public void Send(string senderEmail, string receiverEmail, string subject, string htmlBody)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                MailAddress fromAddress = new MailAddress(senderEmail);
                mail.From = fromAddress;
                mail.To.Add(receiverEmail);
                mail.Subject = subject;
                mail.Body = htmlBody;
                SmtpClient smtpClient = new SmtpClient(_smtp);
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new NetworkCredential(_smtpServerAddress, _smtpServerPassword);

                smtpClient.Send(mail);
                mail.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
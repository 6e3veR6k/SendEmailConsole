using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;

namespace SendEmailConsole
{
    public class OrantaEx
    {
        public OrantaEx(string userLogin, string password)
        {
            UserLogin = userLogin;
            Password = password;
        }

        public string Password { private get; set; }

        public string UserLogin { get; set; }

        private SmtpClient ClientCreator()
        {
            var client = new SmtpClient();
            client.EnableSsl = true;
            client.Host = Properties.Settings.Default.SmtpHost;
            client.Port = Int32.Parse(Properties.Settings.Default.SmtpPort);
            client.Timeout = 60000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.DeliveryFormat = SmtpDeliveryFormat.International;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(UserLogin, Password, Properties.Settings.Default.Domen);
            return client;
        }

        public void Send(string from, string to, string subject, string body)
        {
            this.Send(from, new List<string> {to}, subject, body);
        }

        public void Send(string from, IList to, string subject, string body)
        {
            MailMessage msg = GetMailMessage(from, to, subject, body);

            var client = ClientCreator();
            try
            {
                client.Send(msg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                ((IDisposable)client)?.Dispose();
                ((IDisposable)msg)?.Dispose();
            }
        }

        private static MailMessage GetMailMessage(string from, IList to, string subject, string body)
        {
            var msg = new MailMessage();

            msg.From = new MailAddress(from);

            foreach (string reciver in to)
                msg.To.Add(new MailAddress(reciver));

            msg.SubjectEncoding = Encoding.UTF8;
            msg.BodyEncoding = Encoding.UTF8;

            msg.Subject = subject;
            msg.Body = body;

            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            return msg;
        }
    }
}
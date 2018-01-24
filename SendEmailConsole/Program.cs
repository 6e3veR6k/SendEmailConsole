using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                return; 
            }

            string user = Properties.Settings.Default.UserName;
            string password = Properties.Settings.Default.Password;

            var ornt = new OrantaEx(user, password);

            

            string from = Properties.Settings.Default.SendFrom;

            IList to = Properties.Settings.Default.SendTo1;

            string sbj = "Jupiter maintenance";

            var body = new StringBuilder();
            body.Append($"{String.Join(" ", args)} {DateTime.Now}");

            ornt.Send(from, to, sbj, body.ToString());
            Console.WriteLine($"{body.ToString()} is sent to {String.Join(" ", to)}");

        }
        
    }
}

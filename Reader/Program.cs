
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using System;
using System.Net.Mail;

namespace Reader
{
    class Program
    {
        static void Main(string[] args)
        {

            var client = new ImapClient();

            client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
            client.Authenticate("mail adresi", "şifre");

            client.Inbox.Open(FolderAccess.ReadOnly);
            var results = client.Inbox.Search(SearchOptions.All, SearchQuery.NotSeen);

            foreach (var uniqueId in results.UniqueIds)
            {
                var mail = client.Inbox.GetMessage(uniqueId);

                Console.WriteLine("MESSAGE ID: " + mail.MessageId);
                Console.WriteLine("Date: " + mail.Date);
                Console.WriteLine("From: " + mail.From);
                Console.WriteLine("Subject: " + mail.Subject);
                Console.WriteLine("Content: " + mail.TextBody);
                Console.WriteLine("----------------------------------------------");
            }

            client.Disconnect(true);

        }
    }
}
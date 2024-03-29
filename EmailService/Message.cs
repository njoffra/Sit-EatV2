﻿using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EmailService
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            foreach (var emailAddress in to)
            {
                To.Add(new MailboxAddress(emailAddress, emailAddress));
            }
            Content = content;
            Subject = subject;
        }    
    }
}

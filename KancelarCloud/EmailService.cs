using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Net.Pop3;
using KancelarCloud.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace KancelarCloud
{
    public class EmailService
    {
        public async Task SendEmailAsync(InternetAddressList internetAddressList, string subject, string message, string path, string FileName, FileStream dest)
        {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("Центр информационных технологий", "esed@cit-nnov.ru"));
                emailMessage.To.AddRange(internetAddressList);
                emailMessage.Subject = subject;
                var builder = new BodyBuilder();
                builder.TextBody = message;
         
            if (FileName == null)
            {
                emailMessage.Body = builder.ToMessageBody();
            }

            else
            {
                if (path.Contains(".pdf") == true)
                {
                    ContentType contentType = new ContentType("application", "pdf");
                    builder.Attachments.Add(FileName, dest, contentType);
                }
                else
                {
                    builder.Attachments.Add(FileName, dest);
                }
                emailMessage.Body = builder.ToMessageBody();

            
            }

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("213.177.118.157", 25, false);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);

            }


        }



    




    }
    }

        

        
    


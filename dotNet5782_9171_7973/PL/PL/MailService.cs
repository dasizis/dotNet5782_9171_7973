using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.IO;
using System.Net.Mime;

namespace PL
{

    static public class MailService
    {
        static string HtmlMail(string message, int parcelId) => $@"
        <html>
            <body dir=""ltr"">
            <div style=""background: #e7e7e7;text-align: center;"">
                <div style=""background: white; margin: auto; width: 500px;"">
                    <div style>
                        <img src=""https://img.icons8.com/material/344/center-direction.png"" height=""50"" style=""margin-top: 30px"" />
                        <h1 style=""color: #353535; margin-top: 0px;"">Target</h1>
                    </div>
        
                    <p style=""margin: 20px; font-size: 16px; font-family: sans-serif;"">
                        {message} <span style=""color: gray"">(parcel #{parcelId})</span>
                    </p>
        
                    <img src=""https://images.unsplash.com/photo-1532989029401-439615f3d4b4?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8OXx8ZHJvbmV8ZW58MHx8MHx8&auto=format&fit=crop&w=500&q=60""/>
                </div>
            </div>
            </body>
            </html>";

        static string SenderBody => $"Your package succesfully picked up!";
        static string TargetBody => $"Drone has supplied a parcel to you!"; 

        const string COMPANY_MAIL = "dronesCompany1000@gmail.com";
        const string COMPANY_NAME = "Target";
        const string COMPANY_PASSWORD = "drones.Company1234";
        const string SENDER_SUBJECT = "Drone picked your parcel";
        const string TARGET_SUBJECT = "Drone supplied a parcel to you";

        static public void Send(PO.Parcel parcel)
        {
            if (parcel.PickedUp == null) return;

            bool isSender = true;
            PO.Customer customer = PLService.GetCustomer(parcel.Sender.Id);

            if (parcel.Supplied != null)
            {
                customer = PLService.GetCustomer(parcel.Target.Id);
                isSender = false;
            }

            var fromAddress = new MailAddress(COMPANY_MAIL, COMPANY_NAME);
            var toAddress = new MailAddress(customer.Mail, customer.Name);
            const string fromPassword = COMPANY_PASSWORD;
            string subject = isSender ? SENDER_SUBJECT : TARGET_SUBJECT;
            string body = isSender ? SenderBody : TargetBody;

            var mail = new MailMessage()
            {
                Subject = subject,
                Body = HtmlMail(SenderBody, parcel.Id),
                From = fromAddress,
                IsBodyHtml = true,
            };

            mail.To.Add(toAddress);

            SmtpClient smtp = smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 25,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
            };
            Task.Factory.StartNew(() => smtp.Send(mail));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.IO;

namespace PL
{

    static public class MailService
    {
        static string SenderBody(int parcelId) => "<h1 style=\"color: red\">This is the header</h1>";//$"Drone has just picked up your parcel (parcel #{parcelId}.";
        static string TargetBody(int parcelId) => "<h1 style=\"color: red\">This is the header</h1>";//$"Drone has supplied a parcel to you (parcel #{parcelId}."; 

        const string COMPANY_MAIL = "dronesCompany1000@gmail.com";
        const string COMPANY_NAME = "Drones Company";
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
            string body = isSender ? SenderBody(parcel.Id) : TargetBody(parcel.Id);

            var mail = new MailMessage()
            {
                Subject = subject,
                Body = body,
                From = fromAddress,
                IsBodyHtml = true,
            };

            mail.To.Add(toAddress);
            mail.Attachments.Add(new Attachment($"{Directory.GetCurrentDirectory()}../../../../../../screen-shots/drone.gif"));
            
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

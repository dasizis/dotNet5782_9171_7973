using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace PL
{
    static public class MailService
    {
        const string COMPANY_MAIL = "dronesCompany1000@gmail.com";
        const string COMPANY_NAME = "Drones Company";
        const string COMPANY_PASSWORD = "drones.Company1234";
        const string SENDER_SUBJECT = "Drone picked your parcel";
        const string SENDER_BODY = "Drone has just picked up your parcel.";
        const string TARGET_SUBJECT = "Drone supplied a parcel to you";
        const string TARGET_BODY = "Drone has supplied a parcel to you.";

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
            var toAddress = new MailAddress("yaeldoch@gmail.com", customer.Name);
            const string fromPassword = COMPANY_PASSWORD;
            string subject = isSender? SENDER_SUBJECT : TARGET_SUBJECT;
            string body = isSender ? SENDER_BODY : TARGET_BODY;

            try
            {
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                };
            }
            catch (Exception ef)
            {
                MessageBox.Show(MessageBox.BoxType.Error, ef.Message);
            }
        }
    }
}

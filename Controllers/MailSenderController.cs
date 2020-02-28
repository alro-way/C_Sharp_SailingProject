using System.Net;
using System.Net.Mail;
// using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;
public class MailSenderController : Controller
{
    public ActionResult Index()
    {
        return View("Index");
    }
    // public string SendEmail(string Name, string Email, string Message){
        public string SendEmail(string Name, string Message){
        try
        {
            // Credentials
            //FROM
            var credentials = new NetworkCredential("YOUR_EMAIL", "YOUR_PASSWORD");
            // Mail message
            var mail = new MailMessage()
            {
                From = new MailAddress("YOUR_EMAIL"),
                Subject = "Email Sender App",
                // 
                // Body = $"From: {Name} Message: {Message}"
                Body = "FROM: "+Name+"       MESSAGE: "+Message

            };
          

            mail.IsBodyHtml = true;
            mail.To.Add(new MailAddress("YOUR_EMAIL"));
            // mail.To.Add(new MailAddress(Email));
            // Smtp client
            var client = new SmtpClient()
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Credentials = credentials
            };
            client.Send(mail);
              System.Console.Write("------------------NAME-----");
            System.Console.Write(Name);

            System.Console.Write("------------------NAME-----");

            System.Console.Write(Message);
            return "Email Sent Successfully!";
        }
        catch (System.Exception e)
        {
            return e.Message;
        }
        
    }
}
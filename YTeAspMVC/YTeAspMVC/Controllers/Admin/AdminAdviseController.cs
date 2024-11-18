using YTeAspMVC.Daos;
using YTeAspMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace YTeAspMVC.Controllers.Admin
{
    public class AdminAdviseController : Controller
    {
        PostDao postDao = new PostDao();
        // GET: AdminUser
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = postDao.GetAdvise();
            return View();
        }

        [HttpPost]
        public ActionResult Reply(FormCollection form)
        {
            int idAdvise = Int32.Parse(form["IdAdvise"]);
            var reply = form["Reply"];
            var advise = postDao.GetAdviseDetail(idAdvise);
            string email = advise.Email;
            string html = "Xin chào : " + advise.FullName + " .  Nội dung tư vấn : " + reply;
            sendMail(email, html, advise.Question);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public void sendMail(string email, string body, string question)
        {
            var formEmailAddress = ConfigurationManager.AppSettings["FormEmailAddress"].ToString();
            var formEmailDisplayName = ConfigurationManager.AppSettings["FormEmailDisplayName"].ToString();
            var formEmailPassword = ConfigurationManager.AppSettings["FormEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPost"].ToString();
            bool enableSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());
            MailMessage message = new MailMessage(new MailAddress(formEmailAddress, formEmailDisplayName), new MailAddress(email));
            message.Subject = "Tư vấn cho câu hỏi : " + question;
            message.IsBodyHtml = true;
            message.Body = body;
            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(formEmailAddress, formEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enableSsl;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(message);
        }
    }
}
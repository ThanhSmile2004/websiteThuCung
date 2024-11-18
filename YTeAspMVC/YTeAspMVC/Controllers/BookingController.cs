using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTeAspMVC.Daos;
using YTeAspMVC.Models;
using YTeAspMVC.Request;
using System.Net;
using System.Net.Mail;


namespace YTeAspMVC.Controllers
{
    public class BookingController : Controller
    {

        BookingDao bookingDao = new BookingDao();
        // GET: Booking
        public ActionResult Index(string mess)
        {                
            return View();
        }

        [HttpPost]
        public JsonResult ValidateInDay(AjaxRequest request)
        {
            DateTime aDateTime = DateTime.Now;
            string dateStr = aDateTime.Year + "-" + aDateTime.Month + "-" + aDateTime.Day;
            if ("InDay".Equals(request.type))
            {
                var listHour = bookingDao.GetHourInDay(request.day, aDateTime.Hour);
                return Json(new { Hours = listHour, Status = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var listHour = bookingDao.GetHourInDay(request.day, 7);
                return Json(new { Hours = listHour, Status = true }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult Add(Booking booking)
        {
            User user = (User)Session["USER"];
            bool check = bookingDao.CheckExistScheduleInDay(booking.Day, booking.Time, user.IdUser,booking.IdDoctor);
            if (check) {
                booking.IdUser = user.IdUser;
                bookingDao.Add(booking);
                return RedirectToAction("HistoryBooking", "Authencation", new { mess = "1" });
            }
            else
            {
                return RedirectToAction("HistoryBooking", "Authencation", new { mess = "2"});
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            bookingDao.delete(id);
            return RedirectToAction("HistoryBooking", "Authencation", new { mess = "3" });
        }
        [HttpPost]
        public ActionResult Complete(int idBooking)
        {
            var booking = bookingDao.GetBookingById(idBooking); // Sửa lại để sử dụng BookingDao
            if (booking != null)
            {
                // Cập nhật trạng thái của booking thành 4
                booking.Status = 4; // Gán trạng thái là 4
                bookingDao.UpdateBooking(booking); // Lưu thay đổi vào cơ sở dữ liệu

                // Gửi email thông báo cho khách hàng
                string subject = "Thông báo: Lịch hẹn đã được xác nhận";
                string body = $"Chào {booking.User.FullName},<br/>" +
                              $"Lịch hẹn với bác sĩ {booking.Doctor.FullName} đã được xác nhận là đã khám.<br/>" +
                              $"Ngày: {booking.Day}<br/>" +
                              $"Giờ: {booking.Time}<br/>" +
                              "Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi.";
                SendEmail(booking.User.Email, subject, body); // Gửi email cho người dùng

                ViewBag.Msg = "1"; // Thông báo thành công
            }
            else
            {
                ViewBag.Msg = "0"; // Thông báo thất bại nếu không tìm thấy booking
            }

            // Redirect lại để cập nhật danh sách
            return RedirectToAction("Index"); // Thay đổi thành action của bạn nếu cần
        }
        private void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("thanh010120001@gmail.com"); // Địa chỉ email của bạn
                    mail.To.Add(toEmail);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true; // Gửi email dưới dạng HTML

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("thanh010120001@gmail.com", "wvtg flec xreu levy"); // Mật khẩu ứng dụng
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error message (hoặc hiển thị thông báo cho người dùng)
                Console.WriteLine($"Error sending email: {ex.Message}");
                // Hoặc bạn có thể thêm ViewBag để hiển thị trong View
                ViewBag.EmailError = "Không thể gửi email: " + ex.Message;
            }
        }


    }
}
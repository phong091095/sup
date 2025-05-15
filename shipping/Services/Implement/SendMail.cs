using System.Net.Mail;
using System.Net;

namespace WebAPI.Services
{
    public class SendMail
    {
        public bool SendPasswordEmail(string Email, string key)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("your-email", "Hệ thống xác thực hai bước"); //sửa chỗ này
                mail.To.Add(Email);
                mail.Subject = "OTP xác thực";
                mail.Body = "Dưới đây là OTP của bạn.\n\n"
                    + "Vui lòng không chia sẻ cho người khác OTP này.\n\n" +
                    $"🔑 OTP: {key}";
                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential("your-email", "your-app-password"); // sửa chỗ này
                smtpServer.EnableSsl = true;
                smtpServer.UseDefaultCredentials = false;
                smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpServer.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SendRejectEmail(string Email, string Lydo)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("your-email", "Hệ thống quản lý"); //sửa chỗ này
                mail.To.Add(Email);
                mail.Subject = "Từ chối đăng ký cửa hàng";
                mail.Body = "Xin chào người dùng.\n\n"
                    + "Cảm ơn vì đã đăng ký hệ thống cửa hàng của bạn trên trang của chúng tôi.\n\n" +
                    "Chúng tôi nhận thấy trong quá trình đăng ký không đủ điều kiện đăng ký.\n\n" +
                    $"Lý do từ chối: {Lydo}\n\n"+
                    "Vui lòng thử lại hoặc liên hệ bộ phận hộ trợ.\n\n";
                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential("your-email", "your-app-password"); // sửa chỗ này
                smtpServer.EnableSsl = true;
                smtpServer.UseDefaultCredentials = false;
                smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpServer.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SendEmail(string Email, string Lydo)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("your-email", "Hệ thống quản lý"); //sửa chỗ này
                mail.To.Add(Email);
                mail.Subject = "Thông báo khóa vi phạm cửa hàng";
                mail.Body = "Xin chào người dùng.\n\n"
                    + "Cảm ơn vì đã đăng ký hệ thống cửa hàng của bạn trên trang của chúng tôi.\n\n" +
                    "Chúng tôi nhận thấy trong quá trình hoạt động cửa hàng đã vi phạm quy tắc của trang nên cửa hàng sẽ bị khóa.\n\n" +
                    $"Lý do khóa: {Lydo}\n\n" +
                    "Nếu đây là một sự nhầm lẫn vui lòng liên hệ bộ phận hộ trợ.\n\n";
                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential("your-email", "your-app-password"); // sửa chỗ này
                smtpServer.EnableSsl = true;
                smtpServer.UseDefaultCredentials = false;
                smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpServer.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SendNotify(string Email, string tieude, string noidung)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("your-email", "Hệ thống quản lý"); //sửa chỗ này
                mail.To.Add(Email);
                mail.Subject = tieude;
                mail.Body = noidung;
                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential("your-email", "your-app-password"); // sửa chỗ này
                smtpServer.EnableSsl = true;
                smtpServer.UseDefaultCredentials = false;
                smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpServer.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

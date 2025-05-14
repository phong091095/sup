using System.Net.Mail;
using System.Net;

namespace WebAPI.Services
{
    public class SendMail
    {
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
    }
}

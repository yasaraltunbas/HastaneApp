using System.Threading.Tasks;

namespace HastaneAPP.WebUI.Services.EmailServices
{
    public interface IEmailSender
    {
         
         Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
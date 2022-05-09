using System.Threading.Tasks;

namespace HastaneAPP.WebUI.Services.SmsServices
{
    public interface ISmsSender
    {
           void SendSms(string phone, string subject, string SmsMesage);
         
    }
}
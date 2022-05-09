using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Vonage.Messaging;
using Vonage.Request;

namespace HastaneAPP.WebUI.Services.SmsServices
{
    public class VonageSmsSender : ISmsSender
    {
        private string _api_key;
        private string _api_secret;
        private string _from;

        public VonageSmsSender(string api_key, string api_secret, string from)
        {
            _api_key = api_key;
            _api_secret = api_secret;
            _from = from;
        }



        public void SendSms(string phone, string subject, string SmsMesage)
        {
            var credentials = Credentials.FromApiKeyAndSecret(_api_key, _api_secret);
            var client = new SmsClient(credentials);
            var request = new SendSmsRequest { To = phone, From = _from, Text = SmsMesage };
            var response = client.SendAnSms(request);

        }

    }
}
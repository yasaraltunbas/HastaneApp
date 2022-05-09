namespace HastaneAPP.WebUI.Models.FormModels
{
    public class EditHastaModel
    {
        
        public int HastaID { get; set; }
        public RegisterHastaModel hastaModel { get; set; }
        public EmailModel emailModel { get; set; } = new EmailModel();
        public SMSModel smsModel { get; set; } = new SMSModel();
    }
}
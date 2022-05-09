using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HastaneAPP.HastanebilgiLocal;
using HastaneAPP.WebUI.Extensions;
using HastaneAPP.WebUI.Models.Entity;
using HastaneAPP.WebUI.Models.FormModels;
using HastaneAPP.WebUI.Services.AppDbService.Abstract;
using HastaneAPP.WebUI.Services.EmailServices;
using HastaneAPP.WebUI.Services.SmsServices;
using HastaneAPP.WebUI.Services.WpService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vonage.Messaging;
using Vonage.Request;
using WordPressPCL;
using WordPressPCL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace HastaneAPP.WebUI.Controllers
{
    [Authorize]
    public class HastaController: Controller
    {
        #region default variable
        public ServiceHelper _service = new ServiceHelper();
        private IEmailSender _emailSender;
        private ISmsSender _smsSender;      
        private IHastaRepository _hastaRepo;
        private IOperasyonRepository _operationRepo;


        public HastaController(IHastaRepository hastaRepo, IOperasyonRepository operationRepo,IEmailSender emailSender, ISmsSender smsSender)
        {
            _hastaRepo = hastaRepo;
            _operationRepo = operationRepo;
            _emailSender = emailSender;
            _smsSender = smsSender;
        }
        #endregion

        [HttpGet]
        public IActionResult Register()
        {
            var Operations = _operationRepo.GetAll();
            ViewBag.OperationList = new SelectList(Operations, "Name", "Name");
            
            var rgt = new RegisterHastaModel();
        
            
            return View(rgt);


        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterHastaModel model, int? isSendEmail, int? isSendSMS)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var hasta = new Hastalar()
            {
                Name = model.Name,
                Surname = model.Surname,
                Birthday = model.Birthday,
                Gender = model.Gender,
                Applicant = model.Applicant,
                Operation = model.Operation,
                PhoneNumber = model.PhoneNumber,
                TCNumber = model.TCNumber,
                Email = model.Email,
            };
            _hastaRepo.Create(hasta);


            // try
            // {
                 IConfigurationRoot configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json")
                                .Build();
                    var Domain = configuration.GetConnectionString("Domain");
                    

                WpUsers userWP = new WpUsers();
                userWP.UserLogin = model.Name + "_" + model.Surname;
                userWP.UserPass = _service.Generate(10);
                userWP.UserNicename = model.Name + "_" + model.Surname;
                userWP.UserEmail = model.Email;
                userWP.UserUrl = Domain;
                userWP.UserStatus = 0;
                userWP.DisplayName = model.Name + "_" + model.Surname;
                userWP.Registered = DateTime.Now;
                userWP.UserActivationKey = _service.Generate(50);

                userWP.Age = DateTime.Now.Year - model.Birthday.Value.Year;
                userWP.Gender = model.Gender;
                userWP.Operation = model.Operation;
                _service.CreateUser(userWP, model.Name, model.Surname);



                // if(isSendEmail  == 1)
                // {
                    //Email-Service
                    
                    await _emailSender.SendEmailAsync(model.Email, "KOU Radyoloji Hasta Bilgilendirme Mesajı.",
                                $"<br><br>Sevgili Hastamız,<br><br> Yapılan inceleme sonucu {@hasta.Operation} Operasyonu geçireceğiniz kayıt altına alınmıştır. Operasyon öncesi yapılması ve yapılmaması gerekenleri öğrenmek ve size özel hazırlanmış içeriklerimizi görmek için   <a href='http://{@Domain}/loginpanel.php?username={@userWP.UserLogin}&token={@userWP.UserActivationKey}'>buraya</a> tıklayınız.<br><br> Sağlıklı Günler Dileriz...  ");
                // }
                

               // http://localhost:5000/?returnurl={urlEmailandSMS}
                if(isSendSMS  == 1)
                {
                    //SMS-Service
                    // var client = new SmsClient(creds: new Credentials
                    // {
                    //     ApiKey = "433b8ab7",
                    //     ApiSecret = "qsPkTZZeAe6seFXP"
                    // });

                    // var results = client.SendAnSms(request: new SendSmsRequest
                    // {
                    //     From = "Vonage APIs",
                    //     To = model.PhoneNumber,
                    //     Text = $"http://localhost:5000/?returnurl={urlEmailandSMS}"
                    // });
                    // ViewBag.MessageId = results.Messages[0].MessageId;


            
                    // _smsSender.SendSms(model.PhoneNumber, "sklds",
                    //  $"Hesabınız başarılı bir şekilde oluşturuldu {@model.UserName} Siteye giriş yapabilmek için http://localhost:5000{url} tıklayınız");

                }
            //     TempData.Put("message", new ResultMessage()
            //     {
            //         Title = "Şifresiz Giriş",
            //         Message = "Telefonunuza ve eposta adresinize gelen mail ile şifresiz giriş yapabilirsiniz",
            //         Css = "warning"
            //     });
            //     return RedirectToAction("Login", "Account");
           
            // }
            // catch(Exception)
            // {
            //   return View(model);

            // }
            return RedirectToAction("List");

          
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("List");
            }
            var model = new EditHastaModel();

            var HastaModel = _hastaRepo.GetById((int)id);
            var hasta = new RegisterHastaModel()
            {
                Name = HastaModel.Name,
                Surname = HastaModel.Surname,
                Birthday = HastaModel.Birthday,
                Gender = HastaModel.Gender,
                Applicant = HastaModel.Applicant,
                Operation = HastaModel.Operation,
                PhoneNumber = HastaModel.PhoneNumber,
                TCNumber = HastaModel.TCNumber,
                Email = HastaModel.Email,
            };
            var userWP = _service.GetWpUserByName(hasta.Name, hasta.Surname);
            var emailModel = new EmailModel();
            
            emailModel.Title = "KOU Radyoloji Hasta Bilgilendirme Mesajı";
            emailModel.Text = $"Sevgili Hastamız,<br><br> Yapılan inceleme sonucu {@hasta.Operation} Operasyonu geçireceğiniz kayıt altına alınmıştır. Operasyon öncesi yapılması ve yapılmaması gerekenleri öğrenmek ve size özel hazırlanmış içeriklerimizi görmek için   <a href='http://localhost/wordpress/loginpanel.php?username={@userWP.UserLogin}&token={@userWP.UserActivationKey}'>buraya</a> tıklayınız.<br><br> Sağlıklı Günler Dileriz...  ";

            var smsModel = new SMSModel();
            smsModel.Link = $"http://localhost/wordpress/loginpanel.php?username={@userWP.UserLogin}&token={@userWP.UserActivationKey}";

            model.hastaModel = hasta;
            model.emailModel = emailModel;
            model.smsModel = smsModel;
            model.HastaID  = HastaModel.Id;
            var Operations = _operationRepo.GetAll();
            ViewBag.OperationList = new SelectList(Operations, "Name", "Name");
            
        
            
            return View(model);


        }
        [HttpPost]
        public IActionResult Edit(EditHastaModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
         
            return RedirectToAction("List");
        }
        [HttpPost] 
        public async Task<IActionResult> SendEmail(EditHastaModel model)
        {
            
            await _emailSender.SendEmailAsync(model.emailModel.Email, model.emailModel.Title, model.emailModel.Text);
            
            TempData.Put("message", new ResultMessage()
            {
                Title = "Email Gönderildi !",
                Message = "",
                Css = "success"
            });
            return RedirectToAction("Edit", new { id = model.HastaID });
        }

        [HttpPost] 
        public IActionResult SendSMS(EditHastaModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            //SMS-Service
            var client = new SmsClient(creds: new Credentials
            {
                ApiKey = "433b8ab7",
                ApiSecret = "qsPkTZZeAe6seFXP"
            });

            var results = client.SendAnSms(request: new SendSmsRequest
            {
                From = "Vonage APIs",
                To = model.smsModel.Phone,
                Text = model.smsModel.Link
            });
            ViewBag.MessageId = results.Messages[0].MessageId;
            return View();
        }

        public IActionResult List()
        {
            var hastalar = _hastaRepo.GetAll();

            return View(hastalar);
        }

       
    }
}
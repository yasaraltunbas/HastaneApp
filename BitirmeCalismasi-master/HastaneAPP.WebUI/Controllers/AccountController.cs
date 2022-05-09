using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HastaneAPP.HastanebilgiLocal;
using HastaneAPP.WebUI.Services.EmailServices;
using HastaneAPP.WebUI.Extensions;
using HastaneAPP.WebUI.Models.FormModels;
using HastaneAPP.WebUI.Models.Identity;
using HastaneAPP.WebUI.Services.SmsServices;
using HastaneAPP.WebUI.Services.WpService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vonage.Messaging;
using Vonage.Request;

namespace HastaneAPP.WebUI.Controllers
{
    public class AccountController : Controller
    {
        #region default variable

        public ServiceHelper _service = new ServiceHelper();
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IEmailSender _emailSender;
        private ISmsSender _smsSender;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, ISmsSender smsSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
        }
        #endregion

        [HttpGet]
        public IActionResult Register()
        {

            var rgt = new RegisterDoktorModel();
            return View(rgt);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDoktorModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
               var applicationUser = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(applicationUser,model.Password);
           
            if (result.Succeeded)
            {
                
                // generate token
                // send mail
                // var urlEmailandSMS = Url.Action("LoginEmailandSMS", "Account", new
                // {
                //     email = model.Email,
                //     password = model.Password
                // });
                // urlEmailandSMS = Encrypt(urlEmailandSMS);

                //Email-Service
                // await _emailSender.SendEmailAsync(model.Email, "KOU Radyoloji Hasta Bilgilendirme Mesajı.",
                                // $"Hesabınız başarılı bir şekilde oluşturuldu {@model.UserName} <hr/> Siteye giriş yapabilmek için  <a href='http://localhost/wordpress/loginpanel.php?username=emronr&pass=160201070'>linke</a> tıklayınız");

                // http://localhost:5000/?returnurl={urlEmailandSMS}

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

                TempData.Put("message", new ResultMessage()
                {
                    Title =  "Hoşgediniz Doktor " + model.FirstName,
                    Message = "Artık hasta kaydı yapabilir, hastaların operasyon öncesi bilgilerine ulaşabilirsiniz",
                    Css = "warning"
                });
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ModelState.AddModelError("", "Bilinmeyen bir hata oluştu lütfen tekrar deneyiniz \n veya \n Giriş için Giriş Yap butonuna tıklayınız.");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LoginEmailandSMS(string email, string password)
        {
           
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
             
                var loginModel = new LoginModel();
                loginModel.Email = email;
                loginModel.Password = password;

                var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, true, false);

                if (result.Succeeded)
                {
                    return Redirect(loginModel.ReturnUrl ?? "~/");
                }

                return RedirectToAction("Login", loginModel);
            }

            TempData.Put("message", new ResultMessage()
            {
                Title = "Şifresiz Giriş",
                Message = "Giriş Yapılamadı, Lütfen email adresinizdeki link bağlantısını kontrol ediniz.",
                Css = "danger"
            });

            return View();
        }

        [HttpGet]
        public IActionResult Login(LoginModel? model, string ReturnUrl = null)
        {
            if (model == null)
            {
                var login = new LoginModel();
                login.ReturnUrl = ReturnUrl;
                return View(login);
            }
            model.ReturnUrl = ReturnUrl;
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
   
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Email adresine ait hesap bulunamamaktadır");
                return View(model);
            }



            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "~/");
            }


            ModelState.AddModelError("", "Mail adresi veya parola hatalı");

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            TempData.Put("message", new ResultMessage()
            {
                Title = "Oturum Kapatıldı.",
                Message = "Hesabınız güvenli bir şekilde sonlandırıldı",
                Css = "warning"
            });

            return Redirect("~/");
        }


    }
}
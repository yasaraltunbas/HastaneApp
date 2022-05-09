using System;
using System.Security.Cryptography;
using System.Text;
using HastaneAPP.WebUI.Extensions;
using HastaneAPP.WebUI.Models.FormModels;
using HastaneAPP.WebUI.Services.SmsServices;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace HastaneAPP.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private ISmsSender _smsSender;
        public HomeController(ISmsSender smsSender)
        {
            _smsSender = smsSender;
        }
        
        public IActionResult Index(string returnurl = null)
        {

            var sqlString = "server=20.71.61.215; port=3306; Database=HastaneDB; Uid=WinCon; Pwd=160201070";
            // var sqlString = "server=198.71.227.86; port=3306; Database=diabet; Uid=mustafalin; Pwd=12345678";

            try
            {

                MySqlConnection cnn = new MySqlConnection(sqlString);
                cnn.Open();
                TempData.Put("message", new ResultMessage()
                {
                    Title = "MySQL Hosting",
                    Message = "MySQL uzak bağlantısı sağlandı",
                    Css = "warning"
                });
                cnn.Close();
            }
            catch (MySqlException ex)
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "MySQL Hosting",
                    Message = "MySQL uzak bağlantısı sağlanamadı\n" + ex,
                    Css = "warning"
                });
                
            }
           

            return View();
        }
       


    }
}
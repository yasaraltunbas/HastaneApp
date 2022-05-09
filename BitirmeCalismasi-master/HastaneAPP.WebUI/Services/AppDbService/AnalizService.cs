using System;
using System.Linq;
using System.Threading.Tasks;
using HastaneAPP.WebUI.Models.FormModels;
using HastaneAPP.WebUI.Services.AppDbService.Abstract;
using HastaneAPP.WebUI.Services.WpService;

namespace HastaneAPP.WebUI.Services.AppDbService
{
    public class AnalizService
    {
        public ServiceHelper _service =  new ServiceHelper();
        private IHastaRepository _hastaRepo;
        


        public AnalizService(IHastaRepository hastaRepo)
        {
            _hastaRepo = hastaRepo;
        }
        public async Task<AnalizModel> CreateAnalizModel(int id, string email)
        {
            
            var hasta = _hastaRepo.GetById(id);

            var xapi = await _service.ListxAPIDetailForUser(email);

             xapi = xapi.Where(x => x.xapiVerb.VerbDisplay == "bitirdi").ToList();
           
            var model = new AnalizModel();

            model.Hasta = hasta;
            model.maxPuan = xapi.Count;
            model.Puan = Convert.ToInt32(xapi.Sum(x => x.xapiResult.ResultScoreScaled));
           
            if(model.maxPuan != 0)
                model.Level = model.Puan/model.maxPuan * 100;
            else
                model.Level = 0;
          
            if(model.Level <=33)
            {
                model.WarningMessage = "Hasta gerekli bilgilendirmeye sahip değildir.\nKESİNLİKLE ARANMASI ve YENİDEN BİLGİLENDİRİLMESİ GEREKMEKTEDİR!";
            }
          
            else if(33 < model.Level && model.Level <=66)
            {
                model.WarningMessage = "Operasyon öncesi tekrardan bilgilendirilmesi önerilmektedir.";
            }
            else if(66 < model.Level && model.Level <=100)
            {
                model.WarningMessage = "Hasta gerekli bilgilere sahiptir.";
            }





            return model;           
        }

    }
}
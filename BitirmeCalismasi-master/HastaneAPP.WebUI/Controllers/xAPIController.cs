using System;
using HastaneAPP.HastanebilgiLocal;
using Microsoft.AspNetCore.Mvc;
using HastaneAPP.WebUI.Services.WpService;
using System.Threading.Tasks;
using System.Collections.Generic;
using HastaneAPP.WebUI.Models.FormModels;
using HastaneAPP.WebUI.Services.AppDbService.Abstract;
using System.Linq;
using HastaneAPP.WebUI.Models.AnalizModels;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace HastaneAPP.WebUI.Controllers
{
    public class xAPIController : Controller
    {
        public ServiceHelper _service = new ServiceHelper();
        private IHastaRepository _hastaRepo;

        
        public xAPIController(IHastaRepository hastaRepo)
        {
            _hastaRepo = hastaRepo;
        }
        

        public async Task<IActionResult> List(string? email)
        {
            var model = await _service.ListxAPIDetail();

            if(email != null)
            {
                model = await _service.ListxAPIDetailForUser(email);
            }
            
            if(model == null)
            {
                return NotFound();
            }
            return View(model);
            
        }

        public async Task<IActionResult> ListRapor(string? email)
        {
            var model = await _service.ListxAPIDetail();

            if(email != null)
            {
                model = await _service.ListxAPIDetailForUser(email);
            }
            
            if(model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> xAPIDelete(int id)
        {
            _service.DeletexAPI(id);
            
            return RedirectToAction("List","Hasta");
        }


        [HttpPost]
        public async Task<IActionResult> RaporAnaliz(int id, string email)
        {
            // var model = await _serviceAnaliz.CreateAnalizModel(id, email);

            var hasta = _hastaRepo.GetById(id);

            var xapi = await _service.ListxAPIDetailForUser(email);

            var xapiCompleted = xapi.Where(x => x.xapiVerb.VerbDisplay == "bitirdi").ToList();
           
            var model = new AnalizModel();

            model.Hasta = hasta;
            model.maxPuan = xapiCompleted.Count;
            model.Puan = Convert.ToInt32(xapiCompleted.Sum(x => x.xapiResult.ResultScoreScaled));
           
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

            var xapiAnswered = xapi.Where(x => x.xapiVerb.VerbDisplay == "cevapladı" && x.xapiObject.ObjectChoices.Contains(',')).ToList();


            List<Questions> Question = new List<Questions>();
            foreach(var item in xapiAnswered)
            {
                //objectLinki bulundu
                var link = item.xapiObject.XobjectId.Split("subContent");
            
                // Şıkların Kelimeleri Parçalandı. Bulundu
                var siklar = item.xapiObject.ObjectChoices.Split(',');
                List<string> ChoicesWords = new List<string>();

                foreach(var ChoiceItems in siklar)
                {
                    var words = ChoiceItems.Split(']');
                    ChoicesWords.Add(words[1]);
                }

                //Şıklar Ayrıştırıldı
                List<Choices> choices = new List<Choices>();

                for(int i=0; i<ChoicesWords.Count; i++)
                {
                    var choice = new Choices();
                    choice.Choice = ChoicesWords[i];

                    choices.Add(choice);
                }

                //Doğru Şık Parçalandı. Bulundu
                var dogruSik = item.xapiObject.ObjectCorrectResponsesPattern.Split(':');
                int dogruSik2 = Convert.ToInt32(dogruSik[1]);
                var dogruWord = choices[dogruSik2].Choice;
                
                var TrueChoice = new Choices();
                TrueChoice.Choice = dogruWord;

                //Kullanıcı cevabı Bulundu
                var kullaniciSik = item.xapiResult.ResultResponse;
                int kullaniciSik2 = Convert.ToInt32(kullaniciSik);
                var kullaniciWord = choices[kullaniciSik2].Choice;

                var UserChoice = new Choices();
                UserChoice.Choice = kullaniciWord;

                var questions = new Questions();

                questions.ObjectLink = link[0];
                    questions.QuestionName = item.xapiObject.ObjectName;

                questions.Question = item.xapiObject.ObjectDescription;
                questions.Choice = choices;
                questions.SuccessChoise = TrueChoice;
                questions.AnswerChoice = UserChoice;
                questions.Date = item.xapi.Time;

                model.Questions.Add(questions);
            }
           

            model.SuccessAnswerCount =  model.Questions.Where(x => x.SuccessChoise.Choice == x.AnswerChoice.Choice).Count();
            model.WrongAnswerCount =  model.Questions.Where(x => x.SuccessChoise.Choice != x.AnswerChoice.Choice).Count();
            model.QuestionCount =  model.Questions.Count();


// BRANCH'lerde içindeki interactive_video kelimesini alıyor yani aynı branchin içindeki birden fazla video için sonuç vermeyecektir.


            // List<WpH5pContents> h5pTemp = new List<WpH5pContents>();
            // using(var context = new HastaneBilgiContext())
            // {
            //     var h5pModel = context.WpH5pContents.ToList();
                

            //     foreach(var item in xapi)
            //     {
            //         var myModel = h5pModel.Where(x => x.Title == item.xapiObject.ObjectName  && x.Parameters.Contains("interactiveVideo")).FirstOrDefault();
            //         if(myModel != null)
            //         {
            //             Console.WriteLine(myModel.Title);
            //             h5pTemp.Add(myModel);
            //             Console.WriteLine(item.xapi.Time);
            //         }
            //     }
            // }
            // Console.WriteLine(h5pTemp.Count);
            // foreach(var item in h5pTemp)
            // {
            //     var parameters =  item.Parameters.Split("\"endscreens\":[{\"time\":");
            //     var parameters_time =  parameters[1].Split(',');
            //     string time = parameters_time[0];
            //     Console.WriteLine(time);
            // }

            List<VideoAnaliz> videoList = new List<VideoAnaliz>();
            List<string> temp = new List<string>();
            foreach(var item in xapiCompleted)
            {
                VideoAnaliz video = new VideoAnaliz();
                video.ObjectName = item.xapiObject.ObjectName;
                var tempTime = item.xapiResult.ResultDuration.Substring(2,item.xapiResult.ResultDuration.Length-3);
                var tempTimeStringList = tempTime.Split('.');
                video.WatchTime = Convert.ToInt32(tempTimeStringList[0]);
                video.WatchDate = item.xapi.Time;

                videoList.Add(video);
                temp.Add(item.xapiObject.ObjectName);
                Console.WriteLine(item.xapiObject.ObjectName);
                Console.WriteLine(item.xapiResult.ResultDuration);
                Console.WriteLine(item.xapi.Time);
            }
            Console.WriteLine("------------");
            using(var context = new HastaneBilgiContext())
            {
                var h5pModel = context.WpH5pContents.ToList();
                

                foreach(var item in videoList)
                {
                    var myModel = h5pModel.Where(x => x.Title == item.ObjectName && x.Parameters.Contains("interactiveVideo")).FirstOrDefault();
                    if(myModel != null)
                    {
                        var parameters =  myModel.Parameters.Split("\"endscreens\":[{\"time\":");
                        var parameters_time =  parameters[1].Split(',');
                        string time = parameters_time[0];
                        
                        item.VideoTime = Convert.ToInt32(time);

                        item.WatchRatio = (item.WatchTime/item.VideoTime *100) ;
                        if(item.WatchRatio > 100.0)
                        {
                            var fazlalik = item.WatchRatio%100;
                            item.WatchRatio -= fazlalik;
                        }

                        item.WatchTimeS = Convert.ToInt32(item.WatchTime);
                        item.VideoTimeS = Convert.ToInt32(item.VideoTime);

                        item.WatchTimeM = item.WatchTimeS/60;
                        item.WatchTimeS = item.WatchTimeS - item.WatchTimeM * 60;

                        item.VideoTimeM = item.VideoTimeS/60;
                        item.VideoTimeS = item.VideoTimeS - item.VideoTimeM * 60;


                        var xapitemp = xapi.Where(x  => x.xapiObject.ObjectName == myModel.Title).FirstOrDefault();
                        var link = xapitemp.xapiObject.XobjectId.Split("subContent");
                        item.ObjectLink = link[0];
                        Console.WriteLine(time);
                    }
                    else
                    {
                        var myModel2 = h5pModel.Where(x => x.Parameters.Contains(item.ObjectName)  && x.Parameters.Contains("interactiveVideo")).FirstOrDefault();
                        if(myModel2 != null)
                        {
                            var parameters =  myModel2.Parameters.Split("\"endscreens\":[{\"time\":");
                            var parameters_time =  parameters[1].Split(',');
                            string time = parameters_time[0];
                            
                            item.VideoTime = Convert.ToInt32(time);

                            item.WatchRatio = (item.WatchTime/item.VideoTime *100) ;
                            if(item.WatchRatio > 100.0)
                            {
                                var fazlalik = item.WatchRatio%100;
                                item.WatchRatio -= fazlalik;
                            }


                            item.WatchTimeS = Convert.ToInt32(item.WatchTime);
                            item.VideoTimeS = Convert.ToInt32(item.VideoTime);

                            item.WatchTimeM = item.WatchTimeS/60;
                            item.WatchTimeS = item.WatchTimeS - item.WatchTimeM * 60;

                            item.VideoTimeM = item.VideoTimeS/60;
                            item.VideoTimeS = item.VideoTimeS - item.VideoTimeM * 60;

                            var xapitemp = xapi.Where(x  => x.xapiObject.ObjectName == myModel2.Title).FirstOrDefault();
                            var link = xapitemp.xapiObject.XobjectId.Split("subContent");
                            item.ObjectLink = link[0];
                            Console.WriteLine(time);
                        }
                    }
                }
            }
            model.VideoAnalizs = videoList;

            Console.WriteLine("Video Analiz");
            foreach(var item in videoList)
            {
                Console.WriteLine(item.ObjectName);
                Console.WriteLine(item.VideoTimeS);
                Console.WriteLine(item.WatchTimeS);
                Console.WriteLine(item.WatchDate);
                Console.WriteLine(item.WatchRatio);
                Console.WriteLine("---------");
            }

            string jsonStringQuestion;
            string jsonStringVideo;
            if(model.Questions != null)
                 jsonStringQuestion = JsonSerializer.Serialize(model.Questions);
            else
                jsonStringQuestion = "";


                model.QuestisonJson = jsonStringQuestion;

            Console.WriteLine(jsonStringQuestion);
           
            if(model.VideoAnalizs.FirstOrDefault(x => x.VideoTime == 0.0) == null)
            {
                if(model.VideoAnalizs != null)
                    jsonStringVideo = JsonSerializer.Serialize(model.VideoAnalizs);
                else
                    jsonStringVideo = "";


                    model.VideoJson = jsonStringVideo;
                Console.WriteLine(jsonStringVideo);
            }
            
            return View(model);
        }
    }
}
using System.Collections.Generic;
using HastaneAPP.WebUI.Models.AnalizModels;
using HastaneAPP.WebUI.Models.Entity;

namespace HastaneAPP.WebUI.Models.FormModels
{
    public class AnalizModel
    {
        public int Id { get; set; }
        public Hastalar Hasta { get; set; }

        public float Puan { get; set; }

        public float maxPuan { get; set; }

        public float Level { get; set; }

        public string WarningMessage { get; set; }
        
        public int QuestionCount { get; set; }
        public int SuccessAnswerCount { get; set; }

        public int WrongAnswerCount { get; set; }

        public List<Questions> Questions { get; set; } = new List<Questions>();


        public string QuestisonJson { get; set; }

        public string VideoJson { get; set; }


        public List<VideoAnaliz> VideoAnalizs { get; set; } = new List<VideoAnaliz>();
    }
}
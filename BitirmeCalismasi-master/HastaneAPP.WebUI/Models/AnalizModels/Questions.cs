using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HastaneAPP.WebUI.Models.AnalizModels
{
    public class Questions
    {
        [Key]
        public int Id { get; set; }

        public string ObjectLink { get; set; }
        public string? QuestionName { get; set; }
        public string Question { get; set; }
        public List<Choices>  Choice { get; set; }

        public Choices SuccessChoise { get; set; }

        public Choices AnswerChoice { get; set; }

        public DateTime? Date { get; set; }
    }
}
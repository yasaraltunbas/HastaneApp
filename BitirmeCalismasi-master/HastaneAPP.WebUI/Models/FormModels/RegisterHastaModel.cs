using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HastaneAPP.WebUI.Models.Entity;

namespace HastaneAPP.WebUI.Models.FormModels
{
    public class RegisterHastaModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? Birthday { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Applicant { get; set; }

        [Required]
        public string Operation { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        public string TCNumber { get; set; }

        public List<string> Genders = new List<string>(){ "Erkek", "Kadın" };
        public List<string> Applicants = new List<string>(){ "Kendisi", "Vasisi" };
    }
}
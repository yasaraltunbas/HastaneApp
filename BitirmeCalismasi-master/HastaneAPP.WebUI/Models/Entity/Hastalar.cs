using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HastaneAPP.WebUI.Models.Entity
{
    public class Hastalar
    {   
        [Key]
        public int Id { get; set; }
        public string TCNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
           
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }
        public string Applicant { get; set; }
        public string Operation { get; set; }
        public List<ExtraHastalik> ExtraHastaliklar { get; set; }        

    }
}
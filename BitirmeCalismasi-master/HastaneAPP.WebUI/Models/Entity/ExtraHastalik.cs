using System.ComponentModel.DataAnnotations;

namespace HastaneAPP.WebUI.Models.Entity
{
    public class ExtraHastalik
    {
        [Key]
        public int Id { get; set; }
        public string Question { get; set; }
        public bool Answers { get; set; }
    }
}
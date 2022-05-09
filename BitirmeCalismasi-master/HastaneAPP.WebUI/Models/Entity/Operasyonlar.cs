using System.ComponentModel.DataAnnotations;

namespace HastaneAPP.WebUI.Models.Entity
{
    public class Operasyonlar
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
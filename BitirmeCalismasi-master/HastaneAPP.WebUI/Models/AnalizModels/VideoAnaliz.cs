using System;
using System.ComponentModel.DataAnnotations;

namespace HastaneAPP.WebUI.Models.AnalizModels
{
    public class VideoAnaliz
    {
        [Key]
        public int Id { get; set; }

        public string ObjectLink { get; set; }

        public string ObjectName { get; set; }
        public float WatchTime { get; set; }
        public float VideoTime { get; set; }

        public int WatchTimeM { get; set; }
        public int WatchTimeS { get; set; }
        public int VideoTimeM { get; set; }
        public int VideoTimeS { get; set; }

        public float WatchRatio { get; set; }
        public DateTime? WatchDate { get; set; }
    }
}
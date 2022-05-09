using System;
using System.Collections.Generic;

namespace HastaneAPP.HastanebilgiLocal
{
    public partial class WpH5pResults
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public int MaxScore { get; set; }
        public int Opened { get; set; }
        public int Finished { get; set; }
        public int Time { get; set; }
    }
}

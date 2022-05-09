using System;
using System.Collections.Generic;

namespace HastaneAPP.HastanebilgiLocal
{
    public partial class WpH5pxapikatchuResult
    {
        public int Id { get; set; }
        public string ResultResponse { get; set; }
        public int? ResultScoreRaw { get; set; }
        public float? ResultScoreScaled { get; set; }
        public bool? ResultCompletion { get; set; }
        public bool? ResultSuccess { get; set; }
        public string ResultDuration { get; set; }
    }
}

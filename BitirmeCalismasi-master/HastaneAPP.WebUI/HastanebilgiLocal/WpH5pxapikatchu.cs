using System;
using System.Collections.Generic;

namespace HastaneAPP.HastanebilgiLocal
{
    public partial class WpH5pxapikatchu
    {
        public int Id { get; set; }
        public int? IdActor { get; set; }
        public int? IdVerb { get; set; }
        public int? IdObject { get; set; }
        public int? IdResult { get; set; }
        public string Xapi { get; set; }
        public DateTime Time { get; set; }
    }
}

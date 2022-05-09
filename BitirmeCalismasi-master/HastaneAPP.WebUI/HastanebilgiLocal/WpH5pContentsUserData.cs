using System;
using System.Collections.Generic;

namespace HastaneAPP.HastanebilgiLocal
{
    public partial class WpH5pContentsUserData
    {
        public int ContentId { get; set; }
        public int UserId { get; set; }
        public int SubContentId { get; set; }
        public string DataId { get; set; }
        public string Data { get; set; }
        public byte Preload { get; set; }
        public byte Invalidate { get; set; }
    }
}

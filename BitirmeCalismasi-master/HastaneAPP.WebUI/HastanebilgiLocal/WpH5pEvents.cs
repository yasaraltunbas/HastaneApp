using System;
using System.Collections.Generic;

namespace HastaneAPP.HastanebilgiLocal
{
    public partial class WpH5pEvents
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CreatedAt { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public int ContentId { get; set; }
        public string ContentTitle { get; set; }
        public string LibraryName { get; set; }
        public string LibraryVersion { get; set; }
    }
}

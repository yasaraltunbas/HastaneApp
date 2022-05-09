using System;
using System.Collections.Generic;

namespace HastaneAPP.HastanebilgiLocal
{
    public partial class WpH5pContentsLibraries
    {
        public int ContentId { get; set; }
        public int LibraryId { get; set; }
        public string DependencyType { get; set; }
        public short Weight { get; set; }
        public byte DropCss { get; set; }
    }
}

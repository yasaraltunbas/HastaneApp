using System;
using System.Collections.Generic;

namespace HastaneAPP.HastanebilgiLocal
{
    public partial class WpH5pLibraries
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public int PatchVersion { get; set; }
        public int Runnable { get; set; }
        public int Restricted { get; set; }
        public int Fullscreen { get; set; }
        public string EmbedTypes { get; set; }
        public string PreloadedJs { get; set; }
        public string PreloadedCss { get; set; }
        public string DropLibraryCss { get; set; }
        public string Semantics { get; set; }
        public string TutorialUrl { get; set; }
        public int HasIcon { get; set; }
        public string MetadataSettings { get; set; }
        public string AddTo { get; set; }
    }
}

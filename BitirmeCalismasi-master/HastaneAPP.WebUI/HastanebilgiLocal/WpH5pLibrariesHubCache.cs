using System;
using System.Collections.Generic;

namespace HastaneAPP.HastanebilgiLocal
{
    public partial class WpH5pLibrariesHubCache
    {
        public int Id { get; set; }
        public string MachineName { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public int PatchVersion { get; set; }
        public int? H5pMajorVersion { get; set; }
        public int? H5pMinorVersion { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public int CreatedAt { get; set; }
        public int UpdatedAt { get; set; }
        public int IsRecommended { get; set; }
        public int Popularity { get; set; }
        public string Screenshots { get; set; }
        public string License { get; set; }
        public string Example { get; set; }
        public string Tutorial { get; set; }
        public string Keywords { get; set; }
        public string Categories { get; set; }
        public string Owner { get; set; }
    }
}

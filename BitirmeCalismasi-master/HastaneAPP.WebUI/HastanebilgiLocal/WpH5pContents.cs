using System;
using System.Collections.Generic;

namespace HastaneAPP.HastanebilgiLocal
{
    public partial class WpH5pContents
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public int LibraryId { get; set; }
        public string Parameters { get; set; }
        public string Filtered { get; set; }
        public string Slug { get; set; }
        public string EmbedType { get; set; }
        public int Disable { get; set; }
        public string ContentType { get; set; }
        public string Authors { get; set; }
        public string Source { get; set; }
        public int? YearFrom { get; set; }
        public int? YearTo { get; set; }
        public string License { get; set; }
        public string LicenseVersion { get; set; }
        public string LicenseExtras { get; set; }
        public string AuthorComments { get; set; }
        public string Changes { get; set; }
        public string DefaultLanguage { get; set; }

    }
}

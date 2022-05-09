using System;
using System.Collections.Generic;

namespace HastaneAPP.HastanebilgiLocal
{
    public partial class WpUsers
    {
        public long Id { get; set; }
        public string UserLogin { get; set; }
        public string UserPass { get; set; }
        public string UserNicename { get; set; }
        public string UserEmail { get; set; }
        public string UserUrl { get; set; }
        public string UserActivationKey { get; set; }
        public int UserStatus { get; set; }
        public string DisplayName { get; set; }
        public DateTime Registered { get; set; }
        

        public string Operation {get; set;}
        public int Age { get; set; }
        public string Gender { get; set; }
    }
}

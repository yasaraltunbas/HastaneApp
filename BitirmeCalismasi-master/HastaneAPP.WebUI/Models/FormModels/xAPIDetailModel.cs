using System;
using HastaneAPP.HastanebilgiLocal;

namespace HastaneAPP.WebUI.Models.FormModels
{
    public class xAPIDetailModel
    {
        public WpH5pxapikatchu xapi { get; set; }
        public WpH5pxapikatchuActor xapiActor { get; set; }
        public WpH5pxapikatchuObject xapiObject { get; set; }
        public WpH5pxapikatchuVerb xapiVerb { get; set; }
        public WpH5pxapikatchuResult xapiResult { get; set; }

        


    }
}
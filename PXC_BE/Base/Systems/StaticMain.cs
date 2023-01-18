using FTS.Base.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace FTS.Base.Systems {
    public static class StaticMain
    {
        public static FTSMain FTSMain()
        {
            return (FTSMain)System.Web.HttpContext.Current.Application["FTSMain"];
        }
    }
}
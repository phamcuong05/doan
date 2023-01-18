using FTS.Base.Systems;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FTS.ShareAPI {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {

        private FTSMain _FTSMain;
        public FTSMain mFTSMain
        {
            get
            {
                if (_FTSMain == null)
                {

                    _FTSMain = new FTSMain();
                    _FTSMain.Run();

                    UserInfo mUserInfo = new UserInfo();
                    mUserInfo.OrganizationID = ConfigurationSettings.AppSettings["ORGANIZATION_ID"];
                    mUserInfo.UserID = "ADMIN";
                    mUserInfo.UserGroupID = "ADMIN";

                    _FTSMain.UserInfo = mUserInfo;
                }
                return _FTSMain;
            }
        }
        protected void Application_Start()
        {
            Application["FTSMain"] = mFTSMain;
            AreaRegistration.RegisterAllAreas();
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var jsonFormat = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            jsonFormat.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
        }

    }
}
using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model {
    public class RateMethodObject : ObjectInfoBase {
        public RateMethodObject() : base() {
            this.RATE_METHOD_ID = string.Empty;
            this.RATE_METHOD_NAME = string.Empty;
        }

        public RateMethodObject(string ratemethodid, string ratemethodname) {
            this.RATE_METHOD_ID = ratemethodid;
            this.RATE_METHOD_NAME = ratemethodname;
        }

        public string RATE_METHOD_ID
        {
            get { return this.GetValue("RATE_METHOD_ID").ToString(); }
            set { this.SetValue("RATE_METHOD_ID", value); }
        }

        public string RATE_METHOD_NAME
        {
            get { return this.GetValue("RATE_METHOD_NAME").ToString(); }
            set { this.SetValue("RATE_METHOD_NAME", value); }
        }
    }
}

using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model {
    public class RateTypeObject : ObjectInfoBase {
        public RateTypeObject() : base() {
            this.RATE_TYPE_ID = string.Empty;
            this.RATE_TYPE_NAME = string.Empty;
        }

        public RateTypeObject(string ratetypeid, string ratetypename) {
            this.RATE_TYPE_ID = ratetypeid;
            this.RATE_TYPE_NAME = ratetypename;
        }

        public string RATE_TYPE_ID
        {
            get { return this.GetValue("RATE_TYPE_ID").ToString(); }
            set { this.SetValue("RATE_TYPE_ID", value); }
        }

        public string RATE_TYPE_NAME
        {
            get { return this.GetValue("RATE_TYPE_NAME").ToString(); }
            set { this.SetValue("RATE_TYPE_NAME", value); }
        }
    }
}

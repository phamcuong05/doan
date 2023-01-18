using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model {
    public class PeriodTypeObject : ObjectInfoBase {
        public PeriodTypeObject() : base() {
            this.PERIOD_TYPE_ID = string.Empty;
            this.PERIOD_TYPE_NAME = string.Empty;
        }

        public PeriodTypeObject(string periodtypeid, string periodtypename) {
            this.PERIOD_TYPE_ID = periodtypeid;
            this.PERIOD_TYPE_NAME = periodtypename;
        }

        public string PERIOD_TYPE_ID
        {
            get { return this.GetValueOrDefault<string>("PERIOD_TYPE_ID"); }
            set { this.SetValue("PERIOD_TYPE_ID", value); }
        }

        public string PERIOD_TYPE_NAME
        {
            get { return this.GetValueOrDefault<string>("PERIOD_TYPE_NAME"); }
            set { this.SetValue("PERIOD_TYPE_NAME", value); }
        }
    }
}

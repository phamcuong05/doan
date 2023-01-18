using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model
{
    public class Dm_Estimate_TypeObject : ObjectInfoBase
    {
        public Dm_Estimate_TypeObject() : base()
        {
            this.ESTIMATE_TYPE_ID = string.Empty;
            this.ESTIMATE_TYPE_NAME = string.Empty;
            this.ACTIVE = 1;
            this.USER_ID = "ADMIN";
        }

        public Dm_Estimate_TypeObject(DataRow row) : base(row)
        {

        }

        public string ESTIMATE_TYPE_ID
        {
            get { return this.GetValueOrDefault<string>("ESTIMATE_TYPE_ID"); }
            set { this.SetValue("ESTIMATE_TYPE_ID", value); }
        }

        public string ESTIMATE_TYPE_NAME
        {
            get { return this.GetValueOrDefault<string>("ESTIMATE_TYPE_NAME"); }
            set { this.SetValue("ESTIMATE_TYPE_NAME", value); }
        }

        public string USER_ID
        {
            get { return this.GetValueOrDefault<string>("USER_ID"); }
            set { this.SetValue("USER_ID", value); }
        }

        public Int16 ACTIVE
        {
            get { return this.GetValueOrDefault<Int16>("ACTIVE"); }
            set { this.SetValue("ACTIVE", value); }
        }
    }
}

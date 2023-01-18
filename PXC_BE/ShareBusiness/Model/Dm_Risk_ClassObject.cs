using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Acc
{
    public class Dm_Risk_ClassObject : ObjectInfoBase
    {
        public Dm_Risk_ClassObject() : base()
        {
            this.RISK_CLASS_ID = string.Empty;
            this.RISK_CLASS_NAME = string.Empty;
            this.RISK_CLASS_CATEGORY = 0;
            this.ACTIVE = 1;
            this.USER_ID = "ADMIN";
        }

        public Dm_Risk_ClassObject(DataRow row) : base(row)
        {

        }

        public string RISK_CLASS_ID
        {
            get { return this.GetValueOrDefault<string>("RISK_CLASS_ID"); }
            set { this.SetValue("RISK_CLASS_ID", value); }
        }

        public string RISK_CLASS_NAME
        {
            get { return this.GetValueOrDefault<string>("RISK_CLASS_NAME"); }
            set { this.SetValue("RISK_CLASS_NAME", value); }
        }
        public int RISK_CLASS_CATEGORY
        {
            get { return this.GetValueOrDefault<int>("RISK_CLASS_CATEGORY"); }
            set { this.SetValue("RISK_CLASS_CATEGORY", value); }
        }

        public string USER_ID
        {
            get { return this.GetValueOrDefault<string>("User_ID"); }
            set { this.SetValue("User_ID", value); }
        }

        public Int16 ACTIVE
        {
            get { return this.GetValueOrDefault<Int16>("Active"); }
            set { this.SetValue("Active", value); }
        }
    }
}

using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model
{
    public class Dm_Contract_StatusObject : ObjectInfoBase
    {
        public Dm_Contract_StatusObject() : base()
        {
            this.CONTRACT_STATUS_ID = string.Empty;
            this.CONTRACT_STATUS_NAME = string.Empty;
            this.ACTIVE = 1;
            this.USER_ID = "ADMIN";
        }

        public Dm_Contract_StatusObject(DataRow row) : base(row)
        {

        }

        public string CONTRACT_STATUS_ID
        {
            get { return this.GetValueOrDefault<string>("CONTRACT_STATUS_ID"); }
            set { this.SetValue("CONTRACT_STATUS_ID", value); }
        }

        public string CONTRACT_STATUS_NAME
        {
            get { return this.GetValueOrDefault<string>("CONTRACT_STATUS_NAME"); }
            set { this.SetValue("CONTRACT_STATUS_NAME", value); }
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

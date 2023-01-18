using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model
{
    public class Dm_DistrictObject : ObjectInfoBase {
        public Dm_DistrictObject() : base() {
            this.DISTRICT_ID = string.Empty;
            this.DISTRICT_NAME = string.Empty;
            this.PROVINCE_ID = string.Empty;
            this.PROVINCE_NAME = string.Empty;
            this.ACTIVE = 1;
            this.USER_ID = "ADMIN";
        }

        public Dm_DistrictObject(DataRow row) : base(row) {

        }

        public string DISTRICT_ID
        {
            get { return this.GetValueOrDefault<string>("DISTRICT_ID"); }
            set { this.SetValue("DISTRICT_ID", value); }
        }

        public string DISTRICT_NAME
        {
            get { return this.GetValueOrDefault<string>("DISTRICT_NAME"); }
            set { this.SetValue("DISTRICT_NAME", value); }
        }

        public string PROVINCE_ID
        {
            get { return this.GetValueOrDefault<string>("PROVINCE_ID"); }
            set { this.SetValue("PROVINCE_ID", value); }
        }

        public string PROVINCE_NAME
        {
            get { return this.GetValueOrDefault<string>("PROVINCE_NAME"); }
            set { this.SetValue("PROVINCE_NAME", value); }
        }

        public string USER_ID {
            get { return this.GetValueOrDefault<string>("USER_ID"); }
            set { this.SetValue("USER_ID", value); }
        }

        public Int16 ACTIVE {
            get { return this.GetValueOrDefault<Int16>("ACTIVE"); }
            set { this.SetValue("ACTIVE", value); }
        }
    }
}

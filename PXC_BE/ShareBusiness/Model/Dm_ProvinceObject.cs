using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model
{
    public class Dm_ProvinceObject : ObjectInfoBase {
        public Dm_ProvinceObject() : base() {
            this.PROVINCE_ID = string.Empty;
            this.PROVINCE_NAME = string.Empty;
            this.ACTIVE = 1;
            this.USER_ID = "ADMIN";
        }

        public Dm_ProvinceObject(DataRow row) : base(row) {

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

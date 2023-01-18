using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Acc
{
    public class Dm_UnitObject : ObjectInfoBase
    {
        public Dm_UnitObject() : base()
        {
            this.UNIT_ID = string.Empty;
            this.UNIT_NAME = string.Empty;
            this.ACTIVE = 1;
            this.USER_ID = "ADMIN";
        }

        public Dm_UnitObject(DataRow row) : base(row)
        {

        }

        public string UNIT_ID
        {
            get { return this.GetValueOrDefault<string>("UNIT_ID"); }
            set { this.SetValue("UNIT_ID", value); }
        }

        public string UNIT_NAME
        {
            get { return this.GetValueOrDefault<string>("UNIT_NAME"); }
            set { this.SetValue("UNIT_NAME", value); }
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

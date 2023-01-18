using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model
{
    public class Dm_PeriodObject : ObjectInfoBase
    {
        public Dm_PeriodObject() : base()
        {
            this.PERIOD_ID = string.Empty;
            this.PERIOD_NAME = string.Empty;
            this.PERIOD_NUMBER = 0;
            this.PERIOD_TYPE = string.Empty;
            this.ACTIVE = 1;
            this.USER_ID = "ADMIN";
        }

        public Dm_PeriodObject(DataRow row) : base(row)
        {

        }

        public string PERIOD_ID
        {
            get { return this.GetValueOrDefault<string>("PERIOD_ID"); }
            set { this.SetValue("PERIOD_ID", value); }
        }

        public string PERIOD_NAME
        {
            get { return this.GetValueOrDefault<string>("PERIOD_NAME"); }
            set { this.SetValue("PERIOD_NAME", value); }
        }

        public Int32 PERIOD_NUMBER
        {
            get { return this.GetValueOrDefault<Int32>("PERIOD_NUMBER"); }
            set { this.SetValue("PERIOD_NUMBER", value); }
        }
        public string PERIOD_TYPE
        {
            get { return this.GetValueOrDefault<string>("PERIOD_TYPE"); }
            set { this.SetValue("PERIOD_TYPE", value); }
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

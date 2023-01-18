using FTS.Base.Business;
using System;

namespace FTS.ShareBusiness.Model
{
    public class Dm_Capital_SourceObject : ObjectInfoBase
    {
        public Dm_Capital_SourceObject() : base()
        {
            this.CAPITAL_SOURCE_ID = string.Empty;
            this.CAPITAL_SOURCE_NAME = string.Empty;
            this.ACTIVE = 0;
            this.USER_ID = string.Empty;
        }

        public string CAPITAL_SOURCE_ID
        {
            get { return this.GetValueOrDefault<string>("CAPITAL_SOURCE_ID"); }
            set { this.SetValue("CAPITAL_SOURCE_ID", value); }
        }

        public string CAPITAL_SOURCE_NAME
        {
            get { return this.GetValueOrDefault<string>("CAPITAL_SOURCE_NAME"); }
            set { this.SetValue("CAPITAL_SOURCE_NAME", value); }
        }
        public Int16 ACTIVE
        {
            get { return this.GetValueOrDefault<Int16>("ACTIVE"); }
            set { this.SetValue("ACTIVE", value); }
        }
        public string USER_ID
        {
            get { return this.GetValueOrDefault<string>("USER_ID"); }
            set { this.SetValue("USER_ID", value); }
        }
    }
}

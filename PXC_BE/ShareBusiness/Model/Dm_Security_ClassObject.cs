using FTS.Base.Business;
using System;
using System.Data;

namespace FTS.ShareBusiness.Model
{
    public class Dm_Security_ClassObject : ObjectInfoBase
    {
        public Dm_Security_ClassObject() : base()
        {
            this.SECURITY_CLASS_ID = string.Empty;
            this.SECURITY_CLASS_NAME = string.Empty;
            this.ACTIVE = 1;
            this.USER_ID = string.Empty;
        }

        public Dm_Security_ClassObject(DataRow row) : base(row)
        {

        }

        public string SECURITY_CLASS_ID
        {
            get { return this.GetValueOrDefault<string>("SECURITY_CLASS_ID"); }
            set { this.SetValue("SECURITY_CLASS_ID", value); }
        }

        public string SECURITY_CLASS_NAME
        {
            get { return this.GetValueOrDefault<string>("SECURITY_CLASS_NAME"); }
            set { this.SetValue("SECURITY_CLASS_NAME", value); }
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

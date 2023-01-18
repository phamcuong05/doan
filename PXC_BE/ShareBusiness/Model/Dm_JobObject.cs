using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model {
    public class Dm_JobObject : ObjectInfoBase {
        public Dm_JobObject() : base() {
            this.JOB_ID = string.Empty;
            this.JOB_NAME = string.Empty;
            this.ACTIVE = 1;
            this.USER_ID = "ADMIN";
        }

        public Dm_JobObject(DataRow row) : base(row) {

        }

        public string JOB_ID
        {
            get { return this.GetValueOrDefault<string>("JOB_ID"); }
            set { this.SetValue("JOB_ID", value); }
        }

        public string JOB_NAME
        {
            get { return this.GetValueOrDefault<string>("JOB_NAME"); }
            set { this.SetValue("JOB_NAME", value); }
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

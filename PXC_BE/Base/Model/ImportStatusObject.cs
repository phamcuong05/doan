using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.Base.Model {
    public class ImportStatusObject : ObjectInfoBase {
        public ImportStatusObject() : base() {
            this.STATUS_ID = string.Empty;
            this.STATUS_NAME = string.Empty;
        }

        public ImportStatusObject(string statusid, string statusname) {
            this.STATUS_ID = statusid;
            this.STATUS_NAME = statusname;
        }

        public string STATUS_ID {
            get { return this.GetValueOrDefault<string>("STATUS_ID"); }
            set { this.SetValue("STATUS_ID", value); }
        }

        public string STATUS_NAME {
            get { return this.GetValueOrDefault<string>("STATUS_NAME"); }
            set { this.SetValue("STATUS_NAME", value); }
        }
    }
}
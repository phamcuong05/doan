using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model {
    public class Dm_BankObject : ObjectInfoBase {
        public Dm_BankObject() : base() {
            this.BANK_ID = string.Empty;
            this.BANK_NAME = string.Empty;
            this.ACTIVE = 1;
            this.USER_ID = "ADMIN";
        }

        public Dm_BankObject(DataRow row) : base(row) {

        }

        public string BANK_ID
        {
            get { return this.GetValueOrDefault<string>("BANK_ID"); }
            set { this.SetValue("BANK_ID", value); }
        }

        public string BANK_NAME
        {
            get { return this.GetValueOrDefault<string>("BANK_NAME"); }
            set { this.SetValue("BANK_NAME", value); }
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

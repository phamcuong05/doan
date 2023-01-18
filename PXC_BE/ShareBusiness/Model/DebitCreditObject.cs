using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model {
    public class DebitCreditObject : ObjectInfoBase {
        public DebitCreditObject() : base() {
            this.DEBIT_CREDIT_ID = string.Empty;
            this.DEBIT_CREDIT_NAME = string.Empty;
        }

        public DebitCreditObject(string DebitCreditId, string DebitCreditName) {
            this.DEBIT_CREDIT_ID = DebitCreditId;
            this.DEBIT_CREDIT_NAME = DebitCreditName;
        }

        public string DEBIT_CREDIT_ID
        {
            get { return this.GetValueOrDefault<string>("DEBIT_CREDIT_ID"); }
            set { this.SetValue("DEBIT_CREDIT_ID", value); }
        }

        public string DEBIT_CREDIT_NAME
        {
            get { return this.GetValueOrDefault<string>("DEBIT_CREDIT_NAME"); }
            set { this.SetValue("DEBIT_CREDIT_NAME", value); }
        }
    }
}

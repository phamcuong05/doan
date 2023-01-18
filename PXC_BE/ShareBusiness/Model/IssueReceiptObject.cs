using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model {
    public class IssueReceiptObject : ObjectInfoBase {
        public IssueReceiptObject() : base() {
            this.ISSUE_RECEIPT_ID = string.Empty;
            this.ISSUE_RECEIPT_NAME = string.Empty;
        }

        public IssueReceiptObject(string IssueReceiveId, string IssueReceiveName) {
            this.ISSUE_RECEIPT_ID = IssueReceiveId;
            this.ISSUE_RECEIPT_NAME = IssueReceiveName;
        }

        public string ISSUE_RECEIPT_ID
        {
            get { return this.GetValueOrDefault<string>("ISSUE_RECEIPT_ID"); }
            set { this.SetValue("ISSUE_RECEIPT_ID", value); }
        }

        public string ISSUE_RECEIPT_NAME
        {
            get { return this.GetValueOrDefault<string>("ISSUE_RECEIPT_NAME"); }
            set { this.SetValue("ISSUE_RECEIPT_NAME", value); }
        }
    }
}

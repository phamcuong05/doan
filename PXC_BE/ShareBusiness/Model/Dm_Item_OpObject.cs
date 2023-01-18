using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model
{
    public class Dm_Item_OpObject : ObjectInfoBase
    {
        public Dm_Item_OpObject() : base()
        {
            this.ITEM_OP_ID = string.Empty;
            this.ITEM_OP_NAME = string.Empty;
            this.ISSUE_RECEIPT = string.Empty;
            this.TRANSFER_ITEM_OP_ID = string.Empty;
            this.TRANSFER_ITEM_OP_NAME = string.Empty;
            this.ACTIVE = 1;
            this.USER_ID = "ADMIN";
        }

        public Dm_Item_OpObject(DataRow row) : base(row)
        {

        }

        public string ITEM_OP_ID
        {
            get { return this.GetValueOrDefault<string>("ITEM_OP_ID"); }
            set { this.SetValue("ITEM_OP_ID", value); }
        }

        public string ITEM_OP_NAME
        {
            get { return this.GetValueOrDefault<string>("ITEM_OP_NAME"); }
            set { this.SetValue("ITEM_OP_NAME", value); }
        }

        public string ISSUE_RECEIPT
        {
            get { return this.GetValueOrDefault<string>("ISSUE_RECEIPT"); }
            set { this.SetValue("ISSUE_RECEIPT", value); }
        }
        public string TRANSFER_ITEM_OP_ID
        {
            get { return this.GetValueOrDefault<string>("TRANSFER_ITEM_OP_ID"); }
            set { this.SetValue("TRANSFER_ITEM_OP_ID", value); }
        }
        public string TRANSFER_ITEM_OP_NAME
        {
            get { return this.GetValueOrDefault<string>("TRANSFER_ITEM_OP_NAME"); }
            set { this.SetValue("TRANSFER_ITEM_OP_NAME", value); }
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

using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model
{
    public class Dm_Item_ClassObject : ObjectInfoBase
    {
        public Dm_Item_ClassObject() : base()
        {
            this.ITEM_CLASS_ID = string.Empty;
            this.ITEM_CLASS_NAME = string.Empty;
            this.INV_ACCOUNT_ID = string.Empty;
            this.ACCOUNT_NAME = string.Empty;
            this.ACTIVE = 1;
            this.USER_ID = "ADMIN";
            this.INV_ACCOUNT_ID = string.Empty;
        }

        public Dm_Item_ClassObject(DataRow row) : base(row)
        {

        }

        public string ITEM_CLASS_ID
        {
            get { return this.GetValueOrDefault<string>("ITEM_CLASS_ID"); }
            set { this.SetValue("ITEM_CLASS_ID", value); }
        }

        public string ITEM_CLASS_NAME
        {
            get { return this.GetValueOrDefault<string>("ITEM_CLASS_NAME"); }
            set { this.SetValue("ITEM_CLASS_NAME", value); }
        }
        public string ACCOUNT_NAME
        {
            get { return this.GetValueOrDefault<string>("ACCOUNT_NAME"); }
            set { this.SetValue("ACCOUNT_NAME", value); }
        }



        public string USER_ID
        {
            get { return this.GetValueOrDefault<string>("User_ID"); }
            set { this.SetValue("User_ID", value); }
        }

        public Int16 ACTIVE
        {
            get { return this.GetValueOrDefault<Int16>("Active"); }
            set { this.SetValue("Active", value); }
        }

        public string INV_ACCOUNT_ID {
            get { return this.GetValueOrDefault<string>("INV_ACCOUNT_ID"); }
            set { this.SetValue("INV_ACCOUNT_ID", value); }
        }
    }
}

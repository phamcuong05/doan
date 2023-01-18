using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model
{
    public class Dm_ItemObject : ObjectInfoBase
    {
        public Dm_ItemObject() : base()
        {
            this.ITEM_ID = string.Empty;
            this.ITEM_NAME = string.Empty;
            this.ITEM_CLASS1_ID = string.Empty;
            this.ITEM_CLASS_ID = string.Empty;
            this.UNIT_ID = string.Empty;
            this.ITEM_CLASS1_NAME = string.Empty;
            this.ITEM_CLASS_NAME = string.Empty;
            this.UNIT_NAME = string.Empty;
            this.ACTIVE = 1;
            this.USER_ID = "ADMIN";
        }

        public Dm_ItemObject(DataRow row) : base(row)
        {

        }

        public string ITEM_ID
        {
            get { return this.GetValue("ITEM_ID").ToString(); }
            set { this.SetValue("ITEM_ID", value); }
        }

        public string ITEM_NAME
        {
            get { return this.GetValueOrDefault<string>("ITEM_NAME"); }
            set { this.SetValue("ITEM_NAME", value); }
        }
        public string ITEM_CLASS_ID
        {
            get { return this.GetValueOrDefault<string>("ITEM_CLASS_ID"); }
            set { this.SetValue("ITEM_CLASS_ID", value); }
        }
        public string ITEM_CLASS1_ID
        {
            get { return this.GetValueOrDefault<string>("ITEM_CLASS1_ID"); }
            set { this.SetValue("ITEM_CLASS1_ID", value); }
        }
        public string UNIT_ID
        {
            get { return this.GetValueOrDefault<string>("UNIT_ID"); }
            set { this.SetValue("UNIT_ID", value); }
        }
        public string ITEM_CLASS_NAME
        {
            get { return this.GetValueOrDefault<string>("ITEM_CLASS_NAME"); }
            set { this.SetValue("ITEM_CLASS_NAME", value); }
        }
        public string ITEM_CLASS1_NAME
        {
            get { return this.GetValueOrDefault<string>("ITEM_CLASS1_NAME"); }
            set { this.SetValue("ITEM_CLASS1_NAME", value); }
        }
        public string UNIT_NAME
        {
            get { return this.GetValueOrDefault<string>("UNIT_NAME"); }
            set { this.SetValue("UNIT_NAME", value); }
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
    }
}

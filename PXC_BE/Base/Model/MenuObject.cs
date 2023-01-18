using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.Base.Model
{
    public class MenuObject : ObjectInfoBase
    {
        public MenuObject() : base()
        {
           
        }

        public MenuObject(DataRow row) : base(row)
        {
            this.Id = row["MENU_ID"].ToString();
            this.Name = row["MENU_NAME"].ToString();
            this.IconCls = row["ICON_CLS"].ToString();
            this.Href = row["HREF"].ToString();
            this.MapPath = row["MAP_PATH"].ToString();
            this.Order =(int) row["MENU_ORDER"];

            this.GroupId = row["MODULE_ID"].ToString();
            this.GroupName = row["MODULE_NAME"].ToString();
            this.GroupIconCls = row["MODULE_ICON_CLS"].ToString();
            this.GroupOrder = (int) row["MODULE_ORDER"];

            this.GroupId2 = row["MENU_GROUP_ID"].ToString();
            this.GroupName2 = row["MENU_GROUP_NAME"].ToString();
            this.Group2IconCls = row["MENU_GROUP_ICON_CLS"].ToString();
            this.Group2Order = (int)row["MENU_GROUP_ORDER"];

            this.ExpandType = row["EXPAND_TYPE"].ToString();
        }

        public string Id
        {
            get { return this.GetValue("MENU_ID").ToString(); }
            set { this.SetValue("MENU_ID", value); }
        }

        public string Name
        {
            get { return this.GetValue("MENU_NAME").ToString(); }
            set { this.SetValue("MENU_NAME", value); }
        }

        public string IconCls
        {
            get { return this.GetValue("ICON_CLS").ToString(); }
            set { this.SetValue("ICON_CLS", value); }
        }

        public string Href
        {
            get { return this.GetValue("HREF").ToString(); }
            set { this.SetValue("HREF", value); }
        }

        public string MapPath
        {
            get { return this.GetValue("MAP_PATH").ToString(); }
            set { this.SetValue("MAP_PATH", value); }
        }

        public int Order
        {
            get { return (int) this.GetValue("ORDER"); }
            set { this.SetValue("ORDER", value); }
        }

        public string GroupId
        {
            get { return this.GetValue("MODULE_ID").ToString(); }
            set { this.SetValue("MODULE_ID", value); }
        }

        public string GroupName
        {
            get { return this.GetValue("MODULE_NAME").ToString(); }
            set { this.SetValue("MODULE_NAME", value); }
        }

        public string GroupIconCls
        {
            get { return this.GetValue("MODULE_ICON_CLS").ToString(); }
            set { this.SetValue("MODULE_ICON_CLS", value); }
        }

        public int GroupOrder
        {
            get { return (int)this.GetValue("MODULE_ORDER"); }
            set { this.SetValue("MODULE_ORDER", value); }
        }

        public string GroupId2
        {
            get { return this.GetValue("MENU_GROUP_ID").ToString(); }
            set { this.SetValue("MENU_GROUP_ID", value); }
        }

        public string GroupName2
        {
            get { return this.GetValue("MENU_GROUP_NAME").ToString(); }
            set { this.SetValue("MENU_GROUP_NAME", value); }
        }

        public string Group2IconCls
        {
            get { return this.GetValue("MENU_GROUP_ICON_CLS").ToString(); }
            set { this.SetValue("MENU_GROUP_ICON_CLS", value); }
        }

        public int Group2Order
        {
            get { return (int)this.GetValue("MENU_GROUP_ORDER"); }
            set { this.SetValue("MENU_GROUP_ORDER", value); }
        }

        public string ExpandType
        {
            get { return this.GetValue("EXPAND_TYPE").ToString(); }
            set { this.SetValue("EXPAND_TYPE", value); }
        }
    }
}

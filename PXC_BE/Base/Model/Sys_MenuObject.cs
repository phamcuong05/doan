using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.Base.Model
{
    public class Sys_MenuObject : ObjectInfoBase
    {
        public Sys_MenuObject() : base()
        {
           
        }

        public Sys_MenuObject(DataRow row) : base(row)
        {
            this.MENU_ID = row["MENU_ID"].ToString();
            this.MENU_NAME = row["MENU_NAME"].ToString();
            this.PROJECT_ID = row["PROJECT_ID"].ToString();
            this.MODULE_ID = row["MODULE_ID"].ToString();
            this.MENU_GROUP_ID = row["MENU_GROUP_ID"].ToString();
            this.ICON_CLS = row["ICON_CLS"].ToString();
            this.HREF =  row["HREF"].ToString();
            this.MAP_PATH = row["MAP_PATH"].ToString();
            this.MENU_TAG = row["MENU_TAG"].ToString();
            this.MENU_ORDER =(int) row["MENU_ORDER"];
            this.EXPAND_TYPE = row["EXPAND_TYPE"].ToString();
            this.MENU_TYPE = (int) row["MENU_TYPE"];
            this.ACTIVE = (Int16) row["ACTIVE"];
        }

        public string MENU_ID
        {
            get { return this.GetValue("MENU_ID").ToString(); }
            set { this.SetValue("MENU_ID", value); }
        }

        public string MENU_NAME
        {
            get { return this.GetValue("MENU_NAME").ToString(); }
            set { this.SetValue("MENU_NAME", value); }
        }

        public string PROJECT_ID
        {
            get { return this.GetValue("PROJECT_ID").ToString(); }
            set { this.SetValue("PROJECT_ID", value); }
        }

        public string MODULE_ID
        {
            get { return this.GetValue("MODULE_ID").ToString(); }
            set { this.SetValue("MODULE_ID", value); }
        }

        public string MENU_GROUP_ID
        {
            get { return this.GetValue("MENU_GROUP_ID").ToString(); }
            set { this.SetValue("MENU_GROUP_ID", value); }
        }

        public string ICON_CLS
        {
            get { return this.GetValue("ICON_CLS").ToString(); }
            set { this.SetValue("ICON_CLS", value); }
        }

        public string HREF
        {
            get { return this.GetValue("HREF").ToString(); }
            set { this.SetValue("HREF", value); }
        }

        public string MAP_PATH
        {
            get { return this.GetValue("MAP_PATH").ToString(); }
            set { this.SetValue("MAP_PATH", value); }
        }

        public string MENU_TAG
        {
            get { return this.GetValue("MENU_TAG").ToString(); }
            set { this.SetValue("MENU_TAG", value); }
        }        

        public int MENU_ORDER
        {
            get { return (int) this.GetValue("MENU_ORDER"); }
            set { this.SetValue("MENU_ORDER", value); }
        }

        public string EXPAND_TYPE
        {
            get { return (string)this.GetValue("EXPAND_TYPE"); }
            set { this.SetValue("EXPAND_TYPE", value); }
        }

        public int MENU_TYPE
        {
            get { return (int)this.GetValue("MENU_TYPE"); }
            set { this.SetValue("MENU_TYPE", value); }
        }

        public Int16 ACTIVE
        {
            get { return this.GetValueOrDefault<Int16>("ACTIVE"); }
            set { this.SetValue("ACTIVE", value); }
        }
    }
}

using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.Base.Model {
    public class Sys_SystemvarObject : ObjectInfoBase
    {
        public Sys_SystemvarObject() : base()
        {
            this.VAR_NAME = string.Empty;
            this.VAR_VALUE = string.Empty;
            this.DESCRIPTION = string.Empty;
            this.VAR_TYPE = string.Empty;
            this.VAR_GROUP = string.Empty;
        }

        public Sys_SystemvarObject(DataRow row) : base(row)
        {
            this.VAR_NAME = row["VAR_NAME"].ToString();
            this.VAR_VALUE = row["VAR_VALUE"].ToString();
            this.DESCRIPTION = row["DESCRIPTION"].ToString();
            this.VAR_TYPE = row["VAR_TYPE"].ToString();
            this.VAR_GROUP = row["VAR_GROUP"].ToString();
        }

        public string VAR_NAME
        {
            get { return this.GetValue("VAR_NAME").ToString(); }
            set { this.SetValue("VAR_NAME", value); }
        }

        public string VAR_VALUE
        {
            get { return this.GetValue("VAR_VALUE").ToString(); }
            set { this.SetValue("VAR_VALUE", value); }
        }

        public string DESCRIPTION
        {
            get { return this.GetValue("DESCRIPTION").ToString(); }
            set { this.SetValue("DESCRIPTION", value); }
        }

        public string VAR_TYPE
        {
            get { return this.GetValue("VAR_TYPE").ToString(); }
            set { this.SetValue("VAR_TYPE", value); }
        }

        public string VAR_GROUP
        {
            get { return this.GetValue("VAR_GROUP").ToString(); }
            set { this.SetValue("VAR_GROUP", value); }
        } 
    }
}
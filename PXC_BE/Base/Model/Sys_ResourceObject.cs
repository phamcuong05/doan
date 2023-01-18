using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.Base.Model
{
    public class Sys_ResourceObject : ObjectInfoBase
    {
        public Sys_ResourceObject() : base()
        {
            this.RES_ID = String.Empty;
            this.RES_VALUE = string.Empty;
        }

        public Sys_ResourceObject(DataRow row) : base(row)
        {
            this.RES_ID = row["RES_ID"].ToString();
            this.RES_VALUE = row["RES_VALUE"].ToString();
        }

        public string RES_ID
        {
            get { return this.GetValue("RES_ID").ToString(); }
            set { this.SetValue("RES_ID", value); }
        }

        public string RES_VALUE
        {
            get { return this.GetValue("RES_VALUE").ToString(); }
            set { this.SetValue("RES_VALUE", value); }
        }
    }
}

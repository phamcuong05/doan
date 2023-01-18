using FTS.Base.Business;
using System;

namespace FTS.Base.Model
{
    public class Dm_Template_DetailObject : ObjectInfoBase
    {
        public Dm_Template_DetailObject()
        {
            this.PR_KEY = 0;
            this.FR_KEY = 0;
            this.LIST_ORDER = 0;
            this.EXCEL_COLUMN_NO = string.Empty;
            this.DATA_COLUMN_NAME = string.Empty;
            this.DATA_TYPE = string.Empty;
            this.IS_PR_KEY = 0;
        }
        public decimal PR_KEY
        {
            get { return this.GetValueOrDefault<decimal>("PR_KEY"); }
            set { this.SetValue("PR_KEY", value); }
        }

        public decimal FR_KEY
        {
            get { return this.GetValueOrDefault<decimal>("FR_KEY"); }
            set { this.SetValue("FR_KEY", value); }
        }

        public int LIST_ORDER
        {
            get { return this.GetValueOrDefault<int>("LIST_ORDER"); }
            set { this.SetValue("LIST_ORDER", value); }
        }

        public string EXCEL_COLUMN_NO
        {
            get { return this.GetValueOrDefault<string>("EXCEL_COLUMN_NO"); }
            set { this.SetValue("EXCEL_COLUMN_NO", value); }
        }

        public string DATA_COLUMN_NAME
        {
            get { return this.GetValueOrDefault<string>("DATA_COLUMN_NAME"); }
            set { this.SetValue("DATA_COLUMN_NAME", value); }
        }

        public string DATA_TYPE
        {
            get { return this.GetValueOrDefault<string>("DATA_TYPE"); }
            set { this.SetValue("DATA_TYPE", value); }
        }

        public Int16 IS_PR_KEY
        {
            get { return this.GetValueOrDefault<Int16>("IS_PR_KEY"); }
            set { this.SetValue("IS_PR_KEY", value); }
        }
    }
}

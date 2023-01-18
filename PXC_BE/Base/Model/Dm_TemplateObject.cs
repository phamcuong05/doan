using FTS.Base.Business;
using System;

namespace FTS.Base.Model
{
    public class Dm_TemplateObject:ObjectInfoBase
    {
        public Dm_TemplateObject() : base()
        {
            this.PR_KEY = 0;
            this.TRAN_ID = string.Empty;
            this.TEMPLATE_NAME = string.Empty;
            this.TABLE_NAME = string.Empty;
            this.TRAN_ID_NAME = string.Empty;
            this.IS_FIRST_ROW_DATA = 0;
            this.ACTIVE = 0;
            this.USER_ID = string.Empty;
        }

        public decimal PR_KEY
        {
            get { return this.GetValueOrDefault<decimal>("PR_KEY"); }
            set { this.SetValue("PR_KEY", value); }
        }

        public string TRAN_ID
        {
            get { return this.GetValueOrDefault<string>("TRAN_ID"); }
            set { this.SetValue("TRAN_ID", value); }
        }

        public string TEMPLATE_NAME
        {
            get { return this.GetValueOrDefault<string>("TEMPLATE_NAME"); }
            set { this.SetValue("TEMPLATE_NAME", value); }
        }

        public string TABLE_NAME
        {
            get { return this.GetValueOrDefault<string>("TABLE_NAME"); }
            set { this.SetValue("TABLE_NAME", value); }
        }

        public string TRAN_ID_NAME
        {
            get { return this.GetValueOrDefault<string>("TRAN_ID_NAME"); }
            set { this.SetValue("TRAN_ID_NAME", value); }
        }

        public Int16 IS_FIRST_ROW_DATA
        {
            get { return this.GetValueOrDefault<Int16>("IS_FIRST_ROW_DATA"); }
            set { this.SetValue("IS_FIRST_ROW_DATA", value); }
        }

        public Int16 ACTIVE
        {
            get { return this.GetValueOrDefault<Int16>("ACTIVE"); }
            set { this.SetValue("ACTIVE", value); }
        }

        public string USER_ID
        {
            get { return this.GetValueOrDefault<string>("USER_ID"); }
            set { this.SetValue("USER_ID", value); }
        }
    }
}

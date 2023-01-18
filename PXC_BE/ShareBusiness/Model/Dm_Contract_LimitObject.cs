using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model
{
    public class Dm_Contract_LimitObject : ObjectInfoBase
    {
        public Dm_Contract_LimitObject() : base()
        {
            this.PR_KEY = Guid.Empty;
            this.ORGANIZATION_ID = string.Empty;
            this.PR_DETAIL_ID = string.Empty;
            this.PR_DETAIL_NAME = string.Empty;
            this.VALID_DATE = DateTime.Now.Date;
            this.AMOUNT = 0;
            this.NOTES = string.Empty;
            this.USER_ID = string.Empty;
            this.CREATE_DATE = DateTime.Now.Date;
            this.MODIFY_DATE = DateTime.Now.Date;
        }

        public Dm_Contract_LimitObject(DataRow row) : base(row)
        {

        }

        public Guid PR_KEY
        {
            get { return this.GetValueOrDefault<Guid>("PR_KEY"); }
            set { this.SetValue("PR_KEY", value); }
        }

        public string ORGANIZATION_ID
        {
            get { return this.GetValueOrDefault<string>("ORGANIZATION_ID"); }
            set { this.SetValue("ORGANIZATION_ID", value); }
        }

        public string PR_DETAIL_ID
        {
            get { return this.GetValueOrDefault<string>("PR_DETAIL_ID"); }
            set { this.SetValue("PR_DETAIL_ID", value); }
        }

        public string PR_DETAIL_NAME
        {
            get { return this.GetValueOrDefault<string>("PR_DETAIL_NAME"); }
            set { this.SetValue("PR_DETAIL_NAME", value); }
        }

        public DateTime VALID_DATE
        {
            get { return this.GetValueOrDefault<DateTime>("VALID_DATE"); }
            set { this.SetValue("VALID_DATE", value); }
        }

        public decimal AMOUNT
        {
            get { return this.GetValueOrDefault<decimal>("AMOUNT"); }
            set { this.SetValue("AMOUNT", value); }
        }

        public string NOTES
        {
            get { return this.GetValueOrDefault<string>("NOTES"); }
            set { this.SetValue("NOTES", value); }
        }

        public string USER_ID
        {
            get { return this.GetValueOrDefault<string>("USER_ID"); }
            set { this.SetValue("USER_ID", value); }
        }

        public DateTime CREATE_DATE
        {
            get { return this.GetValueOrDefault<DateTime>("CREATE_DATE"); }
            set { this.SetValue("CREATE_DATE", value); }
        }

        public DateTime MODIFY_DATE
        {
            get { return this.GetValueOrDefault<DateTime>("MODIFY_DATE"); }
            set { this.SetValue("MODIFY_DATE", value); }
        }
    }
}

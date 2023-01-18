using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model
{
    public class Dm_Advance_LimitObject : ObjectInfoBase
    {
        public Dm_Advance_LimitObject() : base()
        {
            this.PR_KEY = Guid.Empty;
            this.ORGANIZATION_ID = string.Empty;
            this.ACCOUNT_ID = string.Empty;
            this.VALID_DATE = DateTime.Now.Date;
            this.ADVANCE_LIMIT = 0;
        }

        public Dm_Advance_LimitObject(DataRow row) : base(row)
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

        public string ACCOUNT_ID
        {
            get { return this.GetValueOrDefault<string>("ACCOUNT_ID"); }
            set { this.SetValue("ACCOUNT_ID", value); }
        }

        public decimal ADVANCE_LIMIT
        {
            get { return this.GetValueOrDefault<decimal>("ADVANCE_LIMIT"); }
            set { this.SetValue("ADVANCE_LIMIT", value); }
        }

        public DateTime VALID_DATE
        {
            get { return this.GetValueOrDefault<DateTime>("VALID_DATE"); }
            set { this.SetValue("VALID_DATE", value); }
        }
    }
}

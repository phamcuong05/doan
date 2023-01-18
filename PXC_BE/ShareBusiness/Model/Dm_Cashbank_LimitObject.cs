using FTS.Base.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.ShareBusiness.Model
{
    public class Dm_Cashbank_LimitObject : ObjectInfoBase
    {
        public Dm_Cashbank_LimitObject() : base()
        {
            this.PR_KEY = Guid.Empty;
            this.ORGANIZATION_ID = string.Empty;
            this.ORGANIZATION_NAME = string.Empty;
            this.ACCOUNT_ID = string.Empty;
            this.ACCOUNT_NAME = string.Empty;
            this.BANK_ID = string.Empty;
            this.BANK_NAME = string.Empty;
            this.VALID_DATE = DateTime.Today.Date;
            this.LIMIT = 0;
            this.NOTES = string.Empty;
            this.USER_ID = string.Empty;
            this.CREATE_DATE = DateTime.Now.Date;
            this.MODIFY_DATE = DateTime.Now.Date;
        }

        public Dm_Cashbank_LimitObject(DataRow row) : base(row)
        {

        }

        public Guid PR_KEY
        {
            get { return (Guid)this.GetValue("PR_KEY"); }
            set { this.SetValue("PR_KEY", value); }
        }

        public string ORGANIZATION_ID {
            get { return this.GetValueOrDefault<string>("ORGANIZATION_ID"); }
            set { this.SetValue("ORGANIZATION_ID", value); }
        }

        public string ORGANIZATION_NAME
        {
            get { return this.GetValueOrDefault<string>("ORGANIZATION_NAME"); }
            set { this.SetValue("ORGANIZATION_NAME", value); }
        }

        public string ACCOUNT_ID
        {
            get { return this.GetValueOrDefault<string>("ACCOUNT_ID"); }
            set { this.SetValue("ACCOUNT_ID", value); }
        }

        public string ACCOUNT_NAME
        {
            get { return this.GetValueOrDefault<string>("ACCOUNT_NAME"); }
            set { this.SetValue("ACCOUNT_NAME", value); }
        }

        public string BANK_ID
        {
            get { return this.GetValueOrDefault<string>("BANK_ID"); }
            set { this.SetValue("BANK_ID", value); }
        }

        public string BANK_NAME
        {
            get { return this.GetValueOrDefault<string>("BANK_NAME"); }
            set { this.SetValue("BANK_NAME", value); }
        }

        public DateTime VALID_DATE
        {
            get { return this.GetValueOrDefault<DateTime>("VALID_DATE"); }
            set { this.SetValue("VALID_DATE", value); }

        }

        public decimal LIMIT {
            get { return this.GetValueOrDefault<decimal>("LIMIT"); }
            set { this.SetValue("LIMIT", value); }
        }

        public string NOTES {
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

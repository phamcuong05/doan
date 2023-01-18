using FTS.Base.Business;
using System;
using System.Data;

namespace FTS.ShareBusiness.Model
{
    public class Dm_ClaimObject : ObjectInfoBase
    {
        public Dm_ClaimObject() : base()
        {
            this.PR_KEY = Guid.Empty;
            this.ORGANIZATION_ID = string.Empty;
            this.TRAN_NO = string.Empty;
            this.TRAN_DATE = DateTime.Now;
            this.STATUS = string.Empty;
        }

        public Dm_ClaimObject(DataRow row) : base(row)
        {

        }

        public Guid PR_KEY
        {
            get { return this.GetValueOrDefault<Guid>("PR_KEY"); }
            set { this.SetValue("PR_KEY", value); }
        }


        public string TRAN_NO
        {
            get { return this.GetValueOrDefault<string>("TRAN_NO"); }
            set { this.SetValue("TRAN_NO", value); }
        }

        public string CLAIM_NO
        {
            get { return this.GetValueOrDefault<string>("CLAIM_NO"); }
            set { this.SetValue("CLAIM_NO", value); }
        }


        public DateTime TRAN_DATE
        {
            get { return this.GetValueOrDefault<DateTime>("TRAN_DATE"); }
            set { this.SetValue("TRAN_DATE", value); }
        }


        public string ORGANIZATION_ID
        {
            get { return this.GetValueOrDefault<string>("ORGANIZATION_ID"); }
            set { this.SetValue("ORGANIZATION_ID", value); }
        }


        public string STATUS
        {
            get { return this.GetValueOrDefault<string>("STATUS"); }
            set { this.SetValue("STATUS", value); }
        }

    }
}

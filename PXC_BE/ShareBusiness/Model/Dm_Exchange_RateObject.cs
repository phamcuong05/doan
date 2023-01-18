using FTS.Base.Business;
using System;
using System.Data;

namespace FTS.ShareBusiness.Model
{
    public class Dm_Exchange_RateObject : ObjectInfoBase
    {
        public Dm_Exchange_RateObject() : base()
        {
            this.PR_KEY = Guid.Empty;
            this.CURRENCY_ID = string.Empty;
            this.TO_CURRENCY_ID = string.Empty;
        }

        public Dm_Exchange_RateObject(DataRow row) : base(row)
        {

        }

        public Guid PR_KEY
        {
            get { return this.GetValueOrDefault<Guid>("PR_KEY"); }
            set { this.SetValue("PR_KEY", value); }
        }

        public string CURRENCY_ID
        {
            get { return this.GetValueOrDefault<string>("CURRENCY_ID"); }
            set { this.SetValue("CURRENCY_ID", value); }
        }

        public string TO_CURRENCY_ID
        {
            get { return this.GetValueOrDefault<string>("TO_CURRENCY_ID"); }
            set { this.SetValue("TO_CURRENCY_ID", value); }
        }

        public decimal EXCHANGE_RATE
        {
            get { return this.GetValueOrDefault<decimal>("EXCHANGE_RATE"); }
            set { this.SetValue("EXCHANGE_RATE", value); }
        }

        public DateTime VALID_DATE
        {
            get { return this.GetValueOrDefault<DateTime>("VALID_DATE"); }
            set { this.SetValue("VALID_DATE", value); }
        }
    }
}

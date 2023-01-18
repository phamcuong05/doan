using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model
{
    public class Dm_Vat_TaxObject : ObjectInfoBase
    {
        public Dm_Vat_TaxObject() : base()
        {
            this.VAT_TAX_ID = string.Empty;
            this.VAT_TAX_NAME = string.Empty;
            this.VAT_TAX_RATE = 0;
            this.ACTIVE = 1;
            this.USER_ID = "ADMIN";
        }

        public Dm_Vat_TaxObject(DataRow row) : base(row)
        {

        }

        public string VAT_TAX_ID
        {
            get { return this.GetValueOrDefault<string>("VAT_TAX_ID"); }
            set { this.SetValue("VAT_TAX_ID", value); }
        }

        public string VAT_TAX_NAME
        {
            get { return this.GetValueOrDefault<string>("VAT_TAX_NAME"); }
            set { this.SetValue("VAT_TAX_NAME", value); }
        }

        public decimal VAT_TAX_RATE
        {
            get { return this.GetValueOrDefault<decimal>("VAT_TAX_RATE"); }
            set { this.SetValue("VAT_TAX_RATE", value); }
        }

        public string USER_ID
        {
            get { return this.GetValueOrDefault<string>("USER_ID"); }
            set { this.SetValue("USER_ID", value); }
        }

        public Int16 ACTIVE
        {
            get { return this.GetValueOrDefault<Int16>("ACTIVE"); }
            set { this.SetValue("ACTIVE", value); }
        }
    }
}

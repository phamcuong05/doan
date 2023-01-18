using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model
{
    public class Dm_CurrencyObject : ObjectInfoBase {
        public Dm_CurrencyObject() : base() {
            this.CURRENCY_ID = string.Empty;
            this.CURRENCY_NAME = string.Empty;
            this.ACTIVE = 1;
            this.USER_ID = "ADMIN";
        }

        public Dm_CurrencyObject(DataRow row) : base(row) {

        }

        public string CURRENCY_ID
        {
            get { return this.GetValueOrDefault<string>("CURRENCY_ID"); }
            set { this.SetValue("CURRENCY_ID", value); }
        }

        public string CURRENCY_NAME
        {
            get { return this.GetValueOrDefault<string>("CURRENCY_NAME"); }
            set { this.SetValue("CURRENCY_NAME", value); }
        }

        public string USER_ID {
            get { return this.GetValueOrDefault<string>("User_ID"); }
            set { this.SetValue("User_ID", value); }
        }

        public Int16 ACTIVE {
            get { return this.GetValueOrDefault<Int16>("Active"); }
            set { this.SetValue("Active", value); }
        }
    }
}

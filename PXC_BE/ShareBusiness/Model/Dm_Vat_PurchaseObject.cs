using FTS.Base.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.ShareBusiness.Model
{
    public class Dm_Vat_PurchaseObject : ObjectInfoBase
    {
        public Dm_Vat_PurchaseObject() : base()
        {
            this.VAT_PURCHASE_ID = string.Empty;
            this.VAT_PURCHASE_NAME = string.Empty;
            this.ACTIVE = 1;
            this.USER_ID = "ADMIN";
        }

        public Dm_Vat_PurchaseObject(DataRow row) : base(row)
        {

        }

        public string VAT_PURCHASE_ID {
            get { return this.GetValue("VAT_PURCHASE_ID").ToString(); }
            set { this.SetValue("VAT_PURCHASE_ID", value); }
        }
        public string VAT_PURCHASE_NAME {
            get { return this.GetValueOrDefault<string>("VAT_PURCHASE_NAME"); }
            set { this.SetValue("VAT_PURCHASE_NAME", value); }
        }
        public Int16 ACTIVE {
            get { return this.GetValueOrDefault<Int16>("ACTIVE"); }
            set { this.SetValue("ACTIVE", value); }
        }
        public string USER_ID {
            get { return this.GetValueOrDefault<string>("USER_ID"); }
            set { this.SetValue("USER_ID", value); }
        }
    }
}

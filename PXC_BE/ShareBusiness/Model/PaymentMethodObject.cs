using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model {
    public class PaymentMethodObject : ObjectInfoBase {
        public PaymentMethodObject() : base() {
            this.PAYMENT_METHOD_ID = string.Empty;
            this.PAYMENT_METHOD_NAME = string.Empty;
        }

        public PaymentMethodObject(string paymentmethodid, string paymentmethodname) {
            this.PAYMENT_METHOD_ID = paymentmethodid;
            this.PAYMENT_METHOD_NAME = paymentmethodname;
        }

        public string PAYMENT_METHOD_ID
        {
            get { return this.GetValueOrDefault<string>("PAYMENT_METHOD_ID"); }
            set { this.SetValue("PAYMENT_METHOD_ID", value); }
        }

        public string PAYMENT_METHOD_NAME
        {
            get { return this.GetValueOrDefault<string>("PAYMENT_METHOD_NAME"); }
            set { this.SetValue("PAYMENT_METHOD_NAME", value); }
        }
    }
}

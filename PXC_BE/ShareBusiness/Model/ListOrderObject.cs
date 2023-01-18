using FTS.Base.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.ShareBusiness.Model
{
    public class ListOrderObject : ObjectInfoBase
    {
        public ListOrderObject() : base()
        {
            this.ORDER_ID = string.Empty;
            this.PACKAGE_CODE = string.Empty;
            this.PACKAGE_NAME = string.Empty;
            this.SERVICE_CHARGE_ID = string.Empty;
            this.SERVICE_CHARGE_NAME = string.Empty;
            this.BUY_FEE = 0;
            this.SHIP_FEE = 0;
            this.TOTAL = 0;
            this.ORDER_DATE = DateTime.Today;
            this.CUSTOMER_NAME = string.Empty;
            this.PHONE = string.Empty;
            this.ADDRESS = string.Empty;
            this.USER_ID = "ADMIN";

        }

        public ListOrderObject(DataRow row) : base(row)
        {

        }
        public string ORDER_ID
        {
            get { return this.GetValueOrDefault<string>("ORDER_ID"); }
            set { this.SetValue("ORDER_ID", value); }
        }

        public string PACKAGE_CODE
        {
            get { return this.GetValueOrDefault<string>("PACKAGE_CODE"); }
            set { this.SetValue("PACKAGE_CODE", value); }
        }

        public string PACKAGE_NAME
        {
            get { return this.GetValueOrDefault<string>("PACKAGE_NAME"); }
            set { this.SetValue("PACKAGE_NAME", value); }
        }

        public string CUSTOMER_NAME
        {
            get { return this.GetValueOrDefault<string>("CUSTOMER_NAME"); }
            set { this.SetValue("CUSTOMER_NAME", value); }
        }

        public string SERVICE_CHARGE_ID
        {
            get { return this.GetValueOrDefault<string>("SERVICE_CHARGE_ID"); }
            set { this.SetValue("SERVICE_CHARGE_ID", value); }
        }

        public string SERVICE_CHARGE_NAME
        {
            get { return this.GetValueOrDefault<string>("SERVICE_CHARGE_NAME"); }
            set { this.SetValue("SERVICE_CHARGE_NAME", value); }
        }

        public decimal BUY_FEE
        {
            get { return this.GetValueOrDefault<decimal>("BUY_FEE"); }
            set { this.SetValue("BUY_FEE", value); }
        }

        public decimal SHIP_FEE
        {
            get { return this.GetValueOrDefault<decimal>("SHIP_FEE"); }
            set { this.SetValue("SHIP_FEE", value); }
        }

        public decimal TOTAL
        {
            get { return this.GetValueOrDefault<decimal>("TOTAL"); }
            set { this.SetValue("TOTAL", value); }
        }

        public DateTime ORDER_DATE
        {
            get { return this.GetValueOrDefault<DateTime>("ORDER_DATE"); }
            set { this.SetValue("ORDER_DATE", value); }
        }


        public string USER_ID
        {
            get { return this.GetValueOrDefault<string>("USER_ID"); }
            set { this.SetValue("USER_ID", value); }
        }

        public string PHONE
        {
            get { return this.GetValueOrDefault<string>("PHONE"); }
            set { this.SetValue("PHONE", value); }
        }


        public string ADDRESS
        {
            get { return this.GetValueOrDefault<string>("ADDRESS"); }
            set { this.SetValue("ADDRESS", value); }
        }

    }
}


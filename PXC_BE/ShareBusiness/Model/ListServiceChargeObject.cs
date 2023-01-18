using FTS.Base.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.ShareBusiness.Model
{
    public class ListServiceChargeObject : ObjectInfoBase
    {
        public ListServiceChargeObject() : base()
        {
            this.SERVICE_CHARGE_ID = string.Empty;
            this.SERVICE_CHARGE_NAME = string.Empty;
            this.DESCRIPTION = string.Empty;
            this.SHIP_FEE = 0;
            this.CREATE_DATE = DateTime.Today;
            this.MODIFIED_DATE = DateTime.Today;
            this.USER_ID = "ADMIN";
            this.REGION = string.Empty;
        }

        public ListServiceChargeObject(DataRow row) : base(row)
        {

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

        public string DESCRIPTION
        {
            get { return this.GetValueOrDefault<string>("DESCRIPTION"); }
            set { this.SetValue("DESCRIPTION", value); }
        }


        public decimal SHIP_FEE
        {
            get { return this.GetValueOrDefault<decimal>("SHIP_FEE"); }
            set { this.SetValue("SHIP_FEE", value); }
        }

        public DateTime CREATE_DATE
        {
            get { return this.GetValueOrDefault<DateTime>("CREATE_DATE"); }
            set { this.SetValue("CREATE_DATE", value); }
        }

        public DateTime MODIFIED_DATE
        {
            get { return this.GetValueOrDefault<DateTime>("MODIFIED_DATE"); }
            set { this.SetValue("MODIFIED_DATE", value); }
        }

        public string USER_ID
        {
            get { return this.GetValueOrDefault<string>("USER_ID"); }
            set { this.SetValue("USER_ID", value); }
        }

       
        public string REGION
        {
            get { return this.GetValueOrDefault<string>("REGION"); }
            set { this.SetValue("REGION", value); }
        }
    }
}

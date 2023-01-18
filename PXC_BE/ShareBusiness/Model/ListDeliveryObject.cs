using FTS.Base.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.ShareBusiness.Model
{
    public class ListDeliveryObject : ObjectInfoBase
    {
        public ListDeliveryObject() : base()
        {
            this.WARE_HOUSE_ID = string.Empty;
            this.WARE_HOUSE_NAME = string.Empty;
            this.ORGANIZATION_ID = string.Empty;
            this.ORGANIZATION_NAME = string.Empty;
            this.FROM_DATE = DateTime.Today;
            this.NOTE = string.Empty;
            this.USER_ID = "ADMIN";
            this.TOTAL_ORDER = string.Empty;
            this.WEIGHT = 0;


        }

        public ListDeliveryObject(DataRow row) : base(row)
        {

        }        

        public DateTime FROM_DATE
        {
            get { return this.GetValueOrDefault<DateTime>("FROM_DATE"); }
            set { this.SetValue("FROM_DATE", value); }
        }

        public string USER_ID
        {
            get { return this.GetValueOrDefault<string>("USER_ID"); }
            set { this.SetValue("USER_ID", value); }
        }

        public string WARE_HOUSE_ID
        {
            get { return this.GetValueOrDefault<string>("WARE_HOUSE_ID"); }
            set { this.SetValue("WARE_HOUSE_ID", value); }
        }


        public string WARE_HOUSE_NAME
        {
            get { return this.GetValueOrDefault<string>("WARE_HOUSE_NAME"); }
            set { this.SetValue("WARE_HOUSE_NAME", value); }
        }

        public string ORGANIZATION_ID
        {
            get { return this.GetValueOrDefault<string>("ORGANIZATION_ID"); }
            set { this.SetValue("ORGANIZATION_ID", value); }
        }

        public string ORGANIZATION_NAME
        {
            get { return this.GetValueOrDefault<string>("ORGANIZATION_NAME"); }
            set { this.SetValue("ORGANIZATION_NAME", value); }
        }

        public string NOTE
        {
            get { return this.GetValueOrDefault<string>("NOTE"); }
            set { this.SetValue("NOTE", value); }
        }

        public string TOTAL_ORDER
        {
            get { return this.GetValueOrDefault<string>("TOTAL_ORDER"); }
            set { this.SetValue("TOTAL_ORDER", value); }
        }

        public decimal WEIGHT
        {
            get { return this.GetValueOrDefault<decimal>("WEIGHT"); }
            set { this.SetValue("WEIGHT", value); }
        }

    }
}

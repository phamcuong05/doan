using FTS.Base.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.ShareBusiness.Model
{
    public class ListWHObject : ObjectInfoBase
    {
        public ListWHObject() : base()
        {
            this.WARE_HOUSE_ID = string.Empty;
            this.WARE_HOUSE_NAME = string.Empty;
            this.CUSTOMER_NAME = string.Empty;
            this.CONTAINER_ID = string.Empty;
            this.CONTAINER_NAME = string.Empty;
            this.ORGANIZATION_ID = string.Empty;
            this.ORGANIZATION_NAME = string.Empty;
            this.TO_DATE = DateTime.Today;
            this.FROM_DATE = DateTime.Today;
            this.NOTE = string.Empty;
            this.USER_ID = "ADMIN";
            this.TOTAL_ORDER = string.Empty;
            this.WEIGHT = 0;
            this.MAWB_ID = string.Empty;
            this.MAWB_NAME = string.Empty;


        }

        public ListWHObject(DataRow row) : base(row)
        {

        }

        public string CONTAINER_ID
        {
            get { return this.GetValueOrDefault<string>("CONTAINER_ID"); }
            set { this.SetValue("CONTAINER_ID", value); }
        }

        public string CONTAINER_NAME
        {
            get { return this.GetValueOrDefault<string>("CONTAINER_NAME"); }
            set { this.SetValue("CONTAINER_NAME", value); }
        }

        public string CUSTOMER_NAME
        {
            get { return this.GetValueOrDefault<string>("CUSTOMER_NAME"); }
            set { this.SetValue("CUSTOMER_NAME", value); }
        }

        public DateTime TO_DATE
        {
            get { return this.GetValueOrDefault<DateTime>("TO_DATE"); }
            set { this.SetValue("TO_DATE", value); }
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

        public string MAWB_ID
        {
            get { return this.GetValueOrDefault<string>("MAWB_ID"); }
            set { this.SetValue("MAWB_ID", value); }
        }

        public string MAWB_NAME
        {
            get { return this.GetValueOrDefault<string>("MAWB_NAME"); }
            set { this.SetValue("MAWB_NAME", value); }
        }

    }
}

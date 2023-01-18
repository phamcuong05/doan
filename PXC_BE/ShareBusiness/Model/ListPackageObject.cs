using FTS.Base.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.ShareBusiness.Model
{
    public class ListPackageObject : ObjectInfoBase
    {
        public ListPackageObject() : base()
        {
            this.PACKAGE_CODE = string.Empty;
            this.PACKAGE_NAME = string.Empty;
            this.TRACKING_CODE = string.Empty;
            this.WEIGHT = 0;
            this.CREATE_DATE = DateTime.Today;
            this.MODIFIED_DATE = DateTime.Today;
            this.USER_ID = "ADMIN";
            this.CONTAINER_ID = string.Empty;
            this.WARE_HOUSE_ID = string.Empty;
            this.SERVICE_CHARGE_CODE = string.Empty;
            this.MAWB_ID = string.Empty;
            this.ITEM = string.Empty;

        }

        public ListPackageObject(DataRow row) : base(row)
        {

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

        public string TRACKING_CODE
        {
            get { return this.GetValueOrDefault<string>("TRACKING_CODE"); }
            set { this.SetValue("TRACKING_CODE", value); }
        }

        public decimal WEIGHT
        {
            get { return this.GetValueOrDefault<decimal>("WEIGHT"); }
            set { this.SetValue("WEIGHT", value); }
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


        public string CONTAINER_ID
        {
            get { return this.GetValueOrDefault<string>("CONTAINER_ID"); }
            set { this.SetValue("CONTAINER_ID", value); }
        }

        public string WARE_HOUSE_ID
        {
            get { return this.GetValueOrDefault<string>("WARE_HOUSE_ID"); }
            set { this.SetValue("WARE_HOUSE_ID", value); }
        }

        public string SERVICE_CHARGE_CODE
        {
            get { return this.GetValueOrDefault<string>("SERVICE_CHARGE_CODE"); }
            set { this.SetValue("SERVICE_CHARGE_CODE", value); }
        }

        public string MAWB_ID
        {
            get { return this.GetValueOrDefault<string>("MAWB_ID"); }
            set { this.SetValue("MAWB_ID", value); }
        }

        public string ITEM
        {
            get { return this.GetValueOrDefault<string>("ITEM"); }
            set { this.SetValue("ITEM", value); }
        }
    }
}

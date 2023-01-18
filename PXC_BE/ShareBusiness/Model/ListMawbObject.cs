using FTS.Base.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.ShareBusiness.Model
{
    public class ListMawbObject : ObjectInfoBase
    {
        public ListMawbObject() : base()
        {
            this.MAWB_ID = string.Empty;
            this.MAWB_NAME = string.Empty;
            this.DEPARTURE = string.Empty;
            this.DESTINATION = string.Empty;
            this.CREATE_DATE = DateTime.Today;
            this.USER_ID = "ADMIN";
            this.TOTAL_ORDER = string.Empty;
            this.WEIGHT = 0;


        }

        public ListMawbObject(DataRow row) : base(row)
        {

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

        public string DEPARTURE
        {
            get { return this.GetValueOrDefault<string>("DEPARTURE"); }
            set { this.SetValue("DEPARTURE", value); }
        }

        public string DESTINATION
        {
            get { return this.GetValueOrDefault<string>("DESTINATION"); }
            set { this.SetValue("DESTINATION", value); }
        }


        public DateTime CREATE_DATE
        {
            get { return this.GetValueOrDefault<DateTime>("CREATE_DATE"); }
            set { this.SetValue("CREATE_DATE", value); }
        }


        public string USER_ID
        {
            get { return this.GetValueOrDefault<string>("USER_ID"); }
            set { this.SetValue("USER_ID", value); }
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

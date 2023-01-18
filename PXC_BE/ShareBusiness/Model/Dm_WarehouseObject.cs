using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Acc
{
    public class Dm_WarehouseObject : ObjectInfoBase
    {
        public Dm_WarehouseObject() : base()
        {
            this.WAREHOUSE_ID = string.Empty;
            this.WAREHOUSE_NAME = string.Empty;
            this.DEPARTMENT_ID = string.Empty;
            this.DEPARTMENT_NAME = string.Empty;
            this.IS_USE_WAREHOUSE = 1;
            this.ACTIVE = 1;
            this.USER_ID = "ADMIN";
        }

        public Dm_WarehouseObject(DataRow row) : base(row)
        {

        }

        public string WAREHOUSE_ID
        {
            get { return this.GetValueOrDefault<string>("WAREHOUSE_ID"); }
            set { this.SetValue("WAREHOUSE_ID", value); }
        }

        public string WAREHOUSE_NAME
        {
            get { return this.GetValueOrDefault<string>("WAREHOUSE_NAME"); }
            set { this.SetValue("WAREHOUSE_NAME", value); }
        }

        public string DEPARTMENT_ID
        {
            get { return this.GetValueOrDefault<string>("DEPARTMENT_ID"); }
            set { this.SetValue("DEPARTMENT_ID", value); }
        }

        public string DEPARTMENT_NAME
        {
            get { return this.GetValueOrDefault<string>("DEPARTMENT_NAME"); }
            set { this.SetValue("DEPARTMENT_NAME", value); }
        }



        public Int16 IS_USE_WAREHOUSE
        {
            get { return this.GetValueOrDefault<Int16>("IS_USE_WAREHOUSE"); }
            set { this.SetValue("IS_USE_WAREHOUSE", value); }
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

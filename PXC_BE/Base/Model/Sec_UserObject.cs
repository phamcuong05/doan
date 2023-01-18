using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.Base.Model
{
    public class Sec_UserObject : ObjectInfoBase
    {
        public Sec_UserObject() : base()
        {
            this.USER_ID = string.Empty;
            this.USER_GROUP_ID = string.Empty;
            this.USER_NAME = string.Empty;
            this.USER_PASSWORD = string.Empty;
            this.EMPLOYEE_ID = string.Empty;
            this.EMPLOYEE_NAME = string.Empty;
            this.ORGANIZATION_ID = string.Empty;
            this.ORGANIZATION_NAME = string.Empty;
            this.ACTIVE = 1;
            this.USER_KEY = string.Empty;
            this.LOGIN_DATE = DateTime.Now;
            this.QUANTITY_INVALID = 0;
        }

        public Sec_UserObject(DataRow row) : base(row)
        {
            this.USER_ID = row["USER_ID"].ToString();
            this.USER_GROUP_ID = row["USER_GROUP_ID"].ToString();
            this.USER_NAME = row["USER_NAME"].ToString();
            this.USER_PASSWORD = row["USER_PASSWORD"].ToString();
            this.EMPLOYEE_ID = row["EMPLOYEE_ID"].ToString();
            this.EMPLOYEE_NAME = row["EMPLOYEE_NAME"].ToString();
            this.ORGANIZATION_ID = row["ORGANIZATION_ID"].ToString();
            this.ORGANIZATION_NAME = row["ORGANIZATION_NAME"].ToString();
            this.ACTIVE = (Int16)row["ACTIVE"];
            this.QUANTITY_INVALID = (Int16)row["QUANTITY_INVALID"];
            this.LOGIN_DATE = (DateTime)row["LOGIN_DATE"];
        }

        public string USER_ID
        {
            get { return this.GetValueOrDefault<string>("USER_ID"); }
            set { this.SetValue("USER_ID", value); }
        }

        public string USER_GROUP_ID
        {
            get { return this.GetValueOrDefault<string>("USER_GROUP_ID"); }
            set { this.SetValue("USER_GROUP_ID", value); }
        }

        public string USER_NAME
        {
            get { return this.GetValueOrDefault<string>("USER_NAME"); }
            set { this.SetValue("USER_NAME", value); }
        }

        public string USER_PASSWORD
        {
            get { return this.GetValueOrDefault<string>("USER_PASSWORD"); }
            set { this.SetValue("USER_PASSWORD", value); }
        }

        public string EMPLOYEE_ID
        {
            get { return this.GetValueOrDefault<string>("EMPLOYEE_ID"); }
            set { this.SetValue("EMPLOYEE_ID", value); }
        }

        public string EMPLOYEE_NAME
        {
            get { return this.GetValueOrDefault<string>("EMPLOYEE_NAME"); }
            set { this.SetValue("EMPLOYEE_NAME", value); }
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

        public Int16 ACTIVE
        {
            get { return this.GetValueOrDefault<Int16>("ACTIVE"); }
            set { this.SetValue("ACTIVE", value); }
        }

        public string USER_KEY
        {
            get { return this.GetValueOrDefault<string>("USER_KEY"); }
            set { this.SetValue("USER_KEY", value); }
        }

        public DateTime LOGIN_DATE
        {
            get { return this.GetValueOrDefault<DateTime>("LOGIN_DATE"); }
            set { this.SetValue("LOGIN_DATE", value); }
        }

        public int QUANTITY_INVALID
        {
            get { return this.GetValueOrDefault<int>("QUANTITY_INVALID"); }
            set { this.SetValue("QUANTITY_INVALID", value); }
        }
    }
}

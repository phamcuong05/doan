using FTS.Base.Business;
using System;
using System.Data;

namespace FTS.ShareBusiness.Model
{
    public class Dm_EmployeeObject : ObjectInfoBase
    {
        public Dm_EmployeeObject() : base()
        {
            this.EMPLOYEE_ID = string.Empty;
            this.EMPLOYEE_NAME = string.Empty;
            this.DEPARTMENT_ID = string.Empty;
            this.DEPARTMENT_NAME = string.Empty;
            this.ADDRESS = string.Empty;
            this.EMAIL = string.Empty;
            this.PHONE = string.Empty;
            this.IDENTITY_NO = string.Empty;
            this.ACTIVE = 0;
            this.USER_ID = string.Empty;
        }

        public Dm_EmployeeObject(DataRow row) : base(row)
        {

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

        public string ADDRESS
        {
            get { return this.GetValueOrDefault<string>("ADDRESS"); }
            set { this.SetValue("ADDRESS", value); }
        }


        public string PHONE
        {
            get { return this.GetValueOrDefault<string>("PHONE"); }
            set { this.SetValue("PHONE", value); }
        }


        public string EMAIL
        {
            get { return this.GetValueOrDefault<string>("EMAIL"); }
            set { this.SetValue("EMAIL", value); }
        }

        public string IDENTITY_NO
        {
            get { return this.GetValueOrDefault<string>("IDENTITY_NO"); }
            set { this.SetValue("IDENTITY_NO", value); }
        }


        public Int16 ACTIVE
        {
            get { return this.GetValueOrDefault<Int16>("ACTIVE"); }
            set { this.SetValue("ACTIVE", value); }
        }

        public string USER_ID
        {
            get { return this.GetValueOrDefault<string>("USER_ID"); }
            set { this.SetValue("USER_ID", value); }
        }
    }
}

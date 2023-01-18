using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.Base.Model
{
    public class Dm_OrganizarionObject : ObjectInfoBase
    {
        public Dm_OrganizarionObject() : base()
        {
            this.ORGANIZATION_ID = string.Empty;
            this.ORGANIZATION_NAME = string.Empty;
            this.ORGANIZATION_NAME_DISPLAY = string.Empty;
            this.PARENT_ORGANIZATION_ID = string.Empty;
            this.PARENT_ORGANIZATION_NAME = string.Empty;
            this.ORGANIZATION_TYPE = string.Empty;
            this.ADDRESS = string.Empty;
            this.CITY = string.Empty;
            this.DISTRICT = string.Empty;
            this.PHONE = string.Empty;
            this.FAX = string.Empty;
            this.EMAIL = string.Empty;
            this.TAX_FILE_NUMBER = string.Empty;
            this.ACTIVE = 1;
            this.ORGANIZATION_NAME_SHORT = string.Empty;
            this.DIRECTOR = string.Empty;
            this.ACCOUNTANT = string.Empty;
            this.CHIEF_ACCOUNTANT = string.Empty;
            this.BANK_NAME = string.Empty;
            this.BANK_ACCOUNT = string.Empty;
            this.BANK_BRANCH = string.Empty;
            this.CASHIER = string.Empty;
        }

        public Dm_OrganizarionObject(DataRow row) : base(row)
        {
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

        public string ORGANIZATION_NAME_DISPLAY
        {
            get { return this.GetValueOrDefault<string>("ORGANIZATION_NAME_DISPLAY"); }
            set { this.SetValue("ORGANIZATION_NAME_DISPLAY", value); }
        }

        public string PARENT_ORGANIZATION_ID
        {
            get { return this.GetValueOrDefault<string>("PARENT_ORGANIZATION_ID"); }
            set { this.SetValue("PARENT_ORGANIZATION_ID", value); }
        }

        public string PARENT_ORGANIZATION_NAME
        {
            get { return this.GetValueOrDefault<string>("PARENT_ORGANIZATION_NAME"); }
            set { this.SetValue("PARENT_ORGANIZATION_NAME", value); }
        }


        public string ORGANIZATION_TYPE
        {
            get { return this.GetValueOrDefault<string>("ORGANIZATION_TYPE"); }
            set { this.SetValue("ORGANIZATION_TYPE", value); }
        }

        public string ADDRESS
        {
            get { return this.GetValueOrDefault<string>("ADDRESS"); }
            set { this.SetValue("ADDRESS", value); }
        }

        public string CITY
        {
            get { return this.GetValueOrDefault<string>("CITY"); }
            set { this.SetValue("CITY", value); }
        }

        public string DISTRICT
        {
            get { return this.GetValueOrDefault<string>("DISTRICT"); }
            set { this.SetValue("DISTRICT", value); }
        }

        public string PHONE
        {
            get { return this.GetValueOrDefault<string>("PHONE"); }
            set { this.SetValue("PHONE", value); }
        }

        public string FAX
        {
            get { return this.GetValueOrDefault<string>("FAX"); }
            set { this.SetValue("FAX", value); }
        }

        public string EMAIL
        {
            get { return this.GetValueOrDefault<string>("EMAIL"); }
            set { this.SetValue("EMAIL", value); }
        }

        public string TAX_FILE_NUMBER
        {
            get { return this.GetValueOrDefault<string>("TAX_FILE_NUMBER"); }
            set { this.SetValue("TAX_FILE_NUMBER", value); }
        }

        public string ORGANIZATION_NAME_SHORT
        {
            get { return this.GetValueOrDefault<string>("ORGANIZATION_NAME_SHORT"); }
            set { this.SetValue("ORGANIZATION_NAME_SHORT", value); }
        }

        public string DIRECTOR
        {
            get { return this.GetValueOrDefault<string>("DIRECTOR"); }
            set { this.SetValue("DIRECTOR", value); }
        }

        public string ACCOUNTANT
        {
            get { return this.GetValueOrDefault<string>("ACCOUNTANT"); }
            set { this.SetValue("ACCOUNTANT", value); }
        }

        public string CHIEF_ACCOUNTANT
        {
            get { return this.GetValueOrDefault<string>("CHIEF_ACCOUNTANT"); }
            set { this.SetValue("CHIEF_ACCOUNTANT", value); }
        }

        public string BANK_NAME
        {
            get { return this.GetValueOrDefault<string>("BANK_NAME"); }
            set { this.SetValue("BANK_NAME", value); }
        }

        public string BANK_ACCOUNT
        {
            get { return this.GetValueOrDefault<string>("BANK_ACCOUNT"); }
            set { this.SetValue("BANK_ACCOUNT", value); }
        }

        public string BANK_BRANCH
        {
            get { return this.GetValueOrDefault<string>("BANK_BRANCH"); }
            set { this.SetValue("BANK_BRANCH", value); }
        }

        public string CASHIER
        {
            get { return this.GetValueOrDefault<string>("CASHIER"); }
            set { this.SetValue("CASHIER", value); }
        }

        public Int16 ACTIVE
        {
            get { return this.GetValueOrDefault<Int16>("ACTIVE"); }
            set { this.SetValue("ACTIVE", value); }
        }
    }
}

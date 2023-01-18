using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Acc
{
    public class Dm_AccountObject : ObjectInfoBase
    {
        public Dm_AccountObject() : base()
        {
            this.ACCOUNT_ID = string.Empty;
            this.ACCOUNT_NAME = string.Empty;
            this.ACTIVE = 1;
            this.USER_ID = "ADMIN";
            this.IS_PARENT = 0;
            this.PARENT_ACCOUNT_ID = string.Empty;
            this.BALANCE_TYPE = string.Empty;
            this.CURRENCY_ID = string.Empty;
            this.CURRENCY_NAME = string.Empty;
            this.RATE_METHOD = string.Empty;
            this.IS_OOB = 0;
            this.IS_PR_DETAIL = 0;
            this.IS_EXPENSE = 0;
            this.IS_JOB = 0;
            this.IS_BANK = 0;
            this.IS_EMPLOYEE = 0;
            this.IS_DEPARTMENT = 0;
            this.IS_AGENT = 0;
            this.IS_INSURANCE_SOURCE = 0;
            this.IS_CAPITAL_SOURCE = 0;
            this.IS_REINSURANCE_SOURCE = 0;
            this.IS_VAT = 0;
            this.IS_CONTRACT = 0;
            this.PARENT_ACCOUNT_NAME = "";
            this.IS_ITEM = 0;
        }

        public Dm_AccountObject(DataRow row) : base(row)
        {

        }

        public String ACCOUNT_ID
        {
            get { return this.GetValueOrDefault<string>("ACCOUNT_ID"); }
            set { this.SetValue("ACCOUNT_ID", value); }
        }
        public String ACCOUNT_NAME
        {
            get { return this.GetValueOrDefault<string>("ACCOUNT_NAME"); }
            set { this.SetValue("ACCOUNT_NAME", value); }
        }
        public Int16 IS_PARENT
        {
            get { return this.GetValueOrDefault<Int16>("IS_PARENT"); }
            set { this.SetValue("IS_PARENT", value); }
        }
        public String PARENT_ACCOUNT_ID
        {
            get { return this.GetValueOrDefault<string>("PARENT_ACCOUNT_ID"); }
            set { this.SetValue("PARENT_ACCOUNT_ID", value); }
        }
        public String BALANCE_TYPE
        {
            get { return this.GetValueOrDefault<string>("BALANCE_TYPE"); }
            set { this.SetValue("BALANCE_TYPE", value); }
        }
        public String CURRENCY_ID
        {
            get { return this.GetValueOrDefault<string>("CURRENCY_ID"); }
            set { this.SetValue("CURRENCY_ID", value); }
        }
        public String CURRENCY_NAME
        {
            get { return this.GetValueOrDefault<string>("CURRENCY_NAME"); }
            set { this.SetValue("CURRENCY_NAME", value); }
        }
        public String PARENT_ACCOUNT_NAME
        {
            get { return this.GetValueOrDefault<string>("PARENT_ACCOUNT_NAME"); }
            set { this.SetValue("PARENT_ACCOUNT_NAME", value); }
        }
        public String RATE_METHOD
        {
            get { return this.GetValueOrDefault<string>("RATE_METHOD"); }
            set { this.SetValue("RATE_METHOD", value); }
        }
        public Int16 IS_OOB
        {
            get { return this.GetValueOrDefault<Int16>("IS_OOB"); }
            set { this.SetValue("IS_OOB", value); }
        }
        public Int16 IS_PR_DETAIL
        {
            get { return this.GetValueOrDefault<Int16>("IS_PR_DETAIL"); }
            set { this.SetValue("IS_PR_DETAIL", value); }
        }
        public Int16 IS_EXPENSE
        {
            get { return this.GetValueOrDefault<Int16>("IS_EXPENSE"); }
            set { this.SetValue("IS_EXPENSE", value); }
        }
        public Int16 IS_JOB
        {
            get { return this.GetValueOrDefault<Int16>("IS_JOB"); }
            set { this.SetValue("IS_JOB", value); }
        }
        public Int16 IS_BANK
        {
            get { return this.GetValueOrDefault<Int16>("IS_BANK"); }
            set { this.SetValue("IS_BANK", value); }
        }
        public Int16 IS_EMPLOYEE
        {
            get { return this.GetValueOrDefault<Int16>("IS_EMPLOYEE"); }
            set { this.SetValue("IS_EMPLOYEE", value); }
        }
        public Int16 IS_DEPARTMENT
        {
            get { return this.GetValueOrDefault<Int16>("IS_DEPARTMENT"); }
            set { this.SetValue("IS_DEPARTMENT", value); }
        }
        public Int16 IS_AGENT
        {
            get { return this.GetValueOrDefault<Int16>("IS_AGENT"); }
            set { this.SetValue("IS_AGENT", value); }
        }
        public Int16 IS_INSURANCE_SOURCE
        {
            get { return this.GetValueOrDefault<Int16>("IS_INSURANCE_SOURCE"); }
            set { this.SetValue("IS_INSURANCE_SOURCE", value); }
        }
        public Int16 IS_CAPITAL_SOURCE
        {
            get { return this.GetValueOrDefault<Int16>("IS_CAPITAL_SOURCE"); }
            set { this.SetValue("IS_CAPITAL_SOURCE", value); }
        }
        public Int16 IS_REINSURANCE_SOURCE
        {
            get { return this.GetValueOrDefault<Int16>("IS_REINSURANCE_SOURCE"); }
            set { this.SetValue("IS_REINSURANCE_SOURCE", value); }
        }
       public Int16 IS_VAT
        {
            get { return this.GetValueOrDefault<Int16>("IS_VAT"); }
            set { this.SetValue("IS_VAT", value); }
        }

       public Int16 IS_CONTRACT {
           get { return this.GetValueOrDefault<Int16>("IS_CONTRACT"); }
           set { this.SetValue("IS_CONTRACT", value); }
       }
        public Int16 IS_ITEM
        {
            get { return this.GetValueOrDefault<Int16>("IS_ITEM"); }
            set { this.SetValue("IS_ITEM", value); }
        }

        public string USER_ID
        {
            get { return this.GetValueOrDefault<string>("USER_ID"); }
            set { this.SetValue("USER_ID", value); }
        }

        public Int16 ACTIVE
        {
            get { return this.GetValueOrDefault<Int16>("Active"); }
            set { this.SetValue("Active", value); }
        }
    }
}

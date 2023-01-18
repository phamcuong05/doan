using FTS.Base.Business;
using System;

namespace FTS.ShareBusiness.Model
{
    public class Dm_AgentObject : ObjectInfoBase
    {
        public Dm_AgentObject() : base()
        {
            this.AGENT_ID = string.Empty;
            this.AGENT_NAME = string.Empty;
            this.ADDRESS = string.Empty;
            this.PHONE = string.Empty;
            this.EMAIL = string.Empty;
            this.TAX_FILE_NUMBER = string.Empty;
            this.IDENTITY_NO = string.Empty;
            this.BANK_NAME = string.Empty;
            this.BANK_ACCOUNT = string.Empty;
            this.BANK_BRANCH = string.Empty;
            this.BANK_CODE = string.Empty;
            this.PROVINCE_ID = string.Empty;
            this.PROVINCE_NAME = string.Empty;
            this.DISTRICT_ID = string.Empty;
            this.DISTRICT_NAME = string.Empty;
            this.ACTIVE = 0;
            this.USER_ID = string.Empty;
        }

        public string AGENT_ID
        {
            get { return this.GetValueOrDefault<string>("AGENT_ID"); }
            set { this.SetValue("AGENT_ID", value); }
        }

        public string AGENT_NAME
        {
            get { return this.GetValueOrDefault<string>("AGENT_NAME"); }
            set { this.SetValue("AGENT_NAME", value); }
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
        public string TAX_FILE_NUMBER
        {
            get { return this.GetValueOrDefault<string>("TAX_FILE_NUMBER"); }
            set { this.SetValue("TAX_FILE_NUMBER", value); }
        }
        public string IDENTITY_NO
        {
            get { return this.GetValueOrDefault<string>("IDENTITY_NO"); }
            set { this.SetValue("IDENTITY_NO", value); }
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
        public string BANK_CODE
        {
            get { return this.GetValueOrDefault<string>("BANK_CODE"); }
            set { this.SetValue("BANK_CODE", value); }
        }
        public string PROVINCE_ID
        {
            get { return this.GetValueOrDefault<string>("PROVINCE_ID"); }
            set { this.SetValue("PROVINCE_ID", value); }
        }

        public string PROVINCE_NAME
        {
            get { return this.GetValueOrDefault<string>("PROVINCE_NAME"); }
            set { this.SetValue("PROVINCE_NAME", value); }
        }

        public string DISTRICT_ID
        {
            get { return this.GetValueOrDefault<string>("DISTRICT_ID"); }
            set { this.SetValue("DISTRICT_ID", value); }
        }

        public string DISTRICT_NAME
        {
            get { return this.GetValueOrDefault<string>("DISTRICT_NAME"); }
            set { this.SetValue("DISTRICT_NAME", value); }
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

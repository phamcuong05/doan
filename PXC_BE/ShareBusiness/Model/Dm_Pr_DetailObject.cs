using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model
{
    public class Dm_Pr_DetailObject : ObjectInfoBase {
        public Dm_Pr_DetailObject() : base() {
            this.PR_DETAIL_ID = string.Empty;
            this.PR_DETAIL_NAME = string.Empty;
            this.PR_DETAIL_CLASS_ID = string.Empty;
            this.PR_DETAIL_CLASS_NAME = string.Empty;
            this.PR_DETAIL_CLASS1_ID = string.Empty;
            this.PR_DETAIL_CLASS1_NAME = string.Empty;
            this.PR_DETAIL_TYPE_ID = "01";
            this.PR_ACCOUNT_ID = string.Empty;
            this.PR_ACCOUNT_NAME = string.Empty;
            this.BANK_ACCOUNT_NAME = string.Empty;
            this.BANK_ACCOUNT_NO = string.Empty;
            this.BANK_BRANCH = string.Empty;
            this.BANK_CODE = string.Empty;
            this.BANK_NAME = string.Empty;
            this.ADDRESS = string.Empty;
            this.PHONE = string.Empty;
            this.EMAIL = string.Empty;
            this.TAX_FILE_NUMBER = string.Empty;
            this.IDENTITY_NO = string.Empty;
            this.DISTRICT_ID = string.Empty;
            this.DISTRICT_NAME = string.Empty;
            this.PROVINCE_ID = string.Empty;
            this.PROVINCE_NAME = string.Empty;
            this.ACTIVE = 1;
            this.USER_ID = "ADMIN";
            this.CREATE_DATE = DateTime.Today;
            this.PASSWORD = string.Empty;
        }

        public Dm_Pr_DetailObject(DataRow row) : base(row) {

        }

        public string PR_DETAIL_ID {
            get { return this.GetValueOrDefault<string>("Pr_Detail_ID"); }
            set { this.SetValue("Pr_Detail_ID",value); }
        }

        public string PR_DETAIL_NAME {
            get { return this.GetValueOrDefault<string>("Pr_Detail_Name"); }
            set { this.SetValue("Pr_Detail_Name", value); }
        }

        public string PR_DETAIL_CLASS_ID {
            get { return this.GetValueOrDefault<string>("Pr_Detail_Class_ID"); }
            set { this.SetValue("Pr_Detail_Class_ID", value); }
        }

        public string PR_DETAIL_CLASS_NAME
        {
            get { return this.GetValueOrDefault<string>("PR_DETAIL_CLASS_NAME"); }
            set { this.SetValue("PR_DETAIL_CLASS_NAME", value); }
        }

        public string PR_DETAIL_CLASS1_ID {
            get { return this.GetValueOrDefault<string>("Pr_Detail_Class1_ID"); }
            set { this.SetValue("Pr_Detail_Class1_ID", value); }
        }

        public string PR_DETAIL_CLASS1_NAME
        {
            get { return this.GetValueOrDefault<string>("PR_DETAIL_CLASS1_NAME"); }
            set { this.SetValue("PR_DETAIL_CLASS1_NAME", value); }
        }

        public string PR_DETAIL_TYPE_ID {
            get { return this.GetValueOrDefault<string>("Pr_Detail_Type_ID"); }
            set { this.SetValue("Pr_Detail_Type_ID", value); }
        }

        public string PR_ACCOUNT_ID {
            get { return this.GetValueOrDefault<string>("Pr_Account_ID"); }
            set { this.SetValue("Pr_Account_ID", value); }
        }

        public string PR_ACCOUNT_NAME
        {
            get { return this.GetValueOrDefault<string>("PR_ACCOUNT_NAME"); }
            set { this.SetValue("PR_ACCOUNT_NAME", value); }
        }

        public string PHONE {
            get { return this.GetValueOrDefault<string>("Phone"); }
            set { this.SetValue("Phone", value); }
        }

        public string EMAIL {
            get { return this.GetValueOrDefault<string>("Email"); }
            set { this.SetValue("Email", value); }
        }

        public string TAX_FILE_NUMBER {
            get { return this.GetValueOrDefault<string>("Tax_File_Number"); }
            set { this.SetValue("Tax_File_Number", value); }
        }

        public string IDENTITY_NO
        {
            get { return this.GetValueOrDefault<string>("IDENTITY_NO"); }
            set { this.SetValue("IDENTITY_NO", value); }
        }

        public string BANK_CODE {
            get { return this.GetValueOrDefault<string>("Bank_Code"); }
            set { this.SetValue("Bank_Code", value); }
        }

        public string BANK_BRANCH {
            get { return this.GetValueOrDefault<string>("Bank_Branch"); }
            set { this.SetValue("Bank_Branch", value); }
        }

        public string BANK_ACCOUNT_NAME {
            get { return this.GetValueOrDefault<string>("BANK_ACCOUNT_NAME"); }
            set { this.SetValue("BANK_ACCOUNT_NAME", value); }
        }

        public string BANK_ACCOUNT_NO {
            get { return this.GetValueOrDefault<string>("BANK_ACCOUNT_NO"); }
            set { this.SetValue("BANK_ACCOUNT_NO", value); }
        }

        public string BANK_NAME {
            get { return this.GetValueOrDefault<string>("Bank_Name"); }
            set { this.SetValue("Bank_Name", value); }
        }

        public string ADDRESS {
            get { return this.GetValueOrDefault<string>("Address"); }
            set { this.SetValue("Address", value); }
        }

        public string DISTRICT_ID {
            get { return this.GetValueOrDefault<string>("DISTRICT_ID"); }
            set { this.SetValue("DISTRICT_ID", value); }
        }

        public string DISTRICT_NAME
        {
            get { return this.GetValueOrDefault<string>("DISTRICT_NAME"); }
            set { this.SetValue("DISTRICT_NAME", value); }
        }

        public string PROVINCE_ID {
            get { return this.GetValueOrDefault<string>("PROVINCE_ID"); }
            set { this.SetValue("PROVINCE_ID", value); }
        }

        public string PROVINCE_NAME
        {
            get { return this.GetValueOrDefault<string>("PROVINCE_NAME"); }
            set { this.SetValue("PROVINCE_NAME", value); }
        }

        public string PASSWORD {
            get { return this.GetValueOrDefault<string>("Password"); }
            set { this.SetValue("Password", value); }
        }

        public string USER_ID {
            get { return this.GetValueOrDefault<string>("User_ID"); }
            set { this.SetValue("User_ID", value); }
        }

        public Int16 ACTIVE {
            get { return this.GetValueOrDefault<Int16>("ACTIVE"); }
            set { this.SetValue("ACTIVE", value); }
        }

        public DateTime CREATE_DATE {
            get { return this.GetValueOrDefault<DateTime>("CREATE_DATE"); }
            set { this.SetValue("CREATE_DATE", value); }
        }

        
    }
}

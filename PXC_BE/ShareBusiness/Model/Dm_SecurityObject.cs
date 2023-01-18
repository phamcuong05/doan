using FTS.Base.Business;
using System;
using System.Data;

namespace FTS.ShareBusiness.Model
{
    public class Dm_SecurityObject : ObjectInfoBase
    {
        public Dm_SecurityObject() : base()
        {
            this.SECURITY_CLASS_ID = string.Empty;
            this.SECURITY_NAME = string.Empty;
            this.SECURITY_CLASS_ID = string.Empty;
            this.BOOK_UNIT_PRICE_ORIG = 0;
            this.CURRENCY_ID = string.Empty;
            this.PR_DETAIL_ID = string.Empty;
            this.PERIOD_ID = string.Empty;
            this.ISSUE_DATE = DateTime.MinValue;
            this.MATURITY_DATE = DateTime.MinValue;
            this.SHORT_TERM_COST_ACCOUNT_ID = string.Empty;
            this.SHORT_TERM_PROFIT_ACCOUNT_ID = string.Empty;
            this.SHORT_TERM_LOSS_ACCOUNT_ID = string.Empty;
            this.SHORT_TERM_RESERVE_ACCOUNT_ID = string.Empty;
            this.LONG_TERM_COST_ACCOUNT_ID = string.Empty;
            this.LONG_TERM_PROFIT_ACCOUNT_ID = string.Empty;
            this.LONG_TERM_LOSS_ACCOUNT_ID = string.Empty;
            this.LONG_TERM_RESERVE_ACCOUNT_ID = string.Empty;
            this.ACTIVE = 1;
            this.USER_ID = string.Empty;
        }

        public Dm_SecurityObject(DataRow row) : base(row)
        {

        }

    public string SECURITY_ID
        {
            get { return this.GetValueOrDefault<string>("SECURITY_ID"); }
            set { this.SetValue("SECURITY_ID", value); }
        }
        public string SECURITY_NAME
        {
            get { return this.GetValueOrDefault<string>("SECURITY_NAME"); }
            set { this.SetValue("SECURITY_NAME", value); }
        }
        public string SECURITY_CLASS_ID
        {
            get { return this.GetValueOrDefault<string>("SECURITY_CLASS_ID"); }
            set { this.SetValue("SECURITY_CLASS_ID", value); }
        }

        public string SECURITY_CLASS_NAME
        {
            get { return this.GetValueOrDefault<string>("SECURITY_CLASS_NAME"); }
            set { this.SetValue("SECURITY_CLASS_NAME", value); }
        }
        

        public decimal BOOK_UNIT_PRICE_ORIG
        {
            get { return this.GetValueOrDefault<decimal>("BOOK_UNIT_PRICE_ORIG"); }
            set { this.SetValue("BOOK_UNIT_PRICE_ORIG", value); }
        }
        public string CURRENCY_ID
        {
            get { return this.GetValueOrDefault<string>("CURRENCY_ID"); }
            set { this.SetValue("CURRENCY_ID", value); }
        }
        public string PR_DETAIL_ID
        {
            get { return this.GetValueOrDefault<string>("PR_DETAIL_ID"); }
            set { this.SetValue("PR_DETAIL_ID", value); }
        }

        public string PR_DETAIL_NAME
        {
            get { return this.GetValueOrDefault<string>("PR_DETAIL_NAME"); }
            set { this.SetValue("PR_DETAIL_NAME", value); }
        }

        public string PERIOD_ID
        {
            get { return this.GetValueOrDefault<string>("PERIOD_ID"); }
            set { this.SetValue("PERIOD_ID", value); }
        }

        public string PERIOD_NAME
        {
            get { return this.GetValueOrDefault<string>("PERIOD_NAME"); }
            set { this.SetValue("PERIOD_NAME", value); }
        }

        public DateTime ISSUE_DATE
        {
            get { return this.GetValueOrDefault<DateTime>("ISSUE_DATE"); }
            set { this.SetValue("ISSUE_DATE", value); }
        }
        public DateTime MATURITY_DATE
        {
            get { return this.GetValueOrDefault<DateTime>("MATURITY_DATE"); }
            set { this.SetValue("MATURITY_DATE", value); }
        }
        public string SHORT_TERM_COST_ACCOUNT_ID
        {
            get { return this.GetValueOrDefault<string>("SHORT_TERM_COST_ACCOUNT_ID"); }
            set { this.SetValue("SHORT_TERM_COST_ACCOUNT_ID", value); }
        }
        public string SHORT_TERM_PROFIT_ACCOUNT_ID
        {
            get { return this.GetValueOrDefault<string>("SHORT_TERM_PROFIT_ACCOUNT_ID"); }
            set { this.SetValue("SHORT_TERM_PROFIT_ACCOUNT_ID", value); }
        }
        public string SHORT_TERM_LOSS_ACCOUNT_ID
        {
            get { return this.GetValueOrDefault<string>("SHORT_TERM_LOSS_ACCOUNT_ID"); }
            set { this.SetValue("SHORT_TERM_LOSS_ACCOUNT_ID", value); }
        }
        public string SHORT_TERM_RESERVE_ACCOUNT_ID
        {
            get { return this.GetValueOrDefault<string>("SHORT_TERM_RESERVE_ACCOUNT_ID"); }
            set { this.SetValue("SHORT_TERM_RESERVE_ACCOUNT_ID", value); }
        }
        public string LONG_TERM_COST_ACCOUNT_ID
        {
            get { return this.GetValueOrDefault<string>("LONG_TERM_COST_ACCOUNT_ID"); }
            set { this.SetValue("LONG_TERM_COST_ACCOUNT_ID", value); }
        }
        public string LONG_TERM_PROFIT_ACCOUNT_ID
        {
            get { return this.GetValueOrDefault<string>("LONG_TERM_PROFIT_ACCOUNT_ID"); }
            set { this.SetValue("LONG_TERM_PROFIT_ACCOUNT_ID", value); }
        }
        public string LONG_TERM_LOSS_ACCOUNT_ID
        {
            get { return this.GetValueOrDefault<string>("LONG_TERM_LOSS_ACCOUNT_ID"); }
            set { this.SetValue("LONG_TERM_LOSS_ACCOUNT_ID", value); }
        }
        public string LONG_TERM_RESERVE_ACCOUNT_ID
        {
            get { return this.GetValueOrDefault<string>("LONG_TERM_RESERVE_ACCOUNT_ID"); }
            set { this.SetValue("LONG_TERM_RESERVE_ACCOUNT_ID", value); }
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

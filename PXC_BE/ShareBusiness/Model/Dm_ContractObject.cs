using FTS.Base.Business;
using System;
using System.Data;

namespace FTS.ShareBusiness.Model
{
    public class Dm_ContractObject : ObjectInfoBase
    {
        public Dm_ContractObject() : base()
        {
            this.PR_KEY = Guid.Empty;
            this.ORGANIZATION_ID = string.Empty;
            this.CONTRACT_NO = string.Empty;
            this.CONTRACT_NAME = string.Empty;
            this.CONTRACT_TYPE = string.Empty;
            this.STATUS = string.Empty;
            this.CONTRACT_DATE = DateTime.Now;
            this.CONTRACT_CLASS_ID = string.Empty;
        }

        public Dm_ContractObject(DataRow row) : base(row)
        {

        }

        public Guid PR_KEY
        {
            get { return this.GetValueOrDefault<Guid>("PR_KEY"); }
            set { this.SetValue("PR_KEY", value); }
        }

        public string ORGANIZATION_ID
        {
            get { return this.GetValueOrDefault<string>("ORGANIZATION_ID"); }
            set { this.SetValue("ORGANIZATION_ID", value); }
        }

        public string CONTRACT_NO
        {
            get { return this.GetValueOrDefault<string>("CONTRACT_NO"); }
            set { this.SetValue("CONTRACT_NO", value); }
        }

        public string CONTRACT_NAME
        {
            get { return this.GetValueOrDefault<string>("CONTRACT_NAME"); }
            set { this.SetValue("CONTRACT_NAME", value); }
        }
        public DateTime CONTRACT_DATE
        {
            get { return this.GetValueOrDefault<DateTime>("CONTRACT_DATE"); }
            set { this.SetValue("CONTRACT_DATE", value); }
        }


        public string CONTRACT_TYPE
        {
            get { return this.GetValueOrDefault<string>("CONTRACT_TYPE"); }
            set { this.SetValue("CONTRACT_TYPE", value); }
        }


        public string STATUS
        {
            get { return this.GetValueOrDefault<string>("STATUS"); }
            set { this.SetValue("STATUS", value); }
        }
       

        public string CONTRACT_CLASS_ID
        {
            get { return this.GetValueOrDefault<string>("CONTRACT_CLASS_ID"); }
            set { this.SetValue("CONTRACT_CLASS_ID", value); }
        }
    }
}

using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model
{
    public class Dm_Pr_Class_TypeObject : ObjectInfoBase {
        public Dm_Pr_Class_TypeObject(string prdetailtypeid, string prdetailtypename)
        {
            this.PR_DETAIL_TYPE_ID = prdetailtypeid;
            this.PR_DETAIL_TYPE_NAME = prdetailtypename;
        }

        public string PR_DETAIL_TYPE_ID { get; set; }

        public string PR_DETAIL_TYPE_NAME { get; set; }
    }
}

using FTS.Base.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.ShareBusiness.Model
{
    public class ListContainerObject : ObjectInfoBase
    {
        public ListContainerObject() : base()
        {
            this.CONTAINER_ID = string.Empty;
            this.CONTAINER_NAME = string.Empty;
            this.CONTAINER_WEIGHT = 0;
        }

        public ListContainerObject(DataRow row) : base(row)
        {

        }
        public string CONTAINER_ID
        {
            get { return this.GetValueOrDefault<string>("CONTAINER_ID"); }
            set { this.SetValue("CONTAINER_ID", value); }
        }

        public string CONTAINER_NAME
        {
            get { return this.GetValueOrDefault<string>("CONTAINER_NAME"); }
            set { this.SetValue("CONTAINER_NAME", value); }
        }

        public decimal CONTAINER_WEIGHT
        {
            get { return this.GetValueOrDefault<decimal>("CONTAINER_WEIGHT"); }
            set { this.SetValue("CONTAINER_WEIGHT", value); }
        }

    }
}

using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.Base.Model
{
    public class ChangePasswordObject : ObjectInfoBase
    {
        public ChangePasswordObject()
        {

        }

        public string userId { get; set; }

        public string oldPwd { get; set; }

        public string newPwd { get; set; }
    }
}

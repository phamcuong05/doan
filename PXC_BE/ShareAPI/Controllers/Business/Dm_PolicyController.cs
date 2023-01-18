﻿using FTS.Base.API;
using FTS.Base.Business;
using FTS.ShareBusiness.Acc;
using System.Web.Http;

namespace FTS.ShareAPI.Controllers
{

    [Authorize]
    public class Dm_PolicyController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Policy(this.FTSMain);
        }
    }
}
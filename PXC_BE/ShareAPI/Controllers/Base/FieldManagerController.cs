using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using FTS.Base.Business;
using Newtonsoft.Json;
using FTS.Base.Systems;
using FTS.ShareBusiness.Acc;
using FTS.Base.API;

namespace FTS.ShareAPI.Controllers
{

    [Authorize]
    public class FieldManagerController : ApiBaseController
    {
        // GET api/<controller>
        [HttpGet]
        public IHttpActionResult IsRequired(string formname, string controlname)
        {
            //return Ok(this.FTSMain.FormManager.IsRequire(formname.ToUpper().Trim(), controlname.ToUpper().Trim()));
            return null;
        }

        [HttpGet]
        public IHttpActionResult IsVisible(string formname, string controlname) {
            //    return Ok(this.FTSMain.FormManager.IsVisible(formname.ToUpper().Trim(), controlname.ToUpper().Trim()));
            return null;

        }

        [HttpGet]
        public IHttpActionResult IsEnabled(string formname, string controlname) {
            //return Ok(this.FTSMain.FormManager.IsEnabled(formname.ToUpper().Trim(), controlname.ToUpper().Trim()));
            return null;

        }
        [HttpGet]
        public IHttpActionResult GetAllConfig(string formname) {
            //return Ok(this.FTSMain.FormManager.GetAllConfig(formname.ToUpper().Trim()));
            return null;

        }
    }
}
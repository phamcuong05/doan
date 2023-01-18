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
    public class GridManagerController : ApiBaseController
    {
        // GET api/<controller>
        [HttpGet]
        public IHttpActionResult IsRequired(string formname, string gridname, string columnname)
        {
            //return Ok(this.FTSMain.GridManager.IsRequire(formname.ToUpper().Trim(), gridname.ToUpper().Trim(), columnname.ToUpper().Trim()));
            return null;

        }

        [HttpGet]
        public IHttpActionResult IsVisible(string formname, string gridname, string columnname) {
            //return Ok(this.FTSMain.GridManager.IsVisible(formname.ToUpper().Trim(), gridname.ToUpper().Trim(), columnname.ToUpper().Trim()));
            return null;

        }

        [HttpGet]
        public IHttpActionResult IsEnabled(string formname, string gridname, string columnname) {
            //return Ok(this.FTSMain.GridManager.IsEnabled(formname.ToUpper().Trim(), gridname.ToUpper().Trim(), columnname.ToUpper().Trim()));
            return null;

        }

        [HttpGet]
        public IHttpActionResult IsSum(string formname, string gridname, string columnname) {
            //return Ok(this.FTSMain.GridManager.IsEnabled(formname.ToUpper().Trim(), gridname.ToUpper().Trim(), columnname.ToUpper().Trim()));
            return null;

        }

        [HttpGet]
        public IHttpActionResult GetAllConfig(string formname, string gridname) {
            //return Ok(this.FTSMain.GridManager.GetAllConfig(formname.ToUpper().Trim(), gridname.ToUpper().Trim()));
            return null;

        }

    }
}
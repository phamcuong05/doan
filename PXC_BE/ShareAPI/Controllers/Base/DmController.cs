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
    public class DmController : ApiBaseController
    {
        // GET api/<controller>
        [HttpGet]
        public IHttpActionResult GetAllData(string tablename)
        {
            return Ok(this.FTSMain.TableManager.LoadObjectList(tablename));
        }
        // GET api/<controller>
        [HttpGet]
        public IHttpActionResult GetNameValue(string tablename, string idvalue) {
            return Ok(this.FTSMain.TableManager.GetNameFieldValue(tablename, this.FTSMain.TableManager.GetNameField(tablename), this.FTSMain.TableManager.GetIDField(tablename),idvalue));
        }
        // GET api/<controller>
        [HttpGet]
        public IHttpActionResult IsValidID(string tablename, string idvalue) {
            return Ok(this.FTSMain.TableManager.IsValidID(tablename,string.Empty, idvalue));
        }
    }
}
using FTS.Base.API;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Acc;
using FTS.ShareBusiness.Model;
using System;
using System.Data;
using System.Net;
using System.Web.Http;

namespace FTS.ShareAPI.Controllers
{

    [Authorize]
    public class Dm_Advance_LimitController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Advance_Limit(this.FTSMain);
        }

        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try {
                Dm_Advance_Limit dmAdvanceLimit = new Dm_Advance_Limit(this.FTSMain);
                dmAdvanceLimit.LoadDataByID(idvalue);
                return Ok(dmAdvanceLimit.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(Dm_Advance_LimitObject dmAdvanceLimitobject) {
            try {
                Dm_Advance_Limit dmAdvanceLimit = new Dm_Advance_Limit(this.FTSMain);
                return Ok(dmAdvanceLimit.IsDataChanged(dmAdvanceLimitobject));
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult AddNewData() {
            try {
                Dm_Advance_Limit dmAdvanceLimit = new Dm_Advance_Limit(this.FTSMain);
                DataRow row = dmAdvanceLimit.AddNew();
                return Ok(dmAdvanceLimit.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult CopyData(string idvalue) {
            try {
                Dm_Advance_Limit dmAdvanceLimitObject = new Dm_Advance_Limit(this.FTSMain);
                dmAdvanceLimitObject.LoadDataByID(idvalue);
                if (dmAdvanceLimitObject.IsValidRow(0)) {
                    DataRow newrow = dmAdvanceLimitObject.CopyRecord(0);
                } else {
                    dmAdvanceLimitObject.AddNew();
                }

                return Ok(dmAdvanceLimitObject.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult UpdateEditData(Dm_Advance_LimitObject dmAdvanceLimitobject)
        {
            try
            {
                Dm_Advance_Limit dmAdvanceLimit = new Dm_Advance_Limit(this.FTSMain);
                dmAdvanceLimit.LoadDataByID(dmAdvanceLimitobject.PR_KEY);
                if (dmAdvanceLimit.IsValidRow(0))
                {
                    dmAdvanceLimit.SyncObjectToTable(dmAdvanceLimitobject);
                    dmAdvanceLimit.UpdateData();
                    return Ok(dmAdvanceLimitobject);
                }
                else
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_NOT_EXISTS")));
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult UpdateNewData(Dm_Advance_LimitObject dmAdvanceLimitobject)
        {
            try
            {
                Dm_Advance_Limit dmAdvanceLimit = new Dm_Advance_Limit(this.FTSMain);
                dmAdvanceLimit.LoadDataByID(dmAdvanceLimitobject.PR_KEY);
                if (dmAdvanceLimit.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmAdvanceLimit.AddNew();
                    dmAdvanceLimit.SyncObjectToTable(dmAdvanceLimitobject);
                    dmAdvanceLimit.UpdateData();
                    return Ok(dmAdvanceLimitobject);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public IHttpActionResult DeleteData(Guid idvalue)
        {
            try
            {
                Dm_Advance_Limit dmAdvanceLimit = new Dm_Advance_Limit(this.FTSMain);
                dmAdvanceLimit.DeleteInData(idvalue);
                return Ok(idvalue);
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }
    }
}
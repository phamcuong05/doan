using System;
using System.Data;
using System.Net;
using System.Web.Http;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Acc;
using FTS.Base.API;

namespace FTS.ShareAPI.Controllers
{

    [Authorize]
    public class Dm_Risk_ClassController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Risk_Class(this.FTSMain);
        }


        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try {
                Dm_Risk_Class dmRiskClass = new Dm_Risk_Class(this.FTSMain);
                dmRiskClass.LoadDataByID(idvalue);
                return Ok(dmRiskClass.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(Dm_Risk_ClassObject dmriskclassobject) {
            try {
                Dm_Risk_Class dmRiskClass = new Dm_Risk_Class(this.FTSMain);
                return Ok(dmRiskClass.IsDataChanged(dmriskclassobject));
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult AddNewData() {
            try {
                Dm_Risk_Class dmRiskClass = new Dm_Risk_Class(this.FTSMain);
                DataRow row = dmRiskClass.AddNew();
                return Ok(dmRiskClass.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult CopyData(string idvalue) {
            try {
                Dm_Risk_Class dmRiskClass = new Dm_Risk_Class(this.FTSMain);
                dmRiskClass.LoadDataByID(idvalue);
                if (dmRiskClass.IsValidRow(0)) {
                    DataRow newrow = dmRiskClass.CopyRecord(0);
                } else {
                    dmRiskClass.AddNew();
                }

                return Ok(dmRiskClass.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult UpdateData(Dm_Risk_ClassObject dmriskclassobject) {
            try {
                Dm_Risk_Class dmRiskClass = new Dm_Risk_Class(this.FTSMain);
                dmRiskClass.LoadDataByID(dmriskclassobject.RISK_CLASS_ID);
                dmRiskClass.SyncObjectToTable(dmriskclassobject);
                dmRiskClass.UpdateData();
                return Ok(dmriskclassobject);
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }


        // DELETE api/<controller>/5
        [HttpDelete]
        public IHttpActionResult DeleteData(string idvalue)
        {
            try
            {
                Dm_Risk_Class dmRiskClass = new Dm_Risk_Class(this.FTSMain);
                dmRiskClass.DeleteInData(idvalue);
                return Ok(idvalue);
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult UpdateEditData(Dm_Risk_ClassObject dmriskclassobject)
        {
            try
            {
                Dm_Risk_Class dmRiskClass = new Dm_Risk_Class(this.FTSMain);
                dmRiskClass.LoadDataByID(dmriskclassobject.RISK_CLASS_ID);
                if (dmRiskClass.IsValidRow(0))
                {
                    dmRiskClass.SyncObjectToTable(dmriskclassobject);
                    dmRiskClass.UpdateData();
                    return Ok(dmriskclassobject);
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

        [HttpPost]
        public IHttpActionResult UpdateNewData(Dm_Risk_ClassObject dmriskclassobject)
        {
            try
            {
                Dm_Risk_Class dmRiskClass = new Dm_Risk_Class(this.FTSMain);
                dmRiskClass.LoadDataByID(dmriskclassobject.RISK_CLASS_ID);
                if (dmRiskClass.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmRiskClass.AddNew();
                    dmRiskClass.SyncObjectToTable(dmriskclassobject);
                    dmRiskClass.UpdateData();
                    return Ok(dmriskclassobject);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }
    }
}
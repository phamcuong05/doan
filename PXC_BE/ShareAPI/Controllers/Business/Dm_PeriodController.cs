using System;
using System.Data;
using System.Net;
using System.Web.Http;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Acc;
using FTS.Base.API;
using FTS.ShareBusiness.Model;
using System.Collections.Generic;

namespace FTS.ShareAPI.Controllers
{

    [Authorize]
    public class Dm_PeriodController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Period(this.FTSMain);
        }

        [HttpGet]
        public IHttpActionResult GetPeriodTypeList()
        {
            try
            {
                List<PeriodTypeObject> periodTypeList = PeriodType.GetPeriodTypeList(this.FTSMain);
                return Ok(periodTypeList);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try {
                Dm_Period dmPeriod = new Dm_Period(this.FTSMain);
                dmPeriod.LoadDataByID(idvalue);
                return Ok(dmPeriod.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(Dm_PeriodObject dmPeriodobject) {
            try {
                Dm_Period dmPeriod = new Dm_Period(this.FTSMain);
                return Ok(dmPeriod.IsDataChanged(dmPeriodobject));
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult AddNewData() {
            try {
                Dm_Period dmPeriod = new Dm_Period(this.FTSMain);
                DataRow row = dmPeriod.AddNew();
                return Ok(dmPeriod.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult CopyData(string idvalue) {
            try {
                Dm_Period dmPeriod = new Dm_Period(this.FTSMain);
                dmPeriod.LoadDataByID(idvalue);
                if (dmPeriod.IsValidRow(0)) {
                    DataRow newrow = dmPeriod.CopyRecord(0);
                } else {
                    dmPeriod.AddNew();
                }

                return Ok(dmPeriod.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult UpdateData(Dm_PeriodObject dmPeriodobject) {
            try {
                Dm_Period dmPeriod = new Dm_Period(this.FTSMain);
                dmPeriod.LoadDataByID(dmPeriodobject.PERIOD_ID);
                dmPeriod.SyncObjectToTable(dmPeriodobject);
                dmPeriod.UpdateData();
                return Ok(dmPeriodobject);
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
                Dm_Period dmPeriod = new Dm_Period(this.FTSMain);
                dmPeriod.DeleteInData(idvalue);
                return Ok(idvalue);
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }
        [HttpPost]
        public IHttpActionResult UpdateNewData(Dm_PeriodObject dmprdetaiclass1lobject)
        {
            try
            {
                Dm_Period dmPeriod = new Dm_Period(this.FTSMain);
                dmPeriod.LoadDataByID(dmprdetaiclass1lobject.PERIOD_ID);
                if (dmPeriod.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmPeriod.AddNew();
                    dmPeriod.SyncObjectToTable(dmprdetaiclass1lobject);
                    dmPeriod.UpdateData();
                    return Ok(dmprdetaiclass1lobject);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }
        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult UpdateEditData(Dm_PeriodObject dmPeriodobject)
        {
            try
            {
                Dm_Period dmPeriod = new Dm_Period(this.FTSMain);
                dmPeriod.LoadDataByID(dmPeriodobject.PERIOD_ID);
                if (dmPeriod.IsValidRow(0))
                {
                    dmPeriod.SyncObjectToTable(dmPeriodobject);
                    dmPeriod.UpdateData();
                    return Ok(dmPeriodobject);
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
    }
}
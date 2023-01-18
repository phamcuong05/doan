using FTS.Base.API;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Acc;
using FTS.ShareBusiness.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace FTS.ShareAPI.Controllers.Business
{
    [Authorize]
    public class Dm_Cashbank_LimitController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Cashbank_Limit(this.FTSMain);
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                Dm_Cashbank_Limit dmCashbankLimit = new Dm_Cashbank_Limit(this.FTSMain);
                dmCashbankLimit.LoadDataByID(idvalue);
                return Ok(dmCashbankLimit.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(Dm_Cashbank_LimitObject dmcashbanklimitobject)
        {
            try
            {
                Dm_Cashbank_Limit dmCashbankLimit = new Dm_Cashbank_Limit(this.FTSMain);
                return Ok(dmCashbankLimit.IsDataChanged(dmcashbanklimitobject));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult AddNewData()
        {
            try
            {
                Dm_Cashbank_Limit dmCashbankLimit = new Dm_Cashbank_Limit(this.FTSMain);
                DataRow row = dmCashbankLimit.AddNew();
                return Ok(dmCashbankLimit.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult CopyData(string idvalue)
        {
            try
            {
                Dm_Cashbank_Limit dmCashbankLimit = new Dm_Cashbank_Limit(this.FTSMain);
                dmCashbankLimit.LoadDataByID(idvalue);
                if (dmCashbankLimit.IsValidRow(0))
                {
                    DataRow newrow = dmCashbankLimit.CopyRecord(0);
                    return Ok(dmCashbankLimit.GetDataObject());
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
        public IHttpActionResult UpdateEditData(Dm_Cashbank_LimitObject dmcashbanklimitobject)
        {
            try
            {
                Dm_Cashbank_Limit dmCashbankLimit = new Dm_Cashbank_Limit(this.FTSMain);
                dmCashbankLimit.LoadDataByID(dmcashbanklimitobject.PR_KEY);
                if (dmCashbankLimit.IsValidRow(0))
                {
                    dmCashbankLimit.SyncObjectToTable(dmcashbanklimitobject);
                    dmCashbankLimit.UpdateData();
                    return Ok(dmcashbanklimitobject);
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
        public IHttpActionResult UpdateNewData(Dm_Cashbank_LimitObject dmcashbanklimitobject)
        {
            try
            {
                Dm_Cashbank_Limit dmCashbankLimit = new Dm_Cashbank_Limit(this.FTSMain);
                dmCashbankLimit.LoadDataByID(dmcashbanklimitobject.PR_KEY);
                if (dmCashbankLimit.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmCashbankLimit.AddNew();
                    dmCashbankLimit.SyncObjectToTable(dmcashbanklimitobject);
                    dmCashbankLimit.UpdateData();
                    return Ok(dmcashbanklimitobject);
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
                Dm_Cashbank_Limit dmCashbankLimit = new Dm_Cashbank_Limit(this.FTSMain);
                dmCashbankLimit.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }
    }
}
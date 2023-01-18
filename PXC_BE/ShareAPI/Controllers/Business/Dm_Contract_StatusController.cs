using FTS.Base.API;
using FTS.Base.Business;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Acc;
using FTS.ShareBusiness.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web.Http;

namespace FTS.ShareAPI.Controllers
{

    [Authorize]
    public class Dm_Contract_StatusController : ApiObjectBaseController
    {

        public override ObjectBase GetObjectBase()
        {
            return new Dm_Contract_Status(this.FTSMain);
        }



        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                Dm_Contract_Status dmContractStatus = new Dm_Contract_Status(this.FTSMain);
                if (dmContractStatus.IsValidRow(0))
                {
                    dmContractStatus.LoadDataByID(idvalue);
                    return Ok(dmContractStatus.GetDataObject());
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

        [HttpGet]
        public IHttpActionResult IsDataChanged(Dm_Contract_StatusObject dmcontractstatusObject)
        {
            try
            {
                Dm_Contract_Status dmContractStatus = new Dm_Contract_Status(this.FTSMain);
                return Ok(dmContractStatus.IsDataChanged(dmcontractstatusObject));
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
                Dm_Contract_Status dmContractStatus = new Dm_Contract_Status(this.FTSMain);
                DataRow row = dmContractStatus.AddNew();
                return Ok(dmContractStatus.GetDataObject());
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
                Dm_Contract_Status dmContractStatus = new Dm_Contract_Status(this.FTSMain);
                dmContractStatus.LoadDataByID(idvalue);
                if (dmContractStatus.IsValidRow(0))
                {
                    DataRow newrow = dmContractStatus.CopyRecord(0);
                    return Ok(dmContractStatus.GetDataObject());
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
        public IHttpActionResult UpdateEditData(Dm_Contract_StatusObject dmcontractstatusObject)
        {
            try
            {
                Dm_Contract_Status dmContractStatus = new Dm_Contract_Status(this.FTSMain);
                dmContractStatus.LoadDataByID(dmcontractstatusObject.CONTRACT_STATUS_ID);
                if (dmContractStatus.IsValidRow(0))
                {
                    dmContractStatus.SyncObjectToTable(dmcontractstatusObject);
                    dmContractStatus.UpdateData();
                    return Ok(dmcontractstatusObject);
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
        public IHttpActionResult UpdateNewData(Dm_Contract_StatusObject dmcontractstatusObject)
        {
            try
            {
                Dm_Contract_Status dmContractStatus = new Dm_Contract_Status(this.FTSMain);
                dmContractStatus.LoadDataByID(dmcontractstatusObject.CONTRACT_STATUS_ID);
                if (dmContractStatus.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmContractStatus.AddNew();
                    dmContractStatus.SyncObjectToTable(dmcontractstatusObject);
                    dmContractStatus.UpdateData();
                    return Ok(dmcontractstatusObject);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }




        // DELETE api/<controller>/5
        [HttpDelete]
        public IHttpActionResult DeleteData(string idvalue)
        {
            try
            {
                Dm_Contract_Status dmContractStatus = new Dm_Contract_Status(this.FTSMain);
                dmContractStatus.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        
    }
}
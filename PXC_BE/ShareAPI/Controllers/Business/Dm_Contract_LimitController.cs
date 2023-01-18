using System;
using System.Data;
using System.Net;
using System.Web.Http;
using FTS.Base.API;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Acc;
using FTS.ShareBusiness.Model;

namespace FTS.ShareAPI.Controllers
{

    [Authorize]
    public class Dm_Contract_LimitController : ApiObjectBaseController
    {

        public override ObjectBase GetObjectBase()
        {
            return new Dm_Contract_Limit(this.FTSMain);
        }

     
        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                Dm_Contract_Limit dmContractLimit = new Dm_Contract_Limit(this.FTSMain);
                if (dmContractLimit.IsValidRow(0))
                {
                    dmContractLimit.LoadDataByID(idvalue);
                    return Ok(dmContractLimit.GetDataObject());
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
        public IHttpActionResult IsDataChanged(Dm_Contract_LimitObject dmContractLimitobject)
        {
            try
            {
                Dm_Contract_Limit dmContractLimit = new Dm_Contract_Limit(this.FTSMain);
                return Ok(dmContractLimit.IsDataChanged(dmContractLimitobject));
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
                Dm_Contract_Limit dmContractLimit = new Dm_Contract_Limit(this.FTSMain);
                DataRow row = dmContractLimit.AddNew();
                return Ok(dmContractLimit.GetDataObject());
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
                Dm_Contract_Limit dmContractLimit = new Dm_Contract_Limit(this.FTSMain);
                dmContractLimit.LoadDataByID(idvalue);
                if (dmContractLimit.IsValidRow(0))
                {
                    DataRow newrow = dmContractLimit.CopyRecord(0);
                    return Ok(dmContractLimit.GetDataObject());
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
        public IHttpActionResult UpdateEditData(Dm_Contract_LimitObject dmContractLimitobject)
        {
            try
            {
                Dm_Contract_Limit dmContractLimit = new Dm_Contract_Limit(this.FTSMain);
                dmContractLimit.LoadDataByID(dmContractLimitobject.PR_KEY);
                if (dmContractLimit.IsValidRow(0))
                {
                    dmContractLimit.SyncObjectToTable(dmContractLimitobject);
                    dmContractLimit.UpdateData();
                    return Ok(dmContractLimitobject);
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
        public IHttpActionResult UpdateNewData(Dm_Contract_LimitObject dmContractLimitobject)
        {
            try
            {
                Dm_Contract_Limit dmContractLimit = new Dm_Contract_Limit(this.FTSMain);
                dmContractLimit.LoadDataByID(dmContractLimitobject.PR_KEY);
                if (dmContractLimit.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmContractLimit.AddNew();
                    dmContractLimit.SyncObjectToTable(dmContractLimitobject);
                    dmContractLimit.UpdateData();
                    return Ok(dmContractLimitobject);
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
                Dm_Contract_Limit dmContractLimit = new Dm_Contract_Limit(this.FTSMain);
                dmContractLimit.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }        
    }
}
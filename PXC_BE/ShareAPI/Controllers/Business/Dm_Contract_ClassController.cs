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
    public class Dm_Contract_ClassController : ApiObjectBaseController
    {

        public override ObjectBase GetObjectBase()
        {
            return new Dm_Contract_Class(this.FTSMain);
        }

     
        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                Dm_Contract_Class dmContractClass = new Dm_Contract_Class(this.FTSMain);
                if (dmContractClass.IsValidRow(0))
                {
                    dmContractClass.LoadDataByID(idvalue);
                    return Ok(dmContractClass.GetDataObject());
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
        public IHttpActionResult IsDataChanged(Dm_Contract_ClassObject dmcontractclassobject)
        {
            try
            {
                Dm_Contract_Class dmContractClass = new Dm_Contract_Class(this.FTSMain);
                return Ok(dmContractClass.IsDataChanged(dmcontractclassobject));
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
                Dm_Contract_Class dmContractClass = new Dm_Contract_Class(this.FTSMain);
                DataRow row = dmContractClass.AddNew();
                return Ok(dmContractClass.GetDataObject());
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
                Dm_Contract_Class dmContractClass = new Dm_Contract_Class(this.FTSMain);
                dmContractClass.LoadDataByID(idvalue);
                if (dmContractClass.IsValidRow(0))
                {
                    DataRow newrow = dmContractClass.CopyRecord(0);
                    return Ok(dmContractClass.GetDataObject());
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
        public IHttpActionResult UpdateEditData(Dm_Contract_ClassObject dmcontractclassobject)
        {
            try
            {
                Dm_Contract_Class dmContractClass = new Dm_Contract_Class(this.FTSMain);
                dmContractClass.LoadDataByID(dmcontractclassobject.CONTRACT_CLASS_ID);
                if (dmContractClass.IsValidRow(0))
                {
                    dmContractClass.SyncObjectToTable(dmcontractclassobject);
                    dmContractClass.UpdateData();
                    return Ok(dmcontractclassobject);
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
        public IHttpActionResult UpdateNewData(Dm_Contract_ClassObject dmcontractclassobject)
        {
            try
            {
                Dm_Contract_Class dmContractClass = new Dm_Contract_Class(this.FTSMain);
                dmContractClass.LoadDataByID(dmcontractclassobject.CONTRACT_CLASS_ID);
                if (dmContractClass.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmContractClass.AddNew();
                    dmContractClass.SyncObjectToTable(dmcontractclassobject);
                    dmContractClass.UpdateData();
                    return Ok(dmcontractclassobject);
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
                Dm_Contract_Class dmContractClass = new Dm_Contract_Class(this.FTSMain);
                dmContractClass.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        
    }
}
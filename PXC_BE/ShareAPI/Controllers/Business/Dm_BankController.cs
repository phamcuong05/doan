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
    public class Dm_BankController : ApiObjectBaseController
    {

        public override ObjectBase GetObjectBase()
        {
            return new Dm_Bank(this.FTSMain);
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                Dm_Bank dmBank = new Dm_Bank(this.FTSMain);
                dmBank.LoadDataByID(idvalue);
                return Ok(dmBank.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(Dm_BankObject dmbankobject)
        {
            try
            {
                Dm_Bank dmBank = new Dm_Bank(this.FTSMain);
                return Ok(dmBank.IsDataChanged(dmbankobject));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        // GET api/<controller>/5
        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult AddNewData()
        {
            try
            {
                Dm_Bank dmBank = new Dm_Bank(this.FTSMain);
                DataRow row = dmBank.AddNew();
                return Ok(dmBank.GetDataObject());
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
                Dm_Bank dmBank = new Dm_Bank(this.FTSMain);
                dmBank.LoadDataByID(idvalue);
                if (dmBank.IsValidRow(0))
                {
                    DataRow newrow = dmBank.CopyRecord(0);
                    return Ok(dmBank.GetDataObject());
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
        public IHttpActionResult UpdateEditData(Dm_BankObject dmbankobject)
        {
            try
            {
                Dm_Bank dmBank = new Dm_Bank(this.FTSMain);
                dmBank.LoadDataByID(dmbankobject.BANK_ID);
                if (dmBank.IsValidRow(0))
                {
                    dmBank.SyncObjectToTable(dmbankobject);
                    dmBank.UpdateData();
                    return Ok(dmbankobject);
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
        public IHttpActionResult UpdateNewData(Dm_BankObject dmbankobject)
        {
            try
            {
                Dm_Bank dmBank = new Dm_Bank(this.FTSMain);
                dmBank.LoadDataByID(dmbankobject.BANK_ID);
                if (dmBank.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmBank.AddNew();
                    dmBank.SyncObjectToTable(dmbankobject);
                    dmBank.UpdateData();
                    return Ok(dmbankobject);
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
                Dm_Bank dmBank = new Dm_Bank(this.FTSMain);
                dmBank.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }
    }
}
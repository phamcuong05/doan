using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using FTS.Base.Business;
using FTS.ShareBusiness.Acc;
using FTS.Base.API;
using FTS.Base.Business;
using System.Data;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;

namespace FTS.ShareAPI.Controllers
{

    [Authorize]
    public class Dm_AccountController : ApiObjectBaseController
    {

        public override ObjectBase GetObjectBase()
        {
            return new Dm_Account(this.FTSMain);
        }

        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try {
                Dm_Account dmAccount = new Dm_Account(this.FTSMain);
                dmAccount.LoadDataByID(idvalue);
                return Ok(dmAccount.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(Dm_AccountObject dmaccountobject)
        {
            try
            {
                Dm_Account dmAccount = new Dm_Account(this.FTSMain);
                return Ok(dmAccount.IsDataChanged(dmaccountobject));
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
                Dm_Account dmAccount = new Dm_Account(this.FTSMain);
                DataRow row = dmAccount.AddNew();
                return Ok(dmAccount.GetDataObject());
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
                Dm_Account dmAccount = new Dm_Account(this.FTSMain);
                dmAccount.LoadDataByID(idvalue);
                if (dmAccount.IsValidRow(0))
                {
                    DataRow newrow = dmAccount.CopyRecord(0);
                    return Ok(dmAccount.GetDataObject());
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
        public IHttpActionResult UpdateEditData(Dm_AccountObject dmaccountobject)
        {
            try
            {
                Dm_Account dmAccount = new Dm_Account(this.FTSMain);
                dmAccount.LoadDataByID(dmaccountobject.ACCOUNT_ID);
                if (dmAccount.IsValidRow(0))
                {
                    dmAccount.SyncObjectToTable(dmaccountobject);
                    dmAccount.UpdateData();
                    return Ok(dmaccountobject);
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
        public IHttpActionResult UpdateNewData(Dm_AccountObject dmaccountobject)
        {
            try
            {
                Dm_Account dmAccount = new Dm_Account(this.FTSMain);
                dmAccount.LoadDataByID(dmaccountobject.ACCOUNT_ID);
                if (dmAccount.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmAccount.AddNew();
                    dmAccount.SyncObjectToTable(dmaccountobject);
                    dmAccount.UpdateData();
                    return Ok(dmaccountobject);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        [HttpGet]
        public IHttpActionResult GetBalanceTypeList()
        {
            try
            {
                List<DebitCreditObject> balanceType = DebitCredit.GetDebitCreditList(this.FTSMain);
                return Ok(balanceType);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }
        [HttpGet]
        public IHttpActionResult GetRateMethodList()
        {
            try
            {
                List<RateMethodObject> balanceType = RateMethodType.GetRateMethodList(this.FTSMain);
                return Ok(balanceType);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }




        // DELETE api/<controller>/5
        [HttpDelete]
        public IHttpActionResult DeleteData(string idvalue)
        {
            try
            {
                Dm_Account dmAccount = new Dm_Account(this.FTSMain);
                dmAccount.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

    }
}
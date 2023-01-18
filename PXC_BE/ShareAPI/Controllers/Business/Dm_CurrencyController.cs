using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web.Http;
using FTS.Base.Business;
using FTS.ShareBusiness.Acc;
using FTS.Base.API;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;

namespace FTS.ShareAPI.Controllers
{

    [Authorize]
    public class Dm_CurrencyController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Currency(this.FTSMain);
        }

        [HttpGet]
        public IHttpActionResult GetExchangeRate(DateTime tranDate, string currencyId)
        {
            try
            {
                decimal EXCHANGE_RATE = 1;
                if (currencyId.ToUpper() != "VND")
                {
                    string sql = "SELECT TOP 1 * FROM DM_EXCHANGE_RATE WHERE VALID_DATE <= " + FTS.Base.Utilities.Functions.ParseDate(tranDate) + " AND CURRENCY_ID = '" + currencyId + "' AND TO_CURRENCY_ID = '" + this.FTSMain.MainCurrency + "' ORDER BY VALID_DATE DESC";
                    DataTable DM_EXCHANGE_RATE = this.FTSMain.DbMain.LoadDataTable(this.FTSMain.DbMain.GetSqlStringCommand(sql), "DM_EXCHANGE_RATE");
                    if (DM_EXCHANGE_RATE.Rows.Count > 0)
                    {
                        EXCHANGE_RATE = (decimal)DM_EXCHANGE_RATE.Rows[0]["EXCHANGE_RATE"];
                    }
                }
                Dictionary<object, object> dict = new Dictionary<object, object>();
                dict.Add("TRAN_DATE", tranDate);
                dict.Add("CURRENCY_ID", currencyId);
                dict.Add("EXCHANGE_RATE", EXCHANGE_RATE);
                dict.Add("EXCHANGE_RATE_EXTRA", EXCHANGE_RATE);
                return Ok(dict);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }

        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                Dm_Currency dmCurrency = new Dm_Currency(this.FTSMain);
                dmCurrency.LoadDataByID(idvalue);
                return Ok(dmCurrency.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(Dm_CurrencyObject dmcurrencyobject)
        {
            try
            {
                Dm_Currency dmCurrency = new Dm_Currency(this.FTSMain);
                return Ok(dmCurrency.IsDataChanged(dmcurrencyobject));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult AddNewData()
        {
            try
            {
                Dm_Currency dmCurrency = new Dm_Currency(this.FTSMain);
                DataRow row = dmCurrency.AddNew();
                return Ok(dmCurrency.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult CopyData(string idvalue)
        {
            try
            {
                Dm_Currency dmCurrency = new Dm_Currency(this.FTSMain);
                dmCurrency.LoadDataByID(idvalue);
                if (dmCurrency.IsValidRow(0))
                {
                    DataRow newrow = dmCurrency.CopyRecord(0);
                }
                else
                {
                    dmCurrency.AddNew();
                }

                return Ok(dmCurrency.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult UpdateNewData(Dm_CurrencyObject dmcurrencyobject)
        {
            try
            {
                Dm_Currency dmCurrency = new Dm_Currency(this.FTSMain);
                dmCurrency.LoadDataByID(dmcurrencyobject.CURRENCY_ID);
                if (dmCurrency.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));
                }
                else
                {
                    dmCurrency.AddNew();
                    dmCurrency.SyncObjectToTable(dmcurrencyobject);
                    dmCurrency.UpdateData();
                    return Ok(dmcurrencyobject);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult UpdateEditData(Dm_CurrencyObject dmcurrencyobject)
        {
            try
            {
                Dm_Currency dmCurrency = new Dm_Currency(this.FTSMain);
                dmCurrency.LoadDataByID(dmcurrencyobject.CURRENCY_ID);
                if (dmCurrency.IsValidRow(0))
                {
                    dmCurrency.SyncObjectToTable(dmcurrencyobject);
                    dmCurrency.UpdateData();
                    return Ok(dmcurrencyobject);
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


        // DELETE api/<controller>/5
        [HttpDelete]
        public IHttpActionResult DeleteData(string idvalue)
        {
            try
            {
                Dm_Currency dmCurrency = new Dm_Currency(this.FTSMain);
                dmCurrency.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }
    }
}
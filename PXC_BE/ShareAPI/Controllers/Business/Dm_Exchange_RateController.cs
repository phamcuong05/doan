using FTS.Base.API;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Acc;
using FTS.ShareBusiness.Model;
using System;
using System.Data;
using System.Net;
using System.Web.Http;

namespace FTS.ShareAPI.Controllers.Business
{
    [Authorize]
    public class Dm_Exchange_RateController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Exchange_Rate(this.FTSMain);
        }

        [HttpPost]
        public IHttpActionResult UpdateEditData(Dm_Exchange_RateObject dmexchangerateobject)
        {
            try
            {
                Dm_Exchange_Rate dmExchangeRate = new Dm_Exchange_Rate(this.FTSMain);
                dmExchangeRate.LoadDataByID(dmexchangerateobject.PR_KEY);
                if (!dmExchangeRate.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_NOT_EXISTS")));
                }
                else
                {
                    dmExchangeRate.SyncObjectToTable(dmexchangerateobject);
                    dmExchangeRate.UpdateData();
                    return Ok(dmexchangerateobject);
                }
                
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateNewData(Dm_Exchange_RateObject dmexchangerateobject)
        {
            try
            {
                Dm_Exchange_Rate dmExchangeRate = new Dm_Exchange_Rate(this.FTSMain);
                dmExchangeRate.LoadDataByID(dmexchangerateobject.PR_KEY);
                if (dmExchangeRate.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmExchangeRate.AddNew();
                    dmExchangeRate.SyncObjectToTable(dmexchangerateobject);
                    dmExchangeRate.UpdateData();
                    return Ok(dmexchangerateobject);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteData(Guid idvalue)
        {
            try
            {
                Dm_Exchange_Rate dmExchangeRate = new Dm_Exchange_Rate(this.FTSMain);
                dmExchangeRate.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        [HttpGet]
        public IHttpActionResult AddNewData()
        {
            try
            {
                Dm_Exchange_Rate dmExchangeRate = new Dm_Exchange_Rate(this.FTSMain);
                dmExchangeRate.AddNew();
                return Ok(dmExchangeRate.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }
    }
}

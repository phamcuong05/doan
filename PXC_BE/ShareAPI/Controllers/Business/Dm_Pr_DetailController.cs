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
    public class Dm_Pr_DetailController : ApiObjectBaseController
    {

        public override ObjectBase GetObjectBase()
        {
            return new Dm_Pr_Detail(this.FTSMain);
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                Dm_Pr_Detail dmPrDetail = new Dm_Pr_Detail(this.FTSMain);
                dmPrDetail.LoadDataByID(idvalue);
                return Ok(dmPrDetail.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(Dm_Pr_DetailObject dmprdetailobject)
        {
            try
            {
                Dm_Pr_Detail dmPrDetail = new Dm_Pr_Detail(this.FTSMain);
                return Ok(dmPrDetail.IsDataChanged(dmprdetailobject));
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
                Dm_Pr_Detail dmPrDetail = new Dm_Pr_Detail(this.FTSMain);
                DataRow row = dmPrDetail.AddNew();
                return Ok(dmPrDetail.GetDataObject());
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
                Dm_Pr_Detail dmPrDetail = new Dm_Pr_Detail(this.FTSMain);
                dmPrDetail.LoadDataByID(idvalue);
                if (dmPrDetail.IsValidRow(0))
                {
                    DataRow newrow = dmPrDetail.CopyRecord(0);
                    return Ok(dmPrDetail.GetDataObject());
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
        public IHttpActionResult UpdateEditData(Dm_Pr_DetailObject dmprdetailobject)
        {
            try
            {
                Dm_Pr_Detail dmPrDetail = new Dm_Pr_Detail(this.FTSMain);
                dmPrDetail.LoadDataByID(dmprdetailobject.PR_DETAIL_ID);
                if (dmPrDetail.IsValidRow(0))
                {
                    dmPrDetail.SyncObjectToTable(dmprdetailobject);
                    dmPrDetail.UpdateData();
                    return Ok(dmprdetailobject);
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
        public IHttpActionResult UpdateNewData(Dm_Pr_DetailObject dmprdetailobject)
        {
            try
            {
                Dm_Pr_Detail dmPrDetail = new Dm_Pr_Detail(this.FTSMain);
                dmPrDetail.LoadDataByID(dmprdetailobject.PR_DETAIL_ID);
                if (dmPrDetail.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmPrDetail.AddNew();
                    dmPrDetail.SyncObjectToTable(dmprdetailobject);
                    dmPrDetail.UpdateData();
                    return Ok(dmprdetailobject);
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
                Dm_Pr_Detail dmPrDetail = new Dm_Pr_Detail(this.FTSMain);
                dmPrDetail.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }


    }
}
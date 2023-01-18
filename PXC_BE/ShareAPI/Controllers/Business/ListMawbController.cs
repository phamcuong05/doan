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
    public class ListMawbController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new ListMawb(this.FTSMain);
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                ListMawb listMawb = new ListMawb(this.FTSMain);
                listMawb.LoadDataByID(idvalue);
                return Ok(listMawb.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(ListMawbObject listMawbObject)
        {
            try
            {
                ListMawb listMawb = new ListMawb(this.FTSMain);
                return Ok(listMawb.IsDataChanged(listMawbObject));
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
                ListMawb listMawb = new ListMawb(this.FTSMain);
                DataRow row = listMawb.AddNew();
                return Ok(listMawb.GetDataObject());
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
                ListMawb listMawb = new ListMawb(this.FTSMain);
                listMawb.LoadDataByID(idvalue);
                if (listMawb.IsValidRow(0))
                {
                    DataRow newrow = listMawb.CopyRecord(0);
                    return Ok(listMawb.GetDataObject());
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
        public IHttpActionResult UpdateEditData(ListMawbObject listMawbObject)
        {
            try
            {
                ListMawb listMawb = new ListMawb(this.FTSMain);
                listMawb.LoadDataByID(listMawbObject.MAWB_ID);
                if (listMawb.IsValidRow(0))
                {
                    listMawb.SyncObjectToTable(listMawbObject);
                    listMawb.UpdateData();
                    return Ok(listMawbObject);
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
        public IHttpActionResult UpdateNewData(ListMawbObject listMawbObject)
        {
            try
            {
                ListMawb listMawb = new ListMawb(this.FTSMain);
                listMawb.LoadDataByID(listMawbObject.MAWB_ID);
                if (listMawb.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    listMawb.AddNew();
                    listMawb.SyncObjectToTable(listMawbObject);
                    listMawb.UpdateData();
                    return Ok(listMawbObject);
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
                ListMawb listMawb = new ListMawb(this.FTSMain);
                listMawb.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }
    }
}
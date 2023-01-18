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
    public class ListWHController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new ListWH(this.FTSMain);
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                ListWH listWH = new ListWH(this.FTSMain);
                listWH.LoadDataByID(idvalue);
                return Ok(listWH.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(ListWHObject listWHObject)
        {
            try
            {
                ListWH listWH = new ListWH(this.FTSMain);
                return Ok(listWH.IsDataChanged(listWHObject));
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
                ListWH listWH = new ListWH(this.FTSMain);
                DataRow row = listWH.AddNew();
                return Ok(listWH.GetDataObject());
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
                ListWH listWH = new ListWH(this.FTSMain);
                listWH.LoadDataByID(idvalue);
                if (listWH.IsValidRow(0))
                {
                    DataRow newrow = listWH.CopyRecord(0);
                    return Ok(listWH.GetDataObject());
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
        public IHttpActionResult UpdateEditData(ListWHObject listWHObject)
        {
            try
            {
                ListWH listWH = new ListWH(this.FTSMain);
                listWH.LoadDataByID(listWHObject.WARE_HOUSE_ID);
                if (listWH.IsValidRow(0))
                {
                    listWH.SyncObjectToTable(listWHObject);
                    listWH.UpdateData();
                    return Ok(listWHObject);
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
        public IHttpActionResult UpdateNewData(ListWHObject listWHObject)
        {
            try
            {
                ListWH listWH = new ListWH(this.FTSMain);
                listWH.LoadDataByID(listWHObject.WARE_HOUSE_ID);
                if (listWH.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    listWH.AddNew();
                    listWH.SyncObjectToTable(listWHObject);
                    listWH.UpdateData();
                    return Ok(listWHObject);
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
                ListWH listWH = new ListWH(this.FTSMain);
                listWH.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }
    }
}
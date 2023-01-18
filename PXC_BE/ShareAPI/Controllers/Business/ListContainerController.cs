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
    public class ListContainerController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new ListContainer(this.FTSMain);
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                ListContainer listContainer = new ListContainer(this.FTSMain);
                listContainer.LoadDataByID(idvalue);
                return Ok(listContainer.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(ListContainerObject listContainerObject)
        {
            try
            {
                ListContainer listContainer = new ListContainer(this.FTSMain);
                return Ok(listContainer.IsDataChanged(listContainerObject));
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
                ListContainer listContainer = new ListContainer(this.FTSMain);
                DataRow row = listContainer.AddNew();
                return Ok(listContainer.GetDataObject());
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
                ListContainer listContainer = new ListContainer(this.FTSMain);
                listContainer.LoadDataByID(idvalue);
                if (listContainer.IsValidRow(0))
                {
                    DataRow newrow = listContainer.CopyRecord(0);
                    return Ok(listContainer.GetDataObject());
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
        public IHttpActionResult UpdateEditData(ListContainerObject listContainerObject)
        {
            try
            {
                ListContainer listContainer = new ListContainer(this.FTSMain);
                listContainer.LoadDataByID(listContainerObject.CONTAINER_ID);
                if (listContainer.IsValidRow(0))
                {
                    listContainer.SyncObjectToTable(listContainerObject);
                    listContainer.UpdateData();
                    return Ok(listContainerObject);
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
        public IHttpActionResult UpdateNewData(ListContainerObject listContainerObject)
        {
            try
            {
                ListContainer listContainer = new ListContainer(this.FTSMain);
                listContainer.LoadDataByID(listContainerObject.CONTAINER_ID);
                if (listContainer.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    listContainer.AddNew();
                    listContainer.SyncObjectToTable(listContainerObject);
                    listContainer.UpdateData();
                    return Ok(listContainerObject);
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
                ListContainer listContainer = new ListContainer(this.FTSMain);
                listContainer.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }
    }
}
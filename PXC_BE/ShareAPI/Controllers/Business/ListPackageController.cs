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
    public class ListPackageController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new ListPackage(this.FTSMain);
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                ListPackage listPackage = new ListPackage(this.FTSMain);
                listPackage.LoadDataByID(idvalue);
                return Ok(listPackage.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(ListPackageObject listpackageobject)
        {
            try
            {
                ListPackage listPackage = new ListPackage(this.FTSMain);
                return Ok(listPackage.IsDataChanged(listpackageobject));
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
                ListPackage listPackage = new ListPackage(this.FTSMain);
                DataRow row = listPackage.AddNew();
                return Ok(listPackage.GetDataObject());
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
                ListPackage listPackage = new ListPackage(this.FTSMain);
                listPackage.LoadDataByID(idvalue);
                if (listPackage.IsValidRow(0))
                {
                    DataRow newrow = listPackage.CopyRecord(0);
                    return Ok(listPackage.GetDataObject());
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
        public IHttpActionResult UpdateEditData(ListPackageObject listpackageobject)
        {
            try
            {
                ListPackage listPackage = new ListPackage(this.FTSMain);
                listPackage.LoadDataByID(listpackageobject.PACKAGE_CODE);
                if (listPackage.IsValidRow(0))
                {
                    listPackage.SyncObjectToTable(listpackageobject);
                    listPackage.UpdateData();
                    return Ok(listpackageobject);
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
        public IHttpActionResult UpdateNewData(ListPackageObject listpackageobject)
        {
            try
            {
                ListPackage listPackage = new ListPackage(this.FTSMain);
                listPackage.LoadDataByID(listpackageobject.PACKAGE_CODE);
                if (listPackage.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    listPackage.AddNew();
                    listPackage.SyncObjectToTable(listpackageobject);
                    listPackage.UpdateData();
                    return Ok(listpackageobject);
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
                ListPackage listPackage = new ListPackage(this.FTSMain);
                listPackage.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }
    }
}
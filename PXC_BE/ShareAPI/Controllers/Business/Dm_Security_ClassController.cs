using FTS.Base.API;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Acc;
using FTS.ShareBusiness.Model;
using System;
using System.Net;
using System.Web.Http;

namespace FTS.ShareAPI.Controllers.Business
{
    public class Dm_Security_ClassController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Security_Class(this.FTSMain);
        }

        [HttpPost]
        public IHttpActionResult UpdateEditData(Dm_Security_ClassObject dmsecurityclassobject)
        {
            try
            {
                Dm_Security_Class dmSecurityClass = new Dm_Security_Class(this.FTSMain);
                dmSecurityClass.LoadDataByID(dmsecurityclassobject.SECURITY_CLASS_ID);
                if (dmSecurityClass.IsValidRow(0))
                {
                    dmSecurityClass.SyncObjectToTable(dmsecurityclassobject);
                    dmSecurityClass.UpdateData();
                    return Ok(dmsecurityclassobject);
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

        [HttpPost]
        public IHttpActionResult UpdateNewData(Dm_Security_ClassObject dmsecurityclassobject)
        {
            try
            {
                Dm_Security_Class dmSecurityClass = new Dm_Security_Class(this.FTSMain);
                dmSecurityClass.LoadDataByID(dmsecurityclassobject.SECURITY_CLASS_ID);
                if (dmSecurityClass.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmSecurityClass.AddNew();
                    dmSecurityClass.SyncObjectToTable(dmsecurityclassobject);
                    dmSecurityClass.UpdateData();
                    return Ok(dmsecurityclassobject);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteData(string idvalue)
        {
            try
            {
                Dm_Security_Class dmSecurityClass = new Dm_Security_Class(this.FTSMain);
                dmSecurityClass.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                Dm_Security_Class dmSecurityClass = new Dm_Security_Class(this.FTSMain);
                dmSecurityClass.LoadDataByID(idvalue);
                return Ok(dmSecurityClass.GetDataObject());
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
                Dm_Security_Class dmSecurityClass = new Dm_Security_Class(this.FTSMain);
                dmSecurityClass.AddNew();
                return Ok(dmSecurityClass.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }
    }
}
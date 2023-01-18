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
    public class Dm_Security_TypeController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Security_Type(this.FTSMain);
        }

        [HttpPost]
        public IHttpActionResult UpdateEditData(Dm_Security_TypeObject dmsecuritytypeobject)
        {
            try
            {
                Dm_Security_Type dmSecurityType = new Dm_Security_Type(this.FTSMain);
                dmSecurityType.LoadDataByID(dmsecuritytypeobject.SECURITY_TYPE_ID);
                if (dmSecurityType.IsValidRow(0))
                {
                    dmSecurityType.SyncObjectToTable(dmsecuritytypeobject);
                    dmSecurityType.UpdateData();
                    return Ok(dmsecuritytypeobject);
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
        public IHttpActionResult UpdateNewData(Dm_Security_TypeObject dmsecuritytypeobject)
        {
            try
            {
                Dm_Security_Type dmSecurityType = new Dm_Security_Type(this.FTSMain);
                dmSecurityType.LoadDataByID(dmsecuritytypeobject.SECURITY_TYPE_ID);
                if (dmSecurityType.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmSecurityType.AddNew();
                    dmSecurityType.SyncObjectToTable(dmsecuritytypeobject);
                    dmSecurityType.UpdateData();
                    return Ok(dmsecuritytypeobject);
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
                Dm_Security_Type dmSecurityType = new Dm_Security_Type(this.FTSMain);
                dmSecurityType.DeleteInData(idvalue);
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
                Dm_Security_Type dmSecurityType = new Dm_Security_Type(this.FTSMain);
                dmSecurityType.LoadDataByID(idvalue);
                return Ok(dmSecurityType.GetDataObject());
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
                Dm_Security_Type dmSecurityType = new Dm_Security_Type(this.FTSMain);
                dmSecurityType.AddNew();
                return Ok(dmSecurityType.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }
    }
}
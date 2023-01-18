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
    public class Dm_DepartmentController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Department(this.FTSMain);
        }

        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                Dm_Department dmDepartment = new Dm_Department(this.FTSMain);
                dmDepartment.LoadDataByID(idvalue);
                return Ok(dmDepartment.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpPost]
        public IHttpActionResult UpdateEditData(Dm_DepartmentObject dmdepartmentobject)
        {
            try
            {
                Dm_Department dmDepartment = new Dm_Department(this.FTSMain);
                dmDepartment.LoadDataByID(dmdepartmentobject.DEPARTMENT_ID);
                if (dmDepartment.IsValidRow(0))
                {
                    dmDepartment.SyncObjectToTable(dmdepartmentobject);
                    dmDepartment.UpdateData();
                    return Ok(dmdepartmentobject);
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
        public IHttpActionResult UpdateNewData(Dm_DepartmentObject dmdepartmentobject)
        {
            try
            {
                Dm_Department dmDepartment = new Dm_Department(this.FTSMain);
                dmDepartment.LoadDataByID(dmdepartmentobject.DEPARTMENT_ID);
                if (dmDepartment.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmDepartment.AddNew();
                    dmDepartment.SyncObjectToTable(dmdepartmentobject);
                    dmDepartment.UpdateData();
                    return Ok(dmdepartmentobject);
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
                Dm_Department dmDepartment = new Dm_Department(this.FTSMain);
                dmDepartment.DeleteInData(idvalue);
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
                Dm_Department dmDepartment = new Dm_Department(this.FTSMain);
                dmDepartment.AddNew();
                return Ok(dmDepartment.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

    }
}

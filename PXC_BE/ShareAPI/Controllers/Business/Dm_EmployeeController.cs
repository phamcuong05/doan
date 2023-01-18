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
    public class Dm_EmployeeController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Employee(this.FTSMain);
        }

        [HttpPost]
        public IHttpActionResult UpdateEditData(Dm_EmployeeObject dmemployeeobject)
        {
            try
            {
                Dm_Employee dmEmployee = new Dm_Employee(this.FTSMain);
                dmEmployee.LoadDataByID(dmemployeeobject.EMPLOYEE_ID);
                if (dmEmployee.IsValidRow(0))
                {
                    dmEmployee.SyncObjectToTable(dmemployeeobject);
                    dmEmployee.UpdateData();
                    return Ok(dmemployeeobject);
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
        public IHttpActionResult UpdateNewData(Dm_EmployeeObject dmemployeeobject)
        {
            try
            {
                Dm_Employee dmEmployee = new Dm_Employee(this.FTSMain);
                dmEmployee.LoadDataByID(dmemployeeobject.EMPLOYEE_ID);
                if (dmEmployee.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmEmployee.AddNew();
                    dmEmployee.SyncObjectToTable(dmemployeeobject);
                    dmEmployee.UpdateData();
                    return Ok(dmemployeeobject);
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
                Dm_Employee dmEmployee = new Dm_Employee(this.FTSMain);
                dmEmployee.DeleteInData(idvalue);
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
                Dm_Employee dmEmployee = new Dm_Employee(this.FTSMain);
                dmEmployee.LoadDataByID(idvalue);
                return Ok(dmEmployee.GetDataObject());
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
                Dm_Employee dmEmployee = new Dm_Employee(this.FTSMain);
                dmEmployee.AddNew();
                return Ok(dmEmployee.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }
    }
}

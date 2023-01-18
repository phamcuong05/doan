
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
    public class Dm_Expense_ClassController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Expense_Class(this.FTSMain);
        }

        [HttpPost]
        public IHttpActionResult UpdateEditData(DM_Expense_ClassObject dmexpenseclassobject)
        {
            try
            {
                Dm_Expense_Class dmExpenseClass = new Dm_Expense_Class(this.FTSMain);
                dmExpenseClass.LoadDataByID(dmexpenseclassobject.EXPENSE_CLASS_ID);
                if (dmExpenseClass.IsValidRow(0))
                {
                    dmExpenseClass.SyncObjectToTable(dmexpenseclassobject);
                    dmExpenseClass.UpdateData();
                    return Ok(dmexpenseclassobject);
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
        public IHttpActionResult UpdateNewData(DM_Expense_ClassObject dmexpenseclassobject)
        {
            try
            {
                Dm_Expense_Class dmExpenseClass = new Dm_Expense_Class(this.FTSMain);
                dmExpenseClass.LoadDataByID(dmexpenseclassobject.EXPENSE_CLASS_ID);
                if (dmExpenseClass.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmExpenseClass.AddNew();
                    dmExpenseClass.SyncObjectToTable(dmexpenseclassobject);
                    dmExpenseClass.UpdateData();
                    return Ok(dmexpenseclassobject);
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
                Dm_Expense_Class dmExpenseClass = new Dm_Expense_Class(this.FTSMain);
                dmExpenseClass.DeleteInData(idvalue);
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
                Dm_Expense_Class dmExpenseClass = new Dm_Expense_Class(this.FTSMain);
                dmExpenseClass.AddNew();
                return Ok(dmExpenseClass.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }
    }
}

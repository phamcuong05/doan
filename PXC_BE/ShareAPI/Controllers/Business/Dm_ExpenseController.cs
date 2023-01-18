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
    public class Dm_ExpenseController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Expense(this.FTSMain);
        }

        [HttpPost]
        public IHttpActionResult UpdateEditData(DM_ExpenseObject dmexpenseobject)
        {
            try
            {
                Dm_Expense dmExpense = new Dm_Expense(this.FTSMain);
                dmExpense.LoadDataByID(dmexpenseobject.EXPENSE_ID);
                if (dmExpense.IsValidRow(0))
                {
                    dmExpense.SyncObjectToTable(dmexpenseobject);
                    dmExpense.UpdateData();
                    return Ok(dmexpenseobject);
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
        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                Dm_Expense dmExpense = new Dm_Expense(this.FTSMain);
                dmExpense.LoadDataByID(idvalue);
                return Ok(dmExpense.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }
        [HttpGet]

        [HttpPost]
        public IHttpActionResult UpdateNewData(DM_ExpenseObject dmexpenseobject)
        {
            try
            {
                Dm_Expense dmExpense = new Dm_Expense(this.FTSMain);
                dmExpense.LoadDataByID(dmexpenseobject.EXPENSE_ID);
                if (dmExpense.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmExpense.AddNew();
                    dmExpense.SyncObjectToTable(dmexpenseobject);
                    dmExpense.UpdateData();
                    return Ok(dmexpenseobject);
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
                Dm_Expense dmExpense = new Dm_Expense(this.FTSMain);
                dmExpense.DeleteInData(idvalue);
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
                Dm_Expense dmExpense = new Dm_Expense(this.FTSMain);
                dmExpense.AddNew();
                return Ok(dmExpense.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }
    }
}

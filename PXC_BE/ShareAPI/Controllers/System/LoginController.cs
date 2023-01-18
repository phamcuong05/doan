using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web.Http;
using FTS.Base.API;
using FTS.Base.Model;
using FTS.Base.Security;

namespace FTS.ShareAPI.Controllers
{

    [Authorize]
    public class LoginController : ApiBaseController
    {

        [HttpGet]
        public IHttpActionResult GetStart()
        {
            try
            {

                DataSet dataSet = new DataSet();
                string sql = "SELECT SEC_USER.*, ORGANIZATION_NAME FROM SEC_USER LEFT JOIN DM_ORGANIZATION ON SEC_USER.ORGANIZATION_ID = DM_ORGANIZATION.ORGANIZATION_ID WHERE USER_ID = '" + this.FTSMain.UserInfo.UserID + "'";

                DataTable SEC_USER = this.FTSMain.DbMain.LoadDataTable(this.FTSMain.DbMain.GetSqlStringCommand(sql), "SEC_USER");
                if (SEC_USER.Rows.Count == 0)
                {
                    throw new Exception("USER_ID_INVALID");
                }
                //SYS_SYSTEMVAR
                sql = "SELECT * FROM SYS_SYSTEMVAR";
                DataTable SYS_SYSTEMVAR = this.FTSMain.DbMain.LoadDataTable(this.FTSMain.DbMain.GetSqlStringCommand(sql), "SYS_SYSTEMVAR");
                //SYS_TABLE
                sql = "SELECT * FROM SYS_TABLE";
                DataTable SYS_TABLE = this.FTSMain.DbMain.LoadDataTable(this.FTSMain.DbMain.GetSqlStringCommand(sql), "SYS_TABLE");


                //sql = @"SELECT SYSWEB_MENU.* ,ISNULL(MODULE.MENU_NAME,'') AS MODULE_NAME,ISNULL(MODULE.ICON_CLS,'') AS MODULE_ICON_CLS,ISNULL(MODULE.MENU_ORDER,'') AS MODULE_ORDER,
                //               ISNULL(MENU_GROUP.MENU_NAME,'') AS MENU_GROUP_NAME,ISNULL(MENU_GROUP.ICON_CLS, '') AS MENU_GROUP_ICON_CLS, ISNULL(MENU_GROUP.MENU_ORDER, '') AS MENU_GROUP_ORDER
                //        FROM SYSWEB_MENU
                //            LEFT JOIN(SELECT* FROM SYSWEB_MENU WHERE MENU_TYPE = 1) MODULE ON SYSWEB_MENU.MODULE_ID = MODULE.MENU_ID
                //            LEFT JOIN(SELECT* FROM SYSWEB_MENU WHERE MENU_TYPE = 2) MENU_GROUP ON SYSWEB_MENU.MENU_GROUP_ID = MENU_GROUP.MENU_ID
                //        WHERE SYSWEB_MENU.ACTIVE = 1 AND SYSWEB_MENU.MENU_TYPE = 3";
                sql = $@"SELECT MENU_ID, MENU_NAME, MODULE_ID,MENU_GROUP_ID, ICON_CLS, HREF, MAP_PATH, MENU_ORDER, EXPAND_TYPE,'MENU' AS MENU_TYPE, ACTIVE,MODULE_ID AS MODULE_NAME,'' AS MODULE_ICON_CLS,1 AS MODULE_ORDER,
                               '' AS MENU_GROUP_NAME,'' AS MENU_GROUP_ICON_CLS, 1 AS MENU_GROUP_ORDER
                        FROM SYSWEB_MENU
                        WHERE SYSWEB_MENU.ACTIVE = 1 AND SYSWEB_MENU.MENU_TYPE = 3";
                this.FTSMain.DbMain.LoadDataSet(this.FTSMain.DbMain.GetSqlStringCommand(sql), dataSet, "SYSWEB_MENU");
                sql = $@"SELECT 'TRAN_' + TRAN_ID AS MENU_ID, TRAN_NAME AS MENU_NAME, MODULE_ID, 'FUNC' MENU_GROUP_ID,'icon fas fa-file-invoice-dollar' AS ICON_CLS, HREF, MAP_PATH,LIST_ORDER AS MENU_ORDER,'popup' AS EXPAND_TYPE,'TRAN' AS MENU_TYPE, ACTIVE,MODULE_ID AS MODULE_NAME,'' AS MODULE_ICON_CLS,1 AS MODULE_ORDER,
                              '' AS MENU_GROUP_NAME,'' AS MENU_GROUP_ICON_CLS, 1 AS MENU_GROUP_ORDER
                        FROM SYS_TRAN
                        WHERE SYS_TRAN.ACTIVE = 1";
                this.FTSMain.DbMain.LoadDataSet(this.FTSMain.DbMain.GetSqlStringCommand(sql), dataSet, "SYSWEB_MENU");
                //UserInfoObject
                UserInfoObject userInfoObject = new UserInfoObject(this.FTSMain.UserInfo.WorkingYear, SEC_USER.Rows[0]);
                //Sys_SystemvarObject
                List<Sys_SystemvarObject> systemvars = new List<Sys_SystemvarObject>();
                foreach (DataRow dr in SYS_SYSTEMVAR.Rows)
                {
                    Sys_SystemvarObject SystemvarObject = new Sys_SystemvarObject(dr);
                    systemvars.Add(SystemvarObject);
                }

                //Sys_SystemvarObject
                List<Sys_TableObject> systable= new List<Sys_TableObject>();
                foreach (DataRow dr in SYS_TABLE.Rows)
                {
                    Sys_TableObject systableObject = new Sys_TableObject(dr);
                    systable.Add(systableObject);
                }

                List<MenuObject> menu = new List<MenuObject>();
                List<ModuleObject> moduleList = ModuleList.GetModuleList(this.FTSMain);
                #region DASHBOARD
                DataView dvMenuNoneModule = new DataView(dataSet.Tables["SYSWEB_MENU"], "MODULE_ID= ''", "MENU_GROUP_ID ASC, MENU_ORDER ASC", DataViewRowState.CurrentRows);
                foreach (DataRowView drvMenuNoneModule in dvMenuNoneModule)
                {
                    MenuObject menuObject = new MenuObject((DataRow)drvMenuNoneModule.Row);
                    menu.Add(menuObject);
                }
                #endregion

                #region MODULE
                int MENU_ORDER = 0;
                foreach (ModuleObject moduleObject in moduleList)
                {
                    DataView dvMenu = new DataView(dataSet.Tables["SYSWEB_MENU"], "MODULE_ID= '" + moduleObject.MODULE_ID + "'", "MENU_GROUP_ID ASC, MENU_ORDER ASC", DataViewRowState.CurrentRows);
                    foreach (DataRowView drvMenu in dvMenu)
                    {
                        MENU_ORDER++;
                        drvMenu["MENU_ORDER"] = MENU_ORDER;
                        drvMenu["MODULE_ICON_CLS"] = "icon fas fa-book";
                        drvMenu["MODULE_NAME"] = this.FTSMain.MsgManager.GetMessage("MODULE_LIST_" + moduleObject.MODULE_ID);
                        if (drvMenu["EXPAND_TYPE"].ToString() == "popup" && drvMenu["MENU_TYPE"].ToString() != "REPORT")
                        {
                            drvMenu["MENU_GROUP_NAME"] = this.FTSMain.MsgManager.GetMessage("MENU_GROUP_" + drvMenu["MENU_GROUP_ID"]);
                            drvMenu["MENU_GROUP_ID"] = drvMenu["MODULE_ID"].ToString() + "_" + drvMenu["MENU_GROUP_ID"].ToString();
                        }
                        if (drvMenu["MENU_TYPE"].ToString() == "REPORT")
                        {
                            drvMenu["MENU_NAME"] = this.FTSMain.ResourceManager.GetReportName(drvMenu["MENU_NAME"].ToString());
                        }
                        MenuObject menuObject = new MenuObject((DataRow)drvMenu.Row);
                        menu.Add(menuObject);
                    }
                }
                #endregion

                /*Khoi tao MainWeb*/
                var FTSMain = new
                {
                    userInfo = userInfoObject,
                    systemVars = systemvars,
                    sysTable = systable,
                    menu = menu,
                };
                return Ok(FTSMain);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }
    }
}
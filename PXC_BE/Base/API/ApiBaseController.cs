
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Net;
using FTS.Base.Systems;
using System.Net.Http;
using System.ServiceModel.Channels;
using FTS.Base.Model;
using Newtonsoft.Json;
using System.IO;

namespace FTS.Base.API
{
    /// <summary>
    /// FTSBaseApiController
    /// Create by: tan.vu
    /// </summary>
    public class ApiBaseController : ApiController
    {
        /// <summary>
        /// create by: TAN.VU
        /// Khởi tạo FTSMain
        /// </summary>
        public FTSMain FTSMain
        {
            get
            {
                UserInfo userInfo = new UserInfo();
                userInfo.UserID = this.GetValueInTokenByKey("UserID");
                userInfo.OrganizationID = this.GetValueInTokenByKey("OrganizationID");
                userInfo.OrganizationName = this.GetValueInTokenByKey("OrganizationName");
                userInfo.UserGroupID = this.GetValueInTokenByKey("UserGroupID");
                userInfo.WorkingYear = int.Parse(this.GetValueInTokenByKey("WorkingYear"));
                userInfo.ClientIP = GetClientIp();

                FTSMain ftsMain = StaticMain.FTSMain();
                ftsMain.UserInfo = userInfo;

                return ftsMain;
            }
        }

        /// <summary>
        /// Create by: TAN.VU
        /// Lấy thông tin được lưu trong token
        /// </summary>
        /// <param name="key">key truyền vào</param>
        /// <returns></returns>
        protected string GetValueInTokenByKey(string key)
        {
            string value = string.Empty;
            /*Lay du thong tin tu header API truyen len*/
            var headers = Request.Headers;
            if (headers.Contains(key))
            {
                value = headers.GetValues(key).First();
                if (value != string.Empty)
                {
                    return value;
                }
            }
            /*Lay thong tin trong token*/
            var identity = HttpContext.Current.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                value = claims.FirstOrDefault(x => x.Type == key).Value;
                return value;
            }
            return string.Empty;
        }

        protected void SetClaimsIdentity(string key, string value)
        {
            var identity = HttpContext.Current.User.Identity as ClaimsIdentity;
            var existingClaim = identity.FindFirst(key);
            if (existingClaim != null)
                identity.RemoveClaim(existingClaim);

            // add new claim
            identity.AddClaim(new Claim(key, value));
        }

        /// <summary>
        /// GetClientIp
        /// Create by: TAN.VU
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string GetClientIp(HttpRequestMessage request = null)
        {
            request = request ?? Request;

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            else if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return string.Empty;
            }
        }

        #region import excel
        /// <summary>
        /// Create by: TAN.VU
        /// Lấy danh sách các template import từ excel
        /// </summary>
        /// <param name="tranid">Mã chứng từ hoặc tableName</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetImportTemplate(string tranid, string tablename)
        {
            try
            {
                DataSet dataSet = new DataSet();
                string sql = @"SELECT  * FROM DM_TEMPLATE
                               WHERE (TRAN_ID_NAME = '" + tranid + "' OR TABLE_NAME = '" + tablename + "') AND ACTIVE = 1 ";
                this.FTSMain.DbMain.LoadDataSet(this.FTSMain.DbMain.GetSqlStringCommand(sql), dataSet, "DM_TEMPLATE");
                sql = @"SELECT DM_TEMPLATE_DETAIL.* FROM DM_TEMPLATE_DETAIL INNER JOIN DM_TEMPLATE ON DM_TEMPLATE_DETAIL.FR_KEY = DM_TEMPLATE.PR_KEY
                        WHERE (TRAN_ID_NAME = '" + tranid + "' OR TABLE_NAME = '" + tablename + "') AND ACTIVE = 1 ORDER BY DM_TEMPLATE_DETAIL.FR_KEY, DM_TEMPLATE_DETAIL.LIST_ORDER ASC";
                this.FTSMain.DbMain.LoadDataSet(this.FTSMain.DbMain.GetSqlStringCommand(sql), dataSet, "DM_TEMPLATE_DETAIL");
                return Ok(dataSet);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }

        
        /// <summary>
        /// Nhận dữ liệu import từ FE
        /// </summary>
        /// <param name="importData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ImportExcel(ImportDataObject importData)
        {
            try
            {
                DataSet dataSet = (DataSet)JsonConvert.DeserializeObject(importData.data, (typeof(DataSet)));
                importData.dm_template = dataSet.Tables["DM_TEMPLATE"];
                importData.dm_template_detail = dataSet.Tables["DM_TEMPLATE_DETAIL"];
                importData.excelData = dataSet.Tables["EXCEL_DATA"];
                this.ImportData(importData);
                /*Tra ve cac dong du lieu import khong thanh cong*/
                return Ok(importData.excelData);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }

        /// <summary>
        /// Xử lý dữ liệu import vào db
        /// create by : tan.vu
        /// </summary>
        /// <param name="importData"></param>
        protected virtual void ImportData(ImportDataObject importData)
        {

        }

        /// <summary>
        /// Tạo file import excel từ khai báo
        /// </summary>
        /// <param name="importData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateExcelFile(ImportDataObject importData)
        {
            try
            {
                DataSet dataSet = (DataSet)JsonConvert.DeserializeObject(importData.data, (typeof(DataSet)));
                importData.dm_template = dataSet.Tables["DM_TEMPLATE"];
                importData.dm_template_detail = dataSet.Tables["DM_TEMPLATE_DETAIL"];
                importData.excelData = dataSet.Tables["EXCEL_DATA"];

                string baseURL = Request.RequestUri.GetLeftPart(UriPartial.Authority);
                string filename = Path.GetRandomFileName();
                string exceloutPutFile = baseURL + @"/FileTemp/" + filename + ".xlsx";
                string excelfolderfile = System.Web.HttpRuntime.AppDomainAppPath + @"FileTemp\\" + filename + ".xlsx";
                importData.CreateExcelFile(exceloutPutFile, excelfolderfile);

                return Ok(exceloutPutFile);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }
        #endregion
    }
}
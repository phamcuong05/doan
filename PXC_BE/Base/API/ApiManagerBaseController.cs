
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Web.Http;
using FTS.Base.Model.Paging;
using Newtonsoft.Json;
using FTS.Base.Business;

namespace FTS.Base.API
{
    /// <summary>
    /// FTSBaseApiController
    /// Create by: tan.vu
    /// </summary>
    public abstract class ApiManagerBaseController : ApiBaseController
    {
        [HttpGet]
        public IHttpActionResult GetTranOutput(string tranid)
        {
            try
            {
                string sql = @"SELECT  PR_KEY, TRAN_ID, OUTPUT_NAME  FROM SYS_TRAN_OUTPUT 
                               WHERE TRAN_ID = '" + tranid + "' AND ACTIVE = 1 ";
                DataTable SYS_TRAN_OUTPUT = this.FTSMain.DbMain.LoadDataTable(this.FTSMain.DbMain.GetSqlStringCommand(sql), "SYS_TRAN_OUTPUT");
                return Ok(SYS_TRAN_OUTPUT);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }



        public abstract ObjectBase GetManagerListing();

        public abstract void PrintRecord(Guid prkeyoutput, Guid prkey, string pdfile, string excelfile, string wordfile);

        [HttpGet]

        public virtual IHttpActionResult LoadPagingData(string tranid, string fields, string summaryfields, string filter, string sorts, int pagesize, int pageindex)
        {
            try
            {
                ObjectBase objectBase = this.GetManagerListing();
                List<FilterGroup> filterGroupList = null;
                List<Sort> sortList = null;
                List<string> fieldList = null;
                List<string> summaryfieldList = null;
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    filterGroupList = JsonConvert.DeserializeObject<List<FilterGroup>>(filter,new JsonSerializerSettings {
                        DateTimeZoneHandling = DateTimeZoneHandling.Local
                    });
                }

                if (!string.IsNullOrWhiteSpace(sorts))
                {
                    sortList = JsonConvert.DeserializeObject<List<Sort>>(sorts);
                }

                if (!string.IsNullOrWhiteSpace(fields))
                {
                    fieldList = JsonConvert.DeserializeObject<List<string>>(fields);
                }

                if (!string.IsNullOrWhiteSpace(summaryfields))
                {
                    summaryfieldList = JsonConvert.DeserializeObject<List<string>>(summaryfields);
                }

                var datas = objectBase.LoadPagingData(fieldList, summaryfieldList, filterGroupList, sortList, pagesize, pageindex);
                return Ok(datas);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        [HttpGet]
        public IHttpActionResult Print(Guid prkeyoutput, Guid prkey)
        {
            try
            {
                string baseURL = Request.RequestUri.GetLeftPart(UriPartial.Authority);
                string filename = Path.GetRandomFileName();

                string pdfoutPutFile = baseURL + @"/FileTemp/" + filename + ".pdf";
                string exceloutPutFile = baseURL + @"/FileTemp/" + filename + ".xlsx";
                string wordoutPutFile = baseURL + @"/FileTemp/" + filename + ".doc";

                string pdffolderfile = System.Web.HttpRuntime.AppDomainAppPath + @"FileTemp\\" + filename + ".pdf";
                string excelfolderfile = System.Web.HttpRuntime.AppDomainAppPath + @"FileTemp\\" + filename + ".xlsx";
                string wordfolderfile = System.Web.HttpRuntime.AppDomainAppPath + @"FileTemp\\" + filename + ".doc";

                Dictionary<object, object> dict = new Dictionary<object, object>();
                dict.Add("PDF_FILE", pdfoutPutFile);
                dict.Add("EXCEL_FILE", exceloutPutFile);
                dict.Add("DOC_FILE", wordoutPutFile);

                this.PrintRecord(prkeyoutput, prkey, pdffolderfile, excelfolderfile, wordfolderfile);

                return Ok(dict);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }
    }
}
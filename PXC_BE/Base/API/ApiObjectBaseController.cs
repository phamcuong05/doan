using System;
using System.Data;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using FTS.Base.Business;
using FTS.Base.Model;
using FTS.Base.Model.Paging;
using Newtonsoft.Json;

namespace FTS.Base.API
{
    /// <summary>
    /// ApiObjectBaseController
    /// Create by: tan.vu
    /// Create date: 16/12/2021
    /// </summary>
    public abstract class ApiObjectBaseController : ApiBaseController
    {
        public abstract ObjectBase GetObjectBase();


        [HttpGet]
        public IHttpActionResult GetDataByFilter(string filter)
        {
            try
            {
                List<FilterGroup> filterGroupList = null;
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    filterGroupList = JsonConvert.DeserializeObject<List<FilterGroup>>(filter);
                }
                ObjectBase objectBase = this.GetObjectBase();
                objectBase.GetDataByFilter(filterGroupList);
                return Ok(objectBase.GetDataObjectList());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }

        [HttpGet]
        public virtual IHttpActionResult LoadPagingData(string tranid, string fields, string summaryfields, string filter, string sorts, int pagesize, int pageindex) {
            try {
                ObjectBase objectBase = this.GetObjectBase();
                List<FilterGroup> filterGroupList = null;
                List<Sort> sortList = null;
                List<string> fieldList = null;
                List<string> summaryfieldList = null;
                if (!string.IsNullOrWhiteSpace(filter)) {
                    filterGroupList = JsonConvert.DeserializeObject<List<FilterGroup>>(filter, new JsonSerializerSettings
                    {
                        DateTimeZoneHandling = DateTimeZoneHandling.Local
                    });
                }

                if (!string.IsNullOrWhiteSpace(sorts)) {
                    sortList = JsonConvert.DeserializeObject<List<Sort>>(sorts);
                }

                if (!string.IsNullOrWhiteSpace(fields)) {
                    fieldList = JsonConvert.DeserializeObject<List<string>>(fields);
                }

                if (!string.IsNullOrWhiteSpace(summaryfields)) {
                    summaryfieldList = JsonConvert.DeserializeObject<List<string>>(summaryfields);
                }

                var datas = objectBase.LoadPagingData(fieldList, summaryfieldList, filterGroupList, sortList, pagesize, pageindex);
                return Ok(datas);
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }


        protected override void ImportData(ImportDataObject importData)
        {
            ObjectBase objectBase = GetObjectBase();
            objectBase.ImportData(importData.excelData, importData.dm_template_detail);
            objectBase.UpdateData();
        }
    }
}
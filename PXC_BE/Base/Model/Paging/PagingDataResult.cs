using System.Collections.Generic;
using FTS.Base.Business;

namespace FTS.Base.Model.Paging
{
    public class PagingDataResult
    {
        public int RecordCount { get; set; }
        public List<ObjectInfoBase> Data { get; set; }
        public List<SummaryDataObject> SummaryData { get; set; }
    }
}

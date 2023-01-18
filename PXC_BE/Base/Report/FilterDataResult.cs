using System.Collections.Generic;
using FTS.Base.Business;
using FTS.Base.Report;

namespace FTS.Base.Model.Paging
{
    public class FilterDataResult
    {
        public int RecordCount { get; set; }
        public List<FieldSelectionObject> Data { get; set; }
    }
}

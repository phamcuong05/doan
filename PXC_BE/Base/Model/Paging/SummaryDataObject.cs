namespace FTS.Base.Model.Paging
{
    public class SummaryDataObject
    {
        public SummaryDataObject(string fieldname, object summaryvalue) {
            this.FieldName = fieldname;
            this.SummaryValue = summaryvalue;
        }
        public string FieldName { get; set; }

        public object SummaryValue { get; set; }

    }
}

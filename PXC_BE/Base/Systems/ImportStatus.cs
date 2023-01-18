#region

using System.Collections.Generic;
using FTS.Base.Business;
using FTS.Base.Model;

#endregion

namespace FTS.Base.Systems {
    public static class ImportStatus {
        public static string SUCCESS = "SUCCESS";
        public static string FAIL = "FAIL";
        
        public static List<ImportStatusObject> GetImportStatusList(FTSMain ftsmain) {
            List<ImportStatusObject> list = new List<ImportStatusObject>();
            list.Add(new ImportStatusObject(SUCCESS, ftsmain.MsgManager.GetMessage("IMPORT_STATUS_" + SUCCESS)));
            list.Add(new ImportStatusObject(FAIL, ftsmain.MsgManager.GetMessage("IMPORT_STATUS_" + FAIL)));
            return list;
        }
    }
}
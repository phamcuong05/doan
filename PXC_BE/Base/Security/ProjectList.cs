#region

using System.Collections.Generic;
using FTS.Base.Model;
using FTS.Base.Systems;

#endregion

namespace FTS.Base.Security
{
    public static class ProjectList {
        public static string Finance = "FIN";
        //public static string Hotel = "HT";
        //public static string HRM = "HRM";
        //public static string POS = "POS";
        //public static string Transport = "TP";
        //public static string Claim = "BT";

        public static List<ProjectObject> GetProjectList(FTSMain ftsmain)
        {
            List<ProjectObject> list = new List<ProjectObject>();
            list.Add(new ProjectObject(Finance, ftsmain.MsgManager.GetMessage("PROJECT_LIST_" + Finance)));
            return list;
        }
    }
}
#region

using System.Collections.Generic;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;

#endregion

namespace FTS.ShareBusiness.Acc
{
    public class IssueReceipt {
        public static string ISSUE = "X";
        public static string RECEIPT = "N";
        
        public static List<IssueReceiptObject> GetIssueReceiptList(FTSMain ftsmain)
        {
            List<IssueReceiptObject> list = new List<IssueReceiptObject>();
            list.Add(new IssueReceiptObject(RECEIPT,ftsmain.MsgManager.GetMessage("ISSUE_RECEIPT_" + RECEIPT)));
            list.Add(new IssueReceiptObject(ISSUE,ftsmain.MsgManager.GetMessage("ISSUE_RECEIPT_" + ISSUE)));
            return list;
        }
    }
}
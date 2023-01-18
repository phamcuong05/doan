using System;
using System.Collections.Generic;
using System.Text;

namespace FTS.Base.Security {
    [Serializable] public class FTSFunction {
        public string Name;
        public string FunctionGroupID;
        public bool IsView;
        public bool IsAddNew;
        public bool IsEdit;
        public bool IsDelete;
        public bool IsExecutte;
        public bool IsApprove;

        
        public FTSFunction(string name, string functiongroupid, bool isview, bool isaddnew, bool isedit, bool isdelete, bool isexecute, bool isapprove) {
            this.Name = name;
            this.FunctionGroupID = functiongroupid;
            this.IsView = isview;
            this.IsAddNew = isaddnew;
            this.IsEdit = isedit;
            this.IsDelete = isdelete;
            this.IsExecutte = isexecute;
            this.IsApprove = isapprove;
        }
    }
}
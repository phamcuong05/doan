// ----------------------------------------------------------------------------------------
// Author:                    Nguyen Van Phu
// Company:                   FTS Company
// Assembly version:          1.0.*
// Date:                      12/28/2006
// Time:                      22:54
// Project Name:              Base
// Project Filename:          Base.csproj
// Project Item Name:         SecurityManager.cs
// Project Item Filename:     SecurityManager.cs
// Project Item Kind:         Code
// Purpose:                   
// ----------------------------------------------------------------------------------------

#region

using System;
using System.Collections;
using System.Data;
using FTS.Base.Business;
using FTS.Base.Security;
using FTS.Base.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;

#endregion

namespace FTS.Base.Systems {
    public class SecurityManager {
        private FTSMain mFTSMain;
        private Hashtable mHs;

        public SecurityManager(FTSMain ftsmain, bool isdataload) {
            this.mFTSMain = ftsmain;
            if (isdataload) {
                this.LoadData();
            } else {
                this.LoadEmptyData();
            }
        }

        public void CheckSecurity(FTSFunction ftsfunction, string actionid, string organizationid) {
            string key = null;
            object[] foundRow = null;
            bool rs = true;
            if (ftsfunction == null) {
                return;
            } else {
                key = ftsfunction.Name;
                foundRow = (object[]) this.mHs[key];
            }
            if (this.mFTSMain.UserInfo.UserID == FTSConstant.DevUser) {
                return;
            }
            
            if (foundRow != null) {
                if (actionid == DataAction.ViewAction && ftsfunction.IsView) {
                    rs = (bool) foundRow[0];
                }
                if (actionid == DataAction.AddAction && ftsfunction.IsAddNew) {
                    rs = (bool) foundRow[1];
                }
                if (actionid == DataAction.EditAction && ftsfunction.IsEdit) {
                    rs = (bool) foundRow[2];
                }
                if (actionid == DataAction.DeleteAction && ftsfunction.IsDelete) {
                    rs = (bool) foundRow[3];
                }
                if (actionid == DataAction.ExecuteAction && ftsfunction.IsExecutte) {
                    rs = (bool) foundRow[4];
                }
                if (actionid == DataAction.ApproveAction && ftsfunction.IsApprove) {
                    rs = (bool) foundRow[5];
                }
            }
            if (!rs) {
                throw (new FTSException("MSG_NO_PERMISSION {0} {1}", new object[] {ftsfunction.Name, actionid}));
            }
        }

        public bool CheckSecurityInvisible(FTSFunction ftsfunction, string actionid, string organizationid) {
            string key = null;
            object[] foundRow = null;
            if (ftsfunction == null) {
                return true;
            } else {
                key = ftsfunction.Name;
                foundRow = (object[]) this.mHs[key];
            }
            if (this.mFTSMain.UserInfo.UserID == FTSConstant.DevUser) {
                return true;
            }
            if (Functions.IsAdmin(this.mFTSMain)) {
                return true;
            }
            bool rs = true;
            if (foundRow != null) {
                if (actionid == DataAction.ViewAction && ftsfunction.IsView) {
                    rs = (bool) foundRow[0];
                }
                if (actionid == DataAction.AddAction && ftsfunction.IsAddNew) {
                    rs = (bool) foundRow[1];
                }
                if (actionid == DataAction.EditAction && ftsfunction.IsEdit) {
                    rs = (bool) foundRow[2];
                }
                if (actionid == DataAction.DeleteAction && ftsfunction.IsDelete) {
                    rs = (bool) foundRow[3];
                }
                if (actionid == DataAction.ExecuteAction && ftsfunction.IsExecutte) {
                    rs = (bool) foundRow[4];
                }
                if (actionid == DataAction.ApproveAction && ftsfunction.IsApprove) {
                    rs = (bool) foundRow[5];
                }
            }
            if (!rs) {
                return false;
            }
            return true;
        }

        public void CheckSecurity(FTSFunction ftsfunction, string actionid) {
            string key = null;
            object[] foundRow = null;
            bool rs = true;
            if (ftsfunction == null) {
                return;
            } else {
                key = ftsfunction.Name;
                foundRow = (object[]) this.mHs[key];
            }
            if (this.mFTSMain.UserInfo.UserID == FTSConstant.DevUser) {
                return;
            }
            

            if (foundRow != null) {
                if (actionid == DataAction.ViewAction && ftsfunction.IsView) {
                    rs = (bool) foundRow[0];
                }
                if (actionid == DataAction.AddAction && ftsfunction.IsAddNew) {
                    rs = (bool) foundRow[1];
                }
                if (actionid == DataAction.EditAction && ftsfunction.IsEdit) {
                    rs = (bool) foundRow[2];
                }
                if (actionid == DataAction.DeleteAction && ftsfunction.IsDelete) {
                    rs = (bool) foundRow[3];
                }
                if (actionid == DataAction.ExecuteAction && ftsfunction.IsExecutte) {
                    rs = (bool) foundRow[4];
                }
                if (actionid == DataAction.ApproveAction && ftsfunction.IsApprove) {
                    rs = (bool) foundRow[5];
                }
            }
            if (!rs) {
                throw (new FTSException("MSG_NO_PERMISSION {0} {1}", new object[] {ftsfunction.Name, actionid}));
            }
        }

        public bool CheckSecurityInvisible(FTSFunction ftsfunction, string actionid) {
            string key = null;
            object[] foundRow = null;
            bool rs = true;
            if (ftsfunction == null) {
                return true;
            } else {
                key = ftsfunction.Name;
                foundRow = (object[]) this.mHs[key];
            }
            if (this.mFTSMain.UserInfo.UserID == FTSConstant.DevUser) {
                return true;
            }
            

            if (foundRow != null) {
                if (actionid == DataAction.ViewAction && ftsfunction.IsView) {
                    rs = (bool) foundRow[0];
                }
                if (actionid == DataAction.AddAction && ftsfunction.IsAddNew) {
                    rs = (bool) foundRow[1];
                }
                if (actionid == DataAction.EditAction && ftsfunction.IsEdit) {
                    rs = (bool) foundRow[2];
                }
                if (actionid == DataAction.DeleteAction && ftsfunction.IsDelete) {
                    rs = (bool) foundRow[3];
                }
                if (actionid == DataAction.ExecuteAction && ftsfunction.IsExecutte) {
                    rs = (bool) foundRow[4];
                }
                if (actionid == DataAction.ApproveAction && ftsfunction.IsApprove) {
                    rs = (bool) foundRow[5];
                }
            }
            return rs;
        }

        private void LoadData() {
            this.mHs = new Hashtable();
            using (
                IDataReader rs =
                    this.mFTSMain.DbMain.ExecuteReader(
                        this.mFTSMain.DbMain.GetSqlStringCommand("select * from sec_permission where user_group_id in " +
                                                                 Functions.PopulateString(this.mFTSMain.UserInfo.UserGroupID) + ""))) {
                while (rs.Read()) {
                    string roleid = rs["FUNCTION_ID"].ToString();
                    object[] foundRow = (object[]) this.mHs[roleid];
                    if (this.mHs.Contains(roleid)) {
                        this.mHs.Remove(roleid);
                    }

                    if (foundRow == null) {
                        this.mHs.Add(roleid,
                            new object[] {
                                (Int16) rs["is_view"] == 1 ? true : false, (Int16) rs["is_addnew"] == 1 ? true : false,
                                (Int16) rs["is_edit"] == 1 ? true : false, (Int16) rs["is_delete"] == 1 ? true : false,
                                (Int16) rs["is_execute"] == 1 ? true : false, (Int16) rs["is_approve"] == 1 ? true : false
                            });
                    } else {
                        this.mHs.Add(roleid,
                            new object[] {
                                (Int16) rs["is_view"] == 1 || (bool) foundRow[0] ? true : false,
                                (Int16) rs["is_addnew"] == 1 || (bool) foundRow[1] ? true : false,
                                (Int16) rs["is_edit"] == 1 || (bool) foundRow[2] ? true : false,
                                (Int16) rs["is_delete"] == 1 || (bool) foundRow[3] ? true : false,
                                (Int16) rs["is_execute"] == 1 || (bool) foundRow[4] ? true : false,
                                (Int16) rs["is_approve"] == 1 || (bool) foundRow[5] ? true : false
                            });
                    }
                }
            }
        }

        private void LoadEmptyData() {
            this.mHs = new Hashtable();
        }
        
    }
}
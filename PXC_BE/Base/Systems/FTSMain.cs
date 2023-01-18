// ----------------------------------------------------------------------------------------
// Author:                    Nguyen Van Phu
// Company:                   FTS Company
// Assembly version:          1.0.*
// Date:                      12/28/2006
// Time:                      22:53
// Project Name:              Base
// Project Filename:          Base.csproj
// Project Item Name:         FTSMain.cs
// Project Item Filename:     FTSMain.cs
// Project Item Kind:         Code
// Purpose:                   
// ----------------------------------------------------------------------------------------

#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using FTS.Base.Business;
using FTS.Base.Security;
using FTS.Base.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;

#endregion

namespace FTS.Base.Systems {
    [Serializable] public class FTSMain : FTSMainBase {
        [NonSerialized]
        private ExceptionManager mExceptionManager;
        [NonSerialized]
        private SecurityManager mSecurityManager;
        [NonSerialized]
        private Dm_Organization mDmOrganization;


        public string LanguageList;
        private List<string> mProjectID;
        private string mSpecialChars = string.Empty;
        public bool IsCheckUserByDomain = false;
        public string DefaultActiveDirectoryServer = string.Empty;
        public FTSMain() : base() {}

        public ExceptionManager ExceptionManager {
            get { return this.mExceptionManager; }
            set { this.mExceptionManager = value; }
        }

        public SecurityManager SecurityManager {
            get { return this.mSecurityManager; }
            set { this.mSecurityManager = value; }
        }

        public Dm_Organization DmOrganization {
            get { return this.mDmOrganization; }
            set { this.mDmOrganization = value; }
        }

        public List<string> ProjectID {
            get { return this.mProjectID; }
            set { this.mProjectID = value; }
        }
        
        public string[] SpecialChars {
            get { return this.mSpecialChars.Split(' '); }
        }
        
        
        
        public void Run() {
            this.Language = "VN";
            this.Language = ConfigurationSettings.AppSettings["LANGUAGE"];
            var database = ConfigurationSettings.AppSettings["DATABASE"];
            var servername = ConfigurationSettings.AppSettings["SERVERNAME"];
            var userid = ConfigurationSettings.AppSettings["USERID"];
            var password = ConfigurationSettings.AppSettings["PASSWORD"];
            
            if (servername == string.Empty) {
                servername = "(local)";
            }

            if (userid == string.Empty) {
                userid = "sa";
            }

            if (userid == string.Empty) {
                userid = "master";
            }

            var reportdatabase = ConfigurationSettings.AppSettings["DATABASE_REPORT"];
            var reportservername = ConfigurationSettings.AppSettings["SERVERNAME_REPORT"];
            var reportuserid = ConfigurationSettings.AppSettings["USERID_REPORT"];
            var reportpassword = ConfigurationSettings.AppSettings["PASSWORD_REPORT"];
            
            if (reportservername == string.Empty) {
                reportservername = "(local)";
            }

            if (reportuserid == string.Empty) {
                reportuserid = "sa";
            }

            if (reportuserid == string.Empty) {
                reportuserid = "master";
            }

            this.CreateDatabase();
            this.DatabaseFile = database;
            this.DbMain.WorkingMode = WorkingMode.LAN;
            this.DbMain.SetConnectionString("Database=" + database + ";Server=" + servername + ";User ID=" + userid + ";Password=" + password + ";");

            this.DbReport.WorkingMode = WorkingMode.LAN;
            this.DbReport.SetConnectionString("Database=" + reportdatabase + ";Server=" + reportservername + ";User ID=" + reportuserid + ";Password=" + reportpassword + ";");


            this.Language = "VN";
            string tmpToken = this.DbMain.BuildParameterName("ABC");
            this.ParameterToken = tmpToken.Trim() != string.Empty ? tmpToken.Trim().Substring(0, 1) : string.Empty;
            
            if (this.UserInfo == null) {
                this.UserInfo = new UserInfo { UserGroupID = "ADMIN", OrganizationID = "0000" };
            }

            this.SystemVars = new SystemVars(this);
            
            this.DEBUG = (bool)this.SystemVars.GetSystemVars("DEBUG");
            this.DayStartOfFirstYear = (DateTime)this.SystemVars.GetSystemVars("DAY_START_YEAR");
            this.TPSL = (int)this.SystemVars.GetSystemVars("DECIMAL_QTY");
            this.TPSTVND = (int)this.SystemVars.GetSystemVars("DECIMAL_AMOUNT");
            this.TPSTNTE = (int)this.SystemVars.GetSystemVars("DECIMAL_AMOUNT_ORIG");
            this.TPDGVND = (int)this.SystemVars.GetSystemVars("DECIMAL_PRICE");
            this.TPDGNTE = (int)this.SystemVars.GetSystemVars("DECIMAL_PRICE_ORIG");
            this.MainCurrency = this.SystemVars.GetSystemVars("MAIN_CURRENCY").ToString();
            this.TPTG = (int)this.SystemVars.GetSystemVars("DECIMAL_EXCHANGE_RATE");

            this.FieldManager = new FieldManager(this);
            this.MsgManager = new MsgManager(this);
            this.SecurityManager = new SecurityManager(this, true);
            this.ResourceManager = new ResourceManager(this, "VN");
            this.ExceptionManager = new ExceptionManager(this);
            this.TableManager = new TableManager(this);
            this.IdManager = new IdManager(this);
            this.mDmOrganization = new Dm_Organization(this);




            DbConnection cnnreport = this.DbReport.CreateConnection();
            DbConnection cnn = this.DbMain.CreateConnection();
            this.DatabaseName = cnn.Database;
            this.DatabaseServer = cnn.DataSource;
        }

        
        
    }
}
// ----------------------------------------------------------------------------------------
// Author:                    Nguyen Van Phu
// Company:                   FTS Company
// Assembly version:          1.0.*
// Date:                      12/28/2006
// Time:                      22:55
// Project Name:              Base
// Project Filename:          Base.csproj
// Project Item Name:         Functions.cs
// Project Item Filename:     Functions.cs
// Project Item Kind:         Code
// Purpose:                   
// ----------------------------------------------------------------------------------------

#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using FTS.Base.Business;
using FTS.Base.Systems;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Net;
using System.Net.Mail;
using System.IO.Compression;
using System.Drawing;
using System.Management;
#endregion

namespace FTS.Base.Utilities {
    public class Functions {
        public static string GetPathName() {
            return FunctionsBase.GetPathName();
        }

        public static void CreateForder(string startPath) {
            System.IO.Directory.CreateDirectory(startPath);
        }
        public static string ValidKey1(string s) {
            MD5 md = MD5CryptoServiceProvider.Create();
            byte[] hash;
            //Create a new instance of ASCIIEncoding to
            //convert the string into an array of Unicode bytes.
            ASCIIEncoding enc = new ASCIIEncoding();
            //Convert the string into an array of bytes.
            byte[] buffer = enc.GetBytes(s);
            //Create the hash value from the array of bytes.
            hash = md.ComputeHash(buffer);
            //Display the hash value to the console.
            StringBuilder ret = new StringBuilder();
            foreach (byte b in hash) {
                ret.Append(b.ToString("x2"));
            }
            return ret.ToString();
        }

        #region string functions

        public static string Substring(string fieldname, int start, int end) {
            if (FTSConstant.DatabaseType == "SQL") {
                return "substring(" + fieldname + "," + start.ToString().Trim() + "," + end.ToString().Trim() + ")";
            } else {
                return "left(" + fieldname + "," + Convert.ToString(end - start) + ")";
            }
        }

        public static string PopulateString(string str) {
            string[] list = FunctionsBase.ParseString(str);
            if (list.Length == 0) {
                return "('abcd_1234')";
            }
            StringBuilder result = new StringBuilder();
            int j = 0;
            for (int i = 0; i < list.Length; i++) {
                if (list[i].Trim().Length != 0) {
                    if (j == 0) {
                        result.Append("('").Append(list[i].Trim());
                    } else {
                        result.Append("','").Append(list[i].Trim());
                    }
                    j++;
                }
            }
            if (result.Length > 0) {
                result.Append("')");
            } else {
                result.Append("('abcd_1234')");
            }

            return result.ToString();
        }

        public static string[] ParseStringbySymbol(string instr, string seperate_symbol) {
            int prepos = 0;
            int pos = 0;
            int start = 0;
            string instr1 = instr;
            if (instr1.Trim().Length > 0) {
                instr1 = instr1.Trim() + seperate_symbol;
            }
            if (instr1.Trim().Length > 0 && instr1.IndexOf(seperate_symbol, 0) == 0) {
                instr1 = instr1.Substring(1);
            }
            ArrayList retstring = new ArrayList();
            pos = instr1.IndexOf(seperate_symbol, start);
            while (pos > 0) {
                retstring.Add(instr1.Substring(prepos, pos - prepos).Trim());
                prepos = pos + seperate_symbol.Length;
                start = pos + seperate_symbol.Length;
                pos = instr1.IndexOf(seperate_symbol, start);
            }
            string[] ret = new string[retstring.Count];
            for (int i = 0; i < retstring.Count; i++) {
                ret[i] = (string) retstring[i];
            }
            return ret;
        }

        public static bool Between(string value, string startvalue, string endvalue) {
            return (value.CompareTo(startvalue) >= 0 && value.CompareTo(endvalue) <= 0);
        }

        public static bool Between(decimal value, decimal startvalue, decimal endvalue) {
            return (value >= startvalue && value <= endvalue);
        }

        public static bool InListRelative(string value, string list) {
            if (value.Trim().Length == 0) {
                return true;
            }
            string[] strsource = FunctionsBase.ParseString(list);
            for (int i = 0; i < strsource.Length; i++) {
                string listvalue = strsource[i].Trim();
                if (value.Trim().Length <= listvalue.Trim().Length) {
                    if (value.Trim() == listvalue.Substring(0, value.Trim().Length)) {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool InListRelative1(string value, string list) {
            if (value.Trim().Length == 0) {
                return true;
            }
            string[] strsource = FunctionsBase.ParseString(list);
            for (int i = 0; i < strsource.Length; i++) {
                string listvalue = strsource[i].Trim();
                if (listvalue != string.Empty && value.Trim().Length >= listvalue.Trim().Length) {
                    if (listvalue.Trim() == value.Substring(0, listvalue.Trim().Length)) {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool InListAbsolute(string value, string list) {
            if (value == "") {
                return false;
            }
            string[] strsource = FunctionsBase.ParseString(list);
            for (int i = 0; i < strsource.Length; i++) {
                string listvalue = strsource[i].Trim();
                if (value.Trim() == listvalue.Trim()) {
                    return true;
                }
            }
            return false;
        }

        public static string Right(string value, int numb) {
            if (value.Length < numb) {
                return value;
            }
            return value.Substring(value.Length - numb);
        }

        public static string Left(string value, int numb) {
            if (value.Length < numb) {
                return value;
            }
            return value.Substring(0, numb);
        }

        public static string ParseDate(DateTime d) {
            if (FTSConstant.DatabaseType == "ACCESS") {
                string year = d.Year.ToString();
                string month = d.Month.ToString();
                string day = d.Day.ToString();
                if (month.Length == 1) {
                    month = "0" + month;
                }
                if (day.Length == 1) {
                    day = "0" + day;
                }
                return "#" + year + "-" + month + "-" + day + "#";
            } else {
                if (FTSConstant.DatabaseType == "VISTADB") {
                    return "'" + d.Date.ToString("d", CultureInfo.InstalledUICulture) + "'";
                } else {
                    string year = d.Year.ToString();
                    string month = d.Month.ToString();
                    string day = d.Day.ToString();
                    if (month.Length == 1) {
                        month = "0" + month;
                    }
                    if (day.Length == 1) {
                        day = "0" + day;
                    }
                    return "'" + month + "/" + day + "/" + year + "'";
                }
            }
        }

        public static string ParseDateFilter(DateTime d) {
            string year = d.Year.ToString();
            string month = d.Month.ToString();
            string day = d.Day.ToString();
            if (month.Length == 1) {
                month = "0" + month;
            }
            if (day.Length == 1) {
                day = "0" + day;
            }
            return "#" + month + "/" + day + "/" + year + "#";
        }

        public static string ParseDateAccess(DateTime d) {
            string year = d.Year.ToString();
            string month = d.Month.ToString();
            string day = d.Day.ToString();
            if (month.Length == 1) {
                month = "0" + month;
            }
            if (day.Length == 1) {
                day = "0" + day;
            }
            return "#" + year + "-" + month + "-" + day + "#";
        }

        public static string ParseDateDisplay(FTSMain ftsmain, DateTime d) {
            string year = d.Year.ToString();
            string month = d.Month.ToString();
            string day = d.Day.ToString();
            if (month.Length == 1) {
                month = "0" + month;
            }
            if (day.Length == 1) {
                day = "0" + day;
            }
            return "#" + year + "-" + month + "-" + day + "#";
        }

        public static string ParseDateEmail(FTSMain ftsmain, DateTime d) {
            string year = d.Year.ToString();
            string month = d.Month.ToString();
            string day = d.Day.ToString();
            if (month.Length == 1) {
                month = "0" + month;
            }
            if (day.Length == 1) {
                day = "0" + day;
            }
            return "" + day+ "/" + month + "/" + year ;
        }

        public static string ParseDateAPI(DateTime d) {
            string year = d.Year.ToString();
            string month = d.Month.ToString();
            string day = d.Day.ToString();
            if (month.Length == 1) {
                month = "0" + month;
            }
            if (day.Length == 1) {
                day = "0" + day;
            }
            return year + "-" + month + "-" + day;
        }

        #endregion

        #region Date Time

        public static DateTime DayStartOfMonth(int month, int year) {
            return new DateTime(year, month, 1);
        }

        public static DateTime DayEndOfMonth(int month, int year) {
            int days = DateTime.DaysInMonth(year, month);
            return new DateTime(year, month, days);
        }

        #endregion

        #region Data Fuctions

        
        public static int FirstRow(DataTable table) {
            int rtn = -1;
            for (int i = 0; i < table.Rows.Count; i++) {
                if (table.Rows[i].RowState != DataRowState.Deleted) {
                    return i;
                }
            }
            return rtn;
        }

        public static void DeleteData(DataTable dt) {
            for (int i = 0; i < dt.Rows.Count; i++) {
                DataRow row = dt.Rows[i];
                if (row.RowState != DataRowState.Deleted) {
                    row.Delete();
                    DeleteData(dt);
                    return;
                }
            }
        }

        public static DataTable GetTablePivot(FTSMain ftsmain, string sSelect, string sfunctionSum, string sPivot, string tablename, string aliasTable_Name) {
            try {
                string sSql = "dbo.crosstab";
                DbCommand cmd = ftsmain.DbMain.GetSqlStringCommand(sSql);
                cmd.CommandType = CommandType.StoredProcedure;
                ftsmain.DbMain.AddInParameter(cmd, "@select", DbType.String, sSelect);
                ftsmain.DbMain.AddInParameter(cmd, "@sumfunc", DbType.String, sfunctionSum);
                ftsmain.DbMain.AddInParameter(cmd, "@pivot", DbType.String, sPivot);
                ftsmain.DbMain.AddInParameter(cmd, "@table", DbType.String, tablename);
                DataTable tbl = ftsmain.DbMain.LoadDataTable(cmd, aliasTable_Name);
                if (tbl.Rows.Count > 0) {
                    return tbl;
                }
                return null;
            } catch (Exception ex) {
                return null;
            }
        }

        //public static void GroupTable(DataSet ds, string tblsrcName, string tbldesName, string fields, string sumfields) {
        //    string tbledesname1 = tbldesName;
        //    if (tbldesName == tblsrcName) {
        //        tbledesname1 = tbldesName + "_ABC";
        //    }
        //    DataSetHelper dh = new DataSetHelper(ref ds);
        //    string[] fieldlist = FunctionsBase.ParseString(fields);
        //    string[] sumfieldlist = FunctionsBase.ParseString(sumfields);
        //    string abc = string.Empty;
        //    foreach (string fl in fieldlist)
        //    {
        //        abc = abc + "," + fl;
        //    }            
        //    foreach (string sfl in sumfieldlist)
        //    {
        //        abc = abc + "," + "sum(" + sfl + ") " + sfl;
        //    }
        //    if (abc.StartsWith(","))
        //        abc = abc.Substring(1);
        //    dh.CreateGroupByTable(tbledesname1, ds.Tables[tblsrcName], abc,fields);
        //    if (tbldesName == tblsrcName) {
        //        ds.Tables.Remove(tblsrcName);
        //        ds.Tables[tbledesname1].TableName = tbldesName;
        //    }
        //}

        public static void GroupTable1(DataSet ds, string tblsrcName, string tbldesName, string fields, string sumfields) {

            
            DataView dv = new DataView(ds.Tables[tblsrcName]);
            string[] fieldlist = fields.Split(',');
            string[] sumfieldlist = sumfields.Split(',');
            for (int i = 0; i < fieldlist.Length; i++) {
                fieldlist[i] = fieldlist[i].ToUpper().Trim();
            }
            for (int i = 0; i < sumfieldlist.Length; i++) {
                sumfieldlist[i] = sumfieldlist[i].ToUpper().Trim();
            }

            //foreach (DataRow row in ds.Tables[tblsrcName].Rows) {
            //    for (int i = 0; i < fieldlist.Length; i++) {
            //        if (row[fieldlist[i]] == DBNull.Value) {
            //            if (row.Table.Columns[fieldlist[i]].DataType == Type.GetType("System.String")) {
            //                row[fieldlist[i]] = string.Empty;
            //            } else {
            //                if (row.Table.Columns[fieldlist[i]].DataType == Type.GetType("System.DateTime")) {
            //                    row[fieldlist[i]] = DateTime.Today.Date;
            //                } else {
            //                    row[fieldlist[i]] = 0;
            //                }
            //            }
            //            row.EndEdit();
            //        }
            //    }

            //    for (int i = 0; i < sumfieldlist.Length; i++) {
            //        if (row[sumfieldlist[i]] == DBNull.Value) {
            //            if (row.Table.Columns[sumfieldlist[i]].DataType == Type.GetType("System.String")) {
            //                row[sumfieldlist[i]] = string.Empty;
            //            } else {
            //                if (row.Table.Columns[sumfieldlist[i]].DataType == Type.GetType("System.DateTime")) {
            //                    row[sumfieldlist[i]] = DateTime.Today.Date;
            //                } else {
            //                    row[sumfieldlist[i]] = 0;
            //                }
            //            }
            //            row.EndEdit();
            //        }
            //    }
            //}


            //getting distinct values for group column
            DataTable dtGroup = dv.ToTable(true, fieldlist);

            for (int i = 0; i < sumfieldlist.Length; i++) {
                if (dtGroup.Columns.IndexOf(sumfieldlist[i]) < 0) {
                    DataColumn c = new DataColumn(sumfieldlist[i], ds.Tables[tblsrcName].Columns[sumfieldlist[i]].DataType);
                    c.DefaultValue = 0;
                    dtGroup.Columns.Add(c);
                }
            }
            //looping thru distinct values for the group, counting
            foreach (DataRow dr in dtGroup.Rows) {
                string condition = string.Empty;
                for (int i = 0; i < fieldlist.Length; i++) {
                    if (dr[fieldlist[i]] == DBNull.Value) {
                        if (dr.Table.Columns[fieldlist[i]].DataType == Type.GetType("System.String")) {
                            dr[fieldlist[i]] = string.Empty;
                        } else {
                            if (dr.Table.Columns[fieldlist[i]].DataType == Type.GetType("System.DateTime")) {
                                dr[fieldlist[i]] = DateTime.Today.Date;
                            } else {
                                dr[fieldlist[i]] = 0;
                            }
                        }
                        dr.EndEdit();
                    }
                    if (ds.Tables[tblsrcName].Columns[fieldlist[i]].DataType == Type.GetType("System.String")) {
                        condition += fieldlist[i] + "='" + dr[fieldlist[i]].ToString().Replace("'","''") + "' AND ";
                    } else {
                        if (ds.Tables[tblsrcName].Columns[fieldlist[i]].DataType == Type.GetType("System.DateTime")) {
                            condition += fieldlist[i] + "=" + Functions.ParseDateFilter((DateTime) dr[fieldlist[i]]) + " AND ";
                        } else {
                            condition += fieldlist[i] + "=" + Functions.ConvertToStringShowZero(dr[fieldlist[i]], ".", "") + " AND ";
                        }
                    }
                }
                if (condition != string.Empty) {
                    condition = condition.Substring(0,condition.Length - 5);
                }
                for (int i = 0; i < sumfieldlist.Length; i++) {
                    
                        dr[sumfieldlist[i]] = ds.Tables[tblsrcName].Compute("SUM(" + sumfieldlist[i] + ")", condition);
                        if (dr[sumfieldlist[i]] == DBNull.Value) {
                            dr[sumfieldlist[i]] = 0;
                        }
                    
                    //dr["Count"] = i_dSourceTable.Compute("Count(" + i_sAggregateColumn + ")", i_sGroupByColumn + " = '" + dr[i_sGroupByColumn] + "'");
                }
            }
            dtGroup.TableName = tbldesName;
            if (tblsrcName != tbldesName) {
                ds.Tables.Add(dtGroup);
            } else {
                ds.Tables.Remove(tblsrcName);
                ds.Tables.Add(dtGroup);
            }
            //returning grouped/counted result
            //return dtGroup;
        }

        public static void GroupTable(DataSet ds, string tblsrcName, string tbldesName, string fields, string sumfields) {
            Functions.GroupTableAllowZero(ds, tblsrcName, tbldesName, fields, sumfields);
            string[] sumfieldlist = FunctionsBase.ParseString(sumfields);
            List<DataRow> deletelist = new List<DataRow>();
            foreach (DataRow row in ds.Tables[tbldesName].Rows) {
                bool isempty = true;
                for (int i = 0; i < sumfieldlist.Length; i++) {
                    if (Convert.ToDecimal(row[sumfieldlist[i]]) != 0) {
                        isempty = false;
                    }
                }
                if (isempty) {
                    deletelist.Add(row);
                }
            }
            foreach (DataRow row in deletelist) {
                row.Delete();
            }
            ds.Tables[tbldesName].AcceptChanges();
        }

        public static void GroupTableAllowZero(DataSet ds, string tblsrcName, string tbldesName, string fields, string sumfields) {
            if (tblsrcName == tbldesName) {
                Functions.GroupTableAllowZeroTheSame(ds, tblsrcName, tbldesName, fields, sumfields);
            } else {
                Functions.GroupTableAllowZeroDifference(ds, tblsrcName, tbldesName, fields, sumfields);
            }
        }

        public static void GroupTableAllowZeroTheSame(DataSet ds, string tblsrcName, string tbldesName, string fields, string sumfields) {
            DataTable tblsrc = ds.Tables[tblsrcName];
            DataTable tbldes = tblsrc.Copy();
            tblsrc.Clear();
            foreach (DataColumn c in tblsrc.Columns) {
                c.Expression = string.Empty;
                c.AllowDBNull = true;
            }

            string[] fieldlist = FunctionsBase.ParseString(fields);
            string[] sumfieldlist = FunctionsBase.ParseString(sumfields);
            DataColumn[] cs = new DataColumn[fieldlist.Length];
            for (int i = 0; i < fieldlist.Length; i++) {
                cs[i] = tblsrc.Columns[fieldlist[i]];
            }
            tblsrc.PrimaryKey = cs;
            foreach (DataRow row in tbldes.Rows) {
                object[] os = new object[fieldlist.Length];
                for (int i = 0; i < fieldlist.Length; i++) {
                    if (row[fieldlist[i]] == DBNull.Value) {
                        if (row.Table.Columns[fieldlist[i]].DataType == Type.GetType("System.String")) {
                            row[fieldlist[i]] = string.Empty;
                        } else {
                            if (row.Table.Columns[fieldlist[i]].DataType == Type.GetType("System.DateTime")) {
                                row[fieldlist[i]] = DateTime.Today.Date;
                            } else {
                                row[fieldlist[i]] = 0;
                            }
                        }
                    }
                    os[i] = row[fieldlist[i]];
                }
                if (fieldlist.Length > 0) {
                    DataRow foundrow = tblsrc.Rows.Find(os);
                    if (foundrow == null) {
                        for (int i = 0; i < sumfieldlist.Length; i++) {
                            if (row[sumfieldlist[i]] == DBNull.Value) {
                                row[sumfieldlist[i]] = 0;
                            }
                        }
                        foundrow = tblsrc.NewRow();
                        foundrow.ItemArray = (object[]) row.ItemArray.Clone();
                        tblsrc.Rows.Add(foundrow);
                    } else {
                        for (int i = 0; i < sumfieldlist.Length; i++) {
                            foundrow[sumfieldlist[i]] = Convert.ToDecimal(foundrow[sumfieldlist[i]]) +
                                                        Convert.ToDecimal(row[sumfieldlist[i]] == System.DBNull.Value ? 0 : row[sumfieldlist[i]]);
                        }
                    }
                } else {
                    if (tblsrc.Rows.Count > 0) {
                        for (int i = 0; i < sumfieldlist.Length; i++) {
                            tblsrc.Rows[0][sumfieldlist[i]] = Convert.ToDecimal(tblsrc.Rows[0][sumfieldlist[i]]) +
                                                              Convert.ToDecimal(row[sumfieldlist[i]] == System.DBNull.Value ? 0 : row[sumfieldlist[i]]);
                        }
                    } else {
                        for (int i = 0; i < sumfieldlist.Length; i++) {
                            if (row[sumfieldlist[i]] == DBNull.Value) {
                                row[sumfieldlist[i]] = 0;
                            }
                        }
                        DataRow foundrow = tblsrc.NewRow();
                        foundrow.ItemArray = (object[]) row.ItemArray.Clone();
                        tblsrc.Rows.Add(foundrow);
                    }
                }
            }

            tblsrc.AcceptChanges();
            tblsrc.PrimaryKey = null;
            foreach (DataColumn c in tblsrc.Columns) {
                c.AllowDBNull = true;
            }
            Functions.ClearTable(tbldes);
        }

        public static void GroupTableAllowZeroDifference(DataSet ds, string tblsrcName, string tbldesName, string fields, string sumfields) {
            DataTable tblsrc = ds.Tables[tblsrcName];
            DataTable tbldes = ds.Tables[tbldesName];

            string[] fieldlist = FunctionsBase.ParseString(fields);
            string[] sumfieldlist = FunctionsBase.ParseString(sumfields);

            if (tbldes == null) {
                tbldes = ds.Tables.Add(tbldesName);
                foreach (DataColumn c in tblsrc.Columns) {
                    DataColumn c1 = new DataColumn(c.ColumnName, c.DataType);
                    c1.AllowDBNull = true;
                    tbldes.Columns.Add(c1);
                }
            }
            DataColumn[] cs = new DataColumn[fieldlist.Length];
            for (int i = 0; i < fieldlist.Length; i++) {
                cs[i] = tbldes.Columns[fieldlist[i]];
            }
            tbldes.PrimaryKey = cs;
            foreach (DataRow row in tblsrc.Rows) {
                object[] os = new object[fieldlist.Length];
                for (int i = 0; i < fieldlist.Length; i++) {
                    if (row[fieldlist[i]] == DBNull.Value) {
                        if (row.Table.Columns[fieldlist[i]].DataType == Type.GetType("System.String")) {
                            row[fieldlist[i]] = string.Empty;
                        } else {
                            if (row.Table.Columns[fieldlist[i]].DataType == Type.GetType("System.DateTime")) {
                                row[fieldlist[i]] = DateTime.Today.Date;
                            } else {
                                row[fieldlist[i]] = 0;
                            }
                        }
                        row.EndEdit();
                    }
                    os[i] = row[fieldlist[i]];
                }
                if (fieldlist.Length > 0) {
                    DataRow foundrow = tbldes.Rows.Find(os);
                    if (foundrow == null) {
                        for (int i = 0; i < sumfieldlist.Length; i++) {
                            if (row[sumfieldlist[i]] == DBNull.Value) {
                                row[sumfieldlist[i]] = 0;
                                row.EndEdit();
                            }
                        }
                        foundrow = tbldes.NewRow();
                        foundrow.ItemArray = (object[]) row.ItemArray.Clone();
                        tbldes.Rows.Add(foundrow);
                    } else {
                        for (int i = 0; i < sumfieldlist.Length; i++) {
                            foundrow[sumfieldlist[i]] = Convert.ToDecimal(foundrow[sumfieldlist[i]]) +
                                                        Convert.ToDecimal(row[sumfieldlist[i]] == System.DBNull.Value ? 0 : row[sumfieldlist[i]]);
                        }
                    }
                } else {
                    if (tblsrc.Rows.Count > 0) {
                        for (int i = 0; i < sumfieldlist.Length; i++) {
                            tblsrc.Rows[0][sumfieldlist[i]] = Convert.ToDecimal(tblsrc.Rows[0][sumfieldlist[i]]) +
                                                              Convert.ToDecimal(row[sumfieldlist[i]] == System.DBNull.Value ? 0 : row[sumfieldlist[i]]);
                        }
                    } else {
                        for (int i = 0; i < sumfieldlist.Length; i++) {
                            if (row[sumfieldlist[i]] == DBNull.Value) {
                                row[sumfieldlist[i]] = 0;
                                row.EndEdit();
                            }
                        }
                        DataRow foundrow = tbldes.NewRow();
                        foundrow.ItemArray = (object[]) row.ItemArray.Clone();
                        tbldes.Rows.Add(foundrow);
                    }
                }
            }
            tbldes.PrimaryKey = null;
            foreach (DataColumn c in tblsrc.Columns) {
                c.AllowDBNull = true;
            }
            tbldes.AcceptChanges();
        }

        public static void ClearTable(DataTable dt) {
            try {
                if (dt != null) {
                    dt.Clear();
                    dt.Dispose();
                    dt = null;
                }
            } catch (Exception) {}
        }

        public static void ClearDataSet(DataSet ds) {
            try {
                if (ds != null) {
                    ds.Relations.Clear();
                    foreach (DataTable dt in ds.Tables) {
                        dt.Constraints.Clear();
                        ds.Tables.Remove(dt);
                        ClearTable(dt);
                        ds.AcceptChanges();
                        ClearDataSet(ds);
                        return;
                    }
                    ds.Dispose();
                    ds = null;
                }
            } catch (Exception) {}
        }

        public static void ClearDataSetExceptTable(DataSet ds, string tablename) {
            try {
                if (ds != null) {
                    ds.Relations.Clear();
                    foreach (DataTable dt in ds.Tables) {
                        if (tablename.ToUpper() != dt.TableName.ToUpper()) {
                            dt.Constraints.Clear();
                            ds.Tables.Remove(dt);
                            ClearTable(dt);
                            ds.AcceptChanges();
                            ClearDataSetExceptTable(ds, tablename);
                            return;
                        }
                    }
                }
            } catch (Exception) {}
        }

        public static void ClearDataSetOneTable(DataSet ds, string tablename) {
            try {
                if (ds != null) {
                    foreach (DataTable dt in ds.Tables) {
                        if (tablename.ToUpper() == dt.TableName.ToUpper()) {
                            dt.Constraints.Clear();
                            ds.Tables.Remove(dt);
                            ClearTable(dt);
                            return;
                        }
                    }
                }
            } catch (Exception) {}
        }

        #endregion

        #region File Functions

        public static bool FileExists(string filename) {
            FileInfo FI = new FileInfo(filename);
            if (FI.Exists) {
                return true;
            } else {
                return false;
            }
        }

        public static void FileCopy(string oldfile, string newfile) {
            FileInfo fileInfo = new FileInfo(oldfile);
            fileInfo.CopyTo(newfile, true);
        }

        public static void FileDelete(string oldfile) {
            FileInfo fileInfo = new FileInfo(oldfile);
            fileInfo.Delete();
        }

        #endregion

        #region Math Functions

        public static decimal Round(decimal num, int place) {
            decimal n;
            n = num*Convert.ToDecimal(Math.Pow(10, place));
            n = Convert.ToDecimal(Math.Sign(n)*Math.Abs(Math.Floor(Convert.ToDouble(n) + .5)));
            return n/Convert.ToDecimal(Math.Pow(10, place));
        }

        public static double Round(double num, int place) {
            double n;
            n = num*Math.Pow(10, place);
            n = Math.Sign(n)*Math.Abs(Math.Floor(n + .5));
            return n/Math.Pow(10, place);
        }

        public static decimal Round(decimal num) {
            decimal n;
            int place = 0;
            n = num*Convert.ToDecimal(Math.Pow(10, place));
            n = Convert.ToDecimal(Math.Sign(n)*Math.Abs(Math.Floor(Convert.ToDouble(n) + .5)));
            return n/Convert.ToDecimal(Math.Pow(10, place));
        }

        public static double Round(double num) {
            int place = 0;
            double n;
            n = num*Math.Pow(10, place);
            n = Math.Sign(n)*Math.Abs(Math.Floor(n + .5));
            return n/Math.Pow(10, place);
        }

        #endregion

        #region dichso

        private static double right(double value1, int numb) {
            string value2 = Convert.ToString(value1);
            if (value2.Length < numb) {
                return Convert.ToDouble(value2);
            }
            return Convert.ToDouble(value2.Substring(value2.Length - numb));
        }

        #endregion

        #region Database related

        public static string GetTranNo(string tranid, string organizationid, DateTime date, FTSMain ftsmain, DbTransaction myTran, bool isupdate) {
            if ((bool)ftsmain.SystemVars.GetSystemVars("GET_TRAN_NO_BY_CODE")) {
                return Functions.GetTranNoCode(tranid, organizationid, date, ftsmain, myTran, isupdate);
            } else {
                string sSql = "dbo.[spGetTranNo]";
                DbCommand cmd = ftsmain.DbMain.GetSqlStringCommand(sSql);
                cmd.CommandType = CommandType.StoredProcedure;
                ftsmain.DbMain.AddInParameter(cmd, "@tranid", DbType.String, tranid);
                ftsmain.DbMain.AddInParameter(cmd, "@organizationid", DbType.String, organizationid);
                ftsmain.DbMain.AddInParameter(cmd, "@date", DbType.Date, date);
                ftsmain.DbMain.AddInParameter(cmd, "@isupdate", DbType.Boolean, isupdate);
                ftsmain.DbMain.AddOutParameter(cmd, "@so_ctu", DbType.String,20);

                //DbParameter prm = cmd.CreateParameter();
                //prm.ParameterName = "@so_ctu";
                //prm.DbType = DbType.String;
                //prm.Direction = ParameterDirection.Output;
                //cmd.Parameters.Add(prm);
                ftsmain.DbMain.ExecuteNonQuery(cmd);
                return ((string)((DbParameter)cmd.Parameters["@so_ctu"]).Value);

            }
        }

        public static string GetTranNoCode(string tranid, string organizationid, DateTime date, FTSMain ftsmain, DbTransaction myTran, bool isupdate) {
            decimal so_ctu;
            string sql = "select * from SYS_TRAN WHERE TRAN_ID='" + tranid + "'";
            DataTable systran = ftsmain.DbMain.LoadDataTable(ftsmain.DbMain.GetSqlStringCommand(sql), "sys_tran");
            
            if (systran.Rows.Count > 0) {
                DataRow tranrow = systran.Rows[0];
                string prefix = tranrow["TRAN_NO_PREFIX"].ToString();
                string postfix = tranrow["TRAN_NO_POSTFIX"].ToString();
                bool bymonth = (Int16) tranrow["TRAN_NO_BY_MONTH"] == 1 ? true : false;
                bool byday = (Int16) tranrow["TRAN_NO_BY_DAY"] == 1 ? true : false;
                bool fixedlength = (Int16) tranrow["IS_FIXED_LENGTH_TRAN_NO"] == 1 ? true : false;
                int length = (int) tranrow["TRAN_NO_LENGTH"];
                int year = 0;
                if (bymonth || byday) {
                    year = date.Year;
                } else {
                    year = ftsmain.DayStartOfCurrentYear.Year;
                }


                sql = "select * from sys_tran_no where TRAN_YEAR=" + year+ " AND tran_id='" + tranid + "'";
                if (organizationid != string.Empty) {
                    sql += " and ORGANIZATION_ID=N'" + organizationid + "'";
                }
                if (bymonth) {
                    sql += " AND TRAN_MONTH=" + date.Month;
                }
                if (byday) {
                    sql += " AND TRAN_DAY=" + date.Day;
                }
                DbCommand cmd = ftsmain.DbMain.GetSqlStringCommand(sql);
                DataTable dt = ftsmain.DbMain.LoadDataTable(cmd, "SYS_TRAN_NO");

                if (dt.Rows.Count > 0) {
                    if (isupdate) {
                        sql = "update sys_tran_no set last_tran_no=last_tran_no+1 where TRAN_YEAR=" + year + " AND tran_id='" + tranid + "'";
                        if (organizationid != string.Empty) {
                            sql += " and ORGANIZATION_ID=N'" + organizationid + "'";
                        }
                        if (bymonth) {
                            sql += " AND TRAN_MONTH=" + date.Month;
                        }
                        if (byday) {
                            sql += " AND TRAN_DAY=" + date.Day;
                        }
                        cmd = ftsmain.DbMain.GetSqlStringCommand(sql);
                            if (myTran != null) {
                                ftsmain.DbMain.ExecuteNonQuery(cmd, myTran);
                            } else {
                                ftsmain.DbMain.ExecuteNonQuery(cmd);
                            }
                        
                        
                    }
                    so_ctu = (decimal) dt.Rows[0]["last_tran_no"];
                } else {
                    if (isupdate) {
                        string sqlqqq = "insert into sys_tran_no(TRAN_ID,LAST_TRAN_NO,PRE_FIX,POST_FIX,ORGANIZATION_ID,TRAN_YEAR,TRAN_MONTH,TRAN_DAY,PR_KEY) values('" +
                                tranid + "',2,'','',N'" + organizationid + "'," + year + "," + date.Month + "," + date.Day + ",'" +
                                Guid.NewGuid() + "')";
                        cmd =
                            ftsmain.DbMain.GetSqlStringCommand(
                                "insert into sys_tran_no(TRAN_ID,LAST_TRAN_NO,PRE_FIX,POST_FIX,ORGANIZATION_ID,TRAN_YEAR,TRAN_MONTH,TRAN_DAY,PR_KEY) values('" +
                                tranid + "',2,'','',N'" + organizationid + "'," + year + "," + date.Month + "," + date.Day + ",'" +
                                Guid.NewGuid() + "')");
                            if (myTran != null) {
                                ftsmain.DbMain.ExecuteNonQuery(cmd, myTran);
                            } else {
                                ftsmain.DbMain.ExecuteNonQuery(cmd);
                            }
                        
                        
                    }
                    so_ctu = new decimal(1);
                }
                CultureInfo ci = new CultureInfo("vi-VN");
                ci.NumberFormat.NumberDecimalSeparator = ".";
                ci.NumberFormat.NumberGroupSeparator = string.Empty;
                if (fixedlength) {
                    return prefix + so_ctu.ToString("0").PadLeft(length, '0') + postfix;
                } else {
                    return prefix + so_ctu.ToString("0") + postfix;
                }
            } else {
                return string.Empty;
            }
        }
        
        public static bool TableExists(string tablename, Database db) {
            try {
                DataTable dt = db.LoadDataTable(db.GetSqlStringCommand("select top 1 'a' from " + tablename), "xx");
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public static void DropTable(Database db, string tablename) {
            db.ExecuteNonQuery(db.GetSqlStringCommand("DROP TABLE " + tablename));
        }

        public static string GetTempTable(Database db) {
            string strSQL = "select max(convert(int,right(name,3))) as MaxID from sysobjects where Name like 'TMPTABLE%'";
            DbCommand cmd = db.GetSqlStringCommand(strSQL);
            object oj = db.ExecuteScalar(cmd);
            int x;
            if (oj == DBNull.Value || oj == null) {
                x = 0;
            } else {
                x = Convert.ToInt32(oj) + 1;
            }
            return "TMPTABLE" + x.ToString().Trim().PadLeft(3, '0');
        }

        #endregion

        public static string Encrypt(string plainText) {
            string passPhrase = "Pas5pr@se"; // can be any string
            string saltValue = "s@1tValue"; // can be any string
            string hashAlgorithm = "SHA1"; // can be "MD5"
            int passwordIterations = 2; // can be any number
            string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
            int keySize = 256;
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = password.GetBytes(keySize/8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string cipherText = Convert.ToBase64String(cipherTextBytes);
            return cipherText;
        }

        public static string Decrypt(string cipherText) {
            if (cipherText.Length == 0) {
                return "";
            }
            try {
                string passPhrase = "Pas5pr@se"; // can be any string
                string saltValue = "s@1tValue"; // can be any string
                string hashAlgorithm = "SHA1"; // can be "MD5"
                int passwordIterations = 2; // can be any number
                string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
                int keySize = 256;
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
                byte[] keyBytes = password.GetBytes(keySize/8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                return plainText;
            } catch (Exception) {
                return string.Empty;
            }
        }

        public static OleDbCommand CreateInsertCommand(string tableName, DataTable dtdes, OleDbConnection cnn) {
            StringBuilder updateQuery = new StringBuilder();
            updateQuery.Append("insert into ").Append(tableName).Append("(");
            foreach (DataColumn c in dtdes.Columns) {
                updateQuery = updateQuery.Append(c.ColumnName).Append(",");
            }
            updateQuery.Remove(updateQuery.Length - 1, 1).Append(") values(");
            foreach (DataColumn c in dtdes.Columns) {
                updateQuery.Append("?,");
            }
            updateQuery.Remove(updateQuery.Length - 1, 1).Append(")");
            OleDbCommand cmd = new OleDbCommand(updateQuery.ToString(), cnn);
            OleDbParameterCollection pc = cmd.Parameters;
            foreach (DataColumn c in dtdes.Columns) {
                if (c.DataType == Type.GetType("System.String")) {
                    pc.Add(c.ColumnName, OleDbType.VarWChar, 0, c.ColumnName);
                } else if (c.DataType == Type.GetType("System.Decimal")) {
                    pc.Add(c.ColumnName, OleDbType.Decimal, 0, c.ColumnName);
                } else if (c.DataType == Type.GetType("System.Int16")) {
                    pc.Add(c.ColumnName, OleDbType.Integer, 0, c.ColumnName);
                } else if (c.DataType == Type.GetType("System.Int32")) {
                    pc.Add(c.ColumnName, OleDbType.Integer, 0, c.ColumnName);
                } else if (c.DataType == Type.GetType("System.Decimal")) {
                    pc.Add(c.ColumnName, OleDbType.Decimal, 0, c.ColumnName);
                } else if (c.DataType == Type.GetType("System.DateTime")) {
                    pc.Add(c.ColumnName, OleDbType.Date, 0, c.ColumnName);
                } else {
                    throw (new Exception("MSG_NOT_IMPLEMENTED"));
                }
            }
            return cmd;
        }

        public static bool IsTiengViet(string id) {
            char[] x = id.ToUpper().ToCharArray();
            for (int i = 0; i < x.Length; i++) {
                int y = (int) x[i];
                if ((y >= 65 && y <= 90) || (y >= 48 && y <= 57) || (y == 95) || (y == 46) || (y == 47) || (y == 45) || (y == 63) || (y == 58)) {} else {
                    return true;
                }
            }
            return false;
        }

        public static string ConvertDateToString(DateTime d) {
            return d.Day.ToString().PadLeft(2, '0') + "/" + d.Month.ToString().PadLeft(2, '0') + "/" + d.Year.ToString();
        }

        public static decimal GetAmount(FTSMain ftsmain, object amount_orig, object exchange_rate) {
               return Round((decimal) amount_orig*(decimal) exchange_rate, ftsmain.TPSTVND);
        }

        public static decimal GetUnitPrice(FTSMain ftsmain, object amount_orig, object exchange_rate) {
                return Round((decimal) amount_orig*(decimal) exchange_rate, ftsmain.TPDGVND);
        }

        public static decimal GetAmountOrig(FTSMain ftsmain, object amount, object exchange_rate) {
                return Round((decimal) amount/(decimal) exchange_rate, ftsmain.TPSTNTE);
        }

        public static decimal GetAmountWithRounding(FTSMain ftsmain, object amount_orig, object exchange_rate, int rounding) {
            decimal result = 0;
                result = Round((decimal) amount_orig*(decimal) exchange_rate, ftsmain.TPSTVND);
            double baseround = 10;
            decimal roundingvalue = (decimal) Math.Pow(10, Convert.ToDouble(rounding));
            return Round(result/roundingvalue, 0)*roundingvalue;
        }

        

        public static string ConvertDateToString(DateTime date, string separated) {
            return Number2String(date.Day) + separated + Number2String(date.Month) + separated + Number2String(date.Year);
        }

        public static string Number2String(int number) {
            string result = number.ToString();
            if (number < 10) {
                result = "0" + result;
            }
            return result;
        }

        public static string ConvertToString(object number, string decimalsymbol, string thousandsymbol, int tp) {
            if (number == null) {
                return "";
            }
            if (Convert.ToDouble(number) == 0) {
                return "";
            }
            string reText = "";
            CultureInfo ci = new CultureInfo("vi-VN");
            ci.NumberFormat.NumberDecimalSeparator = ",";
            string text = Convert.ToString(number, ci);
            int posNumText = text.IndexOf(",", 0, text.Length - 1);
            string NumText = "";
            string DecText = "";
            if (posNumText > 0) {
                NumText = text.Substring(0, posNumText);
                DecText = text.Substring(posNumText + 1).Replace('0', ' ').TrimEnd().Replace(' ', '0').Trim();
                if (DecText != string.Empty) {
                    if (DecText.Length >= tp) {
                        DecText = DecText.Substring(0, tp);
                    }
                } 
            } else {
                NumText = text;
            }
            reText = ProcessConvertToString(NumText, thousandsymbol);
            if (DecText != string.Empty) {
                reText = reText + decimalsymbol + ProcessConvertToString(DecText, thousandsymbol);
            }
            return reText;
        }

        public static string ConvertToStringShowZero(object number, string decimalsymbol, string thousandsymbol, int tp) {
            if (number == null) {
                return "0";
            }
            if (Convert.ToDouble(number) == 0) {
                return "0";
            }
            string reText = "";
            CultureInfo ci = new CultureInfo("vi-VN");
            ci.NumberFormat.NumberDecimalSeparator = ",";
            string text = Convert.ToString(number, ci);
            int posNumText = text.IndexOf(",", 0, text.Length - 1);
            string NumText = "";
            string DecText = "";
            if (posNumText > 0) {
                NumText = text.Substring(0, posNumText);
                DecText = text.Substring(posNumText + 1).Replace('0', ' ').TrimEnd().Replace(' ', '0').Trim();
                if (DecText != string.Empty) {
                    if (DecText.Length >= tp) {
                        DecText = DecText.Substring(0, tp);
                    }
                }
            } else {
                NumText = text;
            }
            reText = ProcessConvertToString(NumText, thousandsymbol);
            if (DecText != string.Empty) {
                reText = reText + decimalsymbol + ProcessConvertToString(DecText, thousandsymbol);
            }
            return reText;
        }

        public static string ConvertToStringShowZero(object number, string decimalsymbol, string thousandsymbol) {
            if (number == null) {
                return "0";
            }
            if (Convert.ToDouble(number) == 0) {
                return "0";
            }
            string reText = "";
            CultureInfo ci = new CultureInfo("vi-VN");
            ci.NumberFormat.NumberDecimalSeparator = ",";
            string text = Convert.ToString(number, ci);
            int posNumText = text.IndexOf(",", 0, text.Length - 1);
            string NumText = "";
            string DecText = "";
            if (posNumText > 0) {
                NumText = text.Substring(0, posNumText);
                DecText = text.Substring(posNumText + 1).Replace('0', ' ').TrimEnd().Replace(' ', '0');
            } else {
                NumText = text;
            }
            reText = ProcessConvertToString(NumText, thousandsymbol);
            if (DecText != string.Empty) {
                reText = reText + decimalsymbol + ProcessConvertToString(DecText, thousandsymbol);
            }
            return reText;
        }

        public static string ConvertToString(object number, string decimalsymbol, string thousandsymbol) {
            if (number == null) {
                return "";
            }
            if (Convert.ToDouble(number) == 0) {
                return "";
            }
            string reText = "";
            CultureInfo ci = new CultureInfo("vi-VN");
            ci.NumberFormat.NumberDecimalSeparator = ",";
            string text = Convert.ToString(number, ci);
            int posNumText = text.IndexOf(",", 0, text.Length - 1);
            string NumText = "";
            string DecText = "";
            if (posNumText > 0) {
                NumText = text.Substring(0, posNumText);
                DecText = text.Substring(posNumText + 1).Replace('0', ' ').TrimEnd().Replace(' ', '0');
            } else {
                NumText = text;
            }
            reText = ProcessConvertToString(NumText, thousandsymbol);
            if (DecText != string.Empty) {
                reText = reText + decimalsymbol + ProcessConvertToString(DecText, thousandsymbol);
            }
            return reText;
        }

        public static string ProcessConvertToString(string values, string thousandsymbol) {
            string retValues = values;
            int leng = retValues.Length;
            decimal group = Math.Round((decimal) leng/3);
            retValues = retValues + thousandsymbol;
            int pos = 3;
            int posfound = 0;
            for (int i = 0; i < group; i++) {
                posfound = retValues.IndexOf(thousandsymbol, 0, retValues.Length);
                int posInsert = posfound - pos;
                if (posInsert > 0) {
                    retValues = retValues.Insert(posInsert, thousandsymbol);
                }
            }
            return retValues.Substring(0, retValues.Length - 1);
        }

        public static string ConvertToString2(object number, CultureInfo ci) {
            if (number == null) {
                return "";
            }
            if (Convert.ToDouble(number) == 0) {
                return "";
            }
            string reText = "";
            string text = Convert.ToString(number, ci);
            int posNumText = text.IndexOf(ci.NumberFormat.NumberDecimalSeparator, 0, text.Length - 1);
            string NumText = "";
            string DecText = "";
            if (posNumText > 0) {
                NumText = text.Substring(0, posNumText);
                DecText = text.Substring(posNumText + 1);
            } else {
                NumText = text;
            }
            reText = ProcessConvertToString(NumText, ci.NumberFormat.NumberGroupSeparator);
            if (DecText != string.Empty) {
                reText = reText + ci.NumberFormat.NumberDecimalSeparator + ProcessConvertToString(DecText, ci.NumberFormat.NumberGroupSeparator);
            }
            return reText;
        }

        public static string GetSubAccountFromList(FTSMain ftsmain, string accountlist) {
            string[] list = FunctionsBase.ParseString(accountlist);
            string result = "";
            foreach (string account_id in list) {
                if (account_id != string.Empty) {
                    string result1 = GetSubAccount1(ftsmain, account_id);
                    if (result1 != string.Empty) {
                        result += "'" + account_id + "'" + result1 + ",";
                    } else {
                        result += "'" + account_id + "',";
                    }
                }
            }
            result = result.Substring(0, result.Length - 1);
            result = "(" + result + ")";
            return result;
        }

        public static string GetSubAccount(FTSMain ftsmain, string account_id) {
            return "('" + account_id + "'" + GetSubAccount1(ftsmain, account_id) + ")";
        }

        private static string GetSubAccount1(FTSMain ftsmain, string account_id) {
            string sql = "select account_id from dm_account where parent_account_id='" + account_id + "' AND ACCOUNT_ID <> '" + account_id + "'";
            DataTable dt = ftsmain.DbMain.LoadDataTable(ftsmain.DbMain.GetSqlStringCommand(sql), "DM_ACCOUNT");
            string result = string.Empty;
            foreach (DataRow row in dt.Rows) {
                result += ",'" + row["ACCOUNT_ID"] + "'";
                result += GetSubAccount1(ftsmain, row["ACCOUNT_ID"].ToString());
            }
            return result;
        }

        public static string GetTempFilePathWithExtension(string extension) {
            var path = Path.GetTempPath();
            var fileName = Guid.NewGuid().ToString() + extension;
            return Path.Combine(path, fileName);
        }

        public static DataTable GetTablePivot(FTSMain ftsmain, string sSelect, string sfunctionSum, string sPivot, string tablename) {
            try {
                string sSql = "dbo.crosstab";
                DbCommand cmd = ftsmain.DbMain.GetSqlStringCommand(sSql);
                cmd.CommandType = CommandType.StoredProcedure;
                ftsmain.DbMain.AddInParameter(cmd, "@select", DbType.String, sSelect);
                ftsmain.DbMain.AddInParameter(cmd, "@sumfunc", DbType.String, sfunctionSum);
                ftsmain.DbMain.AddInParameter(cmd, "@pivot", DbType.String, sPivot);
                ftsmain.DbMain.AddInParameter(cmd, "@table", DbType.String, tablename);
                DataTable tbl = ftsmain.DbMain.LoadDataTable(cmd, tablename);
                if (tbl.Rows.Count > 0) {
                    return tbl;
                }
                return null;
            } catch (Exception ex) {
                return null;
            }
        }

        public static void ChangePeriod(DateTime start_date, DateTime End_Date, out DateTime rStartDate, out DateTime rEnd_Date, FTSMain ftsMain) {
            rStartDate = start_date;
            rEnd_Date = End_Date;
            int End_Day = Convert.ToInt16(ftsMain.SystemVars.GetSystemVars("END_DAY_PAYROLL"));
            int Start_Day = Convert.ToInt16(ftsMain.SystemVars.GetSystemVars("START_DAY_PAYROLL"));
            rEnd_Date = new DateTime(End_Date.Year, End_Date.Month, End_Day);
            rStartDate = new DateTime(start_date.AddMonths(-1).Year, start_date.AddMonths(-1).Month, Start_Day);
        }

        public static int GetRowIndex(DataTable table, DataRow row) {
            IEnumerator ice = table.Rows.GetEnumerator();
            ice.Reset();
            int i = 0;
            while (ice.MoveNext()) {
                if (((ice.Current)) == row) {
                    return i;
                }
                i++;
            }
            return -1;
        }

        public static decimal MathRoundInteger(decimal valueNumber, int numdigit) {
            decimal result = 0;
            if (numdigit > 1) {
                result = Math.Round((valueNumber/numdigit), 0, MidpointRounding.AwayFromZero);
                result = result*numdigit;
            } else {
                numdigit = 1;
                result = valueNumber*numdigit;
            }
            return result;
        }

        public static decimal MathRoundDecimal(decimal valueNumber, int numdigit) {
            decimal result = 0;
            result = Math.Round(valueNumber, numdigit, MidpointRounding.AwayFromZero);
            return result;
        }

        public static DateTime GetDateTimeServer(FTSMain ftsMain) {
            try {
                    string sSql = "dbo.GETDATETIME_NOW";
                    DbCommand cmd = ftsMain.DbMain.GetSqlStringCommand(sSql);
                    cmd.CommandType = CommandType.StoredProcedure;
                    DbParameter prm = cmd.CreateParameter();
                    prm.ParameterName = "@DateTimeServer";
                    prm.DbType = DbType.DateTime;
                    prm.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(prm);
                    ftsMain.DbMain.ExecuteNonQuery(cmd);
                    return ((DateTime) ((DbParameter) cmd.Parameters["@DateTimeServer"]).Value);
                
            } catch (Exception ex) {
                return DateTime.Now;
            }
        }

        public static DateTime GetShortDateTimeServer(FTSMain ftsMain) {
            try {
                DateTime longdatetime = Functions.GetDateTimeServer(ftsMain);
                return new DateTime(longdatetime.Year, longdatetime.Month, longdatetime.Day);
            } catch (Exception ex) {
                return DateTime.Now;
            }
        }
        
        public static void LoadAllData(FTSMain ftsmain) {
            DataSet ds = new DataSet();
            List<string> ListTable = new List<string>();
            ListTable.Add("SYSTABLE");
            ListTable.Add("SYS_TABLE");
            ListTable.Add("SYS_TRAN_NO");
            ListTable.Add("SYS_TRAN_DEFAULT");
            ListTable.Add("SYS_TRAN_CONFIG");
            ListTable.Add("SYS_TRAN_CLASS");
            ListTable.Add("SYS_TRAN_CALCULATION");
            ListTable.Add("SYS_TRAN");
            ListTable.Add("SYS_RESOURCE");
            ListTable.Add("SYS_RESOURCE_EN");
            ListTable.Add("SYS_RESOURCE_JP");
            ListTable.Add("SYS_RESOURCE_KR");
            ListTable.Add("SYS_RESOURCE_LAOS");
            ListTable.Add("SYS_RESOURCE_CN");
            ListTable.Add("SYS_GRIDINFO");
            ListTable.Add("SYS_FORMINFO");
            ListTable.Add("SYS_FORM");
            ListTable.Add("SYS_FIELD");
            ListTable.Add("SEC_USER_GROUP");
            for (int i = 0; i < ListTable.Count; i++) {
                string sql = "select * from " + ListTable[i];
                DataTable dt = ftsmain.DbMain.LoadDataTable(ftsmain.DbMain.GetSqlStringCommand(sql), ListTable[i]);
                ds.Tables.Add(dt);
            }
            ds.WriteXml(Functions.GetPathName() + "Update.xml");
            ds.WriteXmlSchema(Functions.GetPathName() + "Update.dst");
        }

        public static void LoadAllDataToCache(FTSMain ftsmain, string modulelist) {
            DataSet ds = new DataSet();
            List<string> ListTable = new List<string>();
            ListTable.Add("SYS_SYSTEMVAR");
            ListTable.Add("SYS_TABLE");
            ListTable.Add("SYS_TRAN_NO");
            ListTable.Add("SYS_TRAN_DEFAULT");
            ListTable.Add("SYS_TRAN_CONFIG");
            ListTable.Add("SYS_TRAN_CLASS");
            ListTable.Add("SYS_TRAN_CALCULATION");
            ListTable.Add("SYS_TRAN");
            if (Functions.InListAbsolute(Language.VN, modulelist)) {
                ListTable.Add("SYS_RESOURCE");
            }
            if (Functions.InListAbsolute(Language.EN, modulelist)) {
                ListTable.Add("SYS_RESOURCE_EN");
            }
            if (Functions.InListAbsolute(Language.JP, modulelist)) {
                ListTable.Add("SYS_RESOURCE_JP");
            }
            if (Functions.InListAbsolute(Language.KR, modulelist)) {
                ListTable.Add("SYS_RESOURCE_KR");
            }
            if (Functions.InListAbsolute(Language.LAOS, modulelist)) {
                ListTable.Add("SYS_RESOURCE_LAOS");
            }
            if (Functions.InListAbsolute(Language.CN, modulelist)) {
                ListTable.Add("SYS_RESOURCE_CN");
            }
            ListTable.Add("SYS_GRIDINFO");
            ListTable.Add("SYS_FORMINFO");
            ListTable.Add("SYS_FORM");
            ListTable.Add("SYS_FIELD");
            ListTable.Add("SYS_MENU_MAPPING");
            for (int i = 0; i < ListTable.Count; i++) {
                string sql = "select * from " + ListTable[i];
                DataTable dt = ftsmain.DbMain.LoadDataTable(ftsmain.DbMain.GetSqlStringCommand(sql), ListTable[i]);
                ds.Tables.Add(dt);
            }
            ds.WriteXml(Functions.GetPathName() + "FTSSysCaches.xml");
            ds.WriteXmlSchema(Functions.GetPathName() + "FTSSysCaches.dst");
        }
        
        public static void LoadAllDataToUpdate(FTSMain ftsmain) {
            DataSet ds = new DataSet();
            List<string> ListTable = new List<string>();
            ListTable.Add("SYSTABLE");
            ListTable.Add("SYS_SYSTEMVAR");
            ListTable.Add("SYS_TABLE");
            ListTable.Add("SYS_TRAN_DEFAULT");
            ListTable.Add("SYS_TRAN_CONFIG");
            ListTable.Add("SYS_TRAN_CLASS");
            ListTable.Add("SYS_TRAN_CALCULATION");
            ListTable.Add("SYS_TRAN");
            ListTable.Add("SYS_RESOURCE");
            ListTable.Add("SYS_RESOURCE_EN");
            ListTable.Add("SYS_RESOURCE_JP");
            ListTable.Add("SYS_RESOURCE_KR");
            ListTable.Add("SYS_RESOURCE_LAOS");
            ListTable.Add("SYS_RESOURCE_CN");
            ListTable.Add("SYS_GRIDINFO");
            ListTable.Add("SYS_FORMINFO");
            ListTable.Add("SYS_FORM");
            ListTable.Add("SYS_FIELD");
            ListTable.Add("SYS_MENU");
            ListTable.Add("SYS_MENU_MAPPING");
            for (int i = 0; i < ListTable.Count; i++) {
                string sql = "select * from " + ListTable[i];
                DataTable dt = ftsmain.DbMain.LoadDataTable(ftsmain.DbMain.GetSqlStringCommand(sql), ListTable[i]);
                ds.Tables.Add(dt);
            }
            ds.WriteXml(Functions.GetPathName() + "Update.xml");
            ds.WriteXmlSchema(Functions.GetPathName() + "Update.dst");
        }

        public static string InsertPara(string msg, object[] para) {
            try {
                if ((para != null) && (para.Length > 0) && (msg.Trim().Length > 0)) {
                    List<string> buffer = new List<string>();
                    int start = 0;
                    int length = 0;
                    bool locked = false;
                    string pa = string.Empty;
                    string result = string.Empty;
                    for (int i = 0; i <= msg.Length - 1; i++) {
                        if (msg[i] == '{') {
                            buffer.Add(msg.Substring(start, length));
                            locked = true;
                        } else if (msg[i] == '}') {
                            buffer.Add(para[Convert.ToInt16(pa)].ToString());
                            start = i + 1;
                            length = 0;
                            pa = string.Empty;
                            locked = false;
                        } else {
                            if (locked) {
                                pa = pa + msg[i].ToString();
                            } else {
                                length++;
                            }
                        }
                    }
                    buffer.Add(msg.Substring(start, length));
                    foreach (string str in buffer) {
                        result = result + str;
                    }
                    return result;
                } else {
                    return msg;
                }
            } catch (Exception ex) {
                FTSMessageBox.ShowErrorMessage(ex.Message);
                return string.Empty;
            }
        }

        public static string Unicode2NoSign(string strTemp) {
            char str1;
            string str = "", strKQ = "";
            for (int i = 0; i < strTemp.Length; i++) {
                str1 = strTemp[i];
                str = Convert.ToString(str1);
                switch (str) {
                    case "ã":
                    case "à":
                    case "ạ":
                    case "á":
                    case "ả":
                    case "ă":
                    case "ẵ":
                    case "ẳ":
                    case "ằ":
                    case "ắ":
                    case "ặ":
                    case "â":
                    case "ẫ":
                    case "ấ":
                    case "ẩ":
                    case "ầ":
                    case "ậ":
                        str = "a";
                        break;
                    case "Ã":
                    case "À":
                    case "Ạ":
                    case "Á":
                    case "Ả":
                    case "Ă":
                    case "Ẵ":
                    case "Ẳ":
                    case "Ằ":
                    case "Ắ":
                    case "Ặ":
                    case "Â":
                    case "Ẫ":
                    case "Ấ":
                    case "Ẩ":
                    case "Ầ":
                    case "Ậ":
                        str = "A";
                        break;
                    case "đ":
                        str = "d";
                        break;
                    case "Đ":
                        str = "D";
                        break;
                    case "ẽ":
                    case "ẹ":
                    case "ẻ":
                    case "è":
                    case "é":
                    case "ê":
                    case "ễ":
                    case "ể":
                    case "ề":
                    case "ế":
                    case "ệ":
                        str = "e";
                        break;
                    case "Ẽ":
                    case "Ẹ":
                    case "Ẻ":
                    case "È":
                    case "É":
                    case "Ê":
                    case "Ễ":
                    case "Ể":
                    case "Ề":
                    case "Ế":
                    case "Ệ":
                        str = "E";
                        break;
                    case "ĩ":
                    case "ỉ":
                    case "í":
                    case "ì":
                    case "ị":
                        str = "i";
                        break;
                    case "Ĩ":
                    case "Ỉ":
                    case "Í":
                    case "Ì":
                    case "Ị":
                        str = "I";
                        break;
                    case "õ":
                    case "ỏ":
                    case "ò":
                    case "ó":
                    case "ọ":
                    case "ô":
                    case "ỗ":
                    case "ổ":
                    case "ồ":
                    case "ố":
                    case "ộ":
                    case "ơ":
                    case "ỡ":
                    case "ở":
                    case "ờ":
                    case "ớ":
                    case "ợ":
                        str = "o";
                        break;
                    case "Õ":
                    case "Ỏ":
                    case "Ò":
                    case "Ó":
                    case "Ọ":
                    case "Ô":
                    case "Ỗ":
                    case "Ổ":
                    case "Ồ":
                    case "Ố":
                    case "Ộ":
                    case "Ơ":
                    case "Ỡ":
                    case "Ở":
                    case "Ờ":
                    case "Ớ":
                    case "Ợ":
                        str = "O";
                        break;
                    case "ỹ":
                    case "ỷ":
                    case "ý":
                    case "ỳ":
                        str = "y";
                        break;
                    case "Ỹ":
                    case "Ỷ":
                    case "Ý":
                    case "Ỳ":
                        str = "Y";
                        break;
                    case "ũ":
                    case "ủ":
                    case "ù":
                    case "ú":
                    case "ụ":
                    case "ư":
                    case "ữ":
                    case "ử":
                    case "ứ":
                    case "ừ":
                    case "ự":
                        str = "u";
                        break;
                    case "Ũ":
                    case "Ủ":
                    case "Ù":
                    case "Ú":
                    case "Ụ":
                    case "Ư":
                    case "Ữ":
                    case "Ử":
                    case "Ứ":
                    case "Ừ":
                    case "Ự":
                        str = "U";
                        break;
                }
                strKQ = strKQ + str;
            }
            return strKQ;
        }

        public static string RemoveSpecialChars(string str, string[] specialchars) {
            /*
            string[] chars = new string[] { ",", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "-", "_", "(", ")", ":", "|", "[", "]","?"," " };
            */
            for (int i = 0; i < specialchars.Length; i++) {
                if (str.Contains(specialchars[i])) {
                    str = str.Replace(specialchars[i], "");
                }
            }
            return str.Replace(" ", "");
        }
        

        public static Guid Int2Guid(int value) {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        public static int Guid2Int(Guid value) {
            byte[] b = value.ToByteArray();
            int bint = BitConverter.ToInt32(b, 0);
            return bint;
        }

        public static void SendEmail(string Subject, string Msg, string HostEmail, int PostEmail, string From_Email, string PassFromEmail, string ToEmail) {
            try {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(HostEmail);
                mail.From = new MailAddress(From_Email);
                mail.To.Add(ToEmail);
                mail.Subject = Subject;
                mail.Body = Msg;
                SmtpServer.Port = PostEmail;
                SmtpServer.Credentials = new System.Net.NetworkCredential(From_Email, PassFromEmail);
                SmtpServer.EnableSsl = false;
                SmtpServer.Send(mail);
            } catch (Exception ex) {}
        }
        

        public static byte[] CompressByte(byte[] buffer) {
            MemoryStream ms = new MemoryStream();
            GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true);
            zip.Write(buffer, 0, buffer.Length);
            zip.Close();
            ms.Position = 0;

            MemoryStream outStream = new MemoryStream();

            byte[] compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);

            byte[] gzBuffer = new byte[compressed.Length + 4];
            Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
            return gzBuffer;
        }

        public static byte[] DecompressByte(byte[] gzBuffer) {
            MemoryStream ms = new MemoryStream();
            int msgLength = BitConverter.ToInt32(gzBuffer, 0);
            ms.Write(gzBuffer, 4, gzBuffer.Length - 4);

            byte[] buffer = new byte[msgLength];

            ms.Position = 0;
            GZipStream zip = new GZipStream(ms, CompressionMode.Decompress);
            zip.Read(buffer, 0, buffer.Length);

            return buffer;
        }
      

        public static bool CheckTaxCode(string taxcode) {
            if (taxcode.Trim().Length < 10) {
                return false;
            }
            string subtaxcode = taxcode.Substring(0, 10);
            try {
                decimal i = Convert.ToDecimal(subtaxcode);
            } catch (Exception) {
                return false;
            }
            int[] icheck = new int[] {31, 29, 23, 19, 17, 13, 7, 5, 3};
            int checknumber = 0;
            for (int i = 0; i < subtaxcode.Length - 1; i++) {
                checknumber += Convert.ToInt32(subtaxcode.Substring(i, 1))*icheck[i];
            }
            return Convert.ToDecimal(subtaxcode.Substring(9, 1)) == 10 - checknumber%11 ? true : false;
        }

        public static string ABC2Unicode(string strTemp) {
            char str1;
            string str = "", strKQ = "";
            for (int i = 0; i < strTemp.Length; i++) {
                str1 = strTemp[i];
                str = Convert.ToString(str1);
                switch (str) {
                        #region CONVERT LOWER CASE ABC TO UNICODE

                        //Chữ a					
                    case "·":
                        str = "ã";
                        break;
                    case "µ":
                        str = "à";
                        break;
                    case "¹":
                        str = "ạ";
                        break;
                        //case "ỏ":str="á";break;
                    case "¸":
                        str = "á";
                        break;
                    case "¶":
                        str = "ả";
                        break;
                        //chữ ă
                    case "¨":
                        str = "ă";
                        break;
                    case "½":
                        str = "ẵ";
                        break;
                    case "¼":
                        str = "ẳ";
                        break;
                    case "»":
                        str = "ằ";
                        break;
                    case "¾":
                        str = "ắ";
                        break;
                    case "Æ":
                        str = "ặ";
                        break;
                        //chữ â
                    case "©":
                        str = "â";
                        break;
                    case "É":
                        str = "ẫ";
                        break;
                    case "Ê":
                        str = "ấ";
                        break;
                    case "È":
                        str = "ẩ";
                        break;
                    case "Ç":
                        str = "ầ";
                        break;
                    case "Ë":
                        str = "ậ";
                        break;
                        //chữ đ
                    case "®":
                        str = "đ";
                        break;
                    case "§":
                        str = "Đ";
                        break;
                        //chữ e						
                    case "Ï":
                        str = "ẽ";
                        break;
                    case "Ñ":
                        str = "ẹ";
                        break;
                    case "Î":
                        str = "ẻ";
                        break;
                    case "Ì":
                        str = "è";
                        break;
                    case "Ð":
                        str = "é";
                        break;
                        //Chữ ê						
                    case "ª":
                        str = "ê";
                        break;
                    case "Ô":
                        str = "ễ";
                        break;
                    case "Ó":
                        str = "ể";
                        break;
                    case "Ò":
                        str = "ề";
                        break;
                    case "Õ":
                        str = "ế";
                        break;
                    case "Ö":
                        str = "ệ";
                        break;
                        //Chữ i						
                    case "Ü":
                        str = "ĩ";
                        break;
                    case "Ø":
                        str = "ỉ";
                        break;
                    case "Ý":
                        str = "í";
                        break;
                    case "×":
                        str = "ì";
                        break;
                    case "Þ":
                        str = "ị";
                        break;
                        //Chu o:						
                    case "â":
                        str = "õ";
                        break;
                    case "á":
                        str = "ỏ";
                        break;
                    case "ß":
                        str = "ò";
                        break;
                    case "ã":
                        str = "ó";
                        break;
                    case "ä":
                        str = "ọ";
                        break;
                        //Chữ ô
                    case "«":
                        str = "ô";
                        break;
                    case "ç":
                        str = "ỗ";
                        break;
                    case "æ":
                        str = "ổ";
                        break;
                    case "å":
                        str = "ồ";
                        break;
                    case "è":
                        str = "ố";
                        break;
                    case "é":
                        str = "ộ";
                        break; //						
                        //Chữ ơ
                    case "¬":
                        str = "ơ";
                        break;
                    case "ì":
                        str = "ỡ";
                        break;
                    case "ë":
                        str = "ở";
                        break;
                    case "ê":
                        str = "ờ";
                        break;
                    case "í":
                        str = "ớ";
                        break;
                    case "î":
                        str = "ợ";
                        break;
                        //Chữ y
                    case "ü":
                        str = "ỹ";
                        break;
                    case "û":
                        str = "ỷ";
                        break;
                    case "ý":
                        str = "ý";
                        break;
                    case "ú":
                        str = "ỳ";
                        break;
                        //chữ u 
                    case "ò":
                        str = "ũ";
                        break;
                    case "ñ":
                        str = "ủ";
                        break;
                    case "ï":
                        str = "ù";
                        break;
                    case "ó":
                        str = "ú";
                        break;
                    case "ô":
                        str = "ụ";
                        break;
                        //Chữ ư
                    case "­":
                        str = "ư";
                        break;
                    case "÷":
                        str = "ữ";
                        break;
                    case "ö":
                        str = "ử";
                        break;
                    case "ø":
                        str = "ứ";
                        break;
                    case "õ":
                        str = "ừ";
                        break;
                    case "ù":
                        str = "ự";
                        break;

                        #endregion

                        #region CONVERT UPPER CASE ABC TO UNICODE

                        /*
						//Chữ A					
						case "·":str="Ã";break;
						case "µ":str="Ả";break;
						case "¹":str="À";break;
						case "¸":str="Á";break;
						case "¶":str="Ạ";break;
							//chữ Ă
						case "¨":str="Ă";break;
						case "½":str="Ẵ";break;
						case "¼":str="Ẳ";break;
						case "»":str="Ằ";break;												
						case "¾":str="Ắ";break;
						case "Æ":str="Ặ";break;
							//chữ Â
						case "©":str="Â";break;
						case "É":str="Ẫ";break;
						case "Ê":str="Ấ";break;
						case "È":str="Ẩ";break;												
						case "Ç":str="Ầ";break;
						case "Ë":str="Ậ";break;
							//chữ E						
						case "Ï":str="Ẽ";break;
						case "Ñ":str="Ẹ";break;
						case "Î":str="Ẻ";break;
						case "Ì":str="È";break;												
						case "Ð":str="É";break;						
							//Chữ Ê
						case "ª":str="Ê";break;
						case "Ô":str="Ễ";break;
						case "Ó":str="Ể";break;
						case "Ò":str="Ề";break;												
						case "Õ":str="Ế";break;
						case "Ö":str="Ệ";break;
							//Chữ I						
						case "Ü":str="Ĩ";break;
						case "Ø":str="Ỉ";break;
						case "Ý":str="Í";break;
						case "×":str="Ì";break;
						case "Þ":str="Ị";break;						
							//Chu O
						case "â":str="Õ";break;
						case "á":str="Ỏ";break;
						case "ß":str="Ò";break;
						case "ã":str="Ó";break;
						case "ä":str="Ọ";break;						
							//Chữ Ô
						case "«":str="Ô";break;
						case "ç":str="Ỗ";break;
						case "æ":str="Ổ";break;
						case "å":str="Ồ";break;
						case "è":str="Ố";break;
						case "é":str="Ộ";break;//

							//Chữ Ơ
						case "¬":str="Ơ";break;						
						case "ì":str="Ờ";break;
						case "ë":str="Ỡ";break;												
						case "ê":str="Ớ";break;						
						case "í":str="Ỡ";break;						
						case "î":str="Ợ";break;
							//Chữ Y
						case "ü":str="Ỹ";break;
						case "û":str="Ỷ";break;
						case "ý":str="Ý";break;
						case "ú":str="Ỳ";break;						
							//chữ U
						case "ò":str="Ũ";break;						
						case "ñ":str="Ủ";break;
						case "ï":str="Ù";break;												
						case "ó":str="Ú";break;						
						case "ô":str="Ụ";break;												
							//Chữ Ư
						case "­":str="Ư";break;						
						case "÷":str="Ữ";break;						
						case "ö":str="Ử";break;
						case "ø":str="Ứ";break;												
						case "õ":str="Ừ";break;						
						case "ù":str="Ự";break;
						*/

                        #endregion
                }

                strKQ = strKQ + str;
            }
            return strKQ;
        }

        public static string GetComputerName() {
            return System.Net.Dns.GetHostName();
        }

        public static bool IsAdmin(FTSMain ftsmain) {
            if (FTSConstant.ProductVersion == "FTSACCSTD2015" && ftsmain.UserInfo.UserID != "FTS_SUPPORT") {
                return false;
            } else {
                return Functions.InListAbsolute(ftsmain.UserInfo.UserGroupID, FTSConstant.AdminGroups);
            }
        }

        public static string ConvertStringVNtoEN(string strvalue) {
            string strResult = strvalue;
            string strFind =
                "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
            string strReplace =
                "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY";
            int index = -1;
            char[] arrCharFind = strFind.ToCharArray();
            while ((index = strResult.IndexOfAny(arrCharFind)) != -1) {
                int index1 = strFind.IndexOf(strResult[index]);
                strResult = strResult.Replace(strResult[index], strReplace[index1]);
            }
            return strResult;
        }
        

        public static string PopulateStringWithParentValue(FTSMain ftsmain, string tablename, string idlist) {
            if (idlist == string.Empty) {
                return "('ABCXYZABC')";
            }
            string[] idvaluelist = idlist.Split(',');
            string result = string.Empty;
            for (int i = 0; i < idvaluelist.Length; i++) {
                string idvalue = idvaluelist[i];
                if (idvalue != string.Empty) {
                    result += Functions.ParseParentValue(ftsmain, tablename, ftsmain.TableManager.GetIDField(tablename), idvalue) + ",";
                }
            }
            if (result != string.Empty) {
                result = result.Substring(0, result.Length - 1);
            }
            return Functions.PopulateString(result);
        }

        private static string ParseParentValue(FTSMain ftsmain, string tablename, string tableidfield, string idvalue) {
            int idparts = ftsmain.TableManager.IdParts(tablename);
            if (idparts <= 1) {
                return idvalue;
            } else {
                int partstart = 0;
                string parentcharacter = ftsmain.SystemVars.GetSystemVars("ID_PARENT_CHARACTER").ToString();
                string anycharater = ftsmain.SystemVars.GetSystemVars("ID_ANY_CHARACTER").ToString();
                //"[0-9,A-Z,a-z]";
                string returnvalue = string.Empty;
                if (parentcharacter != string.Empty && anycharater != string.Empty) {
                    for (int i = 0; i < idparts; i++) {
                        int partlength = ftsmain.IdManager.PartLength(tablename, i + 1);
                        string partvalue = idvalue.Substring(partstart, partlength);
                        string partparentvalue = string.Empty;
                        for (int j = 0; j < partlength; j++) {
                            partparentvalue += parentcharacter;
                        }
                        if (partvalue == partparentvalue) {
                            for (int j = 0; j < partlength; j++) {
                                returnvalue += anycharater;
                            }
                        } else {
                            returnvalue += partvalue;
                        }
                        partstart += partlength;
                    }
                    string sql = "SELECT " + tableidfield + " FROM " + tablename + " WHERE " + tableidfield + " LIKE '" + returnvalue + "%' and ACTIVE=1";
                    DataTable dt = ftsmain.DbMain.LoadDataTable(ftsmain.DbMain.GetSqlStringCommand(sql), "DM");
                    returnvalue = string.Empty;
                    foreach (DataRow row in dt.Rows) {
                        returnvalue += row[tableidfield].ToString() + ",";
                    }
                    if (returnvalue == string.Empty) {
                        return idvalue;
                    } else {
                        return returnvalue.Substring(0, returnvalue.Length - 1);
                    }
                } else {
                    return idvalue;
                }
            }

            //string returnvalue = string.Empty;
            //int idparts = ftsmain.TableManager.IdParts(tablename);
            //if (idparts <= 1) {
            //    return idvalue;
            //} else {
            //    int partstart = 0;
            //    string parentcharacter = ftsmain.SystemVars.GetSystemVars("ID_PARENT_CHARACTER").ToString();
            //    string anycharater = ftsmain.SystemVars.GetSystemVars("ID_ANY_CHARACTER").ToString();
            //    //"[0-9,A-Z,a-z]";
            //    if (parentcharacter != string.Empty && anycharater != string.Empty) {
            //        for (int i = 0; i < idparts; i++) {
            //            int partlength = ftsmain.IdManager.PartLength(tablename, i + 1);
            //            string partvalue = idvalue.Substring(partstart, partlength);
            //            string partparentvalue = string.Empty;
            //            for (int j = 0; j < partlength; j++) {
            //                partparentvalue += parentcharacter;
            //            }
            //            if (partvalue == partparentvalue) {
            //                for (int j = 0; j < partlength; j++) {
            //                    returnvalue += anycharater;
            //                }
            //            } else {
            //                returnvalue += partvalue;
            //            }
            //            partstart += partlength;
            //        }
            //        string sql = "SELECT " + tableidfield + " FROM " + tablename + " WHERE " + tableidfield + " LIKE '" + returnvalue +
            //                     "%' and ACTIVE=1 AND " + tableidfield + " NOT LIKE '%" + parentcharacter + "%'";
            //        DataTable dt = ftsmain.DbMain.LoadDataTable(ftsmain.DbMain.GetSqlStringCommand(sql), "DM");
            //        returnvalue = string.Empty;
            //        foreach (DataRow row in dt.Rows) {
            //            returnvalue += row[tableidfield].ToString() + ",";
            //        }
            //        if (returnvalue == string.Empty) {
            //            return idvalue;
            //        } else {
            //            return returnvalue.Substring(0, returnvalue.Length - 1);
            //        }
            //    } else {
            //        return idvalue;
            //    }
            //}
        }

        public static string GetCurrencyText(FTSMain ftsmain, string currencyid) {
            return currencyid;
        }
        
        public static string ParseParentValue(FTSMain ftsmain, string tablename, string idvalue) {
            int idparts = ftsmain.TableManager.IdParts(tablename);
            if (idparts <= 1) {
                return idvalue;
            } else {
                int partstart = 0;
                string parentcharacter = ftsmain.SystemVars.GetSystemVars("ID_PARENT_CHARACTER").ToString();
                string anycharater = ftsmain.SystemVars.GetSystemVars("ID_ANY_CHARACTER").ToString();
                //"[0-9,A-Z,a-z]";
                string returnvalue = string.Empty;
                if (parentcharacter != string.Empty && anycharater != string.Empty) {
                    for (int i = 0; i < idparts; i++) {
                        int partlength = ftsmain.IdManager.PartLength(tablename, i + 1);
                        string partvalue = idvalue.Substring(partstart, partlength);
                        string partparentvalue = string.Empty;
                        for (int j = 0; j < partlength; j++) {
                            partparentvalue += parentcharacter;
                        }
                        if (partvalue == partparentvalue) {
                            for (int j = 0; j < partlength; j++) {
                                returnvalue += anycharater;
                            }
                        } else {
                            returnvalue += partvalue;
                        }
                        partstart += partlength;
                    }
                    return returnvalue;
                } else {
                    return idvalue;
                }
            }
        }
        public static Color String2Color(string color) {
            try {
                if (color.Trim() != string.Empty) {
                    string[] rgb = color.Split(',');
                    if (rgb.Length == 3) {
                        return Color.FromArgb(Int32.Parse(rgb[0]), Int32.Parse(rgb[1]), Int32.Parse(rgb[2]));
                    } else {
                        return Color.FromName(color);
                    }
                } else {
                    return Color.SpringGreen;
                }
            } catch {
                return Color.SpringGreen;
            }
        }

        

        public static Guid GetPr_key(string tablename, FTSMain ftsmain) {
            return Guid.NewGuid();
            
        }
        public static string GetMAC()
        {
            //return "BaseBoardSerialNumber5A244116Q";
            string key = "Win32_BaseBoard";
            string key1 = "Win32_BIOS";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + key);
            string biosserialnumber = string.Empty;
            foreach (ManagementObject share in searcher.Get())
            {
                try
                {
                    biosserialnumber = share["SerialNumber"].ToString().Trim().Replace(" ", string.Empty);
                }
                catch (Exception)
                {
                }
                if (biosserialnumber != string.Empty)
                {
                    break;
                }

            }

            searcher = new ManagementObjectSearcher("select * from " + key1);
            string baseboardserialnumber = string.Empty;
            foreach (ManagementObject share in searcher.Get())
            {
                try
                {
                    baseboardserialnumber = share["SerialNumber"].ToString().Trim().Replace(" ", string.Empty);
                }
                catch (Exception)
                {
                }
                if (baseboardserialnumber != string.Empty)
                {
                    break;
                }

            }
            return biosserialnumber.Trim() + baseboardserialnumber.Trim();

        }
    }

    
}
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;
using System.Collections.Generic;
using System.Data;
using FTS.Base.Utilities;
using System;

namespace FTS.ShareBusiness.Acc
{
    public class Dm_Exchange_Rate : ObjectBase
    {
        public Dm_Exchange_Rate(FTSMain ftsmain) : base(ftsmain, "Dm_Exchange_Rate")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Exchange_Rate(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Exchange_Rate")
        {
            if (!isempty)
            {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Exchange_Rate(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Exchange_Rate", false)
        {
            this.LoadFields();
        }

        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_KEY", DbType.Guid, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CURRENCY_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "TO_CURRENCY_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "EXCHANGE_RATE", DbType.Decimal, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "VALID_DATE", DbType.Date, false));
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_EXCHANGE_RATE;
        }

        public static decimal GetExchangeRate(FTSMain ftsmain, string currency_id, DateTime date)
        {
            if (ftsmain.MainCurrency != currency_id)
            {
                string sql = "select top 1 EXCHANGE_RATE FROM DM_EXCHANGE_RATE WHERE CURRENCY_ID = '" + currency_id + "' and VALID_DATE <= " +
                             Functions.ParseDate(date) + " order by VALID_DATE DESC";
                object oj = ftsmain.DbMain.ExecuteScalar(ftsmain.DbMain.GetSqlStringCommand(sql));
                decimal result = 1;
                if (oj != null && oj != DBNull.Value)
                {
                    result = (decimal)oj;
                }

                if (result == 0)
                {
                    result = 1;
                }

                return result;
            }
            else
            {
                return 1;
            }
        }

        public static decimal GetExchangeRateConversion(FTSMain ftsmain, string fromcurrencyid, string tocurrencyid, DateTime date)
        {
            if (ftsmain.MainCurrency == tocurrencyid)
            {
                string sql = "select top 1 EXCHANGE_RATE FROM DM_EXCHANGE_RATE WHERE CURRENCY_ID = '" + fromcurrencyid + "' and VALID_DATE = " +
                             Functions.ParseDate(date) + " order by VALID_DATE DESC";
                object oj = ftsmain.DbMain.ExecuteScalar(ftsmain.DbMain.GetSqlStringCommand(sql));
                decimal result = 1;
                if (oj != null && oj != DBNull.Value)
                {
                    result = (decimal)oj;
                }

                return result;
            }
            else
            {
                if (ftsmain.MainCurrency == fromcurrencyid)
                {
                    string sql = "select top 1 EXCHANGE_RATE FROM DM_EXCHANGE_RATE WHERE CURRENCY_ID = '" + tocurrencyid + "' and VALID_DATE = " +
                                 Functions.ParseDate(date) + " order by VALID_DATE DESC";
                    object oj = ftsmain.DbMain.ExecuteScalar(ftsmain.DbMain.GetSqlStringCommand(sql));
                    decimal result = 1;
                    if (oj != null && oj != DBNull.Value)
                    {
                        result = (decimal)oj;
                    }

                    return result;
                }
                else
                {
                    string sql = "select top 1 EXCHANGE_RATE FROM DM_EXCHANGE_RATE WHERE CURRENCY_ID = '" + tocurrencyid + "' and VALID_DATE = " +
                                 Functions.ParseDate(date) + " order by VALID_DATE DESC";
                    object oj1 = ftsmain.DbMain.ExecuteScalar(ftsmain.DbMain.GetSqlStringCommand(sql));
                    sql = "select top 1 EXCHANGE_RATE FROM DM_EXCHANGE_RATE WHERE CURRENCY_ID = '" + fromcurrencyid + "' and VALID_DATE = " +
                          Functions.ParseDate(date) + " order by VALID_DATE DESC";
                    object oj2 = ftsmain.DbMain.ExecuteScalar(ftsmain.DbMain.GetSqlStringCommand(sql));
                    if (oj1 != null && oj1 != System.DBNull.Value && oj2 != null && oj2 != System.DBNull.Value)
                    {
                        if ((decimal)oj1 != 0)
                        {
                            return (decimal)oj2 / (decimal)oj1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_Exchange_RateObject dmExchangeRateObject = new Dm_Exchange_RateObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmExchangeRateObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dmExchangeRateObject;
            }
            else
            {
                return null;
            }
        }

        public override List<ObjectInfoBase> GetDataObjectList()
        {
            List<ObjectInfoBase> list = new List<ObjectInfoBase>();
            foreach (DataRow row in this.DataTable.Rows)
            {
                if (this.IsValidRow(row))
                {
                    Dm_Exchange_RateObject dmExchangeRateObject = new Dm_Exchange_RateObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmExchangeRateObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmExchangeRateObject);
                }
            }
            return list;
        }

        public List<Dm_Exchange_RateObject> GetDataByCurrencyAndDay(string currencyid, string tocurrencyid, DateTime date)
        {
            var list = new List<Dm_Exchange_RateObject>();
            string sql = @" SELECT *
                            FROM dbo.DM_EXCHANGE_RATE
                            WHERE CURRENCY_ID = @CURRENCY_ID
                                  AND TO_CURRENCY_ID = @TO_CURRENCY_ID
                                  AND CAST(VALID_DATE AS DATE) = CAST(@VALID_DATE AS DATE);";
            var cmd = this.FTSMain.DbMain.GetSqlStringCommand(sql);
            this.FTSMain.DbMain.AddInParameter(cmd, "CURRENCY_ID", DbType.String, currencyid);
            this.FTSMain.DbMain.AddInParameter(cmd, "TO_CURRENCY_ID", DbType.String, tocurrencyid);
            this.FTSMain.DbMain.AddInParameter(cmd, "VALID_DATE", DbType.DateTime, date);

            var table = this.FTSMain.DbMain.LoadDataTable(cmd, "Dm_Exchange_Rate");
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (this.IsValidRow(row))
                    {
                        Dm_Exchange_RateObject dmExchangeRateObject = new Dm_Exchange_RateObject();
                        foreach (DataColumn c in this.DataTable.Columns)
                        {
                            dmExchangeRateObject.SetValue(c.ColumnName, row[c.ColumnName]);
                        }
                        list.Add(dmExchangeRateObject);
                    }
                }
            }
            return list;
        }

        public bool CheckExistByCurrencyAndDay(string currencyid, string tocurrencyid, DateTime date)
        {
            string sql = @" SELECT TOP(1) 1
                            FROM dbo.DM_EXCHANGE_RATE
                            WHERE CURRENCY_ID = @CURRENCY_ID
                                  AND TO_CURRENCY_ID = @TO_CURRENCY_ID
                                  AND CAST(VALID_DATE AS DATE) = CAST(@VALID_DATE AS DATE);";
            var cmd = this.FTSMain.DbMain.GetSqlStringCommand(sql);
            this.FTSMain.DbMain.AddInParameter(cmd, "CURRENCY_ID", DbType.String, currencyid);
            this.FTSMain.DbMain.AddInParameter(cmd, "TO_CURRENCY_ID", DbType.String, tocurrencyid);
            this.FTSMain.DbMain.AddInParameter(cmd, "VALID_DATE", DbType.Date, date);

            var table = this.FTSMain.DbMain.LoadDataTable(cmd, "Dm_Exchange_Rate");
            if (table != null && table.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public override void UpdateData()
        {
            DataSet ds = this.DataSet.GetChanges();
            if (ds != null && ds.Tables[this.TableName] != null)
            {
                foreach (DataRow row in ds.Tables[this.TableName].Rows)
                {
                    if (row.RowState == DataRowState.Added && this.CheckExistByCurrencyAndDay(row["CURRENCY_ID"].ToString(), row["TO_CURRENCY_ID"].ToString(), (DateTime)row["VALID_DATE"]))
                    {
                        throw new FTSException("MSG_RECORD_ID_EXISTS");
                    }
                    else if (row.RowState == DataRowState.Modified)
                    {
                        var lstExchangeRateInday = this.GetDataByCurrencyAndDay(row["CURRENCY_ID"].ToString(), row["TO_CURRENCY_ID"].ToString(), (DateTime)row["VALID_DATE"]);
                        if ((lstExchangeRateInday?.Count == 1 && lstExchangeRateInday[0].PR_KEY != (Guid)row["PR_KEY"]) || lstExchangeRateInday?.Count > 1)
                        {
                            throw new FTSException("MSG_RECORD_ID_EXISTS");
                        }
                    }
                }
            }
            base.UpdateData();
        }
    }
}

using FTS.Base.Business;
using FTS.Base.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace FTS.Base.Systems
{
    [Serializable]
    public class Dm_TemplateManager : ManagerBase
    {
        public Dm_Template mDmTemplate;
        public Dm_Template_Detail mDmTemplateDetail;

        public Dm_TemplateManager(FTSMain esoftmain, string tran_id) : base(esoftmain, tran_id)
        {
            this.IsOrganizationFilter = true;
            this.mDmTemplate = new Dm_Template(esoftmain, this.DataSet, this);
            this.mDmTemplateDetail = new Dm_Template_Detail(esoftmain, this.DataSet, this);
            this.ObjectList.Add(this.mDmTemplate);
            this.ObjectList.Add(this.mDmTemplateDetail);
            this.TranDateField = string.Empty;
            this.TranNoField = "";
            this.TranIdField = "tran_id";
        }
        

        public override void CheckBusinessRules()
        {
        }

        public override void RemoveEmptyRows()
        {
            this.mDmTemplateDetail.RemoveEmptyRows();
        }


        public override DataRow AddNewDetail(int detailID, DataRow r)
        {
            DataRow row = base.AddNewDetail(detailID, r);
            row.EndEdit();
            return row;
        }

        public override ManagerObjectInfoBase GetDataObject()
        {
            if (this.mDmTemplate.IsValidRow(0))
            {
                Dm_TemplateManagerObject dmTemplateManagerObject = new Dm_TemplateManagerObject();
                foreach (DataColumn c in this.mDmTemplate.DataTable.Columns)
                {
                    dmTemplateManagerObject.dmTemplate.SetValue(c.ColumnName, this.mDmTemplate.DataTable.Rows[0][c.ColumnName]);
                }

                foreach (DataRow detailrow in this.mDmTemplateDetail.DataTable.Rows)
                {
                    if (this.mDmTemplateDetail.IsValidRow(detailrow))
                    {
                        ObjectInfoBase templateDetailObject = dmTemplateManagerObject.AddNewDetailObject();
                        foreach (DataColumn c in this.mDmTemplateDetail.DataTable.Columns)
                        {
                            templateDetailObject.SetValue(c.ColumnName, detailrow[c.ColumnName]);
                        }
                    }
                }

                return dmTemplateManagerObject;
            }
            else
            {
                return null;
            }
        }

        public void SyncObjectToTable(Dm_TemplateManagerObject dmtemplateManagerObject)
        {
            if (!this.mDmTemplate.IsValidRow(0))
            {
                DataRow newrow = this.mDmTemplate.DataTable.NewRow();
                this.mDmTemplate.DataTable.Rows.Add(newrow);
            }

            if (this.mDmTemplate.IsValidRow(0))
            {
                foreach (DataColumn c in this.mDmTemplate.DataTable.Columns)
                {
                    this.mDmTemplate.SetValueIfChange(this.mDmTemplate.DataTable.Rows[0], c.ColumnName, dmtemplateManagerObject.dmTemplate.GetValue(c.ColumnName));
                }

                try
                {
                    this.mDmTemplateDetail.DataTable.PrimaryKey = new DataColumn[] { this.mDmTemplateDetail.DataTable.Columns["PR_KEY"] };
                    foreach (Dm_Template_DetailObject dmTemplateDetailObject in dmtemplateManagerObject.dmTemplateDetail)
                    {
                        dmTemplateDetailObject.FR_KEY = dmtemplateManagerObject.dmTemplate.PR_KEY;
                        DataRow detailrow = this.mDmTemplateDetail.DataTable.Rows.Find(dmTemplateDetailObject.PR_KEY);
                        if (detailrow == null)
                        {
                            detailrow = this.mDmTemplateDetail.DataTable.NewRow();
                            detailrow["PR_KEY"] = 0;
                            this.mDmTemplateDetail.DataTable.Rows.Add(detailrow);
                        }

                        foreach (DataColumn c in this.mDmTemplateDetail.DataTable.Columns)
                        {
                            this.mDmTemplateDetail.SetValueIfChange(detailrow, c.ColumnName, dmTemplateDetailObject.GetValue(c.ColumnName));
                        }
                    }

                    List<DataRow> list = new List<DataRow>();
                    foreach (DataRow detailrow in this.mDmTemplateDetail.DataTable.Rows)
                    {
                        if (this.mDmTemplateDetail.IsValidRow(detailrow))
                        {
                            if (dmtemplateManagerObject.FindDetailObject((decimal)detailrow["PR_KEY"]) == null)
                            {
                                list.Add(detailrow);
                            }
                        }
                    }

                    foreach (DataRow detailrow in list)
                    {
                        detailrow.Delete();
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    this.mDmTemplateDetail.DataTable.PrimaryKey = null;
                }
            }
        }

        public override void UpdateData()
        {
            DataTable dtlogdata = this.ObjectList[0].DataTable.Copy();
            this.PrepareUpdateData();
            //string oldTran_no = string.Empty;
            //string oldOtherTran_no = string.Empty;
            DbTransaction tran = null;
            try
            {
                DataSet ds = this.DataSet.GetChanges();
                if (ds != null)
                {
                    using (DbConnection connection = this.FTSMain.DbMain.CreateConnection())
                    {
                        connection.Open();
                        if ((bool)this.FTSMain.SystemVars.GetSystemVars("USE_SNAPSHOT_TRANSACTION"))
                        {
                            tran = connection.BeginTransaction(IsolationLevel.Snapshot);
                        }
                        else
                        {
                            tran = connection.BeginTransaction();
                        }

                        //if ((bool)this.GetConfigValue("AUTO_TRAN_NO") && this.mTranNoField.Length != 0 &&
                        //    this.mObjectList[0].DataTable.Rows.Count > 0 &&
                        //    this.mObjectList[0].DataTable.Rows[0].RowState != DataRowState.Deleted)
                        //{
                        //    oldTran_no = this.mObjectList[0].DataTable.Rows[0][this.mTranNoField].ToString();
                        //    if (oldTran_no == this.FTSMain.SystemVars.GetSystemVars("DF_EMPTY_TRANS_NO").ToString())
                        //    {
                        //        this.mObjectList[0].DataTable.Rows[0][this.mTranNoField] = this.GetTranNoTemp(null);
                        //        this.CheckDuplicateTranNo();
                        //    }
                        //}

                        //oldOtherTran_no = this.UpdateOtherTranNoTemp(null);
                        ds = this.DataSet.GetChanges();
                        if (ds != null)
                        {
                            foreach (ObjectBase ob in this.ObjectList)
                            {
                                ob.UpdateData(ds, tran);
                            }

                            this.UpdateOtherData(tran);
                            //if (oldTran_no == this.FTSMain.SystemVars.GetSystemVars("DF_EMPTY_TRANS_NO").ToString())
                            //{
                            //    this.GetTranNo(tran);
                            //}

                            this.UpdateOtherTranNo(tran);
                            tran.Commit();

                            ds.AcceptChanges();
                            this.AcceptChanges();
                        }
                        else
                        {
                            //if (oldTran_no == this.FTSMain.SystemVars.GetSystemVars("DF_EMPTY_TRANS_NO").ToString())
                            //{
                            //    this.mObjectList[0].DataTable.Rows[0][this.mTranNoField] = oldTran_no;
                            //}

                            //this.RestoreOtherTranNo(oldOtherTran_no);
                            try
                            {
                                tran.Rollback();
                            }
                            catch (Exception) { }
                        }
                    }
                }
            }
            catch (FTSException)
            {
                //if (oldTran_no == this.mFTSMain.SystemVars.GetSystemVars("DF_EMPTY_TRANS_NO").ToString())
                //{
                //    this.mObjectList[0].DataTable.Rows[0][this.mTranNoField] = oldTran_no;
                //}

                //this.RestoreOtherTranNo(oldOtherTran_no);
                try
                {
                    tran.Rollback();
                }
                catch (Exception) { }

                throw;
            }
            catch (Exception ex)
            {
                //if (oldTran_no == this.mFTSMain.SystemVars.GetSystemVars("DF_EMPTY_TRANS_NO").ToString())
                //{
                //    this.mObjectList[0].DataTable.Rows[0][this.mTranNoField] = oldTran_no;
                //}

                //this.RestoreOtherTranNo(oldOtherTran_no);
                try
                {
                    tran.Rollback();
                }
                catch (Exception) { }

                throw (new FTSException(ex));
            }

        }

        public override void UpdateData(DbTransaction tran)
        {
            DataTable dtlogdata = this.ObjectList[0].DataTable.Copy();
            this.PrepareUpdateData();
            //string oldTran_no = string.Empty;
            //string oldOtherTran_no = string.Empty;
            try
            {
                DataSet ds = this.DataSet.GetChanges();
                if (ds != null)
                {

                    //if ((bool)this.GetConfigValue("AUTO_TRAN_NO") && this.mTranNoField.Length != 0 && this.mObjectList[0].DataTable.Rows.Count > 0 &&
                    //    this.mObjectList[0].DataTable.Rows[0].RowState != DataRowState.Deleted)
                    //{
                    //    oldTran_no = this.mObjectList[0].DataTable.Rows[0][this.mTranNoField].ToString();
                    //    if (oldTran_no == this.FTSMain.SystemVars.GetSystemVars("DF_EMPTY_TRANS_NO").ToString())
                    //    {
                    //        this.mObjectList[0].DataTable.Rows[0][this.mTranNoField] = this.GetTranNoTemp(null);
                    //        this.CheckDuplicateTranNo();
                    //    }
                    //}

                    //oldOtherTran_no = this.UpdateOtherTranNoTemp(null);
                    ds = this.DataSet.GetChanges();
                    if (ds != null)
                    {
                        foreach (ObjectBase ob in this.ObjectList)
                        {
                            ob.UpdateData(ds, tran);
                        }

                        this.UpdateOtherData(tran);
                        //if (oldTran_no == this.FTSMain.SystemVars.GetSystemVars("DF_EMPTY_TRANS_NO").ToString())
                        //{
                        //    this.GetTranNo(tran);
                        //}

                        this.UpdateOtherTranNo(tran);

                        ds.AcceptChanges();
                        this.AcceptChanges();
                    }
                    //else
                    //{
                    //    if (oldTran_no == this.mFTSMain.SystemVars.GetSystemVars("DF_EMPTY_TRANS_NO").ToString())
                    //    {
                    //        this.mObjectList[0].DataTable.Rows[0][this.mTranNoField] = oldTran_no;
                    //    }

                    //    this.RestoreOtherTranNo(oldOtherTran_no);

                    //}
                }

            }
            //catch (FTSException)
            //{
            //    if (oldTran_no == this.mFTSMain.SystemVars.GetSystemVars("DF_EMPTY_TRANS_NO").ToString())
            //    {
            //        this.mObjectList[0].DataTable.Rows[0][this.mTranNoField] = oldTran_no;
            //    }

            //    this.RestoreOtherTranNo(oldOtherTran_no);
            //    throw;
            //}
            catch (Exception ex)
            {
                //if (oldTran_no == this.mFTSMain.SystemVars.GetSystemVars("DF_EMPTY_TRANS_NO").ToString())
                //{
                //    this.mObjectList[0].DataTable.Rows[0][this.mTranNoField] = oldTran_no;
                //}

                //this.RestoreOtherTranNo(oldOtherTran_no);
                throw (new FTSException(ex));
            }
        }
    }
}

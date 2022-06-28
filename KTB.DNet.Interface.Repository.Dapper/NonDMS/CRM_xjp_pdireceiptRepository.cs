#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xjp_pdireceipt class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Nando (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 03 Sep 2020 10:30:47
 ===========================================================================
*/
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.NonDMS.SqlQuery.CRM_xjp_pdireceipt;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.NonDMS
{
    public class CRM_xjp_pdireceiptRepository : BaseDNetRepository<CRM_xjp_pdireceipt>, ICRM_xjp_pdireceiptRepository<CRM_xjp_pdireceipt, int>
    {
        #region Constructor
        public CRM_xjp_pdireceiptRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

		
        #region Create CRM_xjp_pdireceipt
        public ResponseMessage Create(CRM_xjp_pdireceipt entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                //SetCreatedLog(entity);
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(CRM_xjp_pdireceiptQuery.InsertCRM_xjp_pdireceipt, entity, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Predeliveryinspectionreceipt {0} has been successfully created", entity.xjp_pdireceiptid);
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create Predeliveryinspectionreceipt. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		
		
        public bool BulkInsert(List<CRM_xjp_pdireceipt> data)
        {	
            bool result = false;
            try
            {
                DataTable dataTableData = new DataTable("CRM_xjp_pdireceipt");
                dataTableData = data.ToDataTableForCreate();
                dataTableData = UpdateEmptyGuid(dataTableData);
                int batchSize = AppConfigs.GetInt(Constants.DBInsertBatchSizeConfigName);

                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    using (SqlBulkCopy insert = new SqlBulkCopy((SqlConnection)connection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction))
                    {
                        if (batchSize > 0)
                        {
                            insert.BatchSize = batchSize;
                        }
                        insert.DestinationTableName = "CRM_xjp_pdireceipt";

                        insert.WriteToServer(dataTableData);

                        result = true;
                        return true;
                    }
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		

        #region Update CRM_xjp_pdireceipt
        public ResponseMessage Update(CRM_xjp_pdireceipt entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (entity != null)
                {
                    //SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(CRM_xjp_pdireceiptQuery.UpdateCRM_xjp_pdireceipt, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Predeliveryinspectionreceipt{0} has been successfully updated", entity.xjp_pdireceiptid);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Predeliveryinspectionreceipt does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Predeliveryinspectionreceipt. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		

        #region Delete CRM_xjp_pdireceipt
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get CRM_xjp_pdireceipt By Id
        public CRM_xjp_pdireceipt Get(Guid xjp_pdireceiptid)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<CRM_xjp_pdireceipt>(
                        CRM_xjp_pdireceiptQuery.GetCRM_xjp_pdireceiptByID, new { xjp_pdireceiptid = xjp_pdireceiptid }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CRM_xjp_pdireceipt Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All CRM_xjp_pdireceipt
        public List<CRM_xjp_pdireceipt> GetAll()
        {
            return null;
        }
        #endregion

        #region Search CRM_xjp_pdireceipt
        public List<CRM_xjp_pdireceipt> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<CRM_xjp_pdireceipt>();
        }
        #endregion

		#region Search CRM_xjp_pdireceipt        
        public new List<CRM_xjp_pdireceipt> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<CRM_xjp_pdireceipt> result = SearchFetchPaging<CRM_xjp_pdireceipt>((connection, query, sqlParams) =>
                {
                    return connection.Query<CRM_xjp_pdireceipt>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(CRM_xjp_pdireceiptQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "CRM_xjp_pdireceipt.xjp_pdireceiptid", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(CRM_xjp_pdireceiptQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "xjp_pdireceiptid");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<CRM_xjp_pdireceipt>();
            }
        }
        #endregion

        public void SetCreatedLog(CRM_xjp_pdireceipt CRM_xjp_pdireceipt)
        {
            CRM_xjp_pdireceipt.createdbyname = UserLogin;
            CRM_xjp_pdireceipt.createdon = DateTime.Now;
            CRM_xjp_pdireceipt.RowStatus = "0";
            SetLastModifiedLog(CRM_xjp_pdireceipt);
        }

        public void SetLastModifiedLog(CRM_xjp_pdireceipt CRM_xjp_pdireceipt)
        {
            CRM_xjp_pdireceipt.modifiedbyname = UserLogin;
            CRM_xjp_pdireceipt.modifiedon = DateTime.Now;
        }

		
        private DataTable UpdateEmptyGuid(DataTable dt)
        {
            foreach (DataColumn col in dt.Columns)
            {
                if (col.DataType.Name.ToString() == "Guid")
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row[col.ColumnName].ToString() == Guid.Empty.ToString())
                        {
                            row[col.ColumnName] = DBNull.Value;
                        }
                    }
                }
            }
            return dt;
        }
		

    }
}

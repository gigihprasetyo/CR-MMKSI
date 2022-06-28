#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_salesorder class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Fika
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 01 Sep 2020 11:08:19
 ===========================================================================
*/
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.NonDMS.SqlQuery.CRM_xts_salesorder;
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
    public class CRM_xts_salesorderRepository : BaseDNetRepository<CRM_xts_salesorder>, ICRM_xts_salesorderRepository<CRM_xts_salesorder, int>
    {
        #region Constructor
        public CRM_xts_salesorderRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

		
        #region Create CRM_xts_salesorder
        public ResponseMessage Create(CRM_xts_salesorder entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                //SetCreatedLog(entity);
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(CRM_xts_salesorderQuery.InsertCRM_xts_salesorder, entity, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Salesorder {0} has been successfully created", entity.xts_salesorderid);
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create Salesorder. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		
		
        public bool BulkInsert(List<CRM_xts_salesorder> data)
        {	
            bool result = false;
            try
            {
                DataTable dataTableData = new DataTable("CRM_xts_salesorder");
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
                        insert.DestinationTableName = "CRM_xts_salesorder";

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
		

        #region Update CRM_xts_salesorder
        public ResponseMessage Update(CRM_xts_salesorder entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (entity != null)
                {
                    //SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(CRM_xts_salesorderQuery.UpdateCRM_xts_salesorder, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Salesorder{0} has been successfully updated", entity.xts_salesorderid);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Salesorder does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Salesorder. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		

        #region Delete CRM_xts_salesorder
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get CRM_xts_salesorder By Id
        public CRM_xts_salesorder Get(Guid xts_salesorderid)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<CRM_xts_salesorder>(
                        CRM_xts_salesorderQuery.GetCRM_xts_salesorderByID, new { xts_salesorderid = xts_salesorderid }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CRM_xts_salesorder Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All CRM_xts_salesorder
        public List<CRM_xts_salesorder> GetAll()
        {
            return null;
        }
        #endregion

        #region Search CRM_xts_salesorder
        public List<CRM_xts_salesorder> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<CRM_xts_salesorder>();
        }
        #endregion

		#region Search CRM_xts_salesorder        
        public new List<CRM_xts_salesorder> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<CRM_xts_salesorder> result = SearchFetchPaging<CRM_xts_salesorder>((connection, query, sqlParams) =>
                {
                    return connection.Query<CRM_xts_salesorder>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(CRM_xts_salesorderQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "CRM_xts_salesorder.xts_salesorderid", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(CRM_xts_salesorderQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "xts_salesorderid");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<CRM_xts_salesorder>();
            }
        }
        #endregion

        public void SetCreatedLog(CRM_xts_salesorder CRM_xts_salesorder)
        {
            CRM_xts_salesorder.createdbyname = UserLogin;
            CRM_xts_salesorder.createdon = DateTime.Now;
            CRM_xts_salesorder.RowStatus = "0";
            SetLastModifiedLog(CRM_xts_salesorder);
        }

        public void SetLastModifiedLog(CRM_xts_salesorder CRM_xts_salesorder)
        {
            CRM_xts_salesorder.modifiedbyname = UserLogin;
            CRM_xts_salesorder.modifiedon = DateTime.Now;
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

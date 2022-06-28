#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_deliveryorder class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Khalil
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 31 Aug 2020 15:46:23
 ===========================================================================
*/
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.NonDMS.SqlQuery.CRM_xts_deliveryorder;
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
    public class CRM_xts_deliveryorderRepository : BaseDNetRepository<CRM_xts_deliveryorder>, ICRM_xts_deliveryorderRepository<CRM_xts_deliveryorder, int>
    {
        #region Constructor
        public CRM_xts_deliveryorderRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

		
        #region Create CRM_xts_deliveryorder
        public ResponseMessage Create(CRM_xts_deliveryorder entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                //SetCreatedLog(entity);
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(CRM_xts_deliveryorderQuery.InsertCRM_xts_deliveryorder, entity, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Deliveryorder {0} has been successfully created", entity.xts_deliveryorderid);
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create Deliveryorder. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		
		
        public bool BulkInsert(List<CRM_xts_deliveryorder> data)
        {	
            bool result = false;
            try
            {
                DataTable dataTableData = new DataTable("CRM_xts_deliveryorder");
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
                        insert.DestinationTableName = "CRM_xts_deliveryorder";

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
		

        #region Update CRM_xts_deliveryorder
        public ResponseMessage Update(CRM_xts_deliveryorder entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (entity != null)
                {
                    //SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(CRM_xts_deliveryorderQuery.UpdateCRM_xts_deliveryorder, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Deliveryorder{0} has been successfully updated", entity.xts_deliveryorderid);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Deliveryorder does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Deliveryorder. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		

        #region Delete CRM_xts_deliveryorder
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get CRM_xts_deliveryorder By Id
        public CRM_xts_deliveryorder Get(Guid xts_deliveryorderid)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<CRM_xts_deliveryorder>(
                        CRM_xts_deliveryorderQuery.GetCRM_xts_deliveryorderByID, new { xts_deliveryorderid = xts_deliveryorderid }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CRM_xts_deliveryorder Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All CRM_xts_deliveryorder
        public List<CRM_xts_deliveryorder> GetAll()
        {
            return null;
        }
        #endregion

        #region Search CRM_xts_deliveryorder
        public List<CRM_xts_deliveryorder> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<CRM_xts_deliveryorder>();
        }
        #endregion

		#region Search CRM_xts_deliveryorder        
        public new List<CRM_xts_deliveryorder> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<CRM_xts_deliveryorder> result = SearchFetchPaging<CRM_xts_deliveryorder>((connection, query, sqlParams) =>
                {
                    return connection.Query<CRM_xts_deliveryorder>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(CRM_xts_deliveryorderQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "CRM_xts_deliveryorder.xts_deliveryorderid", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(CRM_xts_deliveryorderQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "xts_deliveryorderid");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<CRM_xts_deliveryorder>();
            }
        }
        #endregion

        public void SetCreatedLog(CRM_xts_deliveryorder CRM_xts_deliveryorder)
        {
            CRM_xts_deliveryorder.createdbyname = UserLogin;
            CRM_xts_deliveryorder.createdon = DateTime.Now;
            CRM_xts_deliveryorder.RowStatus = "0";
            SetLastModifiedLog(CRM_xts_deliveryorder);
        }

        public void SetLastModifiedLog(CRM_xts_deliveryorder CRM_xts_deliveryorder)
        {
            CRM_xts_deliveryorder.modifiedbyname = UserLogin;
            CRM_xts_deliveryorder.modifiedon = DateTime.Now;
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
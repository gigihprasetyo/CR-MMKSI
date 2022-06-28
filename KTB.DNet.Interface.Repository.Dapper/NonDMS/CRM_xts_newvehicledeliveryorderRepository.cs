#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_newvehicledeliveryorder class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Nando
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 31 Aug 2020 15:39:05
 ===========================================================================
*/
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.NonDMS.SqlQuery.CRM_xts_newvehicledeliveryorder;
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
    public class CRM_xts_newvehicledeliveryorderRepository : BaseDNetRepository<CRM_xts_newvehicledeliveryorder>, ICRM_xts_newvehicledeliveryorderRepository<CRM_xts_newvehicledeliveryorder, int>
    {
        #region Constructor
        public CRM_xts_newvehicledeliveryorderRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

		
        #region Create CRM_xts_newvehicledeliveryorder
        public ResponseMessage Create(CRM_xts_newvehicledeliveryorder entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                //SetCreatedLog(entity);
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(CRM_xts_newvehicledeliveryorderQuery.InsertCRM_xts_newvehicledeliveryorder, entity, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Newvehicledeliveryorder {0} has been successfully created", entity.xts_newvehicledeliveryorderid);
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create Newvehicledeliveryorder. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		
		
        public bool BulkInsert(List<CRM_xts_newvehicledeliveryorder> data)
        {	
            bool result = false;
            try
            {
                DataTable dataTableData = new DataTable("CRM_xts_newvehicledeliveryorder");
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
                        insert.DestinationTableName = "CRM_xts_newvehicledeliveryorder";

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
		

        #region Update CRM_xts_newvehicledeliveryorder
        public ResponseMessage Update(CRM_xts_newvehicledeliveryorder entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (entity != null)
                {
                    //SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(CRM_xts_newvehicledeliveryorderQuery.UpdateCRM_xts_newvehicledeliveryorder, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Newvehicledeliveryorder{0} has been successfully updated", entity.xts_newvehicledeliveryorderid);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Newvehicledeliveryorder does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Newvehicledeliveryorder. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		

        #region Delete CRM_xts_newvehicledeliveryorder
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get CRM_xts_newvehicledeliveryorder By Id
        public CRM_xts_newvehicledeliveryorder Get(Guid xts_newvehicledeliveryorderid)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<CRM_xts_newvehicledeliveryorder>(
                        CRM_xts_newvehicledeliveryorderQuery.GetCRM_xts_newvehicledeliveryorderByID, new { xts_newvehicledeliveryorderid = xts_newvehicledeliveryorderid }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CRM_xts_newvehicledeliveryorder Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All CRM_xts_newvehicledeliveryorder
        public List<CRM_xts_newvehicledeliveryorder> GetAll()
        {
            return null;
        }
        #endregion

        #region Search CRM_xts_newvehicledeliveryorder
        public List<CRM_xts_newvehicledeliveryorder> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<CRM_xts_newvehicledeliveryorder>();
        }
        #endregion

		#region Search CRM_xts_newvehicledeliveryorder        
        public new List<CRM_xts_newvehicledeliveryorder> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<CRM_xts_newvehicledeliveryorder> result = SearchFetchPaging<CRM_xts_newvehicledeliveryorder>((connection, query, sqlParams) =>
                {
                    return connection.Query<CRM_xts_newvehicledeliveryorder>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(CRM_xts_newvehicledeliveryorderQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "CRM_xts_newvehicledeliveryorder.xts_newvehicledeliveryorderid", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(CRM_xts_newvehicledeliveryorderQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "xts_newvehicledeliveryorderid");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<CRM_xts_newvehicledeliveryorder>();
            }
        }
        #endregion

        public void SetCreatedLog(CRM_xts_newvehicledeliveryorder CRM_xts_newvehicledeliveryorder)
        {
            CRM_xts_newvehicledeliveryorder.createdbyname = UserLogin;
            CRM_xts_newvehicledeliveryorder.createdon = DateTime.Now;
            CRM_xts_newvehicledeliveryorder.RowStatus = "0";
            SetLastModifiedLog(CRM_xts_newvehicledeliveryorder);
        }

        public void SetLastModifiedLog(CRM_xts_newvehicledeliveryorder CRM_xts_newvehicledeliveryorder)
        {
            CRM_xts_newvehicledeliveryorder.modifiedbyname = UserLogin;
            CRM_xts_newvehicledeliveryorder.modifiedon = DateTime.Now;
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
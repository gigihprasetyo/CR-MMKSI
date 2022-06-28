#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_servicequeue class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Gugun (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 03 Sep 2020 16:17:24
 ===========================================================================
*/
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.NonDMS.SqlQuery.CRM_xts_servicequeue;
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
    public class CRM_xts_servicequeueRepository : BaseDNetRepository<CRM_xts_servicequeue>, ICRM_xts_servicequeueRepository<CRM_xts_servicequeue, int>
    {
        #region Constructor
        public CRM_xts_servicequeueRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

		
        #region Create CRM_xts_servicequeue
        public ResponseMessage Create(CRM_xts_servicequeue entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                //SetCreatedLog(entity);
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(CRM_xts_servicequeueQuery.InsertCRM_xts_servicequeue, entity, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Servicequeue {0} has been successfully created", entity.xts_servicequeueid);
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create Servicequeue. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		
		
        public bool BulkInsert(List<CRM_xts_servicequeue> data)
        {	
            bool result = false;
            try
            {
                DataTable dataTableData = new DataTable("CRM_xts_servicequeue");
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
                        insert.DestinationTableName = "CRM_xts_servicequeue";

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
		

        #region Update CRM_xts_servicequeue
        public ResponseMessage Update(CRM_xts_servicequeue entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (entity != null)
                {
                    //SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(CRM_xts_servicequeueQuery.UpdateCRM_xts_servicequeue, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Servicequeue{0} has been successfully updated", entity.xts_servicequeueid);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Servicequeue does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Servicequeue. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		

        #region Delete CRM_xts_servicequeue
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get CRM_xts_servicequeue By Id
        public CRM_xts_servicequeue Get(Guid xts_servicequeueid)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<CRM_xts_servicequeue>(
                        CRM_xts_servicequeueQuery.GetCRM_xts_servicequeueByID, new { xts_servicequeueid = xts_servicequeueid }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CRM_xts_servicequeue Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All CRM_xts_servicequeue
        public List<CRM_xts_servicequeue> GetAll()
        {
            return null;
        }
        #endregion

        #region Search CRM_xts_servicequeue
        public List<CRM_xts_servicequeue> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<CRM_xts_servicequeue>();
        }
        #endregion

		#region Search CRM_xts_servicequeue        
        public new List<CRM_xts_servicequeue> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<CRM_xts_servicequeue> result = SearchFetchPaging<CRM_xts_servicequeue>((connection, query, sqlParams) =>
                {
                    return connection.Query<CRM_xts_servicequeue>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(CRM_xts_servicequeueQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "CRM_xts_servicequeue.xts_servicequeueid", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(CRM_xts_servicequeueQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "xts_servicequeueid");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<CRM_xts_servicequeue>();
            }
        }
        #endregion

        public void SetCreatedLog(CRM_xts_servicequeue CRM_xts_servicequeue)
        {
            CRM_xts_servicequeue.createdbyname = UserLogin;
            CRM_xts_servicequeue.createdon = DateTime.Now;
            CRM_xts_servicequeue.RowStatus = 0;
            SetLastModifiedLog(CRM_xts_servicequeue);
        }

        public void SetLastModifiedLog(CRM_xts_servicequeue CRM_xts_servicequeue)
        {
            CRM_xts_servicequeue.modifiedbyname = UserLogin;
            CRM_xts_servicequeue.modifiedon = DateTime.Now;
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

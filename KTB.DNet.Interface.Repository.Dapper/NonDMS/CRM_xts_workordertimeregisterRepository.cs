#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_workordertimeregister class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Nando
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 01 Sep 2020 12:25:09
 ===========================================================================
*/
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.NonDMS.SqlQuery.CRM_xts_workordertimeregister;
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
    public class CRM_xts_workordertimeregisterRepository : BaseDNetRepository<CRM_xts_workordertimeregister>, ICRM_xts_workordertimeregisterRepository<CRM_xts_workordertimeregister, int>
    {
        #region Constructor
        public CRM_xts_workordertimeregisterRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

		
        #region Create CRM_xts_workordertimeregister
        public ResponseMessage Create(CRM_xts_workordertimeregister entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                //SetCreatedLog(entity);
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(CRM_xts_workordertimeregisterQuery.InsertCRM_xts_workordertimeregister, entity, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Wotimeregister {0} has been successfully created", entity.xts_workordertimeregisterid);
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create Wotimeregister. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		
		
        public bool BulkInsert(List<CRM_xts_workordertimeregister> data)
        {	
            bool result = false;
            try
            {
                DataTable dataTableData = new DataTable("CRM_xts_workordertimeregister");
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
                        insert.DestinationTableName = "CRM_xts_workordertimeregister";

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
		

        #region Update CRM_xts_workordertimeregister
        public ResponseMessage Update(CRM_xts_workordertimeregister entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (entity != null)
                {
                    //SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(CRM_xts_workordertimeregisterQuery.UpdateCRM_xts_workordertimeregister, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Wotimeregister{0} has been successfully updated", entity.xts_workordertimeregisterid);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Wotimeregister does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Wotimeregister. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		

        #region Delete CRM_xts_workordertimeregister
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get CRM_xts_workordertimeregister By Id
        public CRM_xts_workordertimeregister Get(Guid xts_workordertimeregisterid)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<CRM_xts_workordertimeregister>(
                        CRM_xts_workordertimeregisterQuery.GetCRM_xts_workordertimeregisterByID, new { xts_workordertimeregisterid = xts_workordertimeregisterid }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CRM_xts_workordertimeregister Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All CRM_xts_workordertimeregister
        public List<CRM_xts_workordertimeregister> GetAll()
        {
            return null;
        }
        #endregion

        #region Search CRM_xts_workordertimeregister
        public List<CRM_xts_workordertimeregister> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<CRM_xts_workordertimeregister>();
        }
        #endregion

		#region Search CRM_xts_workordertimeregister        
        public new List<CRM_xts_workordertimeregister> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<CRM_xts_workordertimeregister> result = SearchFetchPaging<CRM_xts_workordertimeregister>((connection, query, sqlParams) =>
                {
                    return connection.Query<CRM_xts_workordertimeregister>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(CRM_xts_workordertimeregisterQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "CRM_xts_workordertimeregister.xts_workordertimeregisterid", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(CRM_xts_workordertimeregisterQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "xts_workordertimeregisterid");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<CRM_xts_workordertimeregister>();
            }
        }
        #endregion

        public void SetCreatedLog(CRM_xts_workordertimeregister CRM_xts_workordertimeregister)
        {
            CRM_xts_workordertimeregister.createdbyname = UserLogin;
            CRM_xts_workordertimeregister.createdon = DateTime.Now;
            CRM_xts_workordertimeregister.RowStatus = "0";
            SetLastModifiedLog(CRM_xts_workordertimeregister);
        }

        public void SetLastModifiedLog(CRM_xts_workordertimeregister CRM_xts_workordertimeregister)
        {
            CRM_xts_workordertimeregister.modifiedbyname = UserLogin;
            CRM_xts_workordertimeregister.modifiedon = DateTime.Now;
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

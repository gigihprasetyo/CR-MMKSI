#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xjp_vehicletransfer class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Nando (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 02 Sep 2020 12:03:21
 ===========================================================================
*/
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.NonDMS.SqlQuery.CRM_xjp_vehicletransfer;
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
    public class CRM_xjp_vehicletransferRepository : BaseDNetRepository<CRM_xjp_vehicletransfer>, ICRM_xjp_vehicletransferRepository<CRM_xjp_vehicletransfer, int>
    {
        #region Constructor
        public CRM_xjp_vehicletransferRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

		
        #region Create CRM_xjp_vehicletransfer
        public ResponseMessage Create(CRM_xjp_vehicletransfer entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                //SetCreatedLog(entity);
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(CRM_xjp_vehicletransferQuery.InsertCRM_xjp_vehicletransfer, entity, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Vehicletransfer {0} has been successfully created", entity.xjp_vehicletransferid);
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create Vehicletransfer. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		
		
        public bool BulkInsert(List<CRM_xjp_vehicletransfer> data)
        {	
            bool result = false;
            try
            {
                DataTable dataTableData = new DataTable("CRM_xjp_vehicletransfer");
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
                        insert.DestinationTableName = "CRM_xjp_vehicletransfer";

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
		

        #region Update CRM_xjp_vehicletransfer
        public ResponseMessage Update(CRM_xjp_vehicletransfer entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (entity != null)
                {
                    //SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(CRM_xjp_vehicletransferQuery.UpdateCRM_xjp_vehicletransfer, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Vehicletransfer{0} has been successfully updated", entity.xjp_vehicletransferid);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Vehicletransfer does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Vehicletransfer. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		

        #region Delete CRM_xjp_vehicletransfer
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get CRM_xjp_vehicletransfer By Id
        public CRM_xjp_vehicletransfer Get(Guid xjp_vehicletransferid)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<CRM_xjp_vehicletransfer>(
                        CRM_xjp_vehicletransferQuery.GetCRM_xjp_vehicletransferByID, new { xjp_vehicletransferid = xjp_vehicletransferid }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CRM_xjp_vehicletransfer Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All CRM_xjp_vehicletransfer
        public List<CRM_xjp_vehicletransfer> GetAll()
        {
            return null;
        }
        #endregion

        #region Search CRM_xjp_vehicletransfer
        public List<CRM_xjp_vehicletransfer> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<CRM_xjp_vehicletransfer>();
        }
        #endregion

		#region Search CRM_xjp_vehicletransfer        
        public new List<CRM_xjp_vehicletransfer> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<CRM_xjp_vehicletransfer> result = SearchFetchPaging<CRM_xjp_vehicletransfer>((connection, query, sqlParams) =>
                {
                    return connection.Query<CRM_xjp_vehicletransfer>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(CRM_xjp_vehicletransferQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "CRM_xjp_vehicletransfer.xjp_vehicletransferid", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(CRM_xjp_vehicletransferQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "xjp_vehicletransferid");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<CRM_xjp_vehicletransfer>();
            }
        }
        #endregion

        public void SetCreatedLog(CRM_xjp_vehicletransfer CRM_xjp_vehicletransfer)
        {
            CRM_xjp_vehicletransfer.createdbyname = UserLogin;
            CRM_xjp_vehicletransfer.createdon = DateTime.Now;
            CRM_xjp_vehicletransfer.RowStatus = "0";
            SetLastModifiedLog(CRM_xjp_vehicletransfer);
        }

        public void SetLastModifiedLog(CRM_xjp_vehicletransfer CRM_xjp_vehicletransfer)
        {
            CRM_xjp_vehicletransfer.modifiedbyname = UserLogin;
            CRM_xjp_vehicletransfer.modifiedon = DateTime.Now;
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
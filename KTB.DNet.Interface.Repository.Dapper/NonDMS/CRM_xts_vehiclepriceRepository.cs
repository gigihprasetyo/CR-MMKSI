#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_vehicleprice class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 05 Feb 2021 13:44:53
 ===========================================================================
*/
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.NonDMS.SqlQuery.CRM_xts_vehicleprice;
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
    public class CRM_xts_vehiclepriceRepository : BaseDNetRepository<CRM_xts_vehicleprice>, ICRM_xts_vehiclepriceRepository<CRM_xts_vehicleprice, int>
    {
        #region Constructor
        public CRM_xts_vehiclepriceRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

		
        #region Create CRM_xts_vehicleprice
        public ResponseMessage Create(CRM_xts_vehicleprice entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                //SetCreatedLog(entity);
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(CRM_xts_vehiclepriceQuery.InsertCRM_xts_vehicleprice, entity, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Vehicleprice {0} has been successfully created", entity.xts_vehiclepriceid);
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create Vehicleprice. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		
		
        public bool BulkInsert(List<CRM_xts_vehicleprice> data)
        {	
            bool result = false;
            try
            {
                DataTable dataTableData = new DataTable("CRM_xts_vehicleprice");
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
                        insert.DestinationTableName = "CRM_xts_vehicleprice";

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
		

        #region Update CRM_xts_vehicleprice
        public ResponseMessage Update(CRM_xts_vehicleprice entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (entity != null)
                {
                    //SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(CRM_xts_vehiclepriceQuery.UpdateCRM_xts_vehicleprice, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Vehicleprice{0} has been successfully updated", entity.xts_vehiclepriceid);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Vehicleprice does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Vehicleprice. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		

        #region Delete CRM_xts_vehicleprice
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get CRM_xts_vehicleprice By Id
        public CRM_xts_vehicleprice Get(Guid xts_vehiclepriceid)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<CRM_xts_vehicleprice>(
                        CRM_xts_vehiclepriceQuery.GetCRM_xts_vehiclepriceByID, new { xts_vehiclepriceid = xts_vehiclepriceid }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CRM_xts_vehicleprice Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All CRM_xts_vehicleprice
        public List<CRM_xts_vehicleprice> GetAll()
        {
            return null;
        }
        #endregion

        #region Search CRM_xts_vehicleprice
        public List<CRM_xts_vehicleprice> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<CRM_xts_vehicleprice>();
        }
        #endregion

		#region Search CRM_xts_vehicleprice        
        public new List<CRM_xts_vehicleprice> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<CRM_xts_vehicleprice> result = SearchFetchPaging<CRM_xts_vehicleprice>((connection, query, sqlParams) =>
                {
                    return connection.Query<CRM_xts_vehicleprice>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(CRM_xts_vehiclepriceQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "CRM_xts_vehicleprice.xts_vehiclepriceid", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(CRM_xts_vehiclepriceQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "xts_vehiclepriceid");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<CRM_xts_vehicleprice>();
            }
        }
        #endregion

        public void SetCreatedLog(CRM_xts_vehicleprice CRM_xts_vehicleprice)
        {
            CRM_xts_vehicleprice.createdbyname = UserLogin;
            CRM_xts_vehicleprice.createdon = DateTime.Now;
            CRM_xts_vehicleprice.RowStatus = "0";
            SetLastModifiedLog(CRM_xts_vehicleprice);
        }

        public void SetLastModifiedLog(CRM_xts_vehicleprice CRM_xts_vehicleprice)
        {
            CRM_xts_vehicleprice.modifiedbyname = UserLogin;
            CRM_xts_vehicleprice.modifiedon = DateTime.Now;
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

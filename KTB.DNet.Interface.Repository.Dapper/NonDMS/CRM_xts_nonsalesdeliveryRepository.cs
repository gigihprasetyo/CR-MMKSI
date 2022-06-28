#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_nonsalesdelivery class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Gugun (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 03 Sep 2020 11:26:28
 ===========================================================================
*/
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.NonDMS.SqlQuery.CRM_xts_nonsalesdelivery;
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
    public class CRM_xts_nonsalesdeliveryRepository : BaseDNetRepository<CRM_xts_nonsalesdelivery>, ICRM_xts_nonsalesdeliveryRepository<CRM_xts_nonsalesdelivery, int>
    {
        #region Constructor
        public CRM_xts_nonsalesdeliveryRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

		
        #region Create CRM_xts_nonsalesdelivery
        public ResponseMessage Create(CRM_xts_nonsalesdelivery entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                //SetCreatedLog(entity);
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(CRM_xts_nonsalesdeliveryQuery.InsertCRM_xts_nonsalesdelivery, entity, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Nonsalesdelivery {0} has been successfully created", entity.xts_nonsalesdeliveryid);
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create Nonsalesdelivery. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		
		
        public bool BulkInsert(List<CRM_xts_nonsalesdelivery> data)
        {	
            bool result = false;
            try
            {
                DataTable dataTableData = new DataTable("CRM_xts_nonsalesdelivery");
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
                        insert.DestinationTableName = "CRM_xts_nonsalesdelivery";

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
		

        #region Update CRM_xts_nonsalesdelivery
        public ResponseMessage Update(CRM_xts_nonsalesdelivery entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (entity != null)
                {
                    //SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(CRM_xts_nonsalesdeliveryQuery.UpdateCRM_xts_nonsalesdelivery, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Nonsalesdelivery{0} has been successfully updated", entity.xts_nonsalesdeliveryid);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Nonsalesdelivery does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Nonsalesdelivery. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		

        #region Delete CRM_xts_nonsalesdelivery
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get CRM_xts_nonsalesdelivery By Id
        public CRM_xts_nonsalesdelivery Get(Guid xts_nonsalesdeliveryid)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<CRM_xts_nonsalesdelivery>(
                        CRM_xts_nonsalesdeliveryQuery.GetCRM_xts_nonsalesdeliveryByID, new { xts_nonsalesdeliveryid = xts_nonsalesdeliveryid }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CRM_xts_nonsalesdelivery Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All CRM_xts_nonsalesdelivery
        public List<CRM_xts_nonsalesdelivery> GetAll()
        {
            return null;
        }
        #endregion

        #region Search CRM_xts_nonsalesdelivery
        public List<CRM_xts_nonsalesdelivery> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<CRM_xts_nonsalesdelivery>();
        }
        #endregion

		#region Search CRM_xts_nonsalesdelivery        
        public new List<CRM_xts_nonsalesdelivery> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<CRM_xts_nonsalesdelivery> result = SearchFetchPaging<CRM_xts_nonsalesdelivery>((connection, query, sqlParams) =>
                {
                    return connection.Query<CRM_xts_nonsalesdelivery>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(CRM_xts_nonsalesdeliveryQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "CRM_xts_nonsalesdelivery.xts_nonsalesdeliveryid", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(CRM_xts_nonsalesdeliveryQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "xts_nonsalesdeliveryid");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<CRM_xts_nonsalesdelivery>();
            }
        }
        #endregion

        public void SetCreatedLog(CRM_xts_nonsalesdelivery CRM_xts_nonsalesdelivery)
        {
            CRM_xts_nonsalesdelivery.createdbyname = UserLogin;
            CRM_xts_nonsalesdelivery.createdon = DateTime.Now;
            CRM_xts_nonsalesdelivery.RowStatus = "0";
            SetLastModifiedLog(CRM_xts_nonsalesdelivery);
        }

        public void SetLastModifiedLog(CRM_xts_nonsalesdelivery CRM_xts_nonsalesdelivery)
        {
            CRM_xts_nonsalesdelivery.modifiedbyname = UserLogin;
            CRM_xts_nonsalesdelivery.modifiedon = DateTime.Now;
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

#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_inventorytransactiondetail class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Kemin
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 01 Sep 2020 13:46:48
 ===========================================================================
*/
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.NonDMS.SqlQuery.CRM_xts_inventorytransactiondetail;
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
    public class CRM_xts_inventorytransactiondetailRepository : BaseDNetRepository<CRM_xts_inventorytransactiondetail>, ICRM_xts_inventorytransactiondetailRepository<CRM_xts_inventorytransactiondetail, int>
    {
        #region Constructor
        public CRM_xts_inventorytransactiondetailRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

		
        #region Create CRM_xts_inventorytransactiondetail
        public ResponseMessage Create(CRM_xts_inventorytransactiondetail entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                //SetCreatedLog(entity);
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(CRM_xts_inventorytransactiondetailQuery.InsertCRM_xts_inventorytransactiondetail, entity, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Inventorytransactiondetail {0} has been successfully created", entity.xts_inventorytransactiondetailid);
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create Inventorytransactiondetail. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		
		
        public bool BulkInsert(List<CRM_xts_inventorytransactiondetail> data)
        {	
            bool result = false;
            try
            {
                DataTable dataTableData = new DataTable("CRM_xts_inventorytransactiondetail");
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
                        insert.DestinationTableName = "CRM_xts_inventorytransactiondetail";

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
		

        #region Update CRM_xts_inventorytransactiondetail
        public ResponseMessage Update(CRM_xts_inventorytransactiondetail entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (entity != null)
                {
                    //SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(CRM_xts_inventorytransactiondetailQuery.UpdateCRM_xts_inventorytransactiondetail, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Inventorytransactiondetail{0} has been successfully updated", entity.xts_inventorytransactiondetailid);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Inventorytransactiondetail does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Inventorytransactiondetail. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		

        #region Delete CRM_xts_inventorytransactiondetail
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get CRM_xts_inventorytransactiondetail By Id
        public CRM_xts_inventorytransactiondetail Get(Guid xts_inventorytransactiondetailid)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<CRM_xts_inventorytransactiondetail>(
                        CRM_xts_inventorytransactiondetailQuery.GetCRM_xts_inventorytransactiondetailByID, new { xts_inventorytransactiondetailid = xts_inventorytransactiondetailid }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CRM_xts_inventorytransactiondetail Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All CRM_xts_inventorytransactiondetail
        public List<CRM_xts_inventorytransactiondetail> GetAll()
        {
            return null;
        }
        #endregion

        #region Search CRM_xts_inventorytransactiondetail
        public List<CRM_xts_inventorytransactiondetail> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<CRM_xts_inventorytransactiondetail>();
        }
        #endregion

		#region Search CRM_xts_inventorytransactiondetail        
        public new List<CRM_xts_inventorytransactiondetail> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<CRM_xts_inventorytransactiondetail> result = SearchFetchPaging<CRM_xts_inventorytransactiondetail>((connection, query, sqlParams) =>
                {
                    return connection.Query<CRM_xts_inventorytransactiondetail>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(CRM_xts_inventorytransactiondetailQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "CRM_xts_inventorytransactiondetail.xts_inventorytransactiondetailid", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(CRM_xts_inventorytransactiondetailQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "xts_inventorytransactiondetailid");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<CRM_xts_inventorytransactiondetail>();
            }
        }
        #endregion

        public void SetCreatedLog(CRM_xts_inventorytransactiondetail CRM_xts_inventorytransactiondetail)
        {
            CRM_xts_inventorytransactiondetail.createdbyname = UserLogin;
            CRM_xts_inventorytransactiondetail.createdon = DateTime.Now;
            CRM_xts_inventorytransactiondetail.RowStatus = "0";
            SetLastModifiedLog(CRM_xts_inventorytransactiondetail);
        }

        public void SetLastModifiedLog(CRM_xts_inventorytransactiondetail CRM_xts_inventorytransactiondetail)
        {
            CRM_xts_inventorytransactiondetail.modifiedbyname = UserLogin;
            CRM_xts_inventorytransactiondetail.modifiedon = DateTime.Now;
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

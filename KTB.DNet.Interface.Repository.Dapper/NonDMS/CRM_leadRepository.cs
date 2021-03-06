#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_lead class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Kemin
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 31 Aug 2020 14:17:25
 ===========================================================================
*/
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.NonDMS.SqlQuery.CRM_lead;
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
    public class CRM_leadRepository : BaseDNetRepository<CRM_Lead>, ICRM_leadRepository<CRM_Lead, int>
    {
        #region Constructor
        public CRM_leadRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

		
        #region Create CRM_lead
        public ResponseMessage Create(CRM_Lead entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                //SetCreatedLog(entity);
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(CRM_leadQuery.InsertCRM_lead, entity, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Suspect {0} has been successfully created", entity.leadid);
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create Suspect. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		
		
        public bool BulkInsert(List<CRM_Lead> data)
        {	
            bool result = false;
            try
            {
                DataTable dataTableData = new DataTable("CRM_lead");
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
                        insert.DestinationTableName = "CRM_lead";

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
		

        #region Update CRM_lead
        public ResponseMessage Update(CRM_Lead entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (entity != null)
                {
                    //SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(CRM_leadQuery.UpdateCRM_lead, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Suspect{0} has been successfully updated", entity.leadid);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Suspect does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Suspect. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		

        #region Delete CRM_lead
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get CRM_lead By Id
        public CRM_Lead Get(Guid leadid)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<CRM_Lead>(
                        CRM_leadQuery.GetCRM_leadByID, new { leadid = leadid }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CRM_Lead Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All CRM_lead
        public List<CRM_Lead> GetAll()
        {
            return null;
        }
        #endregion

        #region Search CRM_lead
        public List<CRM_Lead> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<CRM_Lead>();
        }
        #endregion

		#region Search CRM_lead        
        public new List<CRM_Lead> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<CRM_Lead> result = SearchFetchPaging<CRM_Lead>((connection, query, sqlParams) =>
                {
                    return connection.Query<CRM_Lead>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(CRM_leadQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "CRM_lead.leadid", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(CRM_leadQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "leadid");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<CRM_Lead>();
            }
        }
        #endregion

        public void SetCreatedLog(CRM_Lead CRM_lead)
        {
            CRM_lead.createdbyname = UserLogin;
            CRM_lead.createdon = DateTime.Now;
            CRM_lead.RowStatus = 0;
            SetLastModifiedLog(CRM_lead);
        }

        public void SetLastModifiedLog(CRM_Lead CRM_lead)
        {
            CRM_lead.modifiedbyname = UserLogin;
            CRM_lead.modifiedon = DateTime.Now;
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

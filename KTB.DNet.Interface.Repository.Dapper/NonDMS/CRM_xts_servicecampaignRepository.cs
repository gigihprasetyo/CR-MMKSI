#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_servicecampaign class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 08 Feb 2021 11:14:57
 ===========================================================================
*/
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.NonDMS.SqlQuery.CRM_xts_servicecampaign;
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
    public class CRM_xts_servicecampaignRepository : BaseDNetRepository<CRM_xts_servicecampaign>, ICRM_xts_servicecampaignRepository<CRM_xts_servicecampaign, int>
    {
        #region Constructor
        public CRM_xts_servicecampaignRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

		
        #region Create CRM_xts_servicecampaign
        public ResponseMessage Create(CRM_xts_servicecampaign entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                //SetCreatedLog(entity);
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(CRM_xts_servicecampaignQuery.InsertCRM_xts_servicecampaign, entity, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Servicecampaign {0} has been successfully created", entity.xts_servicecampaignid);
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create Servicecampaign. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		
		
        public bool BulkInsert(List<CRM_xts_servicecampaign> data)
        {	
            bool result = false;
            try
            {
                DataTable dataTableData = new DataTable("CRM_xts_servicecampaign");
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
                        insert.DestinationTableName = "CRM_xts_servicecampaign";

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
		

        #region Update CRM_xts_servicecampaign
        public ResponseMessage Update(CRM_xts_servicecampaign entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (entity != null)
                {
                    //SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(CRM_xts_servicecampaignQuery.UpdateCRM_xts_servicecampaign, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Servicecampaign{0} has been successfully updated", entity.xts_servicecampaignid);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Servicecampaign does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Servicecampaign. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		

        #region Delete CRM_xts_servicecampaign
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get CRM_xts_servicecampaign By Id
        public CRM_xts_servicecampaign Get(Guid xts_servicecampaignid)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<CRM_xts_servicecampaign>(
                        CRM_xts_servicecampaignQuery.GetCRM_xts_servicecampaignByID, new { xts_servicecampaignid = xts_servicecampaignid }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CRM_xts_servicecampaign Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All CRM_xts_servicecampaign
        public List<CRM_xts_servicecampaign> GetAll()
        {
            return null;
        }
        #endregion

        #region Search CRM_xts_servicecampaign
        public List<CRM_xts_servicecampaign> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<CRM_xts_servicecampaign>();
        }
        #endregion

		#region Search CRM_xts_servicecampaign        
        public new List<CRM_xts_servicecampaign> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<CRM_xts_servicecampaign> result = SearchFetchPaging<CRM_xts_servicecampaign>((connection, query, sqlParams) =>
                {
                    return connection.Query<CRM_xts_servicecampaign>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(CRM_xts_servicecampaignQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "CRM_xts_servicecampaign.xts_servicecampaignid", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(CRM_xts_servicecampaignQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "xts_servicecampaignid");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<CRM_xts_servicecampaign>();
            }
        }
        #endregion

        public void SetCreatedLog(CRM_xts_servicecampaign CRM_xts_servicecampaign)
        {
            CRM_xts_servicecampaign.createdbyname = UserLogin;
            CRM_xts_servicecampaign.createdon = DateTime.Now;
            CRM_xts_servicecampaign.RowStatus = "0";
            SetLastModifiedLog(CRM_xts_servicecampaign);
        }

        public void SetLastModifiedLog(CRM_xts_servicecampaign CRM_xts_servicecampaign)
        {
            CRM_xts_servicecampaign.modifiedbyname = UserLogin;
            CRM_xts_servicecampaign.modifiedon = DateTime.Now;
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

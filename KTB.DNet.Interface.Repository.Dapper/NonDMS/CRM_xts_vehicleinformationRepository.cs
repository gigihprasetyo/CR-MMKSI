#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_vehicleinformation class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Nando (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 03 Sep 2020 14:37:07
 ===========================================================================
*/
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.NonDMS.SqlQuery.CRM_xts_vehicleinformation;
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
    public class CRM_xts_vehicleinformationRepository : BaseDNetRepository<CRM_xts_vehicleinformation>, ICRM_xts_vehicleinformationRepository<CRM_xts_vehicleinformation, int>
    {
        #region Constructor
        public CRM_xts_vehicleinformationRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

		
        #region Create CRM_xts_vehicleinformation
        public ResponseMessage Create(CRM_xts_vehicleinformation entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                //SetCreatedLog(entity);
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(CRM_xts_vehicleinformationQuery.InsertCRM_xts_vehicleinformation, entity, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Vehicleinformation {0} has been successfully created", entity.xts_vehicleinformationid);
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create Vehicleinformation. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		
		
        public bool BulkInsert(List<CRM_xts_vehicleinformation> data)
        {	
            bool result = false;
            try
            {
                DataTable dataTableData = new DataTable("CRM_xts_vehicleinformation");
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
                        insert.DestinationTableName = "CRM_xts_vehicleinformation";

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
		

        #region Update CRM_xts_vehicleinformation
        public ResponseMessage Update(CRM_xts_vehicleinformation entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (entity != null)
                {
                    //SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(CRM_xts_vehicleinformationQuery.UpdateCRM_xts_vehicleinformation, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Vehicleinformation{0} has been successfully updated", entity.xts_vehicleinformationid);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Vehicleinformation does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Vehicleinformation. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		

        #region Delete CRM_xts_vehicleinformation
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get CRM_xts_vehicleinformation By Id
        public CRM_xts_vehicleinformation Get(Guid xts_vehicleinformationid)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<CRM_xts_vehicleinformation>(
                        CRM_xts_vehicleinformationQuery.GetCRM_xts_vehicleinformationByID, new { xts_vehicleinformationid = xts_vehicleinformationid }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CRM_xts_vehicleinformation Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All CRM_xts_vehicleinformation
        public List<CRM_xts_vehicleinformation> GetAll()
        {
            return null;
        }
        #endregion

        #region Search CRM_xts_vehicleinformation
        public List<CRM_xts_vehicleinformation> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<CRM_xts_vehicleinformation>();
        }
        #endregion

		#region Search CRM_xts_vehicleinformation        
        public new List<CRM_xts_vehicleinformation> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<CRM_xts_vehicleinformation> result = SearchFetchPaging<CRM_xts_vehicleinformation>((connection, query, sqlParams) =>
                {
                    return connection.Query<CRM_xts_vehicleinformation>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(CRM_xts_vehicleinformationQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "CRM_xts_vehicleinformation.xts_vehicleinformationid", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(CRM_xts_vehicleinformationQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "xts_vehicleinformationid");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<CRM_xts_vehicleinformation>();
            }
        }
        #endregion

        public void SetCreatedLog(CRM_xts_vehicleinformation CRM_xts_vehicleinformation)
        {
            CRM_xts_vehicleinformation.createdbyname = UserLogin;
            CRM_xts_vehicleinformation.createdon = DateTime.Now;
            CRM_xts_vehicleinformation.RowStatus = "0";
            SetLastModifiedLog(CRM_xts_vehicleinformation);
        }

        public void SetLastModifiedLog(CRM_xts_vehicleinformation CRM_xts_vehicleinformation)
        {
            CRM_xts_vehicleinformation.modifiedbyname = UserLogin;
            CRM_xts_vehicleinformation.modifiedon = DateTime.Now;
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

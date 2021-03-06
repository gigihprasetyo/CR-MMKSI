#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_newvehicleexteriorcolor class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Khalil (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 04 Sep 2020 14:23:04
 ===========================================================================
*/
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.NonDMS.SqlQuery.CRM_xts_newvehicleexteriorcolor;
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
    public class CRM_xts_newvehicleexteriorcolorRepository : BaseDNetRepository<CRM_xts_newvehicleexteriorcolor>, ICRM_xts_newvehicleexteriorcolorRepository<CRM_xts_newvehicleexteriorcolor, int>
    {
        #region Constructor
        public CRM_xts_newvehicleexteriorcolorRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

		
        #region Create CRM_xts_newvehicleexteriorcolor
        public ResponseMessage Create(CRM_xts_newvehicleexteriorcolor entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                //SetCreatedLog(entity);
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(CRM_xts_newvehicleexteriorcolorQuery.InsertCRM_xts_newvehicleexteriorcolor, entity, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Newvehicleexteriorcolor {0} has been successfully created", entity.xts_newvehicleexteriorcolorid);
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create Newvehicleexteriorcolor. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		
		
        public bool BulkInsert(List<CRM_xts_newvehicleexteriorcolor> data)
        {	
            bool result = false;
            try
            {
                DataTable dataTableData = new DataTable("CRM_xts_newvehicleexteriorcolor");
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
                        insert.DestinationTableName = "CRM_xts_newvehicleexteriorcolor";

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
		

        #region Update CRM_xts_newvehicleexteriorcolor
        public ResponseMessage Update(CRM_xts_newvehicleexteriorcolor entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (entity != null)
                {
                    //SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(CRM_xts_newvehicleexteriorcolorQuery.UpdateCRM_xts_newvehicleexteriorcolor, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Newvehicleexteriorcolor{0} has been successfully updated", entity.xts_newvehicleexteriorcolorid);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Newvehicleexteriorcolor does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Newvehicleexteriorcolor. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		

        #region Delete CRM_xts_newvehicleexteriorcolor
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get CRM_xts_newvehicleexteriorcolor By Id
        public CRM_xts_newvehicleexteriorcolor Get(Guid xts_newvehicleexteriorcolorid)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<CRM_xts_newvehicleexteriorcolor>(
                        CRM_xts_newvehicleexteriorcolorQuery.GetCRM_xts_newvehicleexteriorcolorByID, new { xts_newvehicleexteriorcolorid = xts_newvehicleexteriorcolorid }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CRM_xts_newvehicleexteriorcolor Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All CRM_xts_newvehicleexteriorcolor
        public List<CRM_xts_newvehicleexteriorcolor> GetAll()
        {
            return null;
        }
        #endregion

        #region Search CRM_xts_newvehicleexteriorcolor
        public List<CRM_xts_newvehicleexteriorcolor> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<CRM_xts_newvehicleexteriorcolor>();
        }
        #endregion

		#region Search CRM_xts_newvehicleexteriorcolor        
        public new List<CRM_xts_newvehicleexteriorcolor> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<CRM_xts_newvehicleexteriorcolor> result = SearchFetchPaging<CRM_xts_newvehicleexteriorcolor>((connection, query, sqlParams) =>
                {
                    return connection.Query<CRM_xts_newvehicleexteriorcolor>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(CRM_xts_newvehicleexteriorcolorQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "CRM_xts_newvehicleexteriorcolor.xts_newvehicleexteriorcolorid", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(CRM_xts_newvehicleexteriorcolorQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "xts_newvehicleexteriorcolorid");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<CRM_xts_newvehicleexteriorcolor>();
            }
        }
        #endregion

        public void SetCreatedLog(CRM_xts_newvehicleexteriorcolor CRM_xts_newvehicleexteriorcolor)
        {
            CRM_xts_newvehicleexteriorcolor.createdbyname = UserLogin;
            CRM_xts_newvehicleexteriorcolor.createdon = DateTime.Now;
            CRM_xts_newvehicleexteriorcolor.RowStatus = "0";
            SetLastModifiedLog(CRM_xts_newvehicleexteriorcolor);
        }

        public void SetLastModifiedLog(CRM_xts_newvehicleexteriorcolor CRM_xts_newvehicleexteriorcolor)
        {
            CRM_xts_newvehicleexteriorcolor.modifiedbyname = UserLogin;
            CRM_xts_newvehicleexteriorcolor.modifiedon = DateTime.Now;
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

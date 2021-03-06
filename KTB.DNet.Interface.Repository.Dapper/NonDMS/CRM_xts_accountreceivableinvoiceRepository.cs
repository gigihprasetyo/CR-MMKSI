#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_accountreceivableinvoice class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Gugun (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 03 Sep 2020 16:49:32
 ===========================================================================
*/
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.NonDMS.SqlQuery.CRM_xts_accountreceivableinvoice;
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
    public class CRM_xts_accountreceivableinvoiceRepository : BaseDNetRepository<CRM_xts_accountreceivableinvoice>, ICRM_xts_accountreceivableinvoiceRepository<CRM_xts_accountreceivableinvoice, int>
    {
        #region Constructor
        public CRM_xts_accountreceivableinvoiceRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

		
        #region Create CRM_xts_accountreceivableinvoice
        public ResponseMessage Create(CRM_xts_accountreceivableinvoice entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                //SetCreatedLog(entity);
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(CRM_xts_accountreceivableinvoiceQuery.InsertCRM_xts_accountreceivableinvoice, entity, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("ARInvoice {0} has been successfully created", entity.xts_accountreceivableinvoiceid);
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create ARInvoice. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		
		
        public bool BulkInsert(List<CRM_xts_accountreceivableinvoice> data)
        {	
            bool result = false;
            try
            {
                DataTable dataTableData = new DataTable("CRM_xts_accountreceivableinvoice");
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
                        insert.DestinationTableName = "CRM_xts_accountreceivableinvoice";

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
		

        #region Update CRM_xts_accountreceivableinvoice
        public ResponseMessage Update(CRM_xts_accountreceivableinvoice entity)
        {
			
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (entity != null)
                {
                    //SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(CRM_xts_accountreceivableinvoiceQuery.UpdateCRM_xts_accountreceivableinvoice, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("ARInvoice{0} has been successfully updated", entity.xts_accountreceivableinvoiceid);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "ARInvoice does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update ARInvoice. " + GetInnerException(ex).Message;
            }

            return responseMessage;
			
        }
        #endregion
		

        #region Delete CRM_xts_accountreceivableinvoice
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get CRM_xts_accountreceivableinvoice By Id
        public CRM_xts_accountreceivableinvoice Get(Guid xts_accountreceivableinvoiceid)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<CRM_xts_accountreceivableinvoice>(
                        CRM_xts_accountreceivableinvoiceQuery.GetCRM_xts_accountreceivableinvoiceByID, new { xts_accountreceivableinvoiceid = xts_accountreceivableinvoiceid }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CRM_xts_accountreceivableinvoice Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All CRM_xts_accountreceivableinvoice
        public List<CRM_xts_accountreceivableinvoice> GetAll()
        {
            return null;
        }
        #endregion

        #region Search CRM_xts_accountreceivableinvoice
        public List<CRM_xts_accountreceivableinvoice> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<CRM_xts_accountreceivableinvoice>();
        }
        #endregion

		#region Search CRM_xts_accountreceivableinvoice        
        public new List<CRM_xts_accountreceivableinvoice> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<CRM_xts_accountreceivableinvoice> result = SearchFetchPaging<CRM_xts_accountreceivableinvoice>((connection, query, sqlParams) =>
                {
                    return connection.Query<CRM_xts_accountreceivableinvoice>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(CRM_xts_accountreceivableinvoiceQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "CRM_xts_accountreceivableinvoice.xts_accountreceivableinvoiceid", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(CRM_xts_accountreceivableinvoiceQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "xts_accountreceivableinvoiceid");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<CRM_xts_accountreceivableinvoice>();
            }
        }
        #endregion

        public void SetCreatedLog(CRM_xts_accountreceivableinvoice CRM_xts_accountreceivableinvoice)
        {
            CRM_xts_accountreceivableinvoice.createdbyname = UserLogin;
            CRM_xts_accountreceivableinvoice.createdon = DateTime.Now;
            CRM_xts_accountreceivableinvoice.RowStatus = "0";
            SetLastModifiedLog(CRM_xts_accountreceivableinvoice);
        }

        public void SetLastModifiedLog(CRM_xts_accountreceivableinvoice CRM_xts_accountreceivableinvoice)
        {
            CRM_xts_accountreceivableinvoice.modifiedbyname = UserLogin;
            CRM_xts_accountreceivableinvoice.modifiedon = DateTime.Now;
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

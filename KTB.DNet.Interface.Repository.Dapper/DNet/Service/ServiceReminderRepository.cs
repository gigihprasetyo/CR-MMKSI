#region Summary
/*
 ===========================================================================
 AUTHOR        : Ivan
 PURPOSE       : Service Reminder Domain class
 SPECIAL NOTES : DNet WebApi Project
 ---------------------
 Copyright  (c) 2021 
 ---------------------
 $History      : $
 Created on 2021-03-23
 ===========================================================================
*/
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using KTB.DNet.Interface.Repository.Dapper.DNet.Service.SqlQuery.ServiceReminder;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class ServiceReminderRepository : BaseDNetRepository<ServiceReminder>, IServiceReminderRepository<ServiceReminder, int>
    {
        #region Constructor
        public ServiceReminderRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion


        #region Create ServiceReminder
        public ResponseMessage Create(ServiceReminder entity)
        {

            ResponseMessage responseMessage = new ResponseMessage() { Success = false };
            int ReturnId = 0;
            try
            {
                SetCreatedLog(entity);
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    object returnObj = connection.ExecuteScalar(ServiceReminderQuery.InsertServiceReminder, entity, transaction);
                    if (returnObj != null)
                    {
                        int.TryParse(returnObj.ToString(), out ReturnId);
                    }
                    return returnObj;
                });
                
                entity.ID = ReturnId;
                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("ServiceReminder {0} has been successfully created", entity.ID);
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create ServiceReminder. " + GetInnerException(ex).Message;
            }

            return responseMessage;

        }
        #endregion


        public bool BulkInsert(List<ServiceReminder> data)
        {
            bool result = false;
            try
            {
                DataTable dataTableData = new DataTable("ServiceReminder");
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
                        insert.DestinationTableName = "ServiceReminder";

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


        #region Update ServiceReminder
        public ResponseMessage Update(ServiceReminder entity)
        {

            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (entity != null)
                {
                    SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(ServiceReminderQuery.UpdateServiceReminder, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("ServiceReminder{0} has been successfully updated", entity.ID);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "ServiceReminder does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update ServiceReminder. " + GetInnerException(ex).Message;
            }

            return responseMessage;

        }
        #endregion


        #region Delete ServiceReminder
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get ServiceReminder By Id
        public ServiceReminder Get(Guid equipmentid)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<ServiceReminder>(
                        ServiceReminderQuery.GetServiceReminderByID, new { equipmentid = equipmentid }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ServiceReminder Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All ServiceReminder
        public List<ServiceReminder> GetAll()
        {
            return null;
        }
        #endregion

        #region Search ServiceReminder
        public List<ServiceReminder> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<ServiceReminder>();
        }
        #endregion

        #region Search ServiceReminder        
        public new List<ServiceReminder> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<ServiceReminder> result = SearchFetchPaging<ServiceReminder>((connection, query, sqlParams) =>
                {
                    return connection.Query<ServiceReminder>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(ServiceReminderQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "ServiceReminder.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(ServiceReminderQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "ID");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<ServiceReminder>();
            }
        }
        #endregion

        public void SetCreatedLog(ServiceReminder ServiceReminder)
        {
            //ServiceReminder.createdbyname = UserLogin;
            //ServiceReminder.createdon = DateTime.Now;
            //ServiceReminder.RowStatus = "0";
            SetLastModifiedLog(ServiceReminder);
        }

        public void SetLastModifiedLog(ServiceReminder ServiceReminder)
        {
            //ServiceReminder.modifiedbyname = UserLogin;
            //ServiceReminder.modifiedon = DateTime.Now;
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

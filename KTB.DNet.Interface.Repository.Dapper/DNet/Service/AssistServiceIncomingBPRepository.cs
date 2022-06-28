#region Summary
/*
 ===========================================================================
 AUTHOR        : Ivan
 PURPOSE       : AssistServiceIncomingBP Domain class
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
using KTB.DNet.Interface.Repository.Dapper.DNet.Service.SqlQuery.AssistServiceIncomingBP;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class AssistServiceIncomingBPRepository : BaseDNetRepository<AssistServiceIncomingBPIF>, IAssistServiceIncomingBPRepository<AssistServiceIncomingBPIF, int>
    {
        #region Constructor
        public AssistServiceIncomingBPRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion


        #region Create AssistServiceIncomingBP
        public ResponseMessage Create(AssistServiceIncomingBPIF entity)
        {

            ResponseMessage responseMessage = new ResponseMessage() { Success = false };
            int ReturnId = 0;
            try
            {
                SetCreatedLog(entity);
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    object returnObj = connection.ExecuteScalar(AssistServiceIncomingBPQuery.InsertAssistServiceIncomingBP, entity, transaction);
                    if (returnObj != null)
                    {
                        int.TryParse(returnObj.ToString(), out ReturnId);
                    }
                    return returnObj;
                });
                
                entity.ID = ReturnId;
                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("AssistServiceIncomingBP {0} has been successfully created", entity.ID);
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create AssistServiceIncomingBP. " + GetInnerException(ex).Message;
            }

            return responseMessage;

        }
        #endregion


        public bool BulkInsert(List<AssistServiceIncomingBPIF> data)
        {
            bool result = false;
            try
            {
                DataTable dataTableData = new DataTable("AssistServiceIncomingBP");
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
                        insert.DestinationTableName = "AssistServiceIncomingBP";

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


        #region Update AssistServiceIncomingBP
        public ResponseMessage Update(AssistServiceIncomingBPIF entity)
        {

            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (entity != null)
                {
                    SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(AssistServiceIncomingBPQuery.UpdateAssistServiceIncomingBP, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("AssistServiceIncomingBP{0} has been successfully updated", entity.ID);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "AssistServiceIncomingBP does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update AssistServiceIncomingBP. " + GetInnerException(ex).Message;
            }

            return responseMessage;

        }
        #endregion


        #region Delete AssistServiceIncomingBP
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get AssistServiceIncomingBP By Id
        public AssistServiceIncomingBPIF Get(Guid equipmentid)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<AssistServiceIncomingBPIF>(
                        AssistServiceIncomingBPQuery.GetAssistServiceIncomingBPByID, new { equipmentid = equipmentid }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public AssistServiceIncomingBPIF Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All AssistServiceIncomingBP
        public List<AssistServiceIncomingBPIF> GetAll()
        {
            return null;
        }
        #endregion

        #region Search AssistServiceIncomingBP
        public List<AssistServiceIncomingBPIF> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<AssistServiceIncomingBPIF>();
        }
        #endregion

        #region Search AssistServiceIncomingBP        
        public new List<AssistServiceIncomingBPIF> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<AssistServiceIncomingBPIF> result = SearchFetchPaging<AssistServiceIncomingBPIF>((connection, query, sqlParams) =>
                {
                    return connection.Query<AssistServiceIncomingBPIF>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(AssistServiceIncomingBPQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "AssistServiceIncomingBP.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(AssistServiceIncomingBPQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "ID");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<AssistServiceIncomingBPIF>();
            }
        }
        #endregion

        public void SetCreatedLog(AssistServiceIncomingBPIF AssistServiceIncomingBP)
        {
            //AssistServiceIncomingBP.createdbyname = UserLogin;
            //AssistServiceIncomingBP.createdon = DateTime.Now;
            //AssistServiceIncomingBP.RowStatus = "0";
            SetLastModifiedLog(AssistServiceIncomingBP);
        }

        public void SetLastModifiedLog(AssistServiceIncomingBPIF AssistServiceIncomingBP)
        {
            //AssistServiceIncomingBP.modifiedbyname = UserLogin;
            //AssistServiceIncomingBP.modifiedon = DateTime.Now;
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

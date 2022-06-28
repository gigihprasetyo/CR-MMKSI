#region Summary
/*
 ===========================================================================
 AUTHOR        : Ivan
 PURPOSE       : CustomerVehicle Domain class
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
using KTB.DNet.Domain;
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.CustomerVehicle;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class CustomerVehicleRepository : BaseDNetRepository<SFDCustomer>, ICustomerVehicleRepository<SFDCustomer, int>
    {
        #region Constructor
        public CustomerVehicleRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion
        


        public bool BulkInsert(List<SFDCustomer> data)
        {
            bool result = false;
            try
            {
                DataTable dataTableData = new DataTable("SFDCustomer");
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
                        insert.DestinationTableName = "SFDCustomer";

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


        #region Update CustomerVehicle
        public ResponseMessage Update(SFDCustomer entity)
        {

            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (entity != null)
                {
                    SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(CustomerVehicleQuery.UpdateCustomerVehicle, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("CustomerVehicle{0} has been successfully updated", entity.ID);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "CustomerVehicle does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update CustomerVehicle. " + GetInnerException(ex).Message;
            }

            return responseMessage;

        }
        #endregion
        

        public void SetCreatedLog(SFDCustomer CustomerVehicle)
        {
            //CustomerVehicle.createdbyname = UserLogin;
            //CustomerVehicle.createdon = DateTime.Now;
            //CustomerVehicle.RowStatus = "0";
            SetLastModifiedLog(CustomerVehicle);
        }

        public void SetLastModifiedLog(SFDCustomer CustomerVehicle)
        {
            //CustomerVehicle.modifiedbyname = UserLogin;
            //CustomerVehicle.modifiedon = DateTime.Now;
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

        public List<SFDCustomer> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }

        public SFDCustomer Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Create(SFDCustomer entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<SFDCustomer> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<SFDCustomer> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
    }
}



using Dapper;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.VehicleSales.SqlQuery.SAPCustomer;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class SAPCustomerLeadRepository : BaseDNetRepository<KTB.DNet.Interface.Domain.SAPCustomer>, ISAPCustomerLeadRepository<KTB.DNet.Interface.Domain.SAPCustomer, int>
    {
        #region Constructor
        public SAPCustomerLeadRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion



        public bool BulkInsert(List<KTB.DNet.Interface.Domain.SAPCustomer> data)
        {
            bool result = false;
            try
            {
                DataTable dataTableData = new DataTable("SAPCustomer");
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
                        insert.DestinationTableName = "SAPCustomer";

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
        public ResponseMessage Update(KTB.DNet.Interface.Domain.SAPCustomer entity)
        {

            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (entity != null)
                {
                    SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(SAPCustomerQuery.UpdateSAPCustomer, entity, transaction);
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

        public ResponseMessage UpdateQueue(KTB.DNet.Interface.Domain.SAPCustomer entity)
        {

            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (entity != null)
                {
                    SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(SAPCustomerQuery.UpdateQueue, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Queue {0} has been successfully updated", entity.ID);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Queue does not exist";
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


        public void SetCreatedLog(KTB.DNet.Interface.Domain.SAPCustomer CustomerVehicle)
        {
            //CustomerVehicle.createdbyname = UserLogin;
            //CustomerVehicle.createdon = DateTime.Now;
            //CustomerVehicle.RowStatus = "0";
            SetLastModifiedLog(CustomerVehicle);
        }

        public void SetLastModifiedLog(KTB.DNet.Interface.Domain.SAPCustomer CustomerVehicle)
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

        public List<KTB.DNet.Interface.Domain.SAPCustomer> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }

        public KTB.DNet.Interface.Domain.SAPCustomer Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Create(KTB.DNet.Interface.Domain.SAPCustomer entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<KTB.DNet.Interface.Domain.SAPCustomer> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<KTB.DNet.Interface.Domain.SAPCustomer> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
    }
}

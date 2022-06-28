using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class AssistPartSalesRepository : BaseDNetRepository<AssistPartSales>, IAssistPartSalesRepository<AssistPartSales, int>
    {
        public AssistPartSalesRepository(string connectionString)
            : base(connectionString)
        { }

        public async Task<bool> BulkInsertAsync(List<AssistPartSales> data)
        {

            try
            {
                DataTable dataTableData = new DataTable("AssistPartSales");
                dataTableData = data.ToDataTableForCreate(new List<string>() { "SalesmanHeaderID" });
                int batchSize = AppConfigs.GetInt(Constants.DBInsertBatchSizeConfigName);

                await ExecuteTransactionAsync(Connection, async (connection, transaction) =>
                {
                    using (SqlBulkCopy insert = new SqlBulkCopy((SqlConnection)connection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction))
                    {
                        if (batchSize > 0)
                        {
                            insert.BatchSize = batchSize;
                        }
                        insert.DestinationTableName = "AssistPartSales";

                        await insert.WriteToServerAsync(dataTableData);
                        return true;
                    }
                });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool BulkInsert(List<AssistPartSales> data)
        {
            bool result = false;
            try
            {
                DataTable dataTableData = new DataTable("AssistPartSales");
                dataTableData = data.ToDataTableForCreate(new List<string>() { "SalesmanHeaderID" });
                int batchSize = AppConfigs.GetInt(Constants.DBInsertBatchSizeConfigName);

                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    using (SqlBulkCopy insert = new SqlBulkCopy((SqlConnection)connection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction))
                    {
                        //insert.BulkCopyTimeout = ;
                        if (batchSize > 0)
                        {
                            insert.BatchSize = batchSize;
                        }
                        insert.DestinationTableName = "AssistPartSales";

                        insert.WriteToServer(dataTableData);

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

        #region Not Implemented
        public AssistPartSales Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Create(AssistPartSales entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(AssistPartSales entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<AssistPartSales> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<AssistPartSales> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

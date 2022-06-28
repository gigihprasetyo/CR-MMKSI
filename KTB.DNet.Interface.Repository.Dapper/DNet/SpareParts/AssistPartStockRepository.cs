using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.SpareParts.SqlQuery.AssistPartStock;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class AssistPartStockRepository : BaseDNetRepository<AssistPartStock>, IAssistPartStockRepository<AssistPartStock, int>
    {
        public AssistPartStockRepository(string connectionString)
            : base(connectionString)
        { }

        public async Task<bool> BulkInsertAsync(List<AssistPartStock> data)
        {

            try
            {
                DataTable dataTableData = new DataTable("AssistPartStock");
                dataTableData = data.ToDataTableForCreate();
                int batchSize = AppConfigs.GetInt(Constants.DBInsertBatchSizeConfigName);


                await ExecuteTransactionAsync(Connection, async (connection, transaction) =>
                {
                    using (SqlBulkCopy insert = new SqlBulkCopy((SqlConnection)connection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction))
                    {
                        if (batchSize > 0)
                        {
                            insert.BatchSize = batchSize;
                        }
                        insert.DestinationTableName = "AssistPartStock";

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

        public bool BulkInsert(List<AssistPartStock> data)
        {
            try
            {
                DataTable dataTableData = new DataTable("AssistPartStock");
                dataTableData = data.ToDataTableForCreate();
                int batchSize = AppConfigs.GetInt(Constants.DBInsertBatchSizeConfigName);

                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    using (SqlBulkCopy insert = new SqlBulkCopy((SqlConnection)connection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction))
                    {
                        if (batchSize > 0)
                        {
                            insert.BatchSize = batchSize;
                        }
                        insert.DestinationTableName = "AssistPartStock";

                        insert.WriteToServer(dataTableData);

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

        public async Task<List<AssistPartStock>> GetDuplicateDataAsync(string dealerCode, List<string> listOfBranchCode, List<string> listOfMonth, List<string> listOfYear, List<string> listOfPartNo)
        {
            List<AssistPartStock> result = new List<AssistPartStock>();
            try
            {
                int checkBranchCode = listOfBranchCode != null && listOfBranchCode.Count() > 0 ? 1 : 0;
                listOfBranchCode = checkBranchCode == 0 ? new List<string> { "-1" } : listOfBranchCode;

                using (var cn = Connection)
                {
                    var data = await cn.QueryAsync<AssistPartStock>(AssistPartStockQuery.SearchAssistPartStock,
                        new
                        {
                            DealerCode = dealerCode,
                            ListOfMonth = listOfMonth,
                            ListOfYear = listOfYear,
                            ListOfPartNo = listOfPartNo,
                            CheckBranchCode = checkBranchCode,
                            ListOfBranchCode = listOfBranchCode
                        });

                    return data != null ? data.ToList() : result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AssistPartStock> GetDuplicateData(string dealerCode, List<string> listOfBranchCode, List<string> listOfMonth, List<string> listOfYear, List<string> listOfPartNo)
        {
            List<AssistPartStock> result = new List<AssistPartStock>();
            try
            {
                int checkBranchCode = listOfBranchCode != null && listOfBranchCode.Count() > 0 ? 1 : 0;
                listOfBranchCode = checkBranchCode == 0 ? new List<string> { "-1" } : listOfBranchCode;

                using (var cn = Connection)
                {
                    var data = cn.Query<AssistPartStock>(AssistPartStockQuery.SearchAssistPartStock, new
                    {
                        DealerCode = dealerCode,
                        ListOfMonth = listOfMonth,
                        ListOfYear = listOfYear,
                        ListOfPartNo = listOfPartNo,
                        CheckBranchCode = checkBranchCode,
                        ListOfBranchCode = listOfBranchCode
                    });

                    return data != null ? data.ToList() : result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Not Implemented
        public AssistPartStock Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Create(AssistPartStock entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(AssistPartStock entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<AssistPartStock> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<AssistPartStock> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

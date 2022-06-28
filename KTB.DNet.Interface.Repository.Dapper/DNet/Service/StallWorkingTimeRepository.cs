
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
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class StallWorkingTimeRepository : BaseDNetRepository<StallWorkingTime_IF>, IStallWorkingTimeRepository<StallWorkingTime_IF, int>
    {
        #region Constructor
        public StallWorkingTimeRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create StallWorkingTime
        public ResponseMessage Create(StallWorkingTime_IF entity)
        {

            return null;

        }
        #endregion


        public bool BulkInsert(List<StallWorkingTime_IF> data)
        {
            bool result = false;
            try
            {
                DataTable dataTableData = new DataTable("StallWorkingTime");
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
                        insert.DestinationTableName = "StallWorkingTime";

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

        public bool BulkUpdate(List<StallWorkingTime_IF> data)
        {
            bool result = false;

            return result;
        }

        #region Update StallWorkingTime
        public ResponseMessage Update(StallWorkingTime_IF entity)
        {
            return null;
        }
        #endregion


        #region Delete StallWorkingTime
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get StallWorkingTime By Id
        public StallWorkingTime_IF Get(Guid accountid)
        {

            return null;
        }

        public StallWorkingTime_IF Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All StallWorkingTime
        public List<StallWorkingTime_IF> GetAll()
        {
            return null;
        }
        #endregion

        #region Search StallWorkingTime
        public List<StallWorkingTime_IF> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<StallWorkingTime_IF>();
        }
        #endregion

        public void SetCreatedLog(StallWorkingTime_IF StallWorkingTime)
        {
            //StallWorkingTime.createdbyname = UserLogin;
            //StallWorkingTime.createdon = DateTime.Now;
            //StallWorkingTime.RowStatus = "0";
            SetLastModifiedLog(StallWorkingTime);
        }

        public void SetLastModifiedLog(StallWorkingTime_IF StallWorkingTime)
        {
            //StallWorkingTime.modifiedbyname = UserLogin;
            //StallWorkingTime.modifiedon = DateTime.Now;
        }


    }
}

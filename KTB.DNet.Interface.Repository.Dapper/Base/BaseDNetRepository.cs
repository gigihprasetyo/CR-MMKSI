#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AbstractDataMapper.cs class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 28/11/2018 17:42
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
#endregion
namespace KTB.DNet.Interface.Repository.Dapper
{
    /// <summary>
    /// the abstract data mapper
    /// </summary>
    /// <typeparam name="T">
    /// the data domain entity
    /// </typeparam>
    public abstract class BaseDNetRepository<T> : BaseControl
    {
        protected string _connectionString { get; set; }
        private int? _timeout;

        public BaseDNetRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int? Timeout
        {
            get
            {
                if (_timeout != null && _timeout.HasValue)
                {
                    return _timeout;
                }

                _timeout = AppConfigs.GetInt(Constants.DBTimeoutConfigName);
                _timeout = _timeout.Value == 0 ? null : _timeout;

                return _timeout;
            }
        }
        protected IDbConnection Connection
        {
            get
            {
                IDbConnection connection = new SqlConnection(_connectionString);
                connection.Open();
                return connection;
            }
        }

        protected string UserLogin { get; set; }

        public void SetUserLogin(string username)
        {
            UserLogin = username;
        }

        public List<T> Search<T>(Func<IDbConnection, string, object, List<T>> executeQuery, IDbConnection connection, string searchQuery, string indexColumn, object sqlParams, string orderBy, out int total, int page = 1, int take = 10, string totalQuery = null, string rowNumberName = "NUMBER") where T : class
        {
            total = 0;
            page = page < 1 ? 1 : page;
            int start = (page - 1) * take;
            take = take < 1 ? 10 : take;

            string pagingIndexQuery = string.Empty;
            string pagingLimitQuery = string.Empty;

            // get table name
            string[] splitIndex = indexColumn.Split('.');
            string tblName = splitIndex[0];

            List<T> result = new List<T>();
            using (IDbConnection cn = connection)
            {

                // execute to get total
                totalQuery = string.IsNullOrEmpty(totalQuery) ? Regex.Replace(searchQuery, @"(\/\*\*PagingIndexQuery\*\*\/)(.|\r|\n|\t)*(\/\*\*EndPagingIndexQuery\*\*\/)", " COUNT(*) ") : totalQuery;
                total = cn.ExecuteScalar<int>(totalQuery, sqlParams, null, Timeout, null);

                var orderByStr = string.IsNullOrEmpty(orderBy) ? string.Empty : " ORDER BY " + orderBy.Replace(tblName,"o");

                pagingIndexQuery = string.Format(" * FROM ( SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 0)) AS {0}, ", rowNumberName);
                pagingLimitQuery = string.Format(" ) o WHERE o.{2} BETWEEN ({0} + 1) AND ({0} + {1}) ", start, take, rowNumberName);

                string query = searchQuery.Replace("/**PagingIndexQuery**/", pagingIndexQuery) + pagingLimitQuery + orderByStr;
                query = query.Replace("/**EndPagingIndexQuery**/", string.Empty);

                result = executeQuery(connection, query, sqlParams);
                return result;
            }
        }

        public List<T> SearchFilteredRow<T>(Func<IDbConnection, string, object, List<T>> executeQuery, IDbConnection connection, string searchQuery, string indexColumn, object sqlParams, string orderBy, out int total, int page = 1, int take = 10, string totalQuery = null, string rowNumberName = "NUMBER") where T : class
        {
            total = 0;
            page = page < 1 ? 1 : page;
            int start = (page - 1) * take;
            take = take < 1 ? 10 : take;

            string pagingIndexQuery = string.Empty;
            string pagingLimitQuery = string.Empty;

            // get table name
            string[] splitIndex = indexColumn.Split('.');
            string tblName = splitIndex[0];

            List<T> result = new List<T>();
            using (IDbConnection cn = connection)
            {

                // execute to get total
                //totalQuery = string.IsNullOrEmpty(totalQuery) ? Regex.Replace(searchQuery, @"(\/\*\*PagingIndexQuery\*\*\/)(.|\r|\n|\t)*(\/\*\*EndPagingIndexQuery\*\*\/)", " COUNT(*) ") : totalQuery;
                //total = cn.ExecuteScalar<int>(totalQuery, sqlParams, null, Timeout, null);

                var orderByStr = string.IsNullOrEmpty(orderBy) ? string.Empty : " ORDER BY " + orderBy.Replace(tblName,"o");

                pagingIndexQuery = string.Format(" * FROM ( SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 0)) AS {0}, ", rowNumberName);
                pagingLimitQuery = string.Format(" ) o WHERE o.{2} BETWEEN ({0} + 1) AND ({0} + {1}) ", start, take, rowNumberName);

                string query = searchQuery.Replace("/**PagingIndexQuery**/", pagingIndexQuery) + pagingLimitQuery + orderByStr;
                query = query.Replace("/**EndPagingIndexQuery**/", string.Empty);

                result = executeQuery(connection, query, sqlParams);
                total = result.Count;
                return result;
            }
        }

        public List<T> SearchFetchPaging<T>(Func<IDbConnection, string, object, List<T>> executeQuery, IDbConnection connection, string searchQuery, string indexColumn, object sqlParams, string orderBy, out int total, int page = 1, int take = 10, string totalQuery = null, string rowNumberName = "NUMBER") where T : class
        {
            total = 0;
            page = page < 1 ? 1 : page;
            int start = (page - 1) * take;
            take = take < 1 ? 10 : take;    

            string pagingIndexQuery = string.Empty;
            string pagingLimitQuery = string.Empty;

            // get table name
            string[] splitIndex = indexColumn.Split('.');
            string tblName = splitIndex[0];

            //select* from Dealer order by id
            // OFFSET @PageSize *(@PageNumber - 1) ROWS
            // FETCH NEXT @PageSize ROWS ONLY;

            List<T> result = new List<T>();
            using (IDbConnection cn = connection)
            {

                // execute to get total
                totalQuery = string.IsNullOrEmpty(totalQuery) ? Regex.Replace(searchQuery, @"(\/\*\*PagingIndexQuery\*\*\/)(.|\r|\n|\t)*(\/\*\*EndPagingIndexQuery\*\*\/)", " COUNT(*) ") : totalQuery;
                total = cn.ExecuteScalar<int>(totalQuery, sqlParams, null, Timeout, null);

                string newOrderBy = string.IsNullOrEmpty(orderBy) ? string.Empty : orderBy.Replace("o.", tblName + ".");
                var orderByStr = string.IsNullOrEmpty(newOrderBy) ? indexColumn : newOrderBy.Contains(indexColumn) ? newOrderBy : newOrderBy + ", " + indexColumn;

                pagingLimitQuery = string.Format(" ORDER BY {2} OFFSET {1} * ({0}-1) ROWS FETCH NEXT {1} ROWS ONLY", page, take,orderByStr);

                string query = searchQuery + pagingLimitQuery ;
                query = query.Replace("/**PagingIndexQuery**/", indexColumn + ", ");
                query = query.Replace("/**EndPagingIndexQuery**/", string.Empty);

                result = executeQuery(connection, query, sqlParams);
                return result;
            }
        }

        public List<T> SearchFetchPagingWithoutTotal<T>(Func<IDbConnection, string, object, List<T>> executeQuery, IDbConnection connection, string searchQuery, string indexColumn, object sqlParams, string orderBy, out int total, int page = 1, int take = 10, string totalQuery = null, string rowNumberName = "NUMBER") where T : class
        {
            total = 0;
            page = page < 1 ? 1 : page;
            int start = (page - 1) * take;
            take = take < 1 ? 10 : take;

            string pagingIndexQuery = string.Empty;
            string pagingLimitQuery = string.Empty;

            List<T> result = new List<T>();
            using (IDbConnection cn = connection)
            {

                // execute to get total
                //totalQuery = string.IsNullOrEmpty(totalQuery) ? Regex.Replace(searchQuery, @"(\/\*\*PagingIndexQuery\*\*\/)(.|\r|\n|\t)*(\/\*\*EndPagingIndexQuery\*\*\/)", " COUNT(*) ") : totalQuery;
                //total = cn.ExecuteScalar<int>(totalQuery, sqlParams, null, Timeout, null);

                var orderByStr = string.IsNullOrEmpty(orderBy) ? indexColumn : orderBy.Contains(indexColumn) ? orderBy : orderBy + ", " + indexColumn;

                pagingLimitQuery = string.Format(" ORDER BY {2} OFFSET {1} * ({0}-1) ROWS FETCH NEXT {1} ROWS ONLY", page, take, orderByStr);

                string query = searchQuery + pagingLimitQuery;
                query = query.Replace("/**PagingIndexQuery**/", indexColumn + ", ");
                query = query.Replace("/**EndPagingIndexQuery**/", string.Empty);

                result = executeQuery(connection, query, sqlParams);
                total = result.Count;
                return result;
            }
        }

        public List<T> SearchFetchPagingSP<T>(Func<IDbConnection, string, object, List<T>> executeQuery, IDbConnection connection, string searchQuery, object sqlParams, out int total, string totalQuery = null) where T : class
        {
            total = 0;


            List<T> result = new List<T>();
            using (IDbConnection cn = connection)
            {
                // execute to get total
                total = cn.ExecuteScalar<int>(totalQuery, sqlParams, null, Timeout, null);

                result = executeQuery(connection, searchQuery, sqlParams);
                return result;
            }
        }

        public object ExecuteTransaction(IDbConnection connection, Func<IDbConnection, IDbTransaction, object> executeQuery)
        {
            object result = null;
            using (IDbConnection cn = connection)
            {
                if (cn.State != ConnectionState.Open)
                {
                    cn.Open();
                }

                IDbTransaction transaction = cn.BeginTransaction();

                try
                {
                    result = executeQuery(cn, transaction);
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
                finally
                {
                    cn.Close();
                }
            }

            return result;
        }

        public async Task<object> ExecuteTransactionAsync(IDbConnection connection, Func<IDbConnection, IDbTransaction, Task<object>> executeQuery)
        {
            object result = null;
            using (IDbConnection cn = connection)
            {
                if (cn.State != ConnectionState.Open)
                {
                    cn.Open();
                }

                IDbTransaction transaction = cn.BeginTransaction();

                try
                {
                    result = await executeQuery(cn, transaction);
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
                finally
                {
                    cn.Close();
                }
            }

            return result;
        }

        public List<T> SearchData<T>(Func<IDbConnection, string, object, List<T>> executeQuery, IDbConnection connection, string searchQuery, string indexColumn, object sqlParams, string orderBy, out int total, int page = 1, int take = 10, string totalQuery = null, string rowNumberName = "NUMBER") where T : class
        {
            total = 0;
            page = page < 1 ? 1 : page;
            int start = (page - 1) * take;
            take = take < 1 ? 10 : take;

            string pagingIndexQuery = string.Empty;
            string pagingLimitQuery = string.Empty;

            // get table name
            string[] splitIndex = indexColumn.Split('.');
            string tblName = splitIndex[0];
            string fieldName = splitIndex[1];

            List<T> result = new List<T>();
            using (IDbConnection cn = connection)
            {
                // execute to get total
                totalQuery = string.IsNullOrEmpty(totalQuery) ? Regex.Replace(searchQuery, @"(\/\*\*PagingIndexQuery\*\*\/)(.|\r|\n|\t)*(\/\*\*EndPagingIndexQuery\*\*\/)", " COUNT(*) ") : totalQuery;
                total = cn.ExecuteScalar<int>(totalQuery, sqlParams, null, Timeout, null);

                string newOrderBy = string.IsNullOrEmpty(orderBy) ? string.Empty : orderBy.Replace("o.", tblName + ".");
                var orderByStr = string.IsNullOrEmpty(newOrderBy) ? indexColumn : newOrderBy.ToLower().Contains(fieldName.ToLower().Replace(" desc", "")) ? newOrderBy : newOrderBy + ", " + indexColumn;

                pagingLimitQuery = string.Format(" ORDER BY {2} OFFSET {1} * ({0}-1) ROWS FETCH NEXT {1} ROWS ONLY", page, take, orderByStr);

                string RowNumber = string.Format("ROW_NUMBER() OVER (ORDER BY {0} ) AS IDRow,", string.IsNullOrEmpty(orderBy) ? fieldName : orderBy).Replace("o.", "");

                string query = searchQuery + pagingLimitQuery;
                query = query.Replace("/**RowNumber**/", RowNumber);
                
                result = executeQuery(connection, query, sqlParams);
                return result;
            }
        }
    }
}

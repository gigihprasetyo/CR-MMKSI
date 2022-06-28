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
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

#endregion
namespace KTB.DNet.Interface.Repository.Dapper
{
    /// <summary>
    /// the abstract data mapper
    /// </summary>
    /// <typeparam name="T">
    /// the data domain entity
    /// </typeparam>
    public abstract class BaseRepository<T> : BaseControl
    {
        protected string _connectionString { get; set; }
        private int? _timeout;

        #region Constructor
        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        #endregion

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

        #region SetUserLogin
        /// <summary>
        /// SetUserLogin
        /// </summary>
        /// <param name="username"></param>
        public void SetUserLogin(string username)
        {
            UserLogin = username;
        }
        #endregion

        #region SetCreatedLog
        /// <summary>
        /// SetCreatedLog
        /// </summary>
        /// <param name="domain"></param>
        protected void SetCreatedLog(BaseDomain domain)
        {
            domain.CreatedBy = UserLogin;
            domain.CreatedTime = DateTime.Now;
            SetLastModifiedLog(domain);
        }
        #endregion

        #region SetLastModifiedLog
        /// <summary>
        /// SetLastModifiedLog
        /// </summary>
        /// <param name="domain"></param>
        protected void SetLastModifiedLog(BaseDomain domain)
        {
            domain.UpdatedBy = UserLogin;
            domain.UpdatedTime = DateTime.Now;
        }
        #endregion

        #region GetPostModelData
        /// <summary>
        /// GetPostModelData
        /// </summary>
        /// <param name="postModel"></param>
        /// <param name="defaultOrderedPropertyName"></param>
        /// <param name="keyword"></param>
        /// <param name="orderby"></param>
        protected void GetPostModelData(DataTablePostModel postModel, string defaultOrderedPropertyName, out string keyword, out List<string> orderby)
        {
            keyword = postModel.Search ?? string.Empty;
            keyword = keyword.ToUpper();


            string sortBy = defaultOrderedPropertyName;

            orderby = new List<string>();

            if (postModel.Order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = string.IsNullOrEmpty(postModel.Order.Column) ? sortBy : postModel.Order.Column + " " + postModel.Order.Dir.ToUpper();
            }

            orderby.Add(sortBy);
        }
        #endregion

        #region Search
        /// <summary>
        /// Search
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="executeQuery"></param>
        /// <param name="connection"></param>
        /// <param name="searchQuery"></param>
        /// <param name="indexColumn"></param>
        /// <param name="sqlParams"></param>
        /// <param name="orderBy"></param>
        /// <param name="total"></param>
        /// <param name="start"></param>
        /// <param name="take"></param>
        /// <param name="totalQuery"></param>
        /// <param name="rowNumberName"></param>
        /// <returns></returns>
        public List<T> Search<T>(Func<IDbConnection, string, object, List<T>> executeQuery, IDbConnection connection, string searchQuery, string indexColumn, object sqlParams, List<string> orderBy, out int total, int start = 1, int take = 10, string totalQuery = null, string rowNumberName = "NUMBER") where T : class
        {
            total = 0;
            start = start < 0 ? 0 : start;
            take = take < 1 ? 10 : take;

            string pagingIndexQuery = string.Empty;
            string pagingLimitQuery = string.Empty;

            List<T> result = new List<T>();
            using (IDbConnection cn = connection)
            {

                // execute to get total
                totalQuery = string.IsNullOrEmpty(totalQuery) ? Regex.Replace(searchQuery, @"(\/\*\*PagingIndexQuery\*\*\/)(.|\r|\n|\t)*(\/\*\*EndPagingIndexQuery\*\*\/)", " COUNT(*) ") : totalQuery;
                total = cn.ExecuteScalar<int>(totalQuery, sqlParams,null,Timeout);

                var orderByStr = orderBy != null && orderBy.Count > 0 ? string.Join(",", orderBy) : indexColumn;

                pagingIndexQuery = string.Format(" * FROM ( SELECT ROW_NUMBER() OVER (ORDER BY {0}) AS {1}, ", orderByStr, rowNumberName);
                pagingLimitQuery = string.Format(" ) o WHERE o.{2} BETWEEN ({0} + 1) AND ({0} + {1}) ", start, take, rowNumberName);

                string query = searchQuery.Replace("/**PagingIndexQuery**/", pagingIndexQuery) + pagingLimitQuery;
                query = query.Replace("/**EndPagingIndexQuery**/", string.Empty);

                result = executeQuery(connection, query, sqlParams);
                return result;
            }
        }
        #endregion

        #region ExecuteTransaction
        /// <summary>
        /// ExecuteTransaction
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="executeQuery"></param>
        /// <returns></returns>
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
        #endregion

    }
}

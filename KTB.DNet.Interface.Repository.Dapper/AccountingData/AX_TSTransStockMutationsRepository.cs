#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : AX_TSTransStockMutations repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 29/03/2022 9:17:19
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.AX_TSTransStockMutations;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class AX_TSTransStockMutationsRepository : BaseDNetRepository<AX_TSTransStockMutations>, IAX_TSTransStockMutationsRepository<AX_TSTransStockMutations, int>
    {
        #region Constructor
        public AX_TSTransStockMutationsRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create AX_TSTransStockMutations
        /// <summary>
        /// Create AX_TSTransStockMutations
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(AX_TSTransStockMutations entity)
        {
            return null;
        }
        #endregion

        #region Update AX_TSTransStockMutations
        /// <summary>
        /// Update AX_TSTransStockMutations
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(AX_TSTransStockMutations entity)
        {
            return null;
        }
        #endregion

        #region Delete AX_TSTransStockMutations
        /// <summary>
        /// Delete AX_TSTransStockMutations
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get AX_TSTransStockMutations By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AX_TSTransStockMutations Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<AX_TSTransStockMutations>(
                        AX_TSTransStockMutationsQuery.GetAX_TSTransStockMutationsById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All AX_TSTransStockMutations
        /// <summary>
        /// Get All AX_TSTransStockMutations
        /// </summary>
        /// <returns></returns>
        public List<AX_TSTransStockMutations> GetAll()
        {
            return null;
        }
        #endregion

        #region Search AX_TSTransStockMutations
        public List<AX_TSTransStockMutations> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<AX_TSTransStockMutations>();
        }
        #endregion

		#region Search AX_TSTransStockMutations        
        public new List<AX_TSTransStockMutations> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<AX_TSTransStockMutations> result = SearchFetchPaging<AX_TSTransStockMutations>((connection, query, sqlParams) =>
                {
                    return connection.Query<AX_TSTransStockMutations>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(AX_TSTransStockMutationsQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "AX_TSTransStockMutations.RecordId", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(AX_TSTransStockMutationsQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "RecordId");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<AX_TSTransStockMutations>();
            }
        }
        #endregion

        protected void SetCreatedLog(AX_TSTransStockMutations aX_TSTransStockMutations)
        {
            //aX_TSTransStockMutations.CreatedBy = UserLogin;
            //aX_TSTransStockMutations.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(aX_TSTransStockMutations);
        }

        protected void SetLastModifiedLog(AX_TSTransStockMutations aX_TSTransStockMutations)
        {
            //aX_TSTransStockMutations.LastUpdateBy = UserLogin;
            //aX_TSTransStockMutations.LastUpdateTime = DateTime.Now;
        }
    }
}
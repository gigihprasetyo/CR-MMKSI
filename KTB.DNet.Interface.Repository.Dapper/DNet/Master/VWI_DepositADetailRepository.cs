#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : VWI_DepositADetailRepository  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2021 
// ---------------------
// $History      : $
// Created on 13 Sep 2021
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.VWI_DepositADetail;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_DepositADetailRepository : BaseDNetRepository<VWI_DepositADetail_IF>, IVWI_DepositADetailRepository<VWI_DepositADetail_IF, int>
    {
        #region Constructor
        public VWI_DepositADetailRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_DepositADetail
        /// <summary>
        /// Create VWI_DepositADetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_DepositADetail_IF entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_DepositADetail
        /// <summary>
        /// Update VWI_DepositADetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_DepositADetail_IF entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_DepositADetail
        /// <summary>
        /// Delete VWI_DepositADetail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_DepositADetail By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_DepositADetail_IF Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All VWI_DepositADetail
        /// <summary>
        /// Get All VWI_DepositADetail
        /// </summary>
        /// <returns></returns>
        public List<VWI_DepositADetail_IF> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_DepositADetail
        public List<VWI_DepositADetail_IF> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_DepositADetail_IF>();
        }
        #endregion

        #region Search VWI_DepositADetail        
        public new List<VWI_DepositADetail_IF> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_DepositADetail_IF> result = Search<VWI_DepositADetail_IF>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_DepositADetail_IF>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_DepositADetailQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_DepositADetail.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_DepositADetailQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_DepositADetail_IF>();
            }
        }

        #endregion

        protected void SetCreatedLog(VWI_DepositADetail_IF VWI_DepositADetail)
        {
            //VWI_DepositADetail.CreatedBy = UserLogin;
            //VWI_DepositADetail.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(VWI_DepositADetail);
        }

        protected void SetLastModifiedLog(VWI_DepositADetail_IF VWI_DepositADetail)
        {
            //VWI_DepositADetail.LastUpdateBy = UserLogin;
            //VWI_DepositADetail.LastUpdateTime = DateTime.Now;
        }
    }
}

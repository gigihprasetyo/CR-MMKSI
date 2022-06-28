#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : VWI_DepositBDetailRepository  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2021 
// ---------------------
// $History      : $
// Created on 14 Sep 2021
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.VWI_DepositBDetail;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_DepositBDetailRepository : BaseDNetRepository<VWI_DepositBDetail_IF>, IVWI_DepositBDetailRepository<VWI_DepositBDetail_IF, int>
    {
        #region Constructor
        public VWI_DepositBDetailRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_DepositBDetail
        /// <summary>
        /// Create VWI_DepositBDetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_DepositBDetail_IF entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_DepositBDetail
        /// <summary>
        /// Update VWI_DepositBDetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_DepositBDetail_IF entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_DepositBDetail
        /// <summary>
        /// Delete VWI_DepositBDetail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_DepositBDetail By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_DepositBDetail_IF Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All VWI_DepositBDetail
        /// <summary>
        /// Get All VWI_DepositBDetail
        /// </summary>
        /// <returns></returns>
        public List<VWI_DepositBDetail_IF> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_DepositBDetail
        public List<VWI_DepositBDetail_IF> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_DepositBDetail_IF>();
        }
        #endregion

        #region Search VWI_DepositBDetail        
        public new List<VWI_DepositBDetail_IF> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_DepositBDetail_IF> result = Search<VWI_DepositBDetail_IF>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_DepositBDetail_IF>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_DepositBDetailQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_DepositBDetail.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_DepositBDetailQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_DepositBDetail_IF>();
            }
        }

        #endregion

        protected void SetCreatedLog(VWI_DepositBDetail_IF VWI_DepositBDetail)
        {
            //VWI_DepositBDetail.CreatedBy = UserLogin;
            //VWI_DepositBDetail.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(VWI_DepositBDetail);
        }

        protected void SetLastModifiedLog(VWI_DepositBDetail_IF VWI_DepositBDetail)
        {
            //VWI_DepositBDetail.LastUpdateBy = UserLogin;
            //VWI_DepositBDetail.LastUpdateTime = DateTime.Now;
        }
    }
}

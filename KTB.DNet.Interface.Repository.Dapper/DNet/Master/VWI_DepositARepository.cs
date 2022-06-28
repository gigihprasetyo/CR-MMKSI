#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : VWI_DepositARepository  class
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
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.VWI_DepositA;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_DepositARepository : BaseDNetRepository<VWI_DepositA_IF>, IVWI_DepositARepository<VWI_DepositA_IF, int>
    {
        #region Constructor
        public VWI_DepositARepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_DepositA
        /// <summary>
        /// Create VWI_DepositA
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_DepositA_IF entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_DepositA
        /// <summary>
        /// Update VWI_DepositA
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_DepositA_IF entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_DepositA
        /// <summary>
        /// Delete VWI_DepositA
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_DepositA By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_DepositA_IF Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All VWI_DepositA
        /// <summary>
        /// Get All VWI_DepositA
        /// </summary>
        /// <returns></returns>
        public List<VWI_DepositA_IF> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_DepositA
        public List<VWI_DepositA_IF> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_DepositA_IF>();
        }
        #endregion

        #region Search VWI_DepositA        
        public new List<VWI_DepositA_IF> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_DepositA_IF> result = Search<VWI_DepositA_IF>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_DepositA_IF>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_DepositAQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_DepositA.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_DepositAQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_DepositA_IF>();
            }
        }

        #endregion

        protected void SetCreatedLog(VWI_DepositA_IF VWI_DepositA)
        {
            //VWI_DepositA.CreatedBy = UserLogin;
            //VWI_DepositA.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(VWI_DepositA);
        }

        protected void SetLastModifiedLog(VWI_DepositA_IF VWI_DepositA)
        {
            //VWI_DepositA.LastUpdateBy = UserLogin;
            //VWI_DepositA.LastUpdateTime = DateTime.Now;
        }
    }
}

#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : VWI_DepositRepository  class
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
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.VWI_Deposit;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_DepositRepository : BaseDNetRepository<VWI_Deposit_IF>, IVWI_DepositRepository<VWI_Deposit_IF, int>
    {
        #region Constructor
        public VWI_DepositRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_Deposit
        /// <summary>
        /// Create VWI_Deposit
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_Deposit_IF entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_Deposit
        /// <summary>
        /// Update VWI_Deposit
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_Deposit_IF entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_Deposit
        /// <summary>
        /// Delete VWI_Deposit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_Deposit By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_Deposit_IF Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All VWI_Deposit
        /// <summary>
        /// Get All VWI_Deposit
        /// </summary>
        /// <returns></returns>
        public List<VWI_Deposit_IF> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_Deposit
        public List<VWI_Deposit_IF> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_Deposit_IF>();
        }
        #endregion

        #region Search VWI_Deposit        
        public new List<VWI_Deposit_IF> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_Deposit_IF> result = Search<VWI_Deposit_IF>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_Deposit_IF>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_DepositQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_Deposit.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_DepositQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_Deposit_IF>();
            }
        }

        #endregion

        protected void SetCreatedLog(VWI_Deposit_IF VWI_Deposit)
        {
            //VWI_Deposit.CreatedBy = UserLogin;
            //VWI_Deposit.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(VWI_Deposit);
        }

        protected void SetLastModifiedLog(VWI_Deposit_IF VWI_Deposit)
        {
            //VWI_Deposit.LastUpdateBy = UserLogin;
            //VWI_Deposit.LastUpdateTime = DateTime.Now;
        }
    }
}

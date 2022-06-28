#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : VWI_DepositBHeaderRepository  class
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
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.VWI_DepositBHeader;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_DepositBHeaderRepository : BaseDNetRepository<VWI_DepositBHeader_IF>, IVWI_DepositBHeaderRepository<VWI_DepositBHeader_IF, int>
    {
        #region Constructor
        public VWI_DepositBHeaderRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_DepositBHeader
        /// <summary>
        /// Create VWI_DepositBHeader
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_DepositBHeader_IF entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_DepositBHeader
        /// <summary>
        /// Update VWI_DepositBHeader
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_DepositBHeader_IF entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_DepositBHeader
        /// <summary>
        /// Delete VWI_DepositBHeader
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_DepositBHeader By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_DepositBHeader_IF Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All VWI_DepositBHeader
        /// <summary>
        /// Get All VWI_DepositBHeader
        /// </summary>
        /// <returns></returns>
        public List<VWI_DepositBHeader_IF> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_DepositBHeader
        public List<VWI_DepositBHeader_IF> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_DepositBHeader_IF>();
        }
        #endregion

        #region Search VWI_DepositBHeader        
        public new List<VWI_DepositBHeader_IF> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_DepositBHeader_IF> result = Search<VWI_DepositBHeader_IF>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_DepositBHeader_IF>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_DepositBHeaderQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_DepositBHeader.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_DepositBHeaderQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_DepositBHeader_IF>();
            }
        }

        #endregion

        protected void SetCreatedLog(VWI_DepositBHeader_IF VWI_DepositBHeader)
        {
            //VWI_DepositBHeader.CreatedBy = UserLogin;
            //VWI_DepositBHeader.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(VWI_DepositBHeader);
        }

        protected void SetLastModifiedLog(VWI_DepositBHeader_IF VWI_DepositBHeader)
        {
            //VWI_DepositBHeader.LastUpdateBy = UserLogin;
            //VWI_DepositBHeader.LastUpdateTime = DateTime.Now;
        }
    }
}

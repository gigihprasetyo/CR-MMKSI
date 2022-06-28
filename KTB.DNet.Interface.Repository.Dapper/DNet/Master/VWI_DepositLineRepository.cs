#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : VWI_DepositLineRepository  class
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
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.VWI_DepositLine;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_DepositLineRepository : BaseDNetRepository<VWI_DepositLine_IF>, IVWI_DepositLineRepository<VWI_DepositLine_IF, int>
    {
        #region Constructor
        public VWI_DepositLineRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_DepositLine
        /// <summary>
        /// Create VWI_DepositLine
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_DepositLine_IF entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_DepositLine
        /// <summary>
        /// Update VWI_DepositLine
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_DepositLine_IF entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_DepositLine
        /// <summary>
        /// Delete VWI_DepositLine
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_DepositLine By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_DepositLine_IF Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All VWI_DepositLine
        /// <summary>
        /// Get All VWI_DepositLine
        /// </summary>
        /// <returns></returns>
        public List<VWI_DepositLine_IF> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_DepositLine
        public List<VWI_DepositLine_IF> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_DepositLine_IF>();
        }
        #endregion

        #region Search VWI_DepositLine        
        public new List<VWI_DepositLine_IF> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_DepositLine_IF> result = Search<VWI_DepositLine_IF>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_DepositLine_IF>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_DepositLineQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_DepositLine.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_DepositLineQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_DepositLine_IF>();
            }
        }

        #endregion

        protected void SetCreatedLog(VWI_DepositLine_IF VWI_DepositLine)
        {
            //VWI_DepositLine.CreatedBy = UserLogin;
            //VWI_DepositLine.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(VWI_DepositLine);
        }

        protected void SetLastModifiedLog(VWI_DepositLine_IF VWI_DepositLine)
        {
            //VWI_DepositLine.LastUpdateBy = UserLogin;
            //VWI_DepositLine.LastUpdateTime = DateTime.Now;
        }
    }
}

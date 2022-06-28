#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : VWI_DepositC2LineRepository  class
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
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.VWI_DepositC2Line;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_DepositC2LineRepository : BaseDNetRepository<VWI_DepositC2Line_IF>, IVWI_DepositC2LineRepository<VWI_DepositC2Line_IF, int>
    {
        #region Constructor
        public VWI_DepositC2LineRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_DepositC2Line
        /// <summary>
        /// Create VWI_DepositC2Line
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_DepositC2Line_IF entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_DepositC2Line
        /// <summary>
        /// Update VWI_DepositC2Line
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_DepositC2Line_IF entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_DepositC2Line
        /// <summary>
        /// Delete VWI_DepositC2Line
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_DepositC2Line By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_DepositC2Line_IF Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All VWI_DepositC2Line
        /// <summary>
        /// Get All VWI_DepositC2Line
        /// </summary>
        /// <returns></returns>
        public List<VWI_DepositC2Line_IF> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_DepositC2Line
        public List<VWI_DepositC2Line_IF> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_DepositC2Line_IF>();
        }
        #endregion

        #region Search VWI_DepositC2Line        
        public new List<VWI_DepositC2Line_IF> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_DepositC2Line_IF> result = Search<VWI_DepositC2Line_IF>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_DepositC2Line_IF>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_DepositC2LineQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_DepositC2Line.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_DepositC2LineQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_DepositC2Line_IF>();
            }
        }

        #endregion

        protected void SetCreatedLog(VWI_DepositC2Line_IF VWI_DepositC2Line)
        {
            //VWI_DepositC2Line.CreatedBy = UserLogin;
            //VWI_DepositC2Line.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(VWI_DepositC2Line);
        }

        protected void SetLastModifiedLog(VWI_DepositC2Line_IF VWI_DepositC2Line)
        {
            //VWI_DepositC2Line.LastUpdateBy = UserLogin;
            //VWI_DepositC2Line.LastUpdateTime = DateTime.Now;
        }
    }
}

#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : VWI_DepositC2Repository  class
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
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.VWI_DepositC2;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_DepositC2Repository : BaseDNetRepository<VWI_DepositC2_IF>, IVWI_DepositC2Repository<VWI_DepositC2_IF, int>
    {
        #region Constructor
        public VWI_DepositC2Repository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_DepositC2
        /// <summary>
        /// Create VWI_DepositC2
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_DepositC2_IF entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_DepositC2
        /// <summary>
        /// Update VWI_DepositC2
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_DepositC2_IF entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_DepositC2
        /// <summary>
        /// Delete VWI_DepositC2
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_DepositC2 By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_DepositC2_IF Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All VWI_DepositC2
        /// <summary>
        /// Get All VWI_DepositC2
        /// </summary>
        /// <returns></returns>
        public List<VWI_DepositC2_IF> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_DepositC2
        public List<VWI_DepositC2_IF> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_DepositC2_IF>();
        }
        #endregion

        #region Search VWI_DepositC2        
        public new List<VWI_DepositC2_IF> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_DepositC2_IF> result = Search<VWI_DepositC2_IF>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_DepositC2_IF>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_DepositC2Query.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_DepositC2.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_DepositC2Query.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_DepositC2_IF>();
            }
        }

        #endregion

        protected void SetCreatedLog(VWI_DepositC2_IF VWI_DepositC2)
        {
            //VWI_DepositC2.CreatedBy = UserLogin;
            //VWI_DepositC2.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(VWI_DepositC2);
        }

        protected void SetLastModifiedLog(VWI_DepositC2_IF VWI_DepositC2)
        {
            //VWI_DepositC2.LastUpdateBy = UserLogin;
            //VWI_DepositC2.LastUpdateTime = DateTime.Now;
        }
    }
}

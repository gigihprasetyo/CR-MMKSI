#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_ChassisMasterPKT repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-11 15:51:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.VehicleSales.SqlQuery.VWI_ChassisMasterPKT;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_ChassisMasterPKTRepository : BaseDNetRepository<VWI_ChassisMasterPKT>, IVWI_ChassisMasterPKTRepository<VWI_ChassisMasterPKT, int>
    {
        #region Constructor
        public VWI_ChassisMasterPKTRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_ChassisMasterPKT
        /// <summary>
        /// Create VWI_ChassisMasterPKT
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_ChassisMasterPKT entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_ChassisMasterPKT
        /// <summary>
        /// Update VWI_ChassisMasterPKT
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_ChassisMasterPKT entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_ChassisMasterPKT
        /// <summary>
        /// Delete VWI_ChassisMasterPKT
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_ChassisMasterPKT By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_ChassisMasterPKT Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_ChassisMasterPKT>(
                        VWI_ChassisMasterPKTQuery.GetVWI_ChassisMasterPKTById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_ChassisMasterPKT
        /// <summary>
        /// Get All VWI_ChassisMasterPKT
        /// </summary>
        /// <returns></returns>
        public List<VWI_ChassisMasterPKT> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_ChassisMasterPKT
        public List<VWI_ChassisMasterPKT> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_ChassisMasterPKT>();
        }
        #endregion

        #region Search VWI_ChassisMasterPKT        
        public new List<VWI_ChassisMasterPKT> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_ChassisMasterPKT> result = Search<VWI_ChassisMasterPKT>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_ChassisMasterPKT>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_ChassisMasterPKTQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_ChassisMasterPKT.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_ChassisMasterPKTQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_ChassisMasterPKT>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_ChassisMasterPKT VWI_ChassisMasterPKT)
        {
            //VWI_ChassisMasterPKT.CreatedBy = UserLogin;
            //VWI_ChassisMasterPKT.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(VWI_ChassisMasterPKT);
        }

        protected void SetLastModifiedLog(VWI_ChassisMasterPKT VWI_ChassisMasterPKT)
        {
            //VWI_ChassisMasterPKT.LastUpdateBy = UserLogin;
            //VWI_ChassisMasterPKT.LastUpdateTime = DateTime.Now;
        }
    }
}
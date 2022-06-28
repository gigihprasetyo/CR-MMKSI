#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_SparepartSalesReturn repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 03/02/2020 15:09:45
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_PRT_SparepartSalesReturn;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_PRT_SparepartSalesReturnRepository : BaseDNetRepository<VWI_CRM_PRT_SparepartSalesReturn>, IVWI_CRM_PRT_SparepartSalesReturnRepository<VWI_CRM_PRT_SparepartSalesReturn, int>
    {
        #region Constructor
        public VWI_CRM_PRT_SparepartSalesReturnRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_PRT_SparepartSalesReturn
        /// <summary>
        /// Create VWI_CRM_PRT_SparepartSalesReturn
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_PRT_SparepartSalesReturn entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_PRT_SparepartSalesReturn
        /// <summary>
        /// Update VWI_CRM_PRT_SparepartSalesReturn
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_PRT_SparepartSalesReturn entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_PRT_SparepartSalesReturn
        /// <summary>
        /// Delete VWI_CRM_PRT_SparepartSalesReturn
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_PRT_SparepartSalesReturn By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_PRT_SparepartSalesReturn Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_PRT_SparepartSalesReturn>(
                        VWI_CRM_PRT_SparepartSalesReturnQuery.GetVWI_CRM_PRT_SparepartSalesReturnById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_PRT_SparepartSalesReturn
        /// <summary>
        /// Get All VWI_CRM_PRT_SparepartSalesReturn
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_PRT_SparepartSalesReturn> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_PRT_SparepartSalesReturn
        public List<VWI_CRM_PRT_SparepartSalesReturn> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_PRT_SparepartSalesReturn>();
        }
        #endregion

		#region Search VWI_CRM_PRT_SparepartSalesReturn        
        public new List<VWI_CRM_PRT_SparepartSalesReturn> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_PRT_SparepartSalesReturn> result = Search<VWI_CRM_PRT_SparepartSalesReturn>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_PRT_SparepartSalesReturn>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_PRT_SparepartSalesReturnQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_PRT_SparepartSalesReturn.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_PRT_SparepartSalesReturnQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_PRT_SparepartSalesReturn>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_PRT_SparepartSalesReturn vWI_CRM_PRT_SparepartSalesReturn)
        {
            //vWI_CRM_PRT_SparepartSalesReturn.CreatedBy = UserLogin;
            //vWI_CRM_PRT_SparepartSalesReturn.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_PRT_SparepartSalesReturn);
        }

        protected void SetLastModifiedLog(VWI_CRM_PRT_SparepartSalesReturn vWI_CRM_PRT_SparepartSalesReturn)
        {
            //vWI_CRM_PRT_SparepartSalesReturn.LastUpdateBy = UserLogin;
            //vWI_CRM_PRT_SparepartSalesReturn.LastUpdateTime = DateTime.Now;
        }
    }
}
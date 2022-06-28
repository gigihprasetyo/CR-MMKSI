#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_grade repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/24/2020 15:38:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_grade;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_gradeRepository : BaseDNetRepository<VWI_CRM_xts_grade>, IVWI_CRM_xts_gradeRepository<VWI_CRM_xts_grade, int>
    {
        #region Constructor
        public VWI_CRM_xts_gradeRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_grade
        /// <summary>
        /// Create VWI_CRM_xts_grade
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_grade entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_grade
        /// <summary>
        /// Update VWI_CRM_xts_grade
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_grade entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_grade
        /// <summary>
        /// Delete VWI_CRM_xts_grade
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_grade By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_grade Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_grade>(
                        VWI_CRM_xts_gradeQuery.GetVWI_CRM_xts_gradeById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_grade
        /// <summary>
        /// Get All VWI_CRM_xts_grade
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_grade> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_grade
        public List<VWI_CRM_xts_grade> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_grade>();
        }
        #endregion

        #region Search VWI_CRM_xts_grade        
        public new List<VWI_CRM_xts_grade> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_grade.rowstatus", "isnull(vwi_crm_xts_grade.rowstatus, 0)");

                List<VWI_CRM_xts_grade> result = SearchData<VWI_CRM_xts_grade>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_grade>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_gradeQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_grade.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_gradeQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_grade>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_grade vWI_CRM_xts_grade)
        {
            //vWI_CRM_xts_grade.CreatedBy = UserLogin;
            //vWI_CRM_xts_grade.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_grade);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_grade vWI_CRM_xts_grade)
        {
            //vWI_CRM_xts_grade.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_grade.LastUpdateTime = DateTime.Now;
        }
    }
}
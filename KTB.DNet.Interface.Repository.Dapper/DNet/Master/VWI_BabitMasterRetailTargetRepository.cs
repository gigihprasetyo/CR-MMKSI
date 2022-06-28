
#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : VWI_BabitMasterRetailTargetRepository  class
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
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.VWI_BabitMasterRetailTarget;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_BabitMasterRetailTargetRepository : BaseDNetRepository<VWI_BabitMasterRetailTarget>, IVWI_BabitMasterRetailTargetRepository<VWI_BabitMasterRetailTarget, int>
    {
        #region Constructor
        public VWI_BabitMasterRetailTargetRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_BabitMasterRetailTarget
        /// <summary>
        /// Create VWI_BabitMasterRetailTarget
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_BabitMasterRetailTarget entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_BabitMasterRetailTarget
        /// <summary>
        /// Update VWI_BabitMasterRetailTarget
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_BabitMasterRetailTarget entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_BabitMasterRetailTarget
        /// <summary>
        /// Delete VWI_BabitMasterRetailTarget
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_BabitMasterRetailTarget By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_BabitMasterRetailTarget Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_BabitMasterRetailTarget>(
                        VWI_BabitMasterRetailTargetQuery.GetVWI_BabitMasterRetailTargetById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_BabitMasterRetailTarget
        /// <summary>
        /// Get All VWI_BabitMasterRetailTarget
        /// </summary>
        /// <returns></returns>
        public List<VWI_BabitMasterRetailTarget> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_BabitMasterRetailTarget
        public List<VWI_BabitMasterRetailTarget> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_BabitMasterRetailTarget>();
        }
        #endregion

        #region Search VWI_BabitMasterRetailTarget     
        public List<VWI_BabitMasterRetailTarget> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }

        public List<VWI_BabitMasterRetailTarget> Search(string criterias, string innerQueryCriteria, System.Collections.ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                criterias = criterias.ToLower().Replace("vwi_babitmasterretailtarget.", "a.");
                criterias = criterias.ToLower().Replace("a.dealercode", "isnull(d.DealerCode, '')");
                criterias = criterias.ToLower().Replace("a.dealerbranchcode", "isnull(b.DealerBranchCode, '')");

                List<VWI_BabitMasterRetailTarget> result = SearchFetchPaging<VWI_BabitMasterRetailTarget>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_BabitMasterRetailTarget>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_BabitMasterRetailTargetQuery.SelectQuery, criterias == null ? string.Empty : criterias.ToString())
                , "VWI_BabitMasterRetailTarget.ID", null, orderBy, out filteredResultsCount, page, pageSize, 
                string.Format(VWI_BabitMasterRetailTargetQuery.GetTotalQuery, criterias == null ? string.Empty : criterias.ToString()));


                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_BabitMasterRetailTarget>();
            }
        }

        #endregion

        protected void SetCreatedLog(VWI_BabitMasterRetailTarget VWI_BabitMasterRetailTarget)
        {
            //VWI_BabitMasterRetailTarget.CreatedBy = UserLogin;
            //VWI_BabitMasterRetailTarget.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(VWI_BabitMasterRetailTarget);
        }

        protected void SetLastModifiedLog(VWI_BabitMasterRetailTarget VWI_BabitMasterRetailTarget)
        {
            //VWI_BabitMasterRetailTarget.LastUpdateBy = UserLogin;
            //VWI_BabitMasterRetailTarget.LastUpdateTime = DateTime.Now;
        }

        
    }
}


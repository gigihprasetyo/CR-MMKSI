#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_Zombie_CampaignRepository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-10-13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_Zombie_Campaign;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_Zombie_CampaignRepository : BaseDNetRepository<VWI_Zombie_Campaign>, IVWI_Zombie_CampaignRepository<VWI_Zombie_Campaign, int>
    {
        #region Constructor
        public VWI_Zombie_CampaignRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_Zombie_Campaign
        /// <summary>
        /// Create VWI_Zombie_Campaign
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_Zombie_Campaign entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_Zombie_Campaign
        /// <summary>
        /// Update VWI_Zombie_Campaign
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_Zombie_Campaign entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_Zombie_Campaign
        /// <summary>
        /// Delete VWI_Zombie_Campaign
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_Zombie_Campaign By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_Zombie_Campaign Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_Zombie_Campaign>(
                        VWI_Zombie_CampaignQuery.GetVWI_Zombie_CampaignById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_Zombie_Campaign
        /// <summary>
        /// Get All VWI_Zombie_Campaign
        /// </summary>
        /// <returns></returns>
        public List<VWI_Zombie_Campaign> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_Zombie_Campaign
        public List<VWI_Zombie_Campaign> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_Zombie_Campaign>();
        }
        #endregion

        #region Search VWI_Zombie_Campaign        
        public new List<VWI_Zombie_Campaign> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_zombie_campaign.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");
                strCriteria = strCriteria.ToLower().Replace("a.dealercode", "a.dealer_code");
                strCriteria = strCriteria.ToLower().Replace("a.dealercompany", "a.dealer_company");
                strCriteria = strCriteria.ToLower().Replace("a.category_zombiedatacode", "c.valueid");

                List<VWI_Zombie_Campaign> result = SearchFetchPaging<VWI_Zombie_Campaign>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_Zombie_Campaign>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_Zombie_CampaignQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_Zombie_Campaign.LastCheckedTime desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_Zombie_CampaignQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_Zombie_Campaign>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_Zombie_Campaign VWI_Zombie_Campaign)
        {
            //VWI_Zombie_Campaign.CreatedBy = UserLogin;
            //VWI_Zombie_Campaign.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(VWI_Zombie_Campaign);
        }

        protected void SetLastModifiedLog(VWI_Zombie_Campaign VWI_Zombie_Campaign)
        {
            //VWI_Zombie_Campaign.LastUpdateBy = UserLogin;
            //VWI_Zombie_Campaign.LastUpdateTime = DateTime.Now;
        }
    }
}


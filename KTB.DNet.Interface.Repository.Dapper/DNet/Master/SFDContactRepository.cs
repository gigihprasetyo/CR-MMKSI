
#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : SFDContactRepository  class
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
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.SFDContact;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class SFDContactRepository : BaseDNetRepository<SFDContact>, ISFDContactRepository<SFDContact, int>
    {
        #region Constructor
        public SFDContactRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create SFDContact
        /// <summary>
        /// Create SFDContact
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(SFDContact entity)
        {
            return null;
        }
        #endregion

        #region Update SFDContact
        /// <summary>
        /// Update SFDContact
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(SFDContact entity)
        {
            return null;
        }
        #endregion

        #region Delete SFDContact
        /// <summary>
        /// Delete SFDContact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get SFDContact By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SFDContact Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<SFDContact>(
                        SFDContactQuery.GetSFDContactById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All SFDContact
        /// <summary>
        /// Get All SFDContact
        /// </summary>
        /// <returns></returns>
        public List<SFDContact> GetAll()
        {
            return null;
        }
        #endregion

        #region Search SFDContact
        public List<SFDContact> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<SFDContact>();
        }
        #endregion

        #region Search SFDContact     
        public List<SFDContact> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }

        public List<SFDContact> Search(string criterias, string innerQueryCriteria, System.Collections.ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                criterias = criterias.ToLower().Replace("sfdcontact.", "a.");
                criterias = criterias.ToLower().Replace("a.dealercode", "isnull(b.DealerCode, '')");
                criterias = criterias.ToLower().Replace("a.countrycode", "isnull(c.CountryCode, '')");
                criterias = criterias.ToLower().Replace("a.citycode", "isnull(d.CityCode, '')");

                List<SFDContact> result = SearchFetchPaging<SFDContact>((connection, query, sqlParams) =>
                {
                    return connection.Query<SFDContact>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(SFDContactQuery.SelectQuery, criterias == null ? string.Empty : criterias.ToString())
                , "SFDContact.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(SFDContactQuery.GetTotalQuery, criterias == null ? string.Empty : criterias.ToString()));


                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<SFDContact>();
            }
        }

        #endregion

        protected void SetCreatedLog(SFDContact SFDContact)
        {
            //SFDContact.CreatedBy = UserLogin;
            //SFDContact.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(SFDContact);
        }

        protected void SetLastModifiedLog(SFDContact SFDContact)
        {
            //SFDContact.LastUpdateBy = UserLogin;
            //SFDContact.LastUpdateTime = DateTime.Now;
        }


    }
}



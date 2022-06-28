
#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : DMSAnnouncementRepository  class
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
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.DMSAnnouncement;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class DMSAnnouncementRepository : BaseDNetRepository<DMSAnnouncement>, IDMSAnnouncementRepository<DMSAnnouncement, int>
    {
        #region Constructor
        public DMSAnnouncementRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create DMSAnnouncement
        /// <summary>
        /// Create DMSAnnouncement
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(DMSAnnouncement entity)
        {
            return null;
        }
        #endregion

        #region Update DMSAnnouncement
        /// <summary>
        /// Update DMSAnnouncement
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(DMSAnnouncement entity)
        {
            return null;
        }
        #endregion

        #region Delete DMSAnnouncement
        /// <summary>
        /// Delete DMSAnnouncement
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get DMSAnnouncement By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DMSAnnouncement Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<DMSAnnouncement>(
                        DMSAnnouncementQuery.GetDMSAnnouncementById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All DMSAnnouncement
        /// <summary>
        /// Get All DMSAnnouncement
        /// </summary>
        /// <returns></returns>
        public List<DMSAnnouncement> GetAll()
        {
            return null;
        }
        #endregion

        #region Search DMSAnnouncement
        public List<DMSAnnouncement> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<DMSAnnouncement>();
        }
        #endregion

        #region Search DMSAnnouncement     
        public List<DMSAnnouncement> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }

        public List<DMSAnnouncement> Search(string criterias, string innerQueryCriteria, System.Collections.ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                List<DMSAnnouncement> result = SearchFetchPaging<DMSAnnouncement>((connection, query, sqlParams) =>
                {
                    return connection.Query<DMSAnnouncement>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(DMSAnnouncementQuery.SelectQuery, criterias == null ? string.Empty : criterias.ToString())
                , "DMSAnnouncement.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(DMSAnnouncementQuery.GetTotalQuery, criterias == null ? string.Empty : criterias.ToString()));


                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<DMSAnnouncement>();
            }
        }

        #endregion

        protected void SetCreatedLog(DMSAnnouncement DMSAnnouncement)
        {
            //DMSAnnouncement.CreatedBy = UserLogin;
            //DMSAnnouncement.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(DMSAnnouncement);
        }

        protected void SetLastModifiedLog(DMSAnnouncement DMSAnnouncement)
        {
            //DMSAnnouncement.LastUpdateBy = UserLogin;
            //DMSAnnouncement.LastUpdateTime = DateTime.Now;
        }


    }
}



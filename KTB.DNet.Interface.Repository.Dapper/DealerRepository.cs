#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Dealer repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 4/12/2018 20:18
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.Dealer;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class DealerRepository : BaseRepository<Dealer>, IDealerRepository<Dealer, int>
    {
        #region Constructor
        public DealerRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Method Get Dealer By Id
        /// <summary>
        /// Get Dealer By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dealer Get(int id)
        {
            try
            {
                using (var connection = Connection)
                {
                    return connection.Query<Dealer>(DealerQuery.GetDealerById, new { Id = id }).SingleOrDefault();
                }
            }
            catch (Exception)
            {

                return new Dealer();
            }
        }
        #endregion

        #region Method Get Dealer By Dealer Group Id
        /// <summary>
        /// Get Dealer By Dealer Group Id
        /// </summary>
        /// <param name="dealerGroupId"></param>
        /// <returns></returns>
        public List<Dealer> GetAllByGroupId(int dealerGroupId)
        {
            try
            {
                using (var connection = Connection)
                {
                    return connection.Query<Dealer>(DealerQuery.GetDealerByDealerGroupId, new { Id = dealerGroupId }).ToList();
                }
            }
            catch (Exception)
            {

                return new List<Dealer>();
            }
        }
        #endregion

        #region Method Get Dealer By Dealer Code
        /// <summary>
        /// Get Dealer by Dealer Code
        /// </summary>
        /// <param name="dealerCode"></param>
        /// <returns></returns>
        public Dealer GetByCode(string dealerCode)
        {
            try
            {
                using (var connection = Connection)
                {
                    return connection.Query<Dealer>(DealerQuery.GetDealerByDealerCode, new { DealerCode = dealerCode }).SingleOrDefault();
                }
            }
            catch (Exception)
            {

                return new Dealer();
            }
        }
        #endregion

        #region Method Get Active Dealers
        /// <summary>
        /// Get Active Dealers
        /// </summary>
        /// <returns></returns>
        public List<Dealer> GetActiveDealers()
        {
            try
            {
                using (var connection = Connection)
                {
                    return connection.Query<Dealer>(DealerQuery.GetActiveDealers).ToList();
                }
            }
            catch (Exception e)
            {

                return new List<Dealer>();
            }
        }
        #endregion

        #region Method Get All Dealers
        /// <summary>
        /// Get All Dealers
        /// </summary>
        /// <returns></returns>
        public List<Dealer> GetAll()
        {
            try
            {
                using (var connection = Connection)
                {
                    return connection.Query<Dealer>(DealerQuery.GetAllDealers).ToList();
                }
            }
            catch (Exception)
            {

                return new List<Dealer>();
            }
        }
        #endregion

        #region Get Dealer Count
        /// <summary>
        /// Get Dealer Count
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetDealerCount(int userId)
        {
            using (var connection = Connection)
            {
                return userId == AppConfigs.GetInt("DMSAdminRoleId") ? connection.Query<int>(DealerQuery.GetDealerCount).FirstOrDefault() : 1;
            }
        }
        #endregion

        #region Not Implemented
        public ResponseMessage Create(Dealer entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(Dealer entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Dealer> Search(Framework.DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

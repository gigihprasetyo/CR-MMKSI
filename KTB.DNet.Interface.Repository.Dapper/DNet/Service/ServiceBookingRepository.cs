#region Summary
/*
 ===========================================================================
 AUTHOR        : Ivan
 PURPOSE       : ServiceBookingRepository Domain class
 SPECIAL NOTES : DNet WebApi Project
 ---------------------
 Copyright  (c) 2021 
 ---------------------
 $History      : $
 Created on 2021-10-26
 ===========================================================================
*/
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.Service.SqlQuery.ServiceBooking;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using KTB.DNet.Domain;
using KTB.DNet.Interface.Model;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class ServiceBookingRepository : BaseDNetRepository<ServiceBookingRealtimeAll_IF>, IServiceBookingRepository<ServiceBookingRealtimeAll_IF, int>
    {
        #region Constructor
        public ServiceBookingRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion


        #region Create ServiceBooking
        public ResponseMessage Create(ServiceBookingRealtimeAll_IF entity)
        {
            return null;
        }
        #endregion


        #region Update ServiceBooking

        public ResponseMessage Update(ServiceBookingRealtimeAll_IF entity)
        {
            return null;
        }
        #endregion

        #region Delete ServiceBooking
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get ServiceBooking By Id
        public ServiceBookingRealtimeAll_IF Get(Guid equipmentid)
        {
            return null;
        }

        public ServiceBookingRealtimeAll_IF Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All ServiceBooking
        public List<ServiceBookingRealtimeAll_IF> GetAll()
        {
            return null;
        }
        #endregion

        #region Search ServiceBooking
        public List<ServiceBookingRealtimeAll_IF> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<ServiceBookingRealtimeAll_IF>();
        }
        #endregion

        #region Search ServiceBooking        
        public new List<ServiceBookingRealtimeAll_IF> SearchRealTimeAll(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.Replace("ServiceBookingRealtimeAll_IF.", "sb.");

                List<ServiceBookingRealtimeAll_IF> result = SearchFetchPaging<ServiceBookingRealtimeAll_IF>((connection, query, sqlParams) =>
                {
                    return connection.Query<ServiceBookingRealtimeAll_IF>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(ServiceBookingQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "sb.ID desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(ServiceBookingQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "ID");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<ServiceBookingRealtimeAll_IF>();
            }
        }
        #endregion
        public void SetCreatedLog(ServiceBookingRealtimeAll_IF ServiceBooking)
        {
            //ServiceBooking.createdbyname = UserLogin;
            //ServiceBooking.createdon = DateTime.Now;
            //ServiceBooking.RowStatus = "0";
            SetLastModifiedLog(ServiceBooking);
        }

        public void SetLastModifiedLog(ServiceBookingRealtimeAll_IF ServiceBooking)
        {
            //ServiceBooking.modifiedbyname = UserLogin;
            //ServiceBooking.modifiedon = DateTime.Now;
        }

    }
}


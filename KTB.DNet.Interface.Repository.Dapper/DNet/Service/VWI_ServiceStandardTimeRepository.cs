
#region Namespace Imports
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using System.Collections;
using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Repository.Dapper.DNet.Service.SqlQuery.VWI_ServiceStandardTime;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion
namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_ServiceStandardTimeRepository : BaseDNetRepository<VWI_ServiceStandardTime>, IVWI_ServiceStandardTimeRepository<VWI_ServiceStandardTime, int>
    {
        #region Constructor
        public VWI_ServiceStandardTimeRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_ServiceStandardTime
        /// <summary>
        /// Create VWI_ServiceStandardTime
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_ServiceStandardTime entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_ServiceStandardTime
        /// <summary>
        /// Update VWI_ServiceStandardTime
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_ServiceStandardTime entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_ServiceStandardTime
        /// <summary>
        /// Delete VWI_ServiceStandardTime
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_ServiceTemplate By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_ServiceStandardTime Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_ServiceStandardTime>(
                        VWI_ServiceStandardTimeQuery.GetVWI_ServiceStandardTimeById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_ServiceStandardTime
        /// <summary>
        /// Get All VWI_ServiceStandardTime
        /// </summary>
        /// <returns></returns>
        public List<VWI_ServiceStandardTime> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_ServiceStandardTime
        public List<VWI_ServiceStandardTime> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_ServiceStandardTime>();
        }
        #endregion

        #region Search VWI_ServiceStandardTime        
        public new List<VWI_ServiceStandardTime> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_ServiceStandardTime> result = Search<VWI_ServiceStandardTime>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_ServiceStandardTime>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_ServiceStandardTimeQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_ServiceStandardTime.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_ServiceStandardTimeQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_ServiceStandardTime>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_ServiceStandardTime serviceTemplateHeader)
        {
            //vWI_CRM_WOInvoice.CreatedBy = UserLogin;
            //vWI_CRM_WOInvoice.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_WOInvoice);
        }

        protected void SetLastModifiedLog(VWI_ServiceStandardTime serviceTemplateHeader)
        {
            //vWI_CRM_WOInvoice.LastUpdateBy = UserLogin;
            //vWI_CRM_WOInvoice.LastUpdateTime = DateTime.Now;
        }

        public new List<VWI_ServiceStandardTime> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
    }
}



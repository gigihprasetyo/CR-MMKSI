
#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.SpareParts.SqlQuery.SparePartForecast;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class SparePartForecastRepository : BaseDNetRepository<SparePartForecast_IF>, ISparePartForecastRepository<SparePartForecast_IF, int>
    {
        #region Constructor
        public SparePartForecastRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create SparePartForecast
        /// <summary>
        /// Create SparePartForecast
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(SparePartForecast_IF entity)
        {
            return null;
        }
        #endregion

        #region Update SparePartForecast
        /// <summary>
        /// Update SparePartForecast
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(SparePartForecast_IF entity)
        {
            return null;
        }
        #endregion

        #region Delete SparePartForecast
        /// <summary>
        /// Delete SparePartForecast
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get SparePartForecast By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SparePartForecast_IF Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All SparePartForecast
        /// <summary>
        /// Get All SparePartForecast
        /// </summary>
        /// <returns></returns>
        public List<SparePartForecast_IF> GetAll()
        {
            return null;
        }
        #endregion

        #region Search SparePartForecast
        public List<SparePartForecast_IF> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<SparePartForecast_IF>();
        }
        #endregion

        #region Search SparePartForecast        
        public new List<SparePartForecast_IF> SearchStockManagement(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("sparepartforecast_if.", "StockManagement.");

                List<SparePartForecastStockManagement_IF> result = SearchFetchPaging<SparePartForecastStockManagement_IF>((connection, query, sqlParams) =>
                {
                    return connection.Query<SparePartForecastStockManagement_IF>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(SparePartForecastQuery.SelectStockManagementQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "StockManagement.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(SparePartForecastQuery.GetTotalStockManagementQuery,
                                                strCriteria,
                                                strInnerCriteria), "StockManagement.ID");

                totalResultsCount = filteredResultsCount;

                List<SparePartForecast_IF> rst = new List<SparePartForecast_IF>();
                SparePartForecast_IF x = new SparePartForecast_IF();
                x.StockManagement = result;
                rst.Add(x);

                return rst;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<SparePartForecast_IF>();
            }
        }

        public new List<SparePartForecast_IF> SearchRejectedData(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("sparepartforecast_if.", "Reject.");

                List<SparepartForecastReject_IF> result = SearchFetchPaging<SparepartForecastReject_IF>((connection, query, sqlParams) =>
                {
                    return connection.Query<SparepartForecastReject_IF>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(SparePartForecastQuery.SelectRejectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "Reject.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(SparePartForecastQuery.GetTotalRejectQuery,
                                                strCriteria,
                                                strInnerCriteria), "Reject.ID");

                totalResultsCount = filteredResultsCount;

                List<SparePartForecast_IF> rst = new List<SparePartForecast_IF>();
                SparePartForecast_IF x = new SparePartForecast_IF();
                x.Reject = result;
                rst.Add(x);

                return rst;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<SparePartForecast_IF>();
            }
        }

        public new List<SparePartForecast_IF> SearchPOEstimateHeader(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("sparepartforecast_if.", "POEstimate.");

                List<SparepartForecastPOEstimateHeader_IF> result = SearchFetchPaging<SparepartForecastPOEstimateHeader_IF>((connection, query, sqlParams) =>
                {
                    return connection.Query<SparepartForecastPOEstimateHeader_IF>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(SparePartForecastQuery.SelectPOEstimateHeaderQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "POEstimate.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(SparePartForecastQuery.GetTotalPOEstimateHeaderQuery,
                                                strCriteria,
                                                strInnerCriteria), "POEstimate.ID");

                totalResultsCount = filteredResultsCount;

                List<SparePartForecast_IF> rst = new List<SparePartForecast_IF>();
                SparePartForecast_IF x = new SparePartForecast_IF();
                x.POEstimateHeader = result;
                rst.Add(x);

                return rst;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<SparePartForecast_IF>();
            }
        }

        public new List<SparePartForecast_IF> SearchPOEstimateDetail(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("sparepartforecast_if.", "POEstimateDetail.");

                List<SparepartForecastPOEstimateDetail_IF> result = SearchFetchPaging<SparepartForecastPOEstimateDetail_IF>((connection, query, sqlParams) =>
                {
                    return connection.Query<SparepartForecastPOEstimateDetail_IF>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(SparePartForecastQuery.SelectPOEstimateDetailQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "POEstimateDetail.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(SparePartForecastQuery.GetTotalPOEstimateDetailQuery,
                                                strCriteria,
                                                strInnerCriteria), "POEstimateDetail.ID");

                totalResultsCount = filteredResultsCount;

                List<SparePartForecast_IF> rst = new List<SparePartForecast_IF>();
                SparePartForecast_IF x = new SparePartForecast_IF();
                x.POEstimateDetail = result;
                rst.Add(x);

                return rst;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<SparePartForecast_IF>();
            }
        }

        public new List<SparePartForecast_IF> SearchGoodReceiptHeader(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("sparepartforecast_if.", "GoodReceipt.");

                List<SparepartForecastGoodReceiptHeader_IF> result = SearchFetchPaging<SparepartForecastGoodReceiptHeader_IF>((connection, query, sqlParams) =>
                {
                    return connection.Query<SparepartForecastGoodReceiptHeader_IF>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(SparePartForecastQuery.SelectGoodReceiptHeaderQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "GoodReceipt.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(SparePartForecastQuery.GetTotalGoodReceiptHeaderQuery,
                                                strCriteria,
                                                strInnerCriteria), "GoodReceipt.ID");

                totalResultsCount = filteredResultsCount;

                List<SparePartForecast_IF> rst = new List<SparePartForecast_IF>();
                SparePartForecast_IF x = new SparePartForecast_IF();
                x.GoodReceiptHeader = result;
                rst.Add(x);

                return rst;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<SparePartForecast_IF>();
            }
        }

        public new List<SparePartForecast_IF> SearchGoodReceiptDetail(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("sparepartforecast_if.", "GoodReceiptDetail.");

                List<SparepartForecastGoodReceiptDetail_IF> result = SearchFetchPaging<SparepartForecastGoodReceiptDetail_IF>((connection, query, sqlParams) =>
                {
                    return connection.Query<SparepartForecastGoodReceiptDetail_IF>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(SparePartForecastQuery.SelectGoodReceiptDetailQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "GoodReceiptDetail.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(SparePartForecastQuery.GetTotalGoodReceiptDetailQuery,
                                                strCriteria,
                                                strInnerCriteria), "GoodReceiptDetail.ID");

                totalResultsCount = filteredResultsCount;

                List<SparePartForecast_IF> rst = new List<SparePartForecast_IF>();
                SparePartForecast_IF x = new SparePartForecast_IF();
                x.GoodReceiptDetail = result;
                rst.Add(x);

                return rst;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<SparePartForecast_IF>();
            }
        }

        #endregion

        protected void SetCreatedLog(SparePartForecast_IF SparePartForecast)
        {
            //SparePartForecast.CreatedBy = UserLogin;
            //SparePartForecast.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(SparePartForecast);
        }

        protected void SetLastModifiedLog(SparePartForecast_IF SparePartForecast)
        {
            //SparePartForecast.LastUpdateBy = UserLogin;
            //SparePartForecast.LastUpdateTime = DateTime.Now;
        }
    }
}

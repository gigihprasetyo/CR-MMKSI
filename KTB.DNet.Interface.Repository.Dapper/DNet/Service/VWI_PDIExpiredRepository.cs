
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.Service.SqlQuery.VWI_PDIExpired;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_PDIExpiredRepository : BaseDNetRepository<VWI_PDIExpired>, IVWI_PDIExpiredRepository<VWI_PDIExpired, int>
    {
        #region Constructor
        public VWI_PDIExpiredRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_PDIExpired
        /// <summary>
        /// Create VWI_PDIExpired
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_PDIExpired entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_PDIExpired
        /// <summary>
        /// Update VWI_PDIExpired
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_PDIExpired entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_PDIExpired
        /// <summary>
        /// Delete VWI_PDIExpired
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_PDIExpired By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_PDIExpired Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_PDIExpired>(
                        VWI_PDIExpiredQuery.GetVWI_PDIExpiredById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_PDIExpired
        /// <summary>
        /// Get All VWI_PDIExpired
        /// </summary>
        /// <returns></returns>
        public List<VWI_PDIExpired> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_PDIExpired
        public List<VWI_PDIExpired> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_PDIExpired>();
        }
        #endregion

        #region Search VWI_PDIExpired        
        public new List<VWI_PDIExpired> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_PDIExpired> result = Search<VWI_PDIExpired>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_PDIExpired>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_PDIExpiredQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_PDIExpired.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_PDIExpiredQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_PDIExpired>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_PDIExpired VWI_PDIExpired)
        {
            //VWI_PDIExpired.CreatedBy = UserLogin;
            //VWI_PDIExpired.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(VWI_PDIExpired);
        }

        protected void SetLastModifiedLog(VWI_PDIExpired VWI_PDIExpired)
        {
            //VWI_PDIExpired.LastUpdateBy = UserLogin;
            //VWI_PDIExpired.LastUpdateTime = DateTime.Now;
        }
    }
}

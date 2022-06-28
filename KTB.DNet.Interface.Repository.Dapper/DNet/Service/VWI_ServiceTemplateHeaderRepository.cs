#region Namespace Imports
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using System.Collections;
using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Repository.Dapper.DNet.Service.SqlQuery.VWI_ServiceTemplateHeader;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion
namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_ServiceTemplateHeaderRepository : BaseDNetRepository<VWI_ServiceTemplateHeader>, IVWI_ServiceTemplateHeaderRepository<VWI_ServiceTemplateHeader, int>
    {
        #region Constructor
        public VWI_ServiceTemplateHeaderRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_ServiceTemplate
        /// <summary>
        /// Create VWI_ServiceTemplate
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_ServiceTemplateHeader entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_ServiceTemplate
        /// <summary>
        /// Update VWI_ServiceTemplate
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_ServiceTemplateHeader entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_ServiceTemplate
        /// <summary>
        /// Delete VWI_ServiceTemplate
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
        public VWI_ServiceTemplateHeader Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_ServiceTemplateHeader>(
                        VWI_ServiceTemplateHeaderQuery.GetVWI_ServiceTemplateHeaderById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_ServiceTemplate
        /// <summary>
        /// Get All VWI_CRM_WOInvoice
        /// </summary>
        /// <returns></returns>
        public List<VWI_ServiceTemplateHeader> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_WOInvoice
        public List<VWI_ServiceTemplateHeader> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_ServiceTemplateHeader>();
        }
        #endregion

        #region Search VWI_CRM_WOInvoice        
        public new List<VWI_ServiceTemplateHeader> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_ServiceTemplateHeader> result = Search<VWI_ServiceTemplateHeader>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_ServiceTemplateHeader>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_ServiceTemplateHeaderQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_ServiceTemplateHeader.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_ServiceTemplateHeaderQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_ServiceTemplateHeader>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_ServiceTemplateHeader serviceTemplateHeader)
        {
            //vWI_CRM_WOInvoice.CreatedBy = UserLogin;
            //vWI_CRM_WOInvoice.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_WOInvoice);
        }

        protected void SetLastModifiedLog(VWI_ServiceTemplateHeader serviceTemplateHeader)
        {
            //vWI_CRM_WOInvoice.LastUpdateBy = UserLogin;
            //vWI_CRM_WOInvoice.LastUpdateTime = DateTime.Now;
        }

        public new List<VWI_ServiceTemplateHeader> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
    }
}


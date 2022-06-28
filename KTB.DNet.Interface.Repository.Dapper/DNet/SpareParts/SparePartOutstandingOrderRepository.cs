#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : SparePartOutstandingOrder class
 SPECIAL NOTES : Generated from database BSIDNET_MMKSI_CR_Sparepart_BO
 GENERATED BY  : Ako
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 13 Jan 2021 10:52:07
 ===========================================================================
*/
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.SpareParts.SqlQuery.SparePartOutstandingOrder;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class SparePartOutstandingOrderRepository : BaseDNetRepository<SparePartOutstandingOrder>, ISparePartOutstandingOrderRepository<SparePartOutstandingOrder, int>
    {
        #region Constructor
        public SparePartOutstandingOrderRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

		
        #region Create SparePartOutstandingOrder
        public ResponseMessage Create(SparePartOutstandingOrder entity)
        {
			return null;
		}
        #endregion
		
		
        #region Update SparePartOutstandingOrder
        public ResponseMessage Update(SparePartOutstandingOrder entity)
        {
			return null;
		}
        #endregion
		

        #region Delete SparePartOutstandingOrder
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get SparePartOutstandingOrder By Id
        public SparePartOutstandingOrder Get(Guid ID)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<SparePartOutstandingOrder>(
                        SparePartOutstandingOrderQuery.GetSparePartOutstandingOrderByID, new { ID = ID }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public SparePartOutstandingOrder Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All SparePartOutstandingOrder
        public List<SparePartOutstandingOrder> GetAll()
        {
            return null;
        }
        #endregion

        #region Search SparePartOutstandingOrder
        public List<SparePartOutstandingOrder> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<SparePartOutstandingOrder>();
        }
        #endregion

		#region Search SparePartOutstandingOrder        
        public new List<SparePartOutstandingOrder> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<SparePartOutstandingOrder> result = SearchFetchPaging<SparePartOutstandingOrder>((connection, query, sqlParams) =>
                {
                    return connection.Query<SparePartOutstandingOrder>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(SparePartOutstandingOrderQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "SparePartOutstandingOrder.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(SparePartOutstandingOrderQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "ID");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<SparePartOutstandingOrder>();
            }
        }
        #endregion

        public void SetCreatedLog(SparePartOutstandingOrder SparePartOutstandingOrder)
        {
            //SparePartOutstandingOrder.createdbyname = UserLogin;
            //SparePartOutstandingOrder.createdon = DateTime.Now;
            SparePartOutstandingOrder.RowStatus = 0;
            SetLastModifiedLog(SparePartOutstandingOrder);
        }

        public void SetLastModifiedLog(SparePartOutstandingOrder SparePartOutstandingOrder)
        {
            //SparePartOutstandingOrder.modifiedbyname = UserLogin;
            //SparePartOutstandingOrder.modifiedon = DateTime.Now;
        }

		
    }
}

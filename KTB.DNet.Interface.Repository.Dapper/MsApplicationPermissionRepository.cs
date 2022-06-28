#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : RolePermission repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 6/12/2018 15:18
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.MsApplicationPermission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class MsApplicationPermissionRepository : BaseRepository<MsApplicationPermission>
    {
        #region Constructor
        public MsApplicationPermissionRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region AddApplicationPermission
        /// <summary>
        /// AddApplicationPermission
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="listOfMsApplicationPermission"></param>
        public void AddApplicationPermission(IDbConnection connection, IDbTransaction transaction, List<MsApplicationPermission> listOfMsApplicationPermission)
        {
            if (listOfMsApplicationPermission != null && listOfMsApplicationPermission.Count() > 0)
            {
                connection.Execute(MsApplicationPermissionQuery.InsertMsApplicationPermission, listOfMsApplicationPermission, transaction);
            }
        }
        #endregion

        #region RemoveApplicationPermission
        /// <summary>
        /// RemoveApplicationPermission
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="msApplicationPermissionId"></param>
        public void RemoveApplicationPermission(IDbConnection connection, IDbTransaction transaction, List<int> msApplicationPermissionId)
        {
            if (msApplicationPermissionId != null && msApplicationPermissionId.Count() > 0)
            {
                connection.Execute(MsApplicationPermissionQuery.DeleteByListOfId, new { listOfId = msApplicationPermissionId }, transaction);
            }
        }
        #endregion

        #region GetByAppId
        /// <summary>
        /// GetByAppId
        /// </summary>
        /// <param name="AppId"></param>
        /// <returns></returns>
        public List<MsApplicationPermission> GetByAppId(Guid AppId)
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    return connection.Query<MsApplicationPermission>(MsApplicationPermissionQuery.GetByAppId, new { AppId = AppId }).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<MsApplicationPermission>();
            }
        }
        #endregion

    }
}

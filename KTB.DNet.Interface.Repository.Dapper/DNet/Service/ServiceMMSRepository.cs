#region Summary
/*
 ===========================================================================
 AUTHOR        : Ivan
 PURPOSE       : ServiceMMSRepository Domain class
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
using KTB.DNet.Interface.Repository.Dapper.DNet.Service.SqlQuery.ServiceMMS;
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
    public class ServiceMMSRepository : BaseDNetRepository<ServiceMMS_IF>, IServiceMMSRepository<ServiceMMS_IF, int>
    {
        #region Constructor
        public ServiceMMSRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion


        #region Create ServiceMMS
        public ResponseMessage Create(ServiceMMS_IF entity)
        {

            ResponseMessage responseMessage = new ResponseMessage() { Success = false };
            int ReturnId = 0;
            try
            {
                SetCreatedLog(entity);
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    object returnObj = connection.ExecuteScalar(ServiceMMSQuery.InsertServiceMMS, entity, transaction);
                    if (returnObj != null)
                    {
                        int.TryParse(returnObj.ToString(), out ReturnId);
                    }
                    return returnObj;
                });

                entity.ID = ReturnId;
                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("ServiceMMS {0} has been successfully created", entity.ID);
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create ServiceMMS. " + GetInnerException(ex).Message;
            }

            return responseMessage;

        }
        #endregion


        #region Update ServiceMMS

        public ResponseMessage Update(ServiceMMS_IF entity)
        {

            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (entity != null)
                {
                    SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(ServiceMMSQuery.UpdateServiceMMS, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("ServiceMMS{0} has been successfully updated", entity.ID);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "ServiceMMS does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update ServiceMMS. " + GetInnerException(ex).Message;
            }

            return responseMessage;

        }
        #endregion

        #region Delete ServiceMMS
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get ServiceMMS By Id
        public ServiceMMS_IF Get(Guid equipmentid)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<ServiceMMS_IF>(
                        ServiceMMSQuery.GetServiceMMSByID, new { equipmentid = equipmentid }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ServiceMMS_IF Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All ServiceMMS
        public List<ServiceMMS_IF> GetAll()
        {
            return null;
        }
        #endregion

        #region Search ServiceMMS
        public List<ServiceMMS_IF> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<ServiceMMS_IF>();
        }
        #endregion

        #region Search ServiceMMS        
        public new List<ServiceMMS_IF> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<ServiceMMS_IF> result = SearchFetchPaging<ServiceMMS_IF>((connection, query, sqlParams) =>
                {
                    return connection.Query<ServiceMMS_IF>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(ServiceMMSQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "ServiceMMS.ID desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(ServiceMMSQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "ID");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<ServiceMMS_IF>();
            }
        }
        #endregion
        public void SetCreatedLog(ServiceMMS_IF ServiceMMS)
        {
            //ServiceMMS.createdbyname = UserLogin;
            //ServiceMMS.createdon = DateTime.Now;
            //ServiceMMS.RowStatus = "0";
            SetLastModifiedLog(ServiceMMS);
        }

        public void SetLastModifiedLog(ServiceMMS_IF ServiceMMS)
        {
            //ServiceMMS.modifiedbyname = UserLogin;
            //ServiceMMS.modifiedon = DateTime.Now;
        }

    }
}

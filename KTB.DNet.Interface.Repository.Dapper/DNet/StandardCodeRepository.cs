#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : StandardCode repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 3/12/2018 8:29
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.SqlQuery.StandardCode;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class StandardCodeRepository : BaseRepository<StandardCode>, IStandardCodeRepository<StandardCode, int>
    {
        #region Constructor
        public StandardCodeRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create Standard Code
        /// <summary>
        /// Create Standard Code
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(StandardCode entity)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                SetCreatedLog(entity);

                var result = ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.ExecuteScalar<int>(StandardCodeQuery.InsertStandardCode, entity, transaction);
                });

                entity.ID = Convert.ToInt32(result);

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = "New Standard Code has been successfully created";
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create Standard Code. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Update Standard Code
        /// <summary>
        /// Update Standard Code
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(StandardCode entity)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                StandardCode existingStandardCode = Get((int)entity.ID);
                if (existingStandardCode != null)
                {
                    SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(StandardCodeQuery.UpdateStandardCode, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Standard Code {0} has been successfully updated", entity.ValueCode);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Standard Code does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Standard Code. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Delete Standard Code
        /// <summary>
        /// Delete Standard Code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(StandardCodeQuery.DeleteStandardCode, new
                    {
                        Id = id
                    }, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Standard Code with id {0} has been successfully deleted", id);

            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to delete Standard Code. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Get Standard Code By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StandardCode Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<StandardCode>(
                        StandardCodeQuery.GetStandardCodeById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get Standard Code By Category
        /// <summary>
        /// Get by Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<StandardCode> GetByCategory(string category)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<StandardCode>(
                        StandardCodeQuery.GetStandardCodeByCategory, new { Category = category }
                        ).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All Standard Code
        /// <summary>
        /// Get All Standard Code
        /// </summary>
        /// <returns></returns>
        public List<StandardCode> GetAll()
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<StandardCode>(StandardCodeQuery.GetAllStandardCode).ToList();
                }
            }
            catch (Exception)
            {
                return new List<StandardCode>();
            }
        }
        #endregion

        #region Search Standard Code
        public List<StandardCode> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string keyword = string.Empty;
                List<string> orderBy = null;
                filteredResultsCount = 0;

                GetPostModelData(model, "CreatedTime DESC", out keyword, out orderBy);
                List<StandardCode> result = Search<StandardCode>((connection, query, sqlParams) =>
                {
                    return connection.Query<StandardCode>(query, sqlParams).ToList();
                }, Connection, StandardCodeQuery.SearchStandardCode
                , "CreatedTime DESC", new { Keyword = keyword }, orderBy, out filteredResultsCount, model.Start, model.Length);

                totalResultsCount = filteredResultsCount;

                return result;
            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<StandardCode>();
            }
        }
        #endregion

        protected void SetCreatedLog(StandardCode standardCode)
        {
            standardCode.CreatedBy = UserLogin;
            standardCode.CreatedTime = DateTime.Now;
            SetLastModifiedLog(standardCode);
        }

        protected void SetLastModifiedLog(StandardCode standardCode)
        {
            standardCode.LastUpdateBy = UserLogin;
            standardCode.LastUpdateTime = DateTime.Now;
        }
    }
}

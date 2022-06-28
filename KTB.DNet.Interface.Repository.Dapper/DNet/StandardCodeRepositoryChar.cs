#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : StandardCodeChar repository class
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
using KTB.DNet.Interface.Repository.Dapper.DNet.SqlQuery.StandardCodeChar;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class StandardCodeCharRepository : BaseRepository<StandardCodeChar>, IStandardCodeRepository<StandardCodeChar, int>
    {
        #region Constructor
        public StandardCodeCharRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create Standard Code Char
        /// <summary>
        /// Create Standard Code Char
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(StandardCodeChar entity)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                SetCreatedLog(entity);

                var result = ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.ExecuteScalar<int>(StandardCodeCharQuery.InsertStandardCodeChar, entity, transaction);
                });

                entity.ID = Convert.ToInt32(result);

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = "New Standard Code Char has been successfully created";
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create Standard Code Char. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Update Standard Code Char
        /// <summary>
        /// Update Standard Code Char
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(StandardCodeChar entity)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                StandardCodeChar existingStandardCodeChar = Get((int)entity.ID);
                if (existingStandardCodeChar != null)
                {
                    SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(StandardCodeCharQuery.UpdateStandardCodeChar, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Standard Code Char {0} has been successfully updated", entity.ValueCode);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Standard Code Char does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Standard Code Char. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Delete Standard Code Char
        /// <summary>
        /// Delete Standard Code Char
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
                    return connection.Execute(StandardCodeCharQuery.DeleteStandardCodeChar, new
                    {
                        Id = id
                    }, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Standard Code Char with id {0} has been successfully deleted", id);

            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to delete Standard Code Char. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Get Standard Code Char By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StandardCodeChar Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<StandardCodeChar>(
                        StandardCodeCharQuery.GetStandardCodeCharById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get Standard Code Char By Category
        /// <summary>
        /// Get by Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<StandardCodeChar> GetByCategory(string category)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<StandardCodeChar>(
                        StandardCodeCharQuery.GetStandardCodeCharByCategory, new { Category = category }
                        ).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All Standard Code Char
        /// <summary>
        /// Get All Standard Code Char
        /// </summary>
        /// <returns></returns>
        public List<StandardCodeChar> GetAll()
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<StandardCodeChar>(StandardCodeCharQuery.GetAllStandardCodeChar).ToList();
                }
            }
            catch (Exception)
            {
                return new List<StandardCodeChar>();
            }
        }
        #endregion

        #region Search Standard Code Char
        public List<StandardCodeChar> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string keyword = string.Empty;
                List<string> orderBy = null;
                filteredResultsCount = 0;

                GetPostModelData(model, "CreatedTime DESC", out keyword, out orderBy);
                List<StandardCodeChar> result = Search<StandardCodeChar>((connection, query, sqlParams) =>
                {
                    return connection.Query<StandardCodeChar>(query, sqlParams).ToList();
                }, Connection, StandardCodeCharQuery.SearchStandardCodeChar
                , "CreatedTime DESC", new { Keyword = keyword }, orderBy, out filteredResultsCount, model.Start, model.Length);

                totalResultsCount = filteredResultsCount;

                return result;
            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<StandardCodeChar>();
            }
        }
        #endregion

        protected void SetCreatedLog(StandardCodeChar standardCode)
        {
            standardCode.CreatedBy = UserLogin;
            standardCode.CreatedTime = DateTime.Now;
            SetLastModifiedLog(standardCode);
        }

        protected void SetLastModifiedLog(StandardCodeChar standardCode)
        {
            standardCode.LastUpdateBy = UserLogin;
            standardCode.LastUpdateTime = DateTime.Now;
        }
    }
}

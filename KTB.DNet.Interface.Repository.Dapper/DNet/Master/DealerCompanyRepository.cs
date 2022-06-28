using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.DealerBranch;
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.DealerCompany;
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.DealerGroup;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class DealerCompanyRepository : BaseRepository<DealerCompany>, IDealerCompanyRepository<DealerCompany, int>
    {
        public DealerCompanyRepository(string connectionString)
            : base(connectionString)
        { }

        public DealerCompany Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<DealerCompany>(
                        DealerCompanyQuery.SearchDealerCompany,
                        new
                        {
                            @ID = id,
                            @DealerCompanyCode = string.Empty,
                            @DealerCompanyName = string.Empty,
                            @DealerGroupID = 0
                        }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ResponseMessage Create(DealerCompany entity)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                var result = ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.ExecuteScalar<int>(DealerCompanyQuery.InsertDealerCompany,
                        new
                        {
                            @DealerCompanyCode = entity.DealerCompanyCode,
                            @DealerCompanyName = entity.DealerCompanyName,
                            @DealerGroupID = entity.DealerGroupID,
                            @RowStatus = 0,
                            @CreatedBy = entity.CreatedBy,
                            @CreatedTime = DateTime.Now,
                            @LastUpdateBy = entity.LastUpdateBy,
                            @LastUpdateTime = DateTime.Now
                        },
                        transaction);
                });

                entity.ID = Convert.ToInt32(result);

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = "New Dealer Company Group has been successfully created";
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create Deal. " + GetInnerException(ex).Message;
            }

            return responseMessage;
            
        }

        public ResponseMessage Update(DealerCompany entity)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                DealerCompany existingServiceCategory = Get((int)entity.ID);
                if (existingServiceCategory != null)
                {
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(DealerCompanyQuery.UpdateDealerCompany,
                            new
                            {
                                @ID = entity.ID,
                                @DealerCompanyCode = entity.DealerCompanyCode,
                                @DealerCompanyName = entity.DealerCompanyName,
                                @DealerGroupID = entity.DealerGroupID,
                                @RowStatus = 0,
                                @LastUpdateBy = entity.LastUpdateBy,
                                @LastUpdateTime = DateTime.Now
                            },
                            transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Dealer Company {0} has been successfully updated", entity.DealerCompanyName);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Dealer Company does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Dealer Company. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }

        public ResponseMessage Delete(int id)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(DealerCompanyQuery.DeleteDealerCompany, new
                    {
                        @ID = id
                    }, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Dealer Company with id {0} has been successfully deleted", id);

            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to delete Dealer Company. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }

        public List<DealerCompany> GetAll()
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<DealerCompany>(DealerCompanyQuery.GetAllDealerCompany).ToList();
                }
            }
            catch (Exception)
            {
                return new List<DealerCompany>();
            }
        }

        public List<DealerCompany> Search(DealerCompanyPostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string keyword = string.Empty;
                List<string> orderBy = null;
                filteredResultsCount = 0;

                GetPostModelData(model, "DealerCompany.LastUpdateTime DESC", out keyword, out orderBy);
                List<DealerCompany> result = Search<DealerCompany>((connection, query, sqlParams) =>
                {
                    return connection.Query<DealerCompany>(query, sqlParams).ToList();
                }, Connection, DealerCompanyQuery.SearchDealerCompany
                , "DealerCompany.LastUpdateTime DESC",
                new
                {
                    @ID = model.ID,
                    @DealerCompanyCode = model.DealerCompanyCode == null ? string.Empty : model.DealerCompanyCode,
                    @DealerCompanyName = model.DealerCompanyName == null ? string.Empty : model.DealerCompanyName,
                    @DealerGroupID = model.DealerGroupID
                    
                }, orderBy, out filteredResultsCount, model.Start, model.Length, DealerCompanyQuery.SearchDealerCompanyTotal);

                totalResultsCount = filteredResultsCount;

                return result;
            }
           
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<DealerCompany>();
            }

        }
        
        List<DealerCompany> IBaseDNetRepository<DealerCompany, int>.Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
        
        public List<DealerGroup> GetAllDealerGroup()
        {
            throw new NotImplementedException();
        }


        public List<DealerCompany> GetAllDealerCompany(int dealerCompanyId)
        {
            throw new NotImplementedException();
        }
    }
}

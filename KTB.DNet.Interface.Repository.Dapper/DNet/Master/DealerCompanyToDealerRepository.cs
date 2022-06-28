using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.DealerBranch;
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.DealerCompanyToDealer;
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.DealerGroup;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class DealerCompanyToDealerRepository : BaseRepository<DealerCompanyToDealer>, IDealerCompanyToDealerRepository<DealerCompanyToDealer, int>
    {
        public DealerCompanyToDealerRepository(string connectionString)
            : base(connectionString)
        { }

        public DealerCompanyToDealer Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<DealerCompanyToDealer>(
                        DealerCompanyToDealerQuery.SearchDealerCompanyToDealer,
                        new
                        {
                            @ID = id,
                            @DealerCompanyID = 0,
                            @DealerID = 0
                        }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ResponseMessage Create(DealerCompanyToDealer entity)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                var result = ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.ExecuteScalar<int>(DealerCompanyToDealerQuery.InsertDealerCompanyToDealer,
                        new
                        {
                            @DealerCompanyID = entity.DealerCompanyID,
                            @DealerID = entity.DealerID,                            
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

        public ResponseMessage Update(DealerCompanyToDealer entity)
        {
            return null;
        }

        public ResponseMessage Delete(int id)
        {
            return null;
        }

        public List<DealerCompanyToDealer> GetAll()
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<DealerCompanyToDealer>(DealerCompanyToDealerQuery.GetAllDealerCompanyToDealer).ToList();
                }
            }
            catch (Exception)
            {
                return new List<DealerCompanyToDealer>();
            }
        }

       
        List<DealerCompanyToDealer> IBaseDNetRepository<DealerCompanyToDealer, int>.Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
        
        public List<DealerGroup> GetAllDealerGroup()
        {
            throw new NotImplementedException();
        }

        public List<KTB.DNet.Interface.Domain.Dealer> GetAllDealer(int dealerCompanyId)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<Domain.Dealer>(
                        DealerCompanyToDealerQuery.SearchDealerCompanyToDealer,
                        new
                        {
                            @ID = 0,
                            @DealerCompanyID = dealerCompanyId,
                            @DealerID = 0
                        }
                        ).ToList();
                }
            }
            catch (Exception)
            {
                return new List<Domain.Dealer>();
            }
        }
        
    }
}

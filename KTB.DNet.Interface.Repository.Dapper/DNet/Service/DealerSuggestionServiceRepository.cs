#region Namespace Imports
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using System.Collections;
using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KTB.DNet.Interface.Repository.Dapper.DNet.Service.SqlQuery.DealerSuggestionService;
using System.Data;
using KTB.DNet.Interface.Model;
using KTB.DNet.Utility;

#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet.Service
{
    public class DealerSuggestionServiceRepository : BaseDNetRepository<DealerSuggestionService>, IDealerSuggestionServiceRepository<DealerSuggestionService, int>
    {
        #region Constructor
        public DealerSuggestionServiceRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        public async Task<List<DealerSuggestionService>> GetSuggestionDealerAsync(string lastServiceDealers, List<DealerSuggestionServiceFavDealerParameterDto> favDealers, List<DealerSuggestionServiceFavDealerParameterDto> selectedDealers,
                                                    decimal custLatitude, decimal custLongitude,
                                                    DateTime requestedDate, short pickupType, List<DealerSuggestionServiceParameterDetailDto> serviceStandardTimes, DateTime checkinTime,
                                                    DateTime checkoutTime, DateTime requestTime, string serviceBookingCode)
        {
            List<DealerSuggestionService> result = new List<DealerSuggestionService>();
            try
            {
                using (var connection = Connection)
                {
                    var data = await connection.QueryAsync<DealerSuggestionService>(DealerSuggestionServiceQuery.GetDealerSuggestion,
                        new
                        {
                            LastServiceDealer = lastServiceDealers,
                            FavDealer = string.Join(",", favDealers.Select(s => s.DealerCode)),
                            SelectedDealer = string.Join(",", selectedDealers.Select(s => s.DealerCode)),
                            CustomerLatitude = custLatitude,
                            CustomerLongitude = custLongitude,
                            RequestedDate = requestedDate,
                            PickupType = pickupType,
                            ServiceStandardTime = CommonFunction.ConvertArrayListToDataTable(
                                new ArrayList(serviceStandardTimes.Select(s => 
                                    new { s.ServiceTypeID, s.VechileModelID, s.VechileTypeID, s.KindCode, s.AssistServiceTypeCode 
                                    }).ToList())).AsTableValuedParameter("dbo.ServiceStandardTimeType"),
                            CheckinTime = checkinTime,
                            CheckoutTime = checkoutTime,
                            RequestTime = requestTime,
                            ServiceBookingCode = serviceBookingCode
                        });

                    return data != null ? data.ToList() : result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Not Implemented

        public DealerSuggestionService Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Create(DealerSuggestionService entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(DealerSuggestionService entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<DealerSuggestionService> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<DealerSuggestionService> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

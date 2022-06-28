using KTB.DNet.Interface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IDealerSuggestionServiceRepository<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
        Task<List<TEntity>> GetSuggestionDealerAsync(string lastServiceDealers, List<DealerSuggestionServiceFavDealerParameterDto> favDealers, List<DealerSuggestionServiceFavDealerParameterDto> selectedDealers,
                                                    decimal custLatitude, decimal custLongitude,
                                                    DateTime requestedDate, short pickupType, List<DealerSuggestionServiceParameterDetailDto> serviceStandardTimes, DateTime checkinTime,
                                                    DateTime checkoutTime, DateTime requestTime, string ServiceBookingCode);
    }
}

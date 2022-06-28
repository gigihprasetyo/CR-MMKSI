using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IEndpointScheduleRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        ResponseMessage AddEndpointSchedule(List<APIEndpointSchedule> listOfEndpointSchedule);
        List<APIEndpointSchedule> GetByEndpointId(int endPointId);
        List<APIEndpointSchedule> GetByEndpointUrl(string endPointUrl);
        List<APIEndpointSchedule> SearchByEndpointId(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount, int endpointId);

    }
}

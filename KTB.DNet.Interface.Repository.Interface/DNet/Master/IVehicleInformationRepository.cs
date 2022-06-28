
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Model;
using System.Collections.Generic;
namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IVehicleInformationRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        List<TEntity> Search(string criteria, string innerQueryCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount);

        List<VWI_VehicleInformationDto> GetByQuery(string query, ICriteria criteria, VWI_VehicleInformationFilterDto filterDto, int pageSize, out int total);
    }
}

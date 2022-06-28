using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IVWI_ChassisStatusFakturRepository<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>Searches the specified chassis number.</summary>
        /// <param name="chassisNumber">The chassis number.</param>
        /// <param name="filteredResultsCount">The filtered results count.</param>
        /// <param name="totalResultsCount">The total results count.</param>
        /// <returns></returns>
        VWI_ChassisStatusFaktur Search(string chassisNumber, out int filteredResultsCount, out int totalResultsCount);

        /// <summary>Searches the list.</summary>
        /// <param name="listChassisNumber">The list chassis number.</param>
        /// <param name="filteredResultsCount">The filtered results count.</param>
        /// <param name="totalResultsCount">The total results count.</param>
        /// <param name="listChassisNumberNull">The list chassis number null.</param>
        /// <returns></returns>
        List<VWI_ChassisStatusFaktur> SearchList(List<string> listChassisNumber, out int filteredResultsCount, out int totalResultsCount, out List<string> listChassisNumberNull);

        /// <summary>Searches the list asynchronous.</summary>
        /// <param name="listChassisNumber">The list chassis number.</param>
        /// <param name="listChassisStatusFaktur">The list chassis status faktur.</param>
        /// <returns></returns>
        Task SearchListAsync(List<string> listChassisNumber, List<VWI_ChassisStatusFaktur> listChassisStatusFaktur);

        /// <summary>Searches the last update time.</summary>
        /// <param name="lastUpdateTime">The last update time.</param>
        /// <param name="dealerCode"></param>
        /// <param name="filteredResultsCount">The filtered results count.</param>
        /// <param name="totalResultsCount">The total results count.</param>
        /// <returns></returns>
        List<VWI_ChassisStatusFaktur> SearchLastUpdateTime(DateTime lastUpdateTime, string dealerCode, out int filteredResultsCount, out int totalResultsCount);
    }
}

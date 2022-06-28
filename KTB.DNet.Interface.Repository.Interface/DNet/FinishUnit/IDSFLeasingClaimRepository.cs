using System;
using System.Collections.Generic;
using KTB.DNet.Domain.Search;
using System.Collections;
using KTB.DNet.Domain;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IDSFLeasingClaimRepository<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
        List<TEntity> Search(ICriteria criteria, ICriteria criteriaDealer, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount);
        List<TEntity> Search(ICriteria criteria);
        bool Save(List<TEntity> data);
        List<ChassisMaster> ListChassisMaster(ICriteria criteria);
        List<DSFLeasingClaimDocument> ListDSFLeasingClaimDocument(ICriteria criteria);
    }

    public interface IDSFLeasingClaimDocumentRepository<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
        List<TEntity> Search(ICriteria criteria, ICriteria criteriaDealer, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount);
        List<TEntity> Search(ICriteria criteria);
        bool Save(TEntity data);
    }

    public interface IChassisMaster<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
        List<TEntity> Search(ICriteria criteria, ICriteria criteriaDealer, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount);
        List<TEntity> Search(ICriteria criteria);
    }
}

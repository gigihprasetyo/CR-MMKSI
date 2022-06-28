using System;
using System.Collections.Generic;
using KTB.DNet.Domain.Search;
using System.Collections;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface ISAPCustomerRepository<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
    }
}

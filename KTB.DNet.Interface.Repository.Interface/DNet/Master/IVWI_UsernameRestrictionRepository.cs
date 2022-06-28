using System.Collections;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IVWI_UsernameRestrictionRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        TEntity Search(string IpAddress);
    }
}

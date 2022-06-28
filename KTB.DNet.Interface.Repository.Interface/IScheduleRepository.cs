using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IScheduleRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        
    }
}

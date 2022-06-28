//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

#region Namespace Imports
using KTB.DNet.Domain.Search;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace KTB.DNet.BusinessValidation.Base
{
    /// <summary>
    /// Abstract class
    /// </summary>
    public abstract class AbstractBusinessValidation
    {
        // intialization
        private ArrayList _domainTypeCollection = new ArrayList();
        private CacheManager cacheManager = CacheFactory.GetCacheManager();

        /// <summary>
        /// Domain type collection property
        /// </summary>
        protected ArrayList DomainTypeCollection
        {
            get
            {
                return _domainTypeCollection;
            }
        }

        /// <summary>
        /// Set task locking
        /// </summary>
        protected void SetTaskLocking()
        {
            foreach (object domainType in _domainTypeCollection)
            {
                cacheManager.Add(this.GetTableName(((Type)(domainType))), true, CacheItemPriority.Normal, null, new AbsoluteTime(DateTime.Now.AddMinutes(2)));
            }
        }

        /// <summary>
        /// Remove task locking
        /// </summary>
        protected void RemoveTaskLocking()
        {
            foreach (object domainType in _domainTypeCollection)
            {
                cacheManager.Remove(this.GetTableName(((Type)(domainType))));
            }

        }

        /// <summary>
        /// Is Task Free checker
        /// </summary>
        /// <returns></returns>
        protected bool IsTaskFree()
        {
            foreach (object domainType in _domainTypeCollection)
            {
                if ((cacheManager[this.GetTableName(((Type)(domainType)))] != null))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Get table Name
        /// </summary>
        /// <param name="DomainType"></param>
        /// <returns></returns>
        private string GetTableName(Type DomainType)
        {
            TableInfoAttribute tableInfoAttr = ((TableInfoAttribute)(Attribute.GetCustomAttribute(DomainType, typeof(TableInfoAttribute))));
            if (tableInfoAttr != null)
            {
                return tableInfoAttr.TableName;
            }
            else
            {
                return String.Empty;
            }

        }



        /// <summary>
        /// Store dealer code information from controller
        /// </summary>
        protected string DealerCode { get; set; }

        /// <summary>
        /// Get or Set Username
        /// </summary>
        protected string UserName { get; set; }


        /// <summary>
        /// Initialize credentials
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="dealerCode"></param>
        public void Initialize(string userName, string dealerCode)
        {
            DealerCode = dealerCode;
            UserName = userName;
        }

    }
}

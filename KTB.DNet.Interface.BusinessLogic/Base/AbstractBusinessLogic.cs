#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AbstractBusinessLogic.cs business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.Domain.Search;
using KTB.DNet.Domain;
using KTB.DNet.Interface.Model;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    /// <summary>
    /// Abstract class
    /// </summary>
    public abstract class AbstractBusinessLogic
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
        /// Populate validation result ErrorMessage
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validationResultList"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected ResponseBase<T> PopulateValidationError<T>(List<DNetValidationResult> validationResultList, T result)
        {
            // define the response
            ResponseBase<T> response = new ResponseBase<T>();
            response.success = false;
            response._id = -1;
            response.total = 0;
            response.lst = result;

            // parse each validation result
            foreach (DNetValidationResult item in validationResultList)
            {
                response.messages.Add(new MessageBase(item.ErrorCode, item.ErrorMessage));
            }

            return response;
        }

        /// <summary>
        /// Validate model attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        protected List<ValidationResult> ValidateModelAttribute<T>(T model)
        {
            // instantiate validator
            ValidationContext validationContext = new ValidationContext(model, serviceProvider: null, items: null);

            var validationResults = new List<ValidationResult>();

            // validate model attribute
            var isValid = Validator.TryValidateObject(model, validationContext, validationResults);

            // return if any errors found
            if (!isValid)
            {
                return validationResults;
            }

            return new List<ValidationResult>();
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
        /// Get loggingUserName
        /// </summary>
        protected string DNetUserName
        {
            get
            {
                return Helper.GetUserName(DealerCode, UserName);
            }
        }

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

        /// <summary>
        /// Get or Set Dealer Company Name
        /// </summary>
        //protected string DealerCompanyName { get; set; }

        /// <summary>
        /// Get or Set Username
        /// </summary>
        //protected List<Dealer> ListDealer { get; set; }

        /// <summary>
        /// Custom Initialize credentials
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="dealerCode"></param>
        //public void CustomInitialize(string userName, string dealerCode, List<Dealer> listDealer, string dealerCompanyName)
        //{
        //    DealerCode = dealerCode;
        //    UserName = userName;
        //    ListDealer = ListDealer;
        //    DealerCompanyName = dealerCompanyName;
        //}

    }
}

#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AppConfig business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 9/11/2018 23:30
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class AppConfigBL : AbstractBusinessLogic, IAppConfigBL
    {
        #region Variables
        private readonly IMapper _appconfigMapper;
        private readonly AutoMapper.IMapper _mapper;
        private const string APP_ID = "KTB.DNet.WebApi";
        #endregion

        #region Constructor
        public AppConfigBL()
        {
            _appconfigMapper = MapperFactory.GetInstance().GetMapper(typeof(AppConfig).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }

        public AppConfigBL(AutoMapper.IMapper mapper)
        {
            _appconfigMapper = MapperFactory.GetInstance().GetMapper(typeof(AppConfig).ToString());
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create a new AppConfig
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<AppConfigDto> Create(AppConfigParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update AppConfig
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<AppConfigDto> Update(AppConfigParameterDto objUpdate)
        {
            return null;
        }

        /// <summary>
        /// Delete AppConfig by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<AppConfigDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Get config for webAPI
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AppConfig GetConfigByName(string name)
        {
            return GetConfigByName(name, APP_ID);
        }

        /// <summary>
        /// Get config by name and appID
        /// </summary>
        /// <param name="name"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public AppConfig GetConfigByName(string name, string appId)
        {
            ResponseBase<AppConfigDto> result = new ResponseBase<AppConfigDto>();
            var criteria = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteria.opAnd(new Criteria(typeof(AppConfig), "Name", MatchType.Exact, name));
            if (!string.IsNullOrEmpty(appId))
                criteria.opAnd(new Criteria(typeof(AppConfig), "AppID", MatchType.Exact, appId));

            ArrayList listOfAppConfig = _appconfigMapper.RetrieveByCriteria(criteria);
            return listOfAppConfig.Count > 0 ? (AppConfig)listOfAppConfig[0] : null;
        }

        /// <summary>
        /// Get config by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ResponseBase<AppConfigDto> GetByName(string name)
        {
            ResponseBase<AppConfigDto> result = new ResponseBase<AppConfigDto>();
            var criteria = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteria.opAnd(new Criteria(typeof(AppConfig), "Name", MatchType.Exact, name));

            ArrayList listOfAppConfig = _appconfigMapper.RetrieveByCriteria(criteria);
            if (listOfAppConfig.Count > 0)
            {
                AppConfig appConfig = (AppConfig)listOfAppConfig[0];

                // map it
                var appconfigDto = _mapper.Map<AppConfigDto>(appConfig);

                result.lst = appconfigDto;
                // return output ID
                result._id = appConfig.ID;
                result.total = 1;
                result.success = true;
            }
            else
            {
                ErrorMsgHelper.DataNotFound(result.messages, typeof(AppConfig), null, "Name", name);
            }

            return result;
        }

        /// <summary>
        /// Get AppConfig by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<AppConfigDto>> Read(AppConfigFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<AppConfigDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                if (filterDto.find != null && filterDto.find.Count > 0)
                {
                    foreach (var filter in filterDto.find)
                    {
                        // handle get all keyword '*'
                        if (filter.PropertyName.Equals("*"))
                            continue;

                        PropertyInfo propInfo = typeof(AppConfig).GetProperty(filter.PropertyName);
                        switch (filter.SqlOperation)
                        {
                            case SQLOperation.And:
                                criterias.opAnd(new Criteria(typeof(AppConfig), filter.PropertyName, filter.MatchType, Helper.GetPropertyValue(propInfo, filter.PropertyValue)));
                                break;
                            case SQLOperation.Or:
                                criterias.opOr(new Criteria(typeof(AppConfig), filter.PropertyName, filter.MatchType, Helper.GetPropertyValue(propInfo, filter.PropertyValue)));
                                break;
                        }
                    }
                }

                // populate the sort info
                if (filterDto.sort != null && filterDto.sort.Count > 0)
                {
                    foreach (var sort in filterDto.sort)
                    {
                        // handle get all keyword '*'
                        if (sort.SortColumn.Equals("*"))
                            continue;

                        sortColl.Add(new Sort(typeof(AppConfig), sort.SortColumn, sort.SortDirection));
                    }
                }

                // validate sort column
                if (filterDto.sort == null || sortColl.Count == 0)
                    sortColl = null;

                var data = _appconfigMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<AppConfig>().ToList();
                    var listData = list.Select(item => _mapper.Map<AppConfigDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(AppConfig), filterDto);
                }

                result.success = true;

            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlExceptionRead(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }
        #endregion
    }
}


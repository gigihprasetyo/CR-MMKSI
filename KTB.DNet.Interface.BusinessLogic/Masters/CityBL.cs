#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : City business logic class
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
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class CityBL : AbstractBusinessLogic, ICityBL
    {
        #region Variables
        private readonly IMapper _cityDomainMapper;
        private readonly IMapper _cityMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public CityBL()
        {
            _cityMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_City).ToString());
            _cityDomainMapper = MapperFactory.GetInstance().GetMapper(typeof(City).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        public CityBL(AutoMapper.IMapper mapper)
        {
            _cityMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_City).ToString());
            _cityDomainMapper = MapperFactory.GetInstance().GetMapper(typeof(City).ToString());
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get City by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<CityDto>> Read(CityFilterDto filterDto, int pageSize)
        {
            return null;
        }

        /// <summary>
        /// Get City by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CityDto>> Get(CityFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var result = new ResponseBase<List<VWI_CityDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                var criterias = Helper.BuildCriteria(typeof(VWI_City), filterDto);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_City), filterDto, sortColl);

                // get data
                var data = _cityMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_City>().ToList();
                    var listData = list.Select(item => _mapper.Map<VWI_CityDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_City), filterDto);
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

        /// <summary>
        /// Delete City by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<CityDto> Delete(int id)
        {
            var result = new ResponseBase<CityDto>();

            try
            {
                var city = (City)_cityMapper.Retrieve(id);
                if (city != null)
                {
                    city.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _cityMapper.Update(city, DNetUserName);
                    if (nResult != 0)
                    {
                        result.success = true;
                        result._id = id;
                        result.total = 1;
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                    }
                }
                else
                {
                    ErrorMsgHelper.DeleteNotAvailable(result.messages);
                }
            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Create a new City
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<CityDto> Create(CityParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update City
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<CityDto> Update(CityParameterDto objUpdate)
        {
            return null;
        }

        /// <summary>
        /// Get city by code
        /// </summary>
        /// <param name="provinceID"></param>
        /// <param name="cityCode"></param>
        /// <returns></returns>
        internal City GetCityByProvinceCityCode(int provinceID, string cityCode)
        {
            City result = null;
            var criterias = new CriteriaComposite(new Criteria(typeof(City), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(City), "Province.ID", MatchType.Exact, provinceID));
            criterias.opAnd(new Criteria(typeof(City), "CityCode", cityCode));

            var data = _cityDomainMapper.RetrieveByCriteria(criterias);
            if (data != null && data.Count > 0)
            {
                result = data[0] as City;
            }

            return result;
        }
        #endregion
    }
}


#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Province business logic class
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
    public class ProvinceBL : AbstractBusinessLogic, IProvinceBL
    {
        #region Variables
        private readonly IMapper _provinceMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public ProvinceBL()
        {
            _provinceMapper = MapperFactory.GetInstance().GetMapper(typeof(Province).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        public ProvinceBL(AutoMapper.IMapper mapper)
        {
            _provinceMapper = MapperFactory.GetInstance().GetMapper(typeof(Province).ToString());
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get Province by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<ProvinceDto>> Read(ProvinceFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(Province), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<ProvinceDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(Province), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(Province), filterDto, sortColl);

                // get data
                var data = _provinceMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<Province>().ToList();
                    var listData = list.Select(item => _mapper.Map<ProvinceDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(Province), filterDto);
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
        /// Delete Province by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<ProvinceDto> Delete(int id)
        {
            var result = new ResponseBase<ProvinceDto>();

            try
            {
                var province = (Province)_provinceMapper.Retrieve(id);
                if (province != null)
                {
                    province.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _provinceMapper.Update(province, DNetUserName);
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
        /// Create a new Province
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<ProvinceDto> Create(ProvinceParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update Province
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<ProvinceDto> Update(ProvinceParameterDto objUpdate)
        {
            return null;
        }

        /// <summary>
        /// Get province by code
        /// </summary>
        /// <param name="provinceCode"></param>
        /// <returns></returns>
        internal Province GetProvinceByCode(string provinceCode)
        {
            Province result = null;
            var data = _provinceMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(Province), "ProvinceCode", provinceCode));
            if (data.Count > 0)
            {
                result = data[0] as Province;
            }

            return result;
        }
        #endregion
    }
}


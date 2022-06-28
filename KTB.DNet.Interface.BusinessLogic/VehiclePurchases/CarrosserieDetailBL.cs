#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CarrosserieDetail business logic class
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
    public class CarrosserieDetailBL : AbstractBusinessLogic, ICarrosserieDetailBL
    {
        #region Variables
        private readonly IMapper _carrosserieDetailMapper;
        private readonly AutoMapper.IMapper _mapper;

        #endregion

        #region Constructor
        public CarrosserieDetailBL()
        {
            _carrosserieDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(CarrosserieDetail).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get CarrosserieDetail by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<CarrosserieDetailDto>> Read(CarrosserieDetailFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(CarrosserieDetail), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<CarrosserieDetailDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(CarrosserieDetail), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(CarrosserieDetail), filterDto, sortColl);

                // get data
                var data = _carrosserieDetailMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<CarrosserieDetail>().ToList();
                    var listData = list.Select(item => _mapper.Map<CarrosserieDetailDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(CarrosserieDetail), filterDto);
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
        /// Delete CarrosserieDetail by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<CarrosserieDetailDto> Delete(int id)
        {
            var result = new ResponseBase<CarrosserieDetailDto>();

            try
            {
                var carrosseriedetail = (CarrosserieDetail)_carrosserieDetailMapper.Retrieve(id);
                if (carrosseriedetail != null)
                {
                    carrosseriedetail.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _carrosserieDetailMapper.Update(carrosseriedetail, DNetUserName);
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
        /// Create a new CarrosserieDetail
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<CarrosserieDetailDto> Create(CarrosserieDetailParameterDto objCreate)
        {
            var result = new ResponseBase<CarrosserieDetailDto>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                // create CarrosserieDetail object
                var newCarrosserieDetail = _mapper.Map<CarrosserieDetail>(objCreate);
                newCarrosserieDetail.CreatedTime = DateTime.Now;

                var success = (int)_carrosserieDetailMapper.Insert(newCarrosserieDetail, newCarrosserieDetail.CreatedBy);
                if (success > 0)
                {
                    result.success = true;
                    result._id = success;
                    result.total = 1;
                }
                else
                {
                    ErrorMsgHelper.DataCorrupt(result.messages);
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
        /// Update CarrosserieDetail
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<CarrosserieDetailDto> Update(CarrosserieDetailParameterDto objUpdate)
        {
            var result = new ResponseBase<CarrosserieDetailDto>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                //Process update
                var newCarrosserieDetail = _mapper.Map<CarrosserieDetail>(objUpdate);
                newCarrosserieDetail.LastUpdateTime = DateTime.Now;

                var success = (int)_carrosserieDetailMapper.Update(newCarrosserieDetail, newCarrosserieDetail.CreatedBy);
                if (success > 0)
                {
                    result.success = true;
                    result._id = success;
                    result.total = 1;
                }
                else
                {
                    ErrorMsgHelper.UpdateNotAvailable(result.messages);
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
        #endregion
    }
}


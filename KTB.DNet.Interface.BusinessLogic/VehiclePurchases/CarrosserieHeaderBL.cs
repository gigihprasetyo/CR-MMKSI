#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CarrosserieHeader business logic class
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
    public class CarrosserieHeaderBL : AbstractBusinessLogic, ICarrosserieHeaderBL
    {
        #region Variables
        private readonly IMapper _carrosserieHeaderMapper;
        private readonly AutoMapper.IMapper _mapper;

        #endregion

        #region Constructor
        public CarrosserieHeaderBL()
        {
            _carrosserieHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(CarrosserieHeader).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get CarrosserieHeader by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<CarrosserieHeaderDto>> Read(CarrosserieHeaderFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(CarrosserieHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<CarrosserieHeaderDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(CarrosserieHeader), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(CarrosserieHeader), filterDto, sortColl);

                // get data
                var data = _carrosserieHeaderMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<CarrosserieHeader>().ToList();
                    var listData = list.Select(item => _mapper.Map<CarrosserieHeaderDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(CarrosserieHeader), filterDto);
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
        /// Delete CarrosserieHeader by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<CarrosserieHeaderDto> Delete(int id)
        {
            var result = new ResponseBase<CarrosserieHeaderDto>();

            try
            {
                var carrosserieheader = (CarrosserieHeader)_carrosserieHeaderMapper.Retrieve(id);
                if (carrosserieheader != null)
                {
                    carrosserieheader.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _carrosserieHeaderMapper.Update(carrosserieheader, DNetUserName);
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
        /// Create a new CarrosserieHeader
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<CarrosserieHeaderDto> Create(CarrosserieHeaderParameterDto objCreate)
        {
            var result = new ResponseBase<CarrosserieHeaderDto>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                // create CarrosserieHeader object
                var newCarrosserieHeader = _mapper.Map<CarrosserieHeader>(objCreate);
                newCarrosserieHeader.CreatedTime = DateTime.Now;

                var success = (int)_carrosserieHeaderMapper.Insert(newCarrosserieHeader, newCarrosserieHeader.CreatedBy);
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
        /// Update CarrosserieHeader
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<CarrosserieHeaderDto> Update(CarrosserieHeaderParameterDto objUpdate)
        {
            var result = new ResponseBase<CarrosserieHeaderDto>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                //Process update
                var newCarrosserieHeader = _mapper.Map<CarrosserieHeader>(objUpdate);
                newCarrosserieHeader.LastUpdateTime = DateTime.Now;

                var success = (int)_carrosserieHeaderMapper.Update(newCarrosserieHeader, newCarrosserieHeader.CreatedBy);
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


#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : LaborMaster business logic class
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
    public class LaborMasterBL : AbstractBusinessLogic, ILaborMasterBL
    {
        #region Variables
        private readonly IMapper _labormasterMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public LaborMasterBL()
        {
            _labormasterMapper = MapperFactory.GetInstance().GetMapper(typeof(LaborMaster).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get LaborMaster by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<LaborMasterDto>> Read(LaborMasterFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(LaborMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<LaborMasterDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(LaborMaster), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(LaborMaster), filterDto, sortColl);

                // get data
                var data = _labormasterMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<LaborMaster>().ToList();
                    var listData = list.Select(item => _mapper.Map<LaborMasterDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(LaborMaster), filterDto);
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
        /// Delete LaborMaster by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<LaborMasterDto> Delete(int id)
        {
            var result = new ResponseBase<LaborMasterDto>();

            try
            {
                var labormaster = (LaborMaster)_labormasterMapper.Retrieve(id);
                if (labormaster != null)
                {
                    labormaster.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _labormasterMapper.Update(labormaster, DNetUserName);
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
        /// Create a new LaborMaster
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<LaborMasterDto> Create(LaborMasterParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update LaborMaster
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<LaborMasterDto> Update(LaborMasterParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}


#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ChassisMaster business logic class
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
    public class ChassisMasterBL : AbstractBusinessLogic, IChassisMasterBL
    {
        #region Variables
        private readonly IMapper _chassismasterMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public ChassisMasterBL()
        {
            _chassismasterMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get ChassisMaster by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<ChassisMasterDto>> Read(ChassisMasterFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(ChassisMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(ChassisMaster), "Dealer.DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<ChassisMasterDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(ChassisMaster), filterDto, criterias, true);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(ChassisMaster), filterDto, sortColl);

                // get data
                var data = _chassismasterMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<ChassisMaster>().ToList();
                    var listData = list.Select(item => _mapper.Map<ChassisMasterDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(ChassisMaster), filterDto);
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
        /// Delete ChassisMaster by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<ChassisMasterDto> Delete(int id)
        {
            var result = new ResponseBase<ChassisMasterDto>();

            try
            {
                var chassismaster = (ChassisMaster)_chassismasterMapper.Retrieve(id);
                if (chassismaster != null)
                {
                    chassismaster.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _chassismasterMapper.Update(chassismaster, DNetUserName);
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
        /// Create a new ChassisMaster
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<ChassisMasterDto> Create(ChassisMasterParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update ChassisMaster
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<ChassisMasterDto> Update(ChassisMasterParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}


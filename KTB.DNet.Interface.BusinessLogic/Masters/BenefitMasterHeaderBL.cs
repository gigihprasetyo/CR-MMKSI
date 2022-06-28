#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : BenefitMasterHeader business logic class
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
    public class BenefitMasterHeaderBL : AbstractBusinessLogic, IBenefitMasterHeaderBL
    {
        #region Variables
        private readonly IMapper _benefitmasterheaderMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public BenefitMasterHeaderBL()
        {
            _benefitmasterheaderMapper = MapperFactory.GetInstance().GetMapper(typeof(BenefitMasterHeader).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get BenefitMasterHeader by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<BenefitMasterHeaderDto>> Read(BenefitMasterHeaderFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(BenefitMasterHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<BenefitMasterHeaderDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(BenefitMasterHeader), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(BenefitMasterHeader), filterDto, sortColl);

                // get data
                var data = _benefitmasterheaderMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<BenefitMasterHeader>().ToList();
                    var listData = list.Select(item => _mapper.Map<BenefitMasterHeaderDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(BenefitMasterHeader), filterDto);
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
        /// Delete BenefitMasterHeader by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<BenefitMasterHeaderDto> Delete(int id)
        {
            var result = new ResponseBase<BenefitMasterHeaderDto>();

            try
            {
                var benefitmasterheader = (BenefitMasterHeader)_benefitmasterheaderMapper.Retrieve(id);
                if (benefitmasterheader != null)
                {
                    benefitmasterheader.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _benefitmasterheaderMapper.Update(benefitmasterheader, DNetUserName);
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
                return result;
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
                return result;
            }

            return result;
        }

        /// <summary>
        /// Create a new BenefitMasterHeader
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<BenefitMasterHeaderDto> Create(BenefitMasterHeaderParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update BenefitMasterHeader
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<BenefitMasterHeaderDto> Update(BenefitMasterHeaderParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}


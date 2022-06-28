#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : BenefitType business logic class
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
    public class BenefitTypeBL : AbstractBusinessLogic, IBenefitTypeBL
    {
        #region Variables
        private readonly IMapper _benefittypeMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public BenefitTypeBL()
        {
            _benefittypeMapper = MapperFactory.GetInstance().GetMapper(typeof(BenefitType).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get BenefitType by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<BenefitTypeDto>> Read(BenefitTypeFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(BenefitType), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<BenefitTypeDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(BenefitType), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(BenefitType), filterDto, sortColl);

                // get data
                var data = _benefittypeMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<BenefitType>().ToList();
                    var listData = list.Select(item => _mapper.Map<BenefitTypeDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(BenefitType), filterDto);
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
        /// Delete BenefitType by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<BenefitTypeDto> Delete(int id)
        {
            var result = new ResponseBase<BenefitTypeDto>();

            try
            {
                var benefittype = (BenefitType)_benefittypeMapper.Retrieve(id);
                if (benefittype != null)
                {
                    benefittype.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _benefittypeMapper.Update(benefittype, DNetUserName);
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
        /// Create a new BenefitType
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<BenefitTypeDto> Create(BenefitTypeParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update BenefitType
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<BenefitTypeDto> Update(BenefitTypeParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}


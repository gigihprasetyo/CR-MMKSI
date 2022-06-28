#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : DealerBranch business logic class
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
    public class DealerBranchBL : AbstractBusinessLogic, IDealerBranchBL
    {
        #region Variables
        private readonly IMapper _dealerbranchMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public DealerBranchBL()
        {
            _dealerbranchMapper = MapperFactory.GetInstance().GetMapper(typeof(DealerBranch).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get DealerBranch by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<DealerBranchDto>> Read(DealerBranchFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(DealerBranch), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<DealerBranchDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(DealerBranch), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(DealerBranch), filterDto, sortColl);

                // get data
                var data = _dealerbranchMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<DealerBranch>().ToList();
                    var listData = new List<DealerBranchDto>();
                    foreach (var item in list)
                    {
                        // map it
                        var dealerbranchDto = _mapper.Map<DealerBranchDto>(item);

                        if (item.City != null)
                        {
                            dealerbranchDto.City = _mapper.Map<CityDto>(item.City);
                        }
                        if (item.Province != null)
                        {
                            dealerbranchDto.Province = _mapper.Map<ProvinceDto>(item.Province);
                        }

                        // add to list
                        listData.Add(dealerbranchDto);
                    }

                    result.lst = listData;
                    // return output _id                    
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(DealerBranch), filterDto);
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
        /// Delete DealerBranch by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<DealerBranchDto> Delete(int id)
        {
            var result = new ResponseBase<DealerBranchDto>();

            try
            {
                var dealerbranch = (DealerBranch)_dealerbranchMapper.Retrieve(id);
                if (dealerbranch != null)
                {
                    dealerbranch.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _dealerbranchMapper.Update(dealerbranch, DNetUserName);
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
        /// Create a new DealerBranch
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<DealerBranchDto> Create(DealerBranchParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update DealerBranch
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<DealerBranchDto> Update(DealerBranchParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}


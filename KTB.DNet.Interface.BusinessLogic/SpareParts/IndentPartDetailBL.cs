#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ndentPartDetail business logic class
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
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class IndentPartDetailBL : AbstractBusinessLogic, IIndentPartDetailBL
    {
        #region Variables
        private readonly IMapper _indentpartdetailMapper;
        private readonly IMapper _indentpartHeaderMapper;
        private readonly IMapper _sparePartMasterMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public IndentPartDetailBL()
        {
            _indentpartdetailMapper = MapperFactory.GetInstance().GetMapper(typeof(IndentPartDetail).ToString());
            _indentpartHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(IndentPartHeader).ToString());
            _sparePartMasterMapper = MapperFactory.GetInstance().GetMapper(typeof(SparePartMaster).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        public IndentPartDetailBL(AutoMapper.IMapper mapper)
        {
            _indentpartdetailMapper = MapperFactory.GetInstance().GetMapper(typeof(IndentPartDetail).ToString());
            _indentpartHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(IndentPartHeader).ToString());
            _sparePartMasterMapper = MapperFactory.GetInstance().GetMapper(typeof(SparePartMaster).ToString());
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get IndentPartDetail by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<IndentPartDetailDto>> Read(IndentPartDetailFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(IndentPartDetail), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<IndentPartDetailDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(IndentPartDetail), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(IndentPartDetail), filterDto, sortColl);

                // get data
                var data = _indentpartdetailMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<IndentPartDetail>().ToList();
                    var listData = new List<IndentPartDetailDto>();
                    foreach (var item in list)
                    {
                        // map it
                        var indentpartdetailDto = _mapper.Map<IndentPartDetailDto>(item);

                        if (item.SparePartMaster != null)
                        {
                            indentpartdetailDto.SparePartMaster = _mapper.Map<SparePartMasterDto>(item.SparePartMaster);
                        }
                        if (item.IndentPartHeader != null)
                        {
                            indentpartdetailDto.IndentPartHeader = _mapper.Map<IndentPartHeaderDto>(item.IndentPartHeader);
                        }

                        // add to list
                        listData.Add(indentpartdetailDto);
                    }

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(IndentPartDetail), filterDto);
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
        /// Delete IndentPartDetail by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<IndentPartDetailDto> Delete(int id)
        {
            var result = new ResponseBase<IndentPartDetailDto>();

            try
            {
                var indentpartdetail = (IndentPartDetail)_indentpartdetailMapper.Retrieve(id);
                if (indentpartdetail != null)
                {
                    indentpartdetail.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _indentpartdetailMapper.Update(indentpartdetail, DNetUserName);
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
        /// Create a new IndentPartDetail
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<IndentPartDetailDto> Create(IndentPartDetailParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update IndentPartDetail
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<IndentPartDetailDto> Update(IndentPartDetailParameterDto objUpdate)
        {
            return null;
        }

        /// <summary>
        /// Validate parameter
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="indentPartDetail"></param>
        /// <returns></returns>
        public List<DNetValidationResult> ValidateCreateParameterDto(IndentPartDetailParameterDto objCreate, out IndentPartDetail indentPartDetail)
        {
            indentPartDetail = new IndentPartDetail();
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();

            if (objCreate.ID > 0)
            {
                IndentPartDetail detailOnDB = (IndentPartDetail)_indentpartdetailMapper.Retrieve(objCreate.ID);
                if (detailOnDB == null)
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.DataUpdateNotAvailable, MessageResource.ErrorMsgDataUpdateNotAvailable + string.Format(MessageResource.ErrorMsgDataNotFound, FieldResource.IndentPartDetail)));
                }
            }

            #region Validate Spare Part master
            SparePartMasterBL spMasterBL = new SparePartMasterBL(_mapper);
            // get sparepart
            //SparePartMaster sparePartMaster = spMasterBL.GetByPartNumberActiveStatus(objCreate.PartNumber);
            SparePartMaster sparePartMaster = spMasterBL.GetValidateSparePartActive(objCreate.PartNumber, validationResults);
            if (sparePartMaster != null)
            {
                objCreate.SparePartMasterID = sparePartMaster.ID;
                objCreate.Price = sparePartMaster.RetalPrice;
            }
            //else
            //{
            //    validationResults.Add(new DNetValidationResult(FieldResource.PartNumber + " " + string.Format(MessageResource.ErrorMsgDataInvalid, objCreate.PartNumber)));
            //}
            #endregion

            indentPartDetail = _mapper.Map<IndentPartDetail>(objCreate);
            indentPartDetail.SparePartMaster = sparePartMaster;

            return validationResults;
        }
        #endregion
    }
}


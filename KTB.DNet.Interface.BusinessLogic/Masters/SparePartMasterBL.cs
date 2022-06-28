#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartMaster business logic class
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
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class SparePartMasterBL : AbstractBusinessLogic, ISparePartMasterBL
    {
        #region Variables
        private readonly IMapper _sparepartmasterMapper;
        private readonly AutoMapper.IMapper _mapper;
        private StandardCodeBL _enumBL;
        #endregion

        #region Constructor
        public SparePartMasterBL()
        {
            _sparepartmasterMapper = MapperFactory.GetInstance().GetMapper(typeof(SparePartMaster).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
        }
        public SparePartMasterBL(AutoMapper.IMapper mapper)
        {
            _sparepartmasterMapper = MapperFactory.GetInstance().GetMapper(typeof(SparePartMaster).ToString());
            _mapper = mapper;
            _enumBL = new StandardCodeBL(mapper);
        }
        #endregion

        #region Public Methods

        #region For API Purpose
        /// <summary>
        /// Get SparePartMaster by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SparePartMasterDto>> Read(SparePartMasterFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(SparePartMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(SparePartMaster), "ProductCategory.ID", MatchType.Exact, 1), "(", true);
            criterias.opOr(new Criteria(typeof(SparePartMaster), "ProductCategory.ID", MatchType.Exact, 3), ")", false);
            var result = new ResponseBase<List<SparePartMasterDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(SparePartMaster), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(SparePartMaster), filterDto, sortColl);

                // get fixed price setting                
                var setting = new AppConfigBL().GetConfigByName("SparePartProductFixedPrice");
                bool fixedPriceStatus;
                if (setting == null || !bool.TryParse(setting.Value, out fixedPriceStatus))
                {
                    result.messages.Add(new MessageBase { ErrorMessage = string.Format(MessageResource.ErrorMsgDataInvalid, "SparePartProductFixedPrice") });
                    return result;
                }

                // get data
                var data = _sparepartmasterMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<SparePartMaster>().ToList();
                    var listData = new List<SparePartMasterDto>();
                    foreach (var item in list)
                    {
                        // map it
                        var sparepartmasterDto = _mapper.Map<SparePartMasterDto>(item);

                        // get alt part number ref and its name if any
                        UpdateAltPartNumberReff(filterDto, ref sparepartmasterDto);

                        // set the status 
                        UpdateStatusBasedOnRowStatus(ref sparepartmasterDto);

                        // get fixed price status
                        sparepartmasterDto.SparePartProductFixedPrice = fixedPriceStatus;

                        // add to list
                        listData.Add(sparepartmasterDto);
                    }

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartMaster), filterDto);
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
        /// Delete SparePartMaster by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<SparePartMasterDto> Delete(int id)
        {
            var result = new ResponseBase<SparePartMasterDto>();

            try
            {
                var sparepartmaster = (SparePartMaster)_sparepartmasterMapper.Retrieve(id);
                if (sparepartmaster != null)
                {
                    sparepartmaster.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _sparepartmasterMapper.Update(sparepartmaster, DNetUserName);
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
        /// Create a new SparePartMaster
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<SparePartMasterDto> Create(SparePartMasterParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update SparePartMaster
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<SparePartMasterDto> Update(SparePartMasterParameterDto objUpdate)
        {
            return null;
        }

        #endregion

        #region For BL Purpose

        /// <summary>
        /// Get by part number and model code
        /// </summary>
        /// <param name="partNumber"></param>
        /// <param name="modelCode"></param>
        /// <returns></returns>
        public SparePartMaster GetByPartNumberAndModelCode(string partNumber, string modelCode)
        {
            ArrayList arrayListResult = new ArrayList();
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(SparePartMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(SparePartMaster), "PartNumber", MatchType.Exact, partNumber));
            criterias.opAnd(new Criteria(typeof(SparePartMaster), "ModelCode", MatchType.Exact, modelCode));
            arrayListResult = _sparepartmasterMapper.RetrieveByCriteria(criterias);

            if (arrayListResult.Count > 0)
            {
                return (SparePartMaster)arrayListResult[0];
            }

            return null;
        }

        /// <summary>
        /// GetDomainByPartNumber 
        /// </summary>
        /// <param name="partNumber"></param>
        /// <returns></returns>
        public ResponseBase<SparePartMaster> GetDomainByPartNumber(string partNumber)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(SparePartMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(SparePartMaster), "PartNumber", MatchType.Exact, partNumber));
            var result = new ResponseBase<SparePartMaster>();

            try
            {
                var data = _sparepartmasterMapper.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    var list = data.Cast<SparePartMaster>().ToList();
                    var resultData = new SparePartMaster();
                    foreach (var item in list)
                    {
                        // add to list
                        resultData = item;
                    };

                    result.lst = resultData;
                    // return output _id
                    result._id = resultData.ID;
                    result.total = 1;
                    result.success = true;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartMaster), null, "PartNumber", partNumber);
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

        /// <summary></summary>
        /// <param name="partNumber"></param>
        public SparePartMaster GetPartByPartNumberRowStatus(string partNumber)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(SparePartMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(SparePartMaster), "PartNumber", MatchType.Exact, partNumber));
            criterias.opAnd(new Criteria(typeof(SparePartMaster), "ProductCategory.ID", MatchType.InSet, "(1,3)"));

            var arrayListResult = _sparepartmasterMapper.RetrieveByCriteria(criterias);
            if (arrayListResult.Count > 0)
            {
                return (SparePartMaster)arrayListResult[0];
            }

            return null;
        }

        public SparePartMaster GetPartByPartNumber(string partNumber)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(SparePartMaster), "PartNumber", MatchType.Exact, partNumber));
            criterias.opAnd(new Criteria(typeof(SparePartMaster), "ProductCategory.ID", MatchType.InSet, "(1,3)"));

            var arrayListResult = _sparepartmasterMapper.RetrieveByCriteria(criterias);
            if (arrayListResult.Count > 0)
            {
                return (SparePartMaster)arrayListResult[0];
            }

            return null;
        }

        public DNetValidationResult ValidateSparePartActive(string partNumber)
        {
            var activePartStatus = _enumBL.GetByCategoryAndCode("EnumSparePartActiveStatus.SparePartActiveStatus", "Active");
            DNetValidationResult result = null;
            SparePartMaster sparePartMasterData = null;
            sparePartMasterData = this.GetPartByPartNumber(partNumber);
            if (sparePartMasterData != null)
            {
                if (sparePartMasterData.RowStatus != -1)
                {
                    if (sparePartMasterData.ActiveStatus == activePartStatus.ValueId)
                    {
                        result = null;
                    }
                    else
                    {
                        result = new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartNotActive, partNumber));
                    }
                }
                else
                    result = new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartNotActive, partNumber));
            }
            else
            {
                result = new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartNotFound, partNumber));
            }
            return result;
        }

        public SparePartMaster GetValidateSparePartActive(string partNumber, List<DNetValidationResult> validationResults)
        {
            var activePartStatus = _enumBL.GetByCategoryAndCode("EnumSparePartActiveStatus.SparePartActiveStatus", "Active");
            SparePartMaster sparePartMasterData = null;
            sparePartMasterData = this.GetPartByPartNumber(partNumber);
            if (sparePartMasterData != null)
            {
                if (sparePartMasterData.RowStatus != -1)
                {
                    if (sparePartMasterData.ActiveStatus != activePartStatus.ValueId)
                    {
                        sparePartMasterData = null;
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartNotActive, partNumber)));
                    }
                }
                else
                {
                    sparePartMasterData = null;
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartNotActive, partNumber)));
                }
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartNotFound, partNumber)));
            }

            return sparePartMasterData;
        }

        /// <summary>
        /// Get By Part Number
        /// </summary>
        /// <param name="partNumber"></param>
        /// <returns></returns>
        public SparePartMaster GetActivePartByPartNumber(string partNumber)
        {
            var activePartStatus = _enumBL.GetByCategoryAndCode("EnumSparePartActiveStatus.SparePartActiveStatus", "Active");
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(SparePartMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(SparePartMaster), "PartNumber", MatchType.Exact, partNumber));
            criterias.opAnd(new Criteria(typeof(SparePartMaster), "ActiveStatus", MatchType.Exact, activePartStatus.ValueId));

            var arrayListResult = _sparepartmasterMapper.RetrieveByCriteria(criterias);
            if (arrayListResult.Count > 0)
            {
                return (SparePartMaster)arrayListResult[0];
            }

            return null;
        }

        /// <summary>
        /// Get the valid spare part by its type code
        /// </summary>
        /// <param name="partNumber"></param>
        /// <returns></returns>
        public SparePartMaster GetValidPartByPartNumber(string partNumber)
        {
            var sparepart = GetActivePartByPartNumber(partNumber);
            if (sparepart == null)
            {
                return null;
            }
            else
            {
                // define excluded type
                var invalidTypeCode = new List<string> { "A", "I", "E", "P" };

                // validate the type code
                if (!invalidTypeCode.Contains(sparepart.TypeCode, StringComparer.OrdinalIgnoreCase))
                {
                    return sparepart;
                }
            }

            return null;
        }

        public SparePartMaster GetValidPartByPartNumber(string partNumber, List<DNetValidationResult> validationResults)
        {
            //var sparepart = GetActivePartByPartNumber(partNumber);
            var sparepart = GetValidateSparePartActive(partNumber, validationResults);
            if (sparepart == null)
            {
                return null;
            }
            else
            {
                // define excluded type
                var invalidTypeCode = new List<string> { "A", "I", "E", "P" };

                // validate the type code
                if (!invalidTypeCode.Contains(sparepart.TypeCode, StringComparer.OrdinalIgnoreCase))
                {
                    return sparepart;
                }
                else
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartNotAllowedByTypeCode, "A, I, E, P", sparepart.PartNumber, sparepart.TypeCode)));
                }
            }

            return null;
        }

        /// <summary>
        /// Update Status field
        /// </summary>
        /// <param name="sparepartmasterDto"></param>
        private void UpdateStatusBasedOnRowStatus(ref SparePartMasterDto sparepartmasterDto)
        {
            if (sparepartmasterDto.Status == 0 && sparepartmasterDto.RowStatus == 0)
            {
                sparepartmasterDto.Status = 0;
            }
            else
            {
                sparepartmasterDto.Status = -1;
            }
        }

        /// <summary>
        /// Get the part number reff if any
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortColl"></param>
        /// <param name="totalRow"></param>
        /// <param name="sparepartmasterDto"></param>
        /// <returns></returns>
        private void UpdateAltPartNumberReff(SparePartMasterFilterDto filterDto, ref SparePartMasterDto sparePartMasterDto)
        {
            // set default values
            sparePartMasterDto.AltPartNumberReffName = string.Empty;
            sparePartMasterDto.AltPartNumberReff = string.Empty;

            if (!string.IsNullOrEmpty(sparePartMasterDto.AltPartNumber))
            {
                var newCriterias = new CriteriaComposite(new Criteria(typeof(SparePartMaster), "PartNumber", MatchType.Exact, sparePartMasterDto.AltPartNumber));
                var spareparts = _sparepartmasterMapper.RetrieveByCriteria(newCriterias);
                if (spareparts.Count > 0)
                {
                    // get the alt part number ref
                    SparePartMaster sparePartMaster = spareparts[0] as SparePartMaster;
                    sparePartMasterDto.AltPartNumberReff = sparePartMaster.PartNumberReff;

                    // get the alt part number ref name
                    if (!string.IsNullOrEmpty(sparePartMaster.PartNumberReff))
                    {
                        var tempCriterias = new CriteriaComposite(new Criteria(typeof(SparePartMaster), "PartNumber", MatchType.Exact, sparePartMaster.PartNumberReff));
                        var sparepartsTemp = _sparepartmasterMapper.RetrieveByCriteria(tempCriterias);
                        if (sparepartsTemp.Count > 0)
                        {
                            sparePartMasterDto.AltPartNumberReffName = (sparepartsTemp[0] as SparePartMaster).PartName;
                        }
                    }
                }
            }
        }
        #endregion

        #endregion
    }
}


#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PartShop business logic class
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
    public class PartShopBL : AbstractBusinessLogic, IPartShopBL
    {
        #region Variables
        private readonly IMapper _partshopMapper;
        private readonly IMapper _vpartshopMapper;
        private readonly IMapper _citypartMapper;
        private readonly AutoMapper.IMapper _mapper;
        private AppConfigBL _appConfigBL;
        #endregion

        #region Constructor
        public PartShopBL()
        {
            _partshopMapper = MapperFactory.GetInstance().GetMapper(typeof(PartShop).ToString());
            _vpartshopMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_PartShop).ToString());
            _citypartMapper = MapperFactory.GetInstance().GetMapper(typeof(CityPart).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _appConfigBL = new AppConfigBL(_mapper);
        }
        #endregion

        #region Public Methods
        public ResponseBase<List<PartShopDto>> Read(PartShopFilterDto filterDto, int pageSize)
        {
            return null;
        }
        /// <summary>
        /// Get PartShop by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VPartShopDto>> ReadView(PartShopFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only            
            var result = new ResponseBase<List<VPartShopDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias                
                var criterias = Helper.BuildCriteria(typeof(VWI_PartShop), filterDto);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_PartShop), filterDto, sortColl);

                // get data
                var data = _vpartshopMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_PartShop>().ToList();
                    var listData = list.Select(item => _mapper.Map<VPartShopDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_PartShop), filterDto);
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
        /// Delete PartShop by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<PartShopDto> Delete(int id)
        {
            var result = new ResponseBase<PartShopDto>();

            try
            {
                var partshop = (PartShop)_partshopMapper.Retrieve(id);
                if (partshop != null)
                {
                    partshop.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _partshopMapper.Update(partshop, DNetUserName);
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
        /// Create a new PartShop
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<PartShopDto> Create(PartShopParameterDto objCreate)
        {
            var result = new ResponseBase<PartShopDto>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                PartShop partShop = _mapper.Map<PartShop>(objCreate);

                ValidateCreatePartShop(objCreate, partShop, validationResults);

                bool isValid = validationResults.Count == 0;

                if (isValid)
                {
                    var id = (int)_partshopMapper.Insert(partShop, DNetUserName);
                    result.success = id > 0;
                    if (!result.success) ErrorMsgHelper.DataCorrupt(result.messages);
                    result._id = id;
                    result.total = 1;

                    if (!MailUtility.SendPartShopEmail(_mapper, partShop, null, partShop.Dealer, validationResults, false))
                    {
                        result.success = false;
                        return PopulateValidationError<PartShopDto>(validationResults, null);
                    }
                }
                else
                {
                    return PopulateValidationError<PartShopDto>(validationResults, null);
                }
            }
            catch (Exception ex)
            {
                result.messages.Add(new MessageBase { ErrorCode = ErrorCode.DBSaveFailed, ErrorMessage = String.Format(MessageResource.ErrorMsgDBSave, ex.Message) });
            }

            return result;
        }

        public ResponseBase<PartShopDto> Update(PartShopParameterDto objUpdate)
        {
            return null;
        }

        /// <summary>
        /// Update PartShop
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<PartShopDto> Update(PartShopUpdateParameterDto objUpdate)
        {
            PartShop oldPartShop = null;
            var result = new ResponseBase<PartShopDto>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                var isValid = ValidationHelper.ValidatePartShop(objUpdate.ID, objUpdate.DealerCode, validationResults, ref oldPartShop);
                if (isValid)
                {
                    // update from the existing as baseline
                    PartShop newPartShop = new PartShop
                    {
                        ID = oldPartShop.ID,
                        Dealer = oldPartShop.Dealer,
                        CityPart = oldPartShop.CityPart,
                        PartShopCode = oldPartShop.PartShopCode,
                        Name = oldPartShop.Name,
                        Address = oldPartShop.Address,
                        Phone = oldPartShop.Phone,
                        Fax = oldPartShop.Fax,
                        Email = oldPartShop.Email,
                        Status = oldPartShop.Status,
                        City = oldPartShop.City,
                        OldPartShopCode = oldPartShop.OldPartShopCode,
                        RowStatus = oldPartShop.RowStatus,
                        CreatedBy = oldPartShop.CreatedBy,
                        CreatedTime = oldPartShop.CreatedTime,
                        LastUpdateBy = oldPartShop.LastUpdateBy,
                        LastUpdateTime = oldPartShop.LastUpdateTime
                    };
                    newPartShop = _mapper.Map<PartShopUpdateParameterDto, PartShop>(objUpdate, newPartShop);

                    var id = (int)_partshopMapper.Update(newPartShop, DNetUserName);
                    result.success = id > 0;
                    if (result.success)
                    {
                        result._id = objUpdate.ID;
                        result.total = 1;

                        if (!newPartShop.Name.Equals(oldPartShop.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            if (!ValidateDuplicateName(objUpdate.Name, validationResults))
                            {
                                result.success = false;
                                return PopulateValidationError<PartShopDto>(validationResults, null);
                            }

                            if (!MailUtility.SendPartShopEmail(_mapper, oldPartShop, newPartShop, oldPartShop.Dealer, validationResults, true))
                            {
                                result.success = false;
                                return PopulateValidationError<PartShopDto>(validationResults, null);
                            }
                        }
                    }
                    else
                    {
                        ErrorMsgHelper.DataCorrupt(result.messages);
                    }
                }
                else
                {
                    return PopulateValidationError<PartShopDto>(validationResults, null);
                }
            }
            catch (Exception ex)
            {
                result.messages.Add(new MessageBase { ErrorCode = ErrorCode.DBSaveFailed, ErrorMessage = String.Format(MessageResource.ErrorMsgDBSave, ex.Message) });
            }

            return result;
        }

        #endregion

        #region private Methods
        /// <summary>
        /// Validate duplicate name
        /// </summary>
        /// <param name="partShopName"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateDuplicateName(string partShopName, List<DNetValidationResult> validationResults)
        {
            var appconfig = _appConfigBL.GetConfigByName("DuplicateNameInPartShop");
            bool isAllowedDuplicatePartshopName;
            if (appconfig == null || !bool.TryParse(appconfig.Value, out isAllowedDuplicatePartshopName))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, "DuplicateNameInPartShop")));
                return false;
            }

            // get the existing partshop name if any
            var duplicatePartShop = ValidationHelper.GetByPartShopByName(partShopName);
            if (duplicatePartShop != null && !isAllowedDuplicatePartshopName)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataIsExist, "PartShop Name")));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate Partshopt
        /// </summary>
        /// <param name="param"></param>
        /// <param name="partshop"></param>
        /// <returns></returns>
        private bool ValidateCreatePartShop(PartShopParameterDto param, PartShop partshop, List<DNetValidationResult> validationResults)
        {
            // validate duplicate partshop name
            if (!ValidateDuplicateName(param.Name, validationResults))
            {
                return false;
            }

            // Get Dealer Info
            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(param.DealerCode, validationResults, this.DealerCode, ref dealer))
            {
                partshop.Dealer = dealer;
            }

            // Get City Info
            City city = null;
            if (ValidationHelper.ValidateCity(param.CityCode, validationResults, ref city, false))
            {
                partshop.City = city;

                // Get City Part Info
                CityPart cityPart = null;
                if (ValidationHelper.ValidateCityPart(city.ID, ref cityPart))
                {
                    if (!ValidationHelper.ValidatePartShopKuota(cityPart.ID, city.CityName, city.ID, validationResults))
                    {
                        return false;
                    }
                }

                // update citypart
                partshop.CityPart = cityPart;

                // Default Status on create = 1                
                int status = 1;

                partshop.Status = (byte)status;
            }

            return validationResults.Count == 0;
        }
        #endregion
    }
}


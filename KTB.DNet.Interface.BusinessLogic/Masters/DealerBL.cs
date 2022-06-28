#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Dealer business logic class
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
    public class DealerBL : AbstractBusinessLogic, IDealerBL
    {
        #region Variables
        private readonly IMapper _dealerMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public DealerBL()
        {
            _dealerMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_Dealer).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get Dealer by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<DealerDto>> Read(DealerFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_Dealer), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            //criterias.opAnd(new Criteria(typeof(Dealer), "DealerCode", MatchType.StartsWith, "10"));
            var result = new ResponseBase<List<DealerDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VWI_Dealer), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_Dealer), filterDto, sortColl);

                // get data
                var data = _dealerMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_Dealer>().ToList();
                    List<DealerDto> listdata = new List<DealerDto>();
                    //var listData = list.Select(item => _mapper.Map<DealerDto>(item)).ToList();
                    foreach (VWI_Dealer iDealer in list)
                    {
                        DealerDto iDealerDto = new DealerDto();
                        iDealerDto.ID = iDealer.id;
                        iDealerDto.DealerCode = iDealer.DealerCode;
                        iDealerDto.DealerName = iDealer.DealerName;
                        iDealerDto.Phone = iDealer.Phone;
                        iDealerDto.Term = iDealer.Term;
                        iDealerDto.Status = iDealer.Status == "1" ? "Aktif" : "Tidak Aktif";
                        iDealerDto.SalesUnitFlag = iDealer.SalesUnitFlag == "1" ? "Ya" : "Tidak";
                        iDealerDto.SparepartFlag = iDealer.SparepartFlag == "1" ? "Ya" : "Tidak";
                        iDealerDto.ServiceFlag = iDealer.ServiceFlag == "1" ? "Ya" : "Tidak";
                        iDealerDto.LastUpdateTime = iDealer.LastUpdateTime;
                        iDealerDto.CityName = iDealer.CityName;
                        iDealerDto.Kategori = iDealer.kategori;
                        iDealerDto.SystemID = iDealer.SystemID;
                        iDealerDto.ProvinceName = iDealer.ProvinceName;
                        iDealerDto.Address = iDealer.Address;

                        listdata.Add(iDealerDto);
                    }
                    result.lst = listdata;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(Dealer), filterDto);
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
        /// Delete Dealer by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<DealerDto> Delete(int id)
        {
            var result = new ResponseBase<DealerDto>();

            try
            {
                var dealer = (Dealer)_dealerMapper.Retrieve(id);
                if (dealer != null)
                {
                    dealer.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _dealerMapper.Update(dealer, DNetUserName);
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
        /// Create a new dealer
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<DealerDto> Create(DealerParameterDto objCreate)
        {
            var result = new ResponseBase<DealerDto>();

            try
            {
                var newDealer = _mapper.Map<Dealer>(objCreate);
                newDealer.CreatedBy = DNetUserName;
                newDealer.CreatedTime = DateTime.Now;
                newDealer.LastUpdateBy = DNetUserName;
                newDealer.LastUpdateTime = DateTime.Now;

                var success = (int)_dealerMapper.Insert(newDealer, newDealer.CreatedBy);
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
            catch (Exception ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Update dealer
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<DealerDto> Update(DealerParameterDto objUpdate)
        {
            var result = new ResponseBase<DealerDto>();
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            Dealer dealer = null;

            isValid = ValidationHelper.ValidateDealer(objUpdate.DealerCode, validationResults, this.DealerCode, ref dealer, false);

            try
            {
                if (isValid)
                {
                    var newDealer = _mapper.Map<DealerParameterDto, Dealer>(objUpdate, dealer);

                    // update the last update information
                    newDealer.LastUpdateBy = DNetUserName;
                    newDealer.LastUpdateTime = DateTime.Now;

                    var success = (int)_dealerMapper.Update(newDealer, newDealer.LastUpdateBy);

                    result.success = success > 0;
                    if (!result.success) ErrorMsgHelper.UpdateNotAvailable(result.messages);
                    // return output ID
                    result._id = success;
                    result.total = 1;
                }
                else
                {
                    return PopulateValidationError<DealerDto>(validationResults, null);
                }
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }

            return result;
        }
        #endregion
    }
}


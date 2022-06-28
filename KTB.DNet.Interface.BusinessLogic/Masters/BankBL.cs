#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Bank business logic class
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
    public class BankBL : AbstractBusinessLogic, IBankBL
    {
        #region Variables
        private readonly IMapper _bankMapper;
        private readonly AutoMapper.IMapper _mapper;

        #endregion

        #region Constructor
        public BankBL()
        {
            _bankMapper = MapperFactory.GetInstance().GetMapper(typeof(Bank).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get Bank by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<BankDto>> Read(BankFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only            
            var criterias = new CriteriaComposite(new Criteria(typeof(Bank), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<BankDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(Bank), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(Bank), filterDto, sortColl);

                // get data
                var data = _bankMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<Bank>().ToList();
                    var listData = list.Select(item => _mapper.Map<BankDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(Bank), filterDto);
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
        /// Delete Bank by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<BankDto> Delete(int id)
        {
            var result = new ResponseBase<BankDto>();

            try
            {
                var bank = (Bank)_bankMapper.Retrieve(id);
                if (bank != null)
                {
                    bank.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _bankMapper.Update(bank, DNetUserName);
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
        /// Create Bank
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<BankDto> Create(BankParameterDto objCreate)
        {
            var result = new ResponseBase<BankDto>();
            try
            {
                //Process Insert
                var bankInsert = new Bank
                {
                    ID = objCreate.ID,
                    BankName = objCreate.BankName,
                    BankCode = objCreate.BankCode,
                    RowStatus = (short)DBRowStatus.Active,
                    CreatedBy = objCreate.CreatedBy,
                    CreatedTime = DateTime.Now,
                    LastUpdateBy = objCreate.LastUpdateBy,
                    LastUpdateTime = DateTime.Now
                };

                var success = (int)_bankMapper.Insert(bankInsert, bankInsert.CreatedBy);
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
        /// Update Bank
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<BankDto> Update(BankParameterDto objUpdate)
        {
            var result = new ResponseBase<BankDto>();
            try
            {
                //Process Insert
                var bankUpdate = new Bank
                {
                    ID = objUpdate.ID,
                    BankName = objUpdate.BankName,
                    BankCode = objUpdate.BankCode,
                    RowStatus = (short)DBRowStatus.Active,
                    CreatedBy = objUpdate.CreatedBy,
                    CreatedTime = DateTime.Now,
                    LastUpdateBy = objUpdate.LastUpdateBy,
                    LastUpdateTime = DateTime.Now
                };

                var success = (int)_bankMapper.Update(bankUpdate, bankUpdate.CreatedBy);
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
            catch (Exception ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }
            return result;
        }
        #endregion
    }
}


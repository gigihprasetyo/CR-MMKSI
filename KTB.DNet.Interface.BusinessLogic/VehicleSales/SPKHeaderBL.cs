#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKHeader business logic class
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
using System.Runtime.ExceptionServices;

#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class SPKHeaderBL : AbstractBusinessLogic, ISPKHeaderBL
    {
        #region Variables
        private readonly IMapper _spkHeaderMapper;
        private readonly AutoMapper.IMapper _mapper;
        private TransactionManager _transactionManager;
        private StandardCodeBL _enumBL;
        #endregion

        #region Constructor
        public SPKHeaderBL()
        {
            _spkHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKHeader).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
            _transactionManager = new TransactionManager();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create a new SPKHeader
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<SPKHeaderDto> Create(SPKHeaderParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update SPKHeader
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<SPKHeaderDto> Update(SPKHeaderParameterDto objUpdate)
        {
            return null;
        }

        /// <summary>
        /// Get complete or canceled spk header status
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SPKHeaderStatusDto>> GetCompleteOrCanceledSPKHeader(SPKHeaderFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<SPKHeaderStatusDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // get the enum status
                List<StandardCodeDto> enumStatusSPK = _enumBL.GetByCategory("EnumStatusSPK.Status");

                // default filter to get the Active Row Status only
                var criterias = new CriteriaComposite(new Criteria(typeof(SPKHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(SPKHeader), "Dealer.DealerCode", MatchType.Exact, DealerCode));
                criterias.opAnd(new Criteria(typeof(SPKHeader), "Status", MatchType.Exact, enumStatusSPK.Where(s => s.ValueCode == "Selesai").SingleOrDefault().ValueId), "(", true);
                criterias.opOr(new Criteria(typeof(SPKHeader), "Status", MatchType.Exact, enumStatusSPK.Where(s => s.ValueCode == "Batal").SingleOrDefault().ValueId), ")", false);

                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(SPKHeader), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(SPKHeader), filterDto, sortColl);

                var data = _spkHeaderMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<SPKHeader>().ToList();
                    var listData = list.Select(item => _mapper.Map<SPKHeaderStatusDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SPKHeader), filterDto);
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
        /// Get SPKHeader by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SPKHeaderDto>> Read(SPKHeaderFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(SPKHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<SPKHeaderDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(SPKHeader), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(SPKHeader), filterDto, sortColl);

                // get data
                var data = _spkHeaderMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<SPKHeader>().ToList();
                    var listData = list.Select(item => _mapper.Map<SPKHeaderDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SPKHeader), filterDto);
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
        /// Delete by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<SPKHeaderDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update SPK Status
        /// </summary>
        /// <param name="spkcoll"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateSPKStatus(SPKHeader spkHeader, int status)
        {
            int returnvalue = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // mark as loaded to prevent it loads from db
                    spkHeader.SPKCustomer.MarkLoaded();
                    spkHeader.MarkLoaded();

                    // If IsStatusExist(spkDetail) Then
                    foreach (SPKDetail spkDetail in spkHeader.SPKDetails)
                    {
                        if (spkDetail.Status != 1 && spkDetail.Status != 3)
                        {
                            spkDetail.Status = (byte)status;
                            spkDetail.RejectedReason = spkHeader.RejectedReason;
                            this._transactionManager.AddUpdate(spkDetail, DNetUserName);
                        }
                    }

                    // update status and flagupdate
                    spkHeader.Status = status.ToString();
                    spkHeader.FlagUpdate = 0;

                    this._transactionManager.AddUpdate(spkHeader, DNetUserName);

                    _transactionManager.PerformTransaction();
                    returnvalue = 0;
                }
                catch (Exception ex)
                {
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
                finally
                {
                    this.RemoveTaskLocking();
                }
            }

            return returnvalue;
        }
        #endregion
    }
}


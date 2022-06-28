
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Web.Configuration;


namespace KTB.DNet.Interface.BusinessLogic
{
    public class DSFPaymentUpdateBL : AbstractBusinessLogic, IDSFPaymentUpdateBL
    {
        #region Variables
        
        private readonly IMapper _dailyPayment;
        private readonly IMapper _poHeader;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public DSFPaymentUpdateBL()
        {
            _dailyPayment = MapperFactory.GetInstance().GetMapper(typeof(DailyPayment).ToString());
            _poHeader = MapperFactory.GetInstance().GetMapper(typeof(POHeader).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get FinishUnitPayment by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<DSFPaymentUpdateDto>> Read(DSFPaymentFilterDto filterDto, int pageSize)
        {
            return null;
        }

        /// <summary>
        /// Delete FinishUnitPayment by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<DSFPaymentUpdateDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new FinishUnitPayment
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<DSFPaymentUpdateDto> Create(DSFPaymentParameterDto objCreate)
        {
            return null;
        }
        /// <summary>
        /// Update FinishUnitPayment
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<DSFPaymentUpdateDto> Update(DSFPaymentParameterDto objUpdate)
        {
            var result = new ResponseBase<DSFPaymentUpdateDto>();
            bool allowToUpdateData = false;
            var criterias = new CriteriaComposite(new Criteria(typeof(DailyPayment), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(DailyPayment), "SlipNumber", MatchType.Exact, objUpdate.SlipNumber));
            try
            {
                var data = _dailyPayment.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    var DailyPaymentData = (DailyPayment)data[0];

                    var criterias2 = new CriteriaComposite(new Criteria(typeof(POHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criterias2.opAnd(new Criteria(typeof(POHeader), "SONumber", MatchType.Exact, objUpdate.SONumber));
                    try
                    {
                        var data2 = _poHeader.RetrieveByCriteria(criterias2);
                        if (data2.Count > 0)
                        {
                            var poHeaderData = (POHeader)data2[0];
                            allowToUpdateData = true;

                        }
                        else
                        {
                            allowToUpdateData = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorMsgHelper.Exception(result.messages, ex.Message);
                    }

                    if (allowToUpdateData == true)
                    {
                        //action update
                        DailyPaymentData.Amount = objUpdate.Amount;
                        DailyPaymentData.RemarkStatus = objUpdate.Status ;
                        //

                        var success = _dailyPayment.Update(DailyPaymentData, DNetUserName);
                        result.success = success > 0;
                        if (result.success)
                        {
                            result._id = DailyPaymentData.ID;
                            result.total = 1;
                            result.lst = new DSFPaymentUpdateDto
                            {
                                SONumber = objUpdate.SONumber,
                                SlipNumber = objUpdate.SlipNumber,
                                Amount = objUpdate.Amount,
                                Status = objUpdate.Status,
                                EffectiveDate = DailyPaymentData.EffectiveDate 

                            };
                        }
                    }
                    else
                    {
                        result.messages.Add(new MessageBase { ErrorCode = ErrorCode.DataUpdateNotAvailable, ErrorMessage = string.Format(MessageResource.ErrorMsgDataNotFound, FieldResource.SparePartPO) });
                    }

                    
                }
                else
                {
                    result.messages.Add(new MessageBase { ErrorCode = ErrorCode.DataUpdateNotAvailable, ErrorMessage = string.Format(MessageResource.ErrorMsgDataNotFound, FieldResource.SparePartPO) });
                }
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get FinishUnitFactoringPaymentMaster 
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>

        #endregion
    }
}

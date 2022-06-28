
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
    public class DSFCeilingUpdateBL : AbstractBusinessLogic, IDSFCeilingUpdateBL
    {
        #region Variables
        
        private readonly IMapper _factoringMaster;
        private readonly IMapper _ccResponseComplaintMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public DSFCeilingUpdateBL()
        {
            _factoringMaster = MapperFactory.GetInstance().GetMapper(typeof(FactoringMaster).ToString());
            
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get FinishUnitCeiling by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<DSFCeilingUpdateDto>> Read(DSFCeilingFilterDto filterDto, int pageSize)
        {
            return null;
        }

        /// <summary>
        /// Delete FinishUnitCeiling by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<DSFCeilingUpdateDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new FinishUnitCeiling
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<DSFCeilingUpdateDto> Create(DSFCeilingParameterDto objCreate)
        {
            return null;
        }
        /// <summary>
        /// Update FinishUnitCeiling
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<DSFCeilingUpdateDto> Update(DSFCeilingParameterDto objUpdate)
        {
            var result = new ResponseBase<DSFCeilingUpdateDto>();
            var criterias = new CriteriaComposite(new Criteria(typeof(FactoringMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(FactoringMaster), "CreditAccount", MatchType.Exact, objUpdate.CreditAccount));
            criterias.opAnd(new Criteria(typeof(FactoringMaster), "ProductCategory.ID", MatchType.Exact, 1));
            try
            {
                var data = _factoringMaster.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    var factoringData = (FactoringMaster)data[0];
                    //action update
                    factoringData.CreditAccount = objUpdate.CreditAccount;
                    factoringData.Outstanding = objUpdate.Outstanding;
                    factoringData.FactoringCeiling = objUpdate.FactoringCeiling;
                    factoringData.AvailableCeiling = objUpdate.AvailableCeiling;
                    //factoringData.MaxTOPDate = objUpdate.MaxTOPDate;
                    //

                    var success = _factoringMaster.Update(factoringData, DNetUserName);
                    result.success = success > 0;
                    if (result.success)
                    {
                        result._id = factoringData.ID;
                        result.total = 1;
                        result.lst = new DSFCeilingUpdateDto
                        {
                            CreditAccount = factoringData.CreditAccount,
                            DealerNAme = objUpdate.DealerName,
                            FactoringCeiling = objUpdate.FactoringCeiling,
                            OutstandingBilling = objUpdate.Outstanding,
                            AvailableCeiling = objUpdate.AvailableCeiling,
                            Status = "Aktif",
                            ValidUntil = factoringData.MaxTOPDate,
                            POAmount = 0

                        };
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

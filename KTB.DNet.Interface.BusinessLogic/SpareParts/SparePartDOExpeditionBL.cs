#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartDOExpedition business logic class
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
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class SparePartDOExpeditionBL : AbstractBusinessLogic, ISparePartDOExpeditionBL
    {
        #region Variables
        private readonly IMapper _sparepartdoMapper;
        private readonly IMapper _sparePartDOExpedition;
        private readonly IMapper _sparePartPackingDetailMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public SparePartDOExpeditionBL()
        {
            _sparepartdoMapper = MapperFactory.GetInstance().GetMapper(typeof(SparePartDO).ToString());
            _sparePartDOExpedition = MapperFactory.GetInstance().GetMapper(typeof(SparePartDOExpedition).ToString());
            _sparePartPackingDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(SparePartPackingDetail).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Update ATA
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ResponseBase<SparePartDOExpeditionDto> UpdateATA(SPDOExpeditionUpdateATAParameterDto param)
        {
            // set default response
            var result = new ResponseBase<SparePartDOExpeditionDto>();

            try
            {
                var criterias = Helper.GenerateCriteria(typeof(SparePartDO), "DONumber", "Dealer.DealerCode", param.DONumber, DealerCode);
                ArrayList arr = _sparepartdoMapper.RetrieveByCriteria(criterias);
                if (arr.Count > 0)
                {
                    SparePartDO domainSPDO = arr[0] as SparePartDO;
                    var packingCriterias = Helper.GenerateCriteria(typeof(SparePartPackingDetail), "SparePartDO.ID", domainSPDO.ID.ToString());
                    ArrayList sparePartPackingDetails = _sparePartPackingDetailMapper.RetrieveByCriteria(packingCriterias);
                    if (sparePartPackingDetails.Count > 0)
                    {
                        SparePartPackingDetail sparePartPackingDetail = sparePartPackingDetails[0] as SparePartPackingDetail;
                        if (sparePartPackingDetail.SparePartPacking.SparePartDOExpedition != null)
                        {
                            SparePartDOExpedition sparePartDOExpedition = sparePartPackingDetail.SparePartPacking.SparePartDOExpedition;
                            sparePartDOExpedition.LastUpdateBy = DNetUserName;
                            sparePartDOExpedition.ATA = param.ATA;
                            sparePartDOExpedition.LastUpdateTime = DateTime.Now;

                            var success = (int)_sparePartDOExpedition.Update(sparePartDOExpedition, DNetUserName);
                            result.success = success > 0;
                            if (result.success)
                            {
                                // return output ID
                                result._id = success;
                                result.total = 1;
                            }
                            else
                            {
                                ErrorMsgHelper.UpdateNotAvailable(result.messages);
                            }
                        }
                        else
                        {
                            result.messages.Add(new MessageBase { ErrorCode = ErrorCode.DataUpdateNotAvailable, ErrorMessage = string.Format(MessageResource.ErrorMsgSparepartDONoDetailOrExpedition, param.DONumber, "SparePartDOExpedition") });
                        }

                    }
                    else
                    {
                        result.messages.Add(new MessageBase { ErrorCode = ErrorCode.DataUpdateNotAvailable, ErrorMessage = string.Format(MessageResource.ErrorMsgSparepartDONoDetailOrExpedition, param.DONumber, "SparePartPackingDetail") });
                    }
                }
                else
                {
                    ErrorMsgHelper.UpdateNotAvailable(result.messages);
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

        public ResponseBase<SparePartDOExpeditionDto> Create(SparePartDOExpeditionParameterDto objCreate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<SparePartDOExpeditionDto> Update(SparePartDOExpeditionParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<List<SparePartDOExpeditionDto>> Read(SparePartDOExpeditionFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<SparePartDOExpeditionDto> Delete(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

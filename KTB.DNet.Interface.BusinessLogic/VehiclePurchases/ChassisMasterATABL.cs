#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ChassisMAsterATA business logic class
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
using KTB.DNet.BusinessValidation;
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Model.Parameters;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class ChassisMasterATABL : AbstractBusinessLogic, IChassisMasterATABL
    {
        #region Variables
        private readonly IMapper _chassisMasterATAMapper;
        private readonly IMapper _chassisMasterMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public ChassisMasterATABL()
        {
            _chassisMasterATAMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterATA).ToString());
            _chassisMasterMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods

        public ResponseBase<ChassisMasterATADto> UpdateATA(ChassisMasterATAParameterDto param)
        {
            // set default response
            var result = new ResponseBase<ChassisMasterATADto>();
            var validationResults = new List<DNetValidationResult>();
            var validResults = new List<ValidResult>();
            ChassisMasterATA chassisMasterATA = null;
            var succeed = 0;


            try
            {
                var chassiMasterATAValidation = new ChassisMasterATAValidation();
                validResults = chassiMasterATAValidation.ValidateChassisMasterATAUpdateATA(param.ChassisNumber, param.ATA, ref chassisMasterATA);

                bool isValid = validResults.Count == 0;
                if (isValid)
                {
                    chassisMasterATA.ATA = param.ATA;
                    chassisMasterATA.LastUpdateTime = DateTime.Now;
                    chassisMasterATA.LastUpdateBy = param.UpdatedBy;
                    if (param.RemarkATA.Trim() != string.Empty)
                    {

                        string strRemarkAta = param.RemarkATA.Trim();
                        if (strRemarkAta.Length > 150)
                        {
                            int pos = strRemarkAta.LastIndexOf(" ", 150);
                            if (pos > 147)
                            {
                                pos = 147;
                            }
                            strRemarkAta = strRemarkAta.Substring(0, pos) + "...";
                        }

                        chassisMasterATA.RemarkATA = strRemarkAta;
                    }

                    succeed = (int)_chassisMasterATAMapper.Update(chassisMasterATA, DNetUserName);

                    result.success = succeed > 0;
                    if (!result.success)
                    {
                        ErrorMsgHelper.DataCorrupt(result.messages);
                    }
                    else
                    {
                        return this.Read(chassisMasterATA);
                    }
                    
                }
                else
                {
                    foreach (var validResult in validResults)
                    {
                        validationResults.Add(new DNetValidationResult(validResult.Message));
                    }
                    return PopulateValidationError<ChassisMasterATADto>(validationResults, null);
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

        /// <summary>Reads the specified chassis master ata.</summary>
        /// <param name="chassisMasterATA">The chassis master ata.</param>
        /// <returns></returns>
        public ResponseBase<ChassisMasterATADto> Read(ChassisMasterATA chassisMasterATA)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(ChassisMasterATA), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(ChassisMasterATA), "ChassisMaster.ID", MatchType.Exact, chassisMasterATA.ChassisMaster.ID));
            var result = new ResponseBase<ChassisMasterATADto>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // get data
                var data = _chassisMasterATAMapper.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    chassisMasterATA = data[0] as ChassisMasterATA;

                    //result.lst = _mapper.Map<ChassisMasterATADto>(data);

                    result.lst = new ChassisMasterATADto { 
                         ID = chassisMasterATA.ID,
                         ChassisNumber = chassisMasterATA.ChassisMaster.ChassisNumber,
                         ETADate = chassisMasterATA.ETA,
                         ATADate = chassisMasterATA.ATA,
                         RemarkATA = chassisMasterATA.RemarkATA
                    };
                    result.total = 1;
                    result._id = chassisMasterATA.ID;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(ChassisMasterPKT), null);
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

        public ResponseBase<ChassisMasterATADto> Create(ChassisMasterATAParameterDto objCreate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<ChassisMasterATADto> Update(ChassisMasterATAParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<List<ChassisMasterATADto>> Read(ChassisMasterATAFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<ChassisMasterATADto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

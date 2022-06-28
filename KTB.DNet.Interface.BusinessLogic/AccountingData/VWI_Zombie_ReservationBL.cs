#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_Zombie_ReservationBL business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-10-13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using KTB.DNet.Interface.Framework;
using x = KTB.DNet.Domain;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_Zombie_ReservationBL : AbstractBusinessLogic, IVWI_Zombie_ReservationBL
    {
        #region Variables
        private IVWI_Zombie_ReservationRepository<VWI_Zombie_Reservation, int> _VWI_Zombie_ReservationRepo;
        #endregion

        #region Constructor
        public VWI_Zombie_ReservationBL(IVWI_Zombie_ReservationRepository<VWI_Zombie_Reservation, int> VWI_Zombie_ReservationRepo)
        {
            _VWI_Zombie_ReservationRepo = VWI_Zombie_ReservationRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_Zombie_Reservation by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_Zombie_ReservationDto>> Read(VWI_Zombie_ReservationFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_Zombie_ReservationDto>> ReadList(VWI_Zombie_ReservationFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_Zombie_ReservationDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_Zombie_Reservation), filterDto);

                // filter by company
                if (DealerCode.ToUpper() == "MKS")
                {
                    string CompanyCode = UserName.Trim().Split('@')[0].Substring(0, 4);
                    criterias = Helper.UpdateStrCriteria(typeof(VWI_Zombie_Reservation), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.InSet.GetHashCode(), "msdyn_companycode", CompanyCode, false, criterias);
                }
                else
                {
                    string DealerName = ValidateLoginSunAndBosowa(validationResults);
                    if (DealerName.Contains("SUN"))
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_Zombie_Reservation), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.InSet.GetHashCode(), "msdyn_companycode", "SSMT", false, criterias);
                    else if (DealerName.Contains("BOSOWA"))
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_Zombie_Reservation), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.InSet.GetHashCode(), "msdyn_companycode", "BSWB", false, criterias);
                    else
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_Zombie_Reservation), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.NotInSet.GetHashCode(), "msdyn_companycode", "SSMT', 'BSWB", false, criterias);
                }

                List<VWI_Zombie_Reservation> data = _VWI_Zombie_ReservationRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_Zombie_Reservation, VWI_Zombie_ReservationDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_Zombie_Reservation), filterDto);
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
        /// Delete VWI_Zombie_Reservation by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_Zombie_ReservationDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_Zombie_Reservation
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_Zombie_ReservationDto> Create(VWI_Zombie_ReservationParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_Zombie_Reservation
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_Zombie_ReservationDto> Update(VWI_Zombie_ReservationParameterDto objUpdate)
        {
            return null;
        }
        #endregion

        #region Private Method

        private string ValidateLoginSunAndBosowa(List<DNetValidationResult> validationResults)
        {
            string DealerName = string.Empty;
            x.Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(DealerCode, validationResults, this.DealerCode, ref dealer))
            {
                DealerName = dealer.DealerName;
            }
            return DealerName;
        }

        #endregion
    }
}


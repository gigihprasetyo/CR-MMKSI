#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_POReceiptDealer business logic class
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
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using System.Threading.Tasks;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_POReceiptDealerBL : AbstractBusinessLogic, IVWI_POReceiptDealerBL
    {

        #region Variables
        private readonly IVWI_POReceiptDealerRepository<VWI_POReceiptDealer, int> _pOReceiptDealerRepository;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public VWI_POReceiptDealerBL(IVWI_POReceiptDealerRepository<VWI_POReceiptDealer, int> pOReceiptDealerRepository)
        {
            _pOReceiptDealerRepository = pOReceiptDealerRepository;
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods

        /// <summary>Reads the list.</summary>
        /// <param name="listChassis">The list chassis.</param>
        /// <returns></returns>
        public ResponseBase<List<VWI_POReceiptDealerDto>> Read(VWI_POReceiptDealerFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<VWI_POReceiptDealerDto>>();
            var sortColl = new SortCollection();
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var dealerCriteria = new CriteriaComposite(new Criteria(typeof(Dealer), "DealerCode", MatchType.Exact, DealerCode));
                var criteria = Helper.BuildCriteria(typeof(VWI_POReceiptDealer), filterDto);

                sortColl = Helper.UpdateSortColumn(typeof(VWI_POReceiptDealer), filterDto, sortColl);

                List<VWI_POReceiptDealer> dataResult = _pOReceiptDealerRepository.Search(criteria, dealerCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (dataResult != null && dataResult.Count > 0)
                {
                    result.lst = dataResult.ConvertList<VWI_POReceiptDealer, VWI_POReceiptDealerDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_POReceiptDealer), filterDto);
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

        #endregion
    }
}

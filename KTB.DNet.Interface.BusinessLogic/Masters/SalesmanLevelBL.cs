#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SalesmanLevel business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/3/2019 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class SalesmanLevelBL : AbstractBusinessLogic, ISalesmanLevelBL
    {
        #region Variables
        private readonly IMapper _salesmanLevelMapper;
        #endregion

        #region Constructor
        public SalesmanLevelBL()
        {
            _salesmanLevelMapper = MapperFactory.GetInstance().GetMapper(typeof(SalesmanLevel).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get SalesmanArea by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SalesmanLevelDto>> Read(FilterDtoBase filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(SalesmanLevel), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<SalesmanLevelDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(SalesmanLevel), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(SalesmanLevel), filterDto, sortColl);

                // get data
                var data = _salesmanLevelMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);

                if (data.Count > 0)
                {
                    result.lst = data.Cast<SalesmanLevel>().ToList().ConvertList<SalesmanLevel, SalesmanLevelDto>();

                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SalesmanLevel), filterDto);
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

        #region Not Implemented
        public ResponseBase<SalesmanLevelDto> Create(ParameterDtoBase objCreate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<SalesmanLevelDto> Update(ParameterDtoBase objUpdate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<SalesmanLevelDto> Delete(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion
    }
}

#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Area1 business logic class
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
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class SalesmanAreaBL : AbstractBusinessLogic, ISalesmanAreaBL
    {
        #region Variables
        private readonly IMapper _salesmanAreaMapper;
        #endregion

        #region Constructor
        public SalesmanAreaBL()
        {
            _salesmanAreaMapper = MapperFactory.GetInstance().GetMapper(typeof(SalesmanArea).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get SalesmanArea by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SalesmanAreaMasterDto>> Read(FilterDtoBase filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(SalesmanArea), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<SalesmanAreaMasterDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(SalesmanArea), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(SalesmanArea), filterDto, sortColl);

                // get data
                var data = _salesmanAreaMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);

                if (data.Count > 0)
                {
                    result.lst = data.Cast<SalesmanArea>().ToList()
                                                                    .ConvertList<SalesmanArea, SalesmanAreaMasterDto>();

                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(Area1), filterDto);
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
        public ResponseBase<SalesmanAreaMasterDto> Create(ParameterDtoBase objCreate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<SalesmanAreaMasterDto> Update(ParameterDtoBase objUpdate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<SalesmanAreaMasterDto> Delete(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion


    }
}


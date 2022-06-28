#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PODestination business logic class
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
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class PODestinationBL : AbstractBusinessLogic, IPODestinationBL
    {
        #region Variables
        private readonly IMapper _poDestination;
        private readonly IMapper _city;
        private readonly IMapper _dealer;
        private readonly AutoMapper.IMapper _mapper;
        private StandardCodeBL _enumBL;
        #endregion

        #region Constructor
        public PODestinationBL()
        {
            _poDestination = MapperFactory.GetInstance().GetMapper(typeof(PODestination).ToString());
            _city = MapperFactory.GetInstance().GetMapper(typeof(City).ToString());
            _dealer = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
        }

        public ResponseBase<PODestinationDto> Create(PODestinationParameterDto objCreate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<List<PODestinationDto>> Read(PODestinationFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Read PODestination Data
        /// </summary>
        /// <param name="filterDto">Filter Parameter</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns></returns>
        public ResponseBase<List<PODestinationDto>> ReadData(PODestinationFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(PODestination), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(PODestination), "Dealer.DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<PODestinationDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(PODestination), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(PODestination), filterDto, sortColl);

                var data = _poDestination.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<PODestination>().ToList();
                    var listData = new List<PODestinationDto>();
                    foreach (var item in list)
                    {
                        // map it
                        var podestinationDto = _mapper.Map<PODestinationDto>(item);

                        // add to list
                        listData.Add(podestinationDto);
                    };

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(PODestination), filterDto);
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
        
        public ResponseBase<PODestinationDto> Update(PODestinationParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }

        ResponseBase<PODestinationDto> IBaseInterface<PODestinationParameterDto, PODestinationFilterDto, PODestinationDto>.Delete(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

}

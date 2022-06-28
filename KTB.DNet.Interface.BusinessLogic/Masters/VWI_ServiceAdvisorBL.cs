#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_ServiceAdvisor business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region "Namespace Imports"
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_ServiceAdvisorBL : AbstractBusinessLogic, IVWI_ServiceAdvisorBL
    {
        #region Variable
        private readonly IMapper _serviceAdvisorMapper;
        private AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public VWI_ServiceAdvisorBL()
        {
            _serviceAdvisorMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_ServiceAdvisor).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Read Service Advisor data
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_ServiceAdvisorDto>> Read(FilterDtoBase filterDto, int pageSize)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_ServiceAdvisor), "ID", MatchType.Greater, 0));
            var result = new ResponseBase<List<VWI_ServiceAdvisorDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VWI_ServiceAdvisor), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_ServiceAdvisor), filterDto, sortColl);

                // get data
                var data = _serviceAdvisorMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_ServiceAdvisor>().ToList();
                    List<VWI_ServiceAdvisorDto> listData = list.Select(item => _mapper.Map<VWI_ServiceAdvisorDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_ServiceAdvisor), filterDto);
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
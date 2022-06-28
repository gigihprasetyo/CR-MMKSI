#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_ProfileDetailFromHeaderCodeBL business logic class
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
    public class VWI_ProfileDetailFromHeaderCodeBL : AbstractBusinessLogic, IVWI_ProfileDetailFromHeaderCodeBL
    {
        #region Variables
        private readonly IMapper _vWI_ProfileDetailFromHeaderCodeMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public VWI_ProfileDetailFromHeaderCodeBL()
        {
            _vWI_ProfileDetailFromHeaderCodeMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_ProfileDetailFromHeaderCode).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get Area1 by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_ProfileDetailFromHeaderCodeDto>> Read(VWI_ProfileDetailFromHeaderCodeFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var result = new ResponseBase<List<VWI_ProfileDetailFromHeaderCodeDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                var criterias = Helper.BuildCriteria(typeof(VWI_ProfileDetailFromHeaderCode), filterDto);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_ProfileDetailFromHeaderCode), filterDto, sortColl);

                // get data
                var data = _vWI_ProfileDetailFromHeaderCodeMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    List<VWI_ProfileDetailFromHeaderCode> list = data.Cast<VWI_ProfileDetailFromHeaderCode>().ToList();
                    var listData = list.ConvertList<VWI_ProfileDetailFromHeaderCode, VWI_ProfileDetailFromHeaderCodeDto>();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_ProfileDetailFromHeaderCode), filterDto);
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

        public ResponseBase<VWI_ProfileDetailFromHeaderCodeDto> Create(VWI_ProfileDetailFromHeaderCodeParameterDto objCreate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<VWI_ProfileDetailFromHeaderCodeDto> Update(VWI_ProfileDetailFromHeaderCodeParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<VWI_ProfileDetailFromHeaderCodeDto> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}


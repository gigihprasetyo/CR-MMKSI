#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_ServiceType business logic class
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
    public class VWI_VehicleTypeBL : AbstractBusinessLogic, IVWI_VehicleTypeBL
    {
        #region Variables
        private readonly IMapper _vehicletypeMapper;
        #endregion

        public VWI_VehicleTypeBL()
        {
            this._vehicletypeMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_VehicleType).ToString());
        }
        public ResponseBase<List<VWI_VehicleTypeDto>> Read(VWI_VehicleTypeFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<VWI_VehicleTypeDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                var criterias = new CriteriaComposite(new Criteria(typeof(VWI_VehicleType), "ID", MatchType.Greater, 0));
                // populate the criterias
                criterias = Helper.BuildCriteria(typeof(VWI_VehicleType), filterDto);
                if (criterias == null)
                {
                    criterias = new CriteriaComposite(new Criteria(typeof(VWI_VehicleType), "ID", MatchType.Greater, 0));
                }
                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_VehicleType), filterDto, sortColl);

                // get data
                var data = _vehicletypeMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_VehicleType>().ToList();
                    List<VWI_VehicleTypeDto> listData = list.ConvertList<VWI_VehicleType, VWI_VehicleTypeDto>();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_ServiceType), filterDto);
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
    }
}

#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_VehicleSpecification business logic class
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
    public class VWI_VehicleSpecificationBL : AbstractBusinessLogic, IVWI_VehicleSpecificationBL
    {
        #region Variables
        private readonly IMapper _vehicleSpecMapper;
        #endregion

        #region Constructor
        public VWI_VehicleSpecificationBL()
        {
            _vehicleSpecMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_VehicleSpecification).ToString());
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Get Vehicle Specification by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_VehicleSpecificationDto>> Read(VWI_VehicleSpecificationFilterDto filterDto, int pageSize)
        {
            var criterias = Helper.BuildCriteria(typeof(VWI_VehicleSpecification), filterDto);
            var result = new ResponseBase<List<VWI_VehicleSpecificationDto>>();
            var sortColl = new SortCollection();

            try
            {
                // define sql
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_VehicleSpecification), filterDto, sortColl);

                // get data
                var data = _vehicleSpecMapper.RetrieveSP("SELECT * FROM VWI_VehicleSpecification " + sql);
                if (data.Count > 0)
                {
                    // calculate the skip 
                    int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                    // filter out the data based on the paging                    
                    List<VWI_VehicleSpecification> list = new List<VWI_VehicleSpecification>();
                    if (sortColl != null && sortColl.Count > 0)
                        list = data.Cast<VWI_VehicleSpecification>().Skip(skip).Take(pageSize).ToList();
                    else
                        list = data.Cast<VWI_VehicleSpecification>().OrderBy(x => x.ClassificationNumber).Skip(skip).Take(pageSize).ToList();

                    // convert to dto object
                    List<VWI_VehicleSpecificationDto> listData = list.ConvertList<VWI_VehicleSpecification, VWI_VehicleSpecificationDto>();

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_VehicleSpecification), filterDto);
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


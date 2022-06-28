#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_OpenFakturForPDI business logic class
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
    public class VWI_ChassisStatusFakturBL : AbstractBusinessLogic, IVWI_ChassisStatusFakturBL
    {
        #region Variables
        private readonly IVWI_ChassisStatusFakturRepository<VWI_ChassisStatusFaktur, int> _chassisStatusFakturRepo;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public VWI_ChassisStatusFakturBL(IVWI_ChassisStatusFakturRepository<VWI_ChassisStatusFaktur, int> chassisStatusFakturRepo)
        {
            _chassisStatusFakturRepo = chassisStatusFakturRepo;
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods

        /// <summary>Reads the specified chassis number.</summary>
        /// <param name="chassisNumber">The chassis number.</param>
        /// <returns></returns>
        public ResponseBase<VWI_ChassisStatusFakturDto> Read(string chassisNumber)
        {
            int filteredResultsCount = 0;
            int totalResultsCount = 0;
            var result = new ResponseBase<VWI_ChassisStatusFakturDto>();
            
            var data = _chassisStatusFakturRepo.Search(chassisNumber, out filteredResultsCount, out totalResultsCount);
            if (data.ID != 0)
            {
                result.lst = _mapper.Map<VWI_ChassisStatusFakturDto>(data);
                result.total = totalResultsCount;
                result.success = true;
            }
            else
            {
                ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_ChassisStatusFaktur), null, "Chassis Number", chassisNumber);
            }
            return result;
        }

        /// <summary>Reads the list.</summary>
        /// <param name="listChassis">The list chassis.</param>
        /// <returns></returns>
        public ResponseBase<List<VWI_ChassisStatusFakturDto>> ReadList(List<VWI_ChassisStatusFakturFilterDto> listChassis)
        {
            int filteredResultsCount = 0;
            int totalResultsCount = 0;
            var result = new ResponseBase<List<VWI_ChassisStatusFakturDto>>();
            List<VWI_ChassisStatusFaktur> listChassisStatusFaktur = null;
            VWI_ChassisStatusFaktur chassisStatusFaktur = null;
            List<string> listChassisNumber = null;
            List<string> listChassisNumberNull = null;

            if (listChassis != null && listChassis.Count > 0)
            {
                listChassisNumber = new List<string>();

                foreach(var data in listChassis)
                {
                    listChassisNumber.Add(data.ChassisNumber);
                }
            }

            listChassisStatusFaktur = _chassisStatusFakturRepo.SearchList(listChassisNumber, out filteredResultsCount, out totalResultsCount, out listChassisNumberNull);

            if (listChassisStatusFaktur.Count > 0 && listChassisStatusFaktur != null)
            {
                result.lst = listChassisStatusFaktur.ConvertList<VWI_ChassisStatusFaktur, VWI_ChassisStatusFakturDto>();
                result.total = totalResultsCount;
                result.success = true;
            }
            else
            {
                ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_ChassisStatusFaktur), null, "Chassis Number", listChassisNumber.ToString());
            }
            return result;
        }

        /// <summary>Reads the list asynchronous.</summary>
        /// <param name="listChassis">The list chassis.</param>
        /// <returns></returns>
        public async Task<ResponseBase<List<VWI_ChassisStatusFakturDto>>> ReadListAsync(List<VWI_ChassisStatusFakturFilterDto> listChassis)
        {
            int filteredResultsCount = 0;
            int totalResultsCount = 0;
            var result = new ResponseBase<List<VWI_ChassisStatusFakturDto>>();
            List<VWI_ChassisStatusFaktur> listChassisStatusFaktur = null;
            VWI_ChassisStatusFaktur chassisStatusFaktur = null;
            List<string> listChassisNumber = null;

            if (listChassis != null && listChassis.Count > 0)
            {
                listChassisNumber = new List<string>();

                foreach (var data in listChassis)
                {
                    listChassisNumber.Add(data.ChassisNumber);
                }

                listChassisStatusFaktur = new List<VWI_ChassisStatusFaktur>();
            }

            await _chassisStatusFakturRepo.SearchListAsync(listChassisNumber, listChassisStatusFaktur);

            if (listChassisStatusFaktur.Count > 0 && listChassisStatusFaktur != null)
            {
                totalResultsCount = listChassisStatusFaktur.Count();
                result.lst = listChassisStatusFaktur.ConvertList<VWI_ChassisStatusFaktur, VWI_ChassisStatusFakturDto>();
                result.total = totalResultsCount;
                result.success = true;
            }
            else
            {
                ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_ChassisStatusFaktur), null, "Chassis Number", listChassisNumber.ToString());
            }
            return result;
        }

        /// <summary>Reads the list by last update time.</summary>
        /// <param name="lastUpdateTime">The last update time.</param>
        /// <returns></returns>
        public ResponseBase<List<VWI_ChassisStatusFakturDto>> ReadListByLastUpdateTime(DateTime lastUpdateTime)
        {
            int filteredResultsCount = 0;
            int totalResultsCount = 0;
            var result = new ResponseBase<List<VWI_ChassisStatusFakturDto>>();
            List<VWI_ChassisStatusFaktur> listChassisStatusFaktur = null;
            var validationResults = new List<DNetValidationResult>();

            listChassisStatusFaktur = _chassisStatusFakturRepo.SearchLastUpdateTime(lastUpdateTime, DealerCode, out filteredResultsCount, out totalResultsCount);

            if (listChassisStatusFaktur.Count > 0 && listChassisStatusFaktur != null)
            {
                result.lst = listChassisStatusFaktur.ConvertList<VWI_ChassisStatusFaktur, VWI_ChassisStatusFakturDto>();
                result.total = totalResultsCount;
                result.success = true;
            }
            else
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataReadNotAvailable, "Data ChassisStatusFaktur dengan LastUpdateTime > '"+lastUpdateTime.ToString()+"'"));
                return PopulateValidationError<List<VWI_ChassisStatusFakturDto>>(validationResults, null);
            }
            return result;
        }

        #endregion

        public ResponseBase<List<VWI_OpenFakturForPDIDto>> Read(FilterDtoBase filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<List<VWI_ChassisStatusFakturDto>> Read(VWI_ChassisStatusFakturFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}

#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ServiceReminder business logic class
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
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using KTB.DNet.Interface.Framework;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_MobileServiceReminderBL : AbstractBusinessLogic, IVWI_MobileServiceReminderBL
    {
       #region Variables
        private readonly IMapper _VWI_MobileServiceReminderMapper;

        #endregion

        #region Constructor
        public VWI_MobileServiceReminderBL()
        {
            _VWI_MobileServiceReminderMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_MobileServiceReminder).ToString());

        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Read Vehicle Code master data
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_MobileServiceReminderDto>> Read(VWI_MobileServiceReminderFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_MobileServiceReminder), "ID", MatchType.Greater, 0));
            var result = new ResponseBase<List<VWI_MobileServiceReminderDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VWI_MobileServiceReminder), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_MobileServiceReminder), filterDto, sortColl);

                // get data
                var data = _VWI_MobileServiceReminderMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_MobileServiceReminder>().ToList();
                    List<VWI_MobileServiceReminderDto> listData = list.ConvertList<VWI_MobileServiceReminder, VWI_MobileServiceReminderDto>();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_MobileServiceReminder), filterDto);
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


#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_ServiceTemplateActivityBL class
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
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using KTB.DNet.Interface.Framework;
#endregion


namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_ServiceTemplateActivityBL : AbstractBusinessLogic, IVWI_ServiceTemplateActivityBL
    {
        #region Variables
        private readonly IMapper _vWI_ServiceTemplateActivityMapper;
        #endregion

        #region Constructor
        public VWI_ServiceTemplateActivityBL()
        {
            _vWI_ServiceTemplateActivityMapper = MapperFactory.GetInstance().GetMapper(typeof(VW_ServiceTemplateActivity).ToString());

        }

        public ResponseBase<VWI_ServiceTemplateActivityDto> Create(VWI_ServiceTemplateActivityParameterDto objCreate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<VWI_ServiceTemplateActivityDto> Delete(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_ServiceTemplateActivity by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_ServiceTemplateActivityDto>> Read(VWI_ServiceTemplateActivityFilterDto filterDto, int pageSize)
        {

            var criterias = new CriteriaComposite(new Criteria(typeof(VW_ServiceTemplateActivity), "ID", MatchType.Greater, 0));

            var result = new ResponseBase<List<VWI_ServiceTemplateActivityDto>>();
            var sortColl = new SortCollection();

            try
            {
                // define sql
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VW_ServiceTemplateActivity), filterDto, sortColl, criterias);

                // get data
                var data = _vWI_ServiceTemplateActivityMapper.RetrieveSP("SELECT * FROM VW_ServiceTemplateActivity " + sql);
                if (data.Count > 0)
                {
                    // calculate the skip 
                    int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                    // filter out the data based on the paging                    
                    List<VW_ServiceTemplateActivity> list = new List<VW_ServiceTemplateActivity>();
                    if (sortColl != null && sortColl.Count > 0)
                        list = data.Cast<VW_ServiceTemplateActivity>().Skip(skip).Take(pageSize).ToList();
                    else
                        list = data.Cast<VW_ServiceTemplateActivity>().OrderBy(x => x.ID).Skip(skip).Take(pageSize).ToList();

                    // convert to dto object
                    List<VWI_ServiceTemplateActivityDto> listData = list.ConvertList<VW_ServiceTemplateActivity, VWI_ServiceTemplateActivityDto>();

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_ServiceReminder), filterDto);
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

        public ResponseBase<VWI_ServiceTemplateActivityDto> Update(VWI_ServiceTemplateActivityParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}


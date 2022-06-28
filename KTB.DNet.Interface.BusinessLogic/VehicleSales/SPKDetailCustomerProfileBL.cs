#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKDetailCustomerProfile business logic class
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
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using SPKDetailCustomerProfile = KTB.DNet.Domain.SPKDetailCustomerProfile;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class SPKDetailCustomerProfileBL : AbstractBusinessLogic, ISPKDetailCustomerProfileBL
    {
        #region Variables
        private readonly IMapper _spkdetailcustomerprofileMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public SPKDetailCustomerProfileBL()
        {
            _spkdetailcustomerprofileMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKDetailCustomerProfile).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }

        public SPKDetailCustomerProfileBL(AutoMapper.IMapper mapper)
        {
            _spkdetailcustomerprofileMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKDetailCustomerProfile).ToString());
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create a new SPKDetailCustomerProfile
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<SPKDetailCustomerProfileDto> Create(SPKDetailCustomerProfileParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update SPKDetailCustomerProfile
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<SPKDetailCustomerProfileDto> Update(SPKDetailCustomerProfileParameterDto objUpdate)
        {
            return null;
        }

        /// <summary>
        /// Delete SPKDetailCustomerProfile by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<SPKDetailCustomerProfileDto> Delete(int id)
        {
            var result = new ResponseBase<SPKDetailCustomerProfileDto>();

            try
            {
                var SPKDetailCustomerProfile = (SPKDetailCustomerProfile)_spkdetailcustomerprofileMapper.Retrieve(id);
                if (SPKDetailCustomerProfile != null)
                {
                    SPKDetailCustomerProfile.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _spkdetailcustomerprofileMapper.Update(SPKDetailCustomerProfile, DNetUserName);
                    if (nResult != 0)
                    {
                        result.success = true;
                        result._id = id;
                        result.total = 1;
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                    }
                }
                else
                {
                    ErrorMsgHelper.DeleteNotAvailable(result.messages);
                }
            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Get SPK Customer Profile
        /// </summary>
        /// <param name="spkDetailCustomerId"></param>
        /// <param name="profGroupId"></param>
        /// <param name="profileHeaderId"></param>
        /// <returns></returns>
        public ResponseBase<SPKDetailCustomerProfileDto> GetSPKDetailCustomerProfiles(int spkDetailCustomerId, int profGroupId, int profileHeaderId)
        {
            var result = new ResponseBase<SPKDetailCustomerProfileDto>();

            try
            {
                CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(SPKDetailCustomerProfile), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                criterias.opAnd(new Criteria(typeof(SPKDetailCustomerProfile), "SPKDetailCustomer.ID", MatchType.Exact, spkDetailCustomerId));
                criterias.opAnd(new Criteria(typeof(SPKDetailCustomerProfile), "ProfileGroup.ID", MatchType.Exact, profGroupId));
                criterias.opAnd(new Criteria(typeof(SPKDetailCustomerProfile), "ProfileHeader.ID", MatchType.Exact, profileHeaderId));

                var data = _spkdetailcustomerprofileMapper.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    var list = data.Cast<SPKDetailCustomerProfile>().ToList();
                    var listData = new List<SPKDetailCustomerProfileDto>();
                    foreach (var item in list)
                    {
                        var SPKDetailCustomerProfileDto = _mapper.Map<SPKDetailCustomerProfileDto>(item);

                        if (item.ProfileHeader != null)
                        {
                            SPKDetailCustomerProfileDto.ProfileHeader = _mapper.Map<ProfileHeaderDto>(item.ProfileHeader);
                        }
                        if (item.ProfileGroup != null)
                        {
                            SPKDetailCustomerProfileDto.ProfileGroup = _mapper.Map<ProfileGroupDto>(item.ProfileGroup);
                        }
                        if (item.SPKDetailCustomer != null)
                        {
                            SPKDetailCustomerProfileDto.SPKDetailCustomer = _mapper.Map<SPKDetailCustomerDto>(item.SPKDetailCustomer);
                        }

                        listData.Add(SPKDetailCustomerProfileDto);
                    };

                    var lst = listData.FirstOrDefault();
                    result.lst = lst;
                    result.messages = null;
                    result._id = lst.ID;
                    result.total = 1;
                    result.success = true;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SPKDetailCustomerProfile), null);
                }



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

        /// <summary>
        /// Get SPKDetailCustomerProfile by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SPKDetailCustomerProfileDto>> Read(SPKDetailCustomerProfileFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(SPKDetailCustomerProfile), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<SPKDetailCustomerProfileDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(SPKDetailCustomerProfile), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(SPKDetailCustomerProfile), filterDto, sortColl);

                // get data
                var data = _spkdetailcustomerprofileMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<SPKDetailCustomerProfile>().ToList();
                    var listData = new List<SPKDetailCustomerProfileDto>();
                    foreach (var item in list)
                    {
                        // map it
                        var SPKDetailCustomerProfileDto = _mapper.Map<SPKDetailCustomerProfileDto>(item);

                        if (item.ProfileHeader != null)
                        {
                            SPKDetailCustomerProfileDto.ProfileHeader = _mapper.Map<ProfileHeaderDto>(item.ProfileHeader);
                        }
                        if (item.ProfileGroup != null)
                        {
                            SPKDetailCustomerProfileDto.ProfileGroup = _mapper.Map<ProfileGroupDto>(item.ProfileGroup);
                        }
                        if (item.SPKDetailCustomer != null)
                        {
                            SPKDetailCustomerProfileDto.SPKDetailCustomer = _mapper.Map<SPKDetailCustomerDto>(item.SPKDetailCustomer);
                        }

                        // add to list
                        listData.Add(SPKDetailCustomerProfileDto);
                    }

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SPKDetailCustomerProfile), filterDto);
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


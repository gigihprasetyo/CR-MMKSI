#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ProfileHeaderToGroup business logic class
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
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class ProfileHeaderToGroupBL : AbstractBusinessLogic, IProfileHeaderToGroupBL
    {
        #region Variables
        private readonly IMapper _profileHeaderToGroupMapper;
        private readonly AutoMapper.IMapper _mapper;

        private const string PROFILE_GROUP_ID = "GroupID";
        #endregion

        #region Constructor
        public ProfileHeaderToGroupBL()
        {
            _profileHeaderToGroupMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileHeaderToGroup).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// REtrieve profile header to group
        /// </summary>
        /// <param name="profileGroupId"></param>
        /// <returns></returns>
        public ResponseBase<List<ProfileHeaderToGroupDto>> RetrieveByProfileGroupId(int profileGroupId)
        {
            ResponseBase<List<ProfileHeaderToGroupDto>> result = new ResponseBase<List<ProfileHeaderToGroupDto>>();

            try
            {
                ArrayList arrayListResult = new ArrayList();
                CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                criteria.opAnd(new Criteria(typeof(ProfileHeaderToGroup), PROFILE_GROUP_ID, MatchType.Exact, profileGroupId));
                arrayListResult = _profileHeaderToGroupMapper.RetrieveByCriteria(criteria);

                var listResult = new List<ProfileHeaderToGroupDto>();
                if (arrayListResult.Count > 0)
                {
                    foreach (var item in arrayListResult)
                    {
                        var itemDto = _mapper.Map<ProfileHeaderToGroupDto>(item);
                        result.lst.Add(itemDto);
                    }
                }

                result.success = true;
                result.total = arrayListResult.Count;
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
        /// Create a new ProfileHeaderToGroup
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<ProfileHeaderToGroupDto> Create(ProfileHeaderToGroupParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update ProfileHeaderToGroup
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<ProfileHeaderToGroupDto> Update(ProfileHeaderToGroupParameterDto objUpdate)
        {
            return null;
        }

        /// <summary>
        /// Delete ProfileHeaderToGroup by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<ProfileHeaderToGroupDto> Delete(int id)
        {
            var result = new ResponseBase<ProfileHeaderToGroupDto>();

            try
            {
                var profileHeaderToGroup = (ProfileHeaderToGroup)_profileHeaderToGroupMapper.Retrieve(id);
                if (profileHeaderToGroup != null)
                {
                    profileHeaderToGroup.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _profileHeaderToGroupMapper.Update(profileHeaderToGroup, DNetUserName);
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
        /// Get ProfileHeaderToGroup by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<ProfileHeaderToGroupDto>> Read(ProfileHeaderToGroupFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<ProfileHeaderToGroupDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(ProfileHeaderToGroup), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(ProfileHeaderToGroup), filterDto, sortColl);

                // get data
                var data = _profileHeaderToGroupMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<ProfileHeaderToGroup>().ToList();
                    var listData = new List<ProfileHeaderToGroupDto>();
                    foreach (var item in list)
                    {
                        // map it
                        var profileheadertogroupDto = _mapper.Map<ProfileHeaderToGroupDto>(item);

                        if (item.ProfileHeader != null)
                        {
                            profileheadertogroupDto.ProfileHeader = _mapper.Map<ProfileHeaderDto>(item.ProfileHeader);
                        }
                        if (item.ProfileGroup != null)
                        {
                            profileheadertogroupDto.ProfileGroup = _mapper.Map<ProfileGroupDto>(item.ProfileGroup);
                        }

                        // add to list
                        listData.Add(profileheadertogroupDto);
                    }

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(ProfileHeaderToGroup), filterDto);
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


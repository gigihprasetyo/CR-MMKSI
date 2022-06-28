#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ProfileGroup business logic class
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
using KTB.DNet.Interface.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class ProfileGroupBL : AbstractBusinessLogic, IProfileGroupBL
    {
        #region Variables
        // declare variables
        private IMapper _profileGroupMapper;
        private IMapper _profileHeaderToGroupMapper;
        private readonly AutoMapper.IMapper _mapper;

        private const string PROFILE_GROUP_NAME = "ProfileGroupName";
        private const string DEALER_ID = "Dealer.ID";
        private const string ROW_STATUS = "RowStatus";
        private const string CODE = "Code";
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ProfileGroupBL()
        {
            _profileGroupMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileGroup).ToString());
            _profileHeaderToGroupMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileHeaderToGroup).ToString());

            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }

        public ProfileGroupBL(AutoMapper.IMapper mapper)
        {
            _profileGroupMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileGroup).ToString());
            _profileHeaderToGroupMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileHeaderToGroup).ToString());

            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create a new profile group
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<ProfileGroupDto> Create(ProfileGroupParameterDto objCreate)
        {
            var result = new ResponseBase<ProfileGroupDto>();

            try
            {
                // create profile group object
                var newProfileGroup = _mapper.Map<ProfileGroup>(objCreate);
                newProfileGroup.CreatedBy = DNetUserName;
                newProfileGroup.CreatedTime = DateTime.Now;

                // insert a new profile group object
                var succeed = _profileGroupMapper.Insert(newProfileGroup, DNetUserName);

                if (succeed > 0)
                {
                    result.success = true;
                    result._id = succeed;
                    result.total = 1;
                }
                else
                {
                    ErrorMsgHelper.DataCorrupt(result.messages);
                }
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Update profile group
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<ProfileGroupDto> Update(ProfileGroupParameterDto objUpdate)
        {
            var result = new ResponseBase<ProfileGroupDto>();
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            ProfileGroup profileGroup = null;

            ValidateProfileGroup(objUpdate, validationResults, ref isValid, ref profileGroup);

            try
            {
                if (isValid)
                {
                    // create profile group object
                    var newProfileGroup = _mapper.Map<ProfileGroupParameterDto, ProfileGroup>(objUpdate, profileGroup);
                    newProfileGroup.LastUpdateBy = DNetUserName;
                    newProfileGroup.LastUpdateTime = DateTime.Now;

                    var success = (int)_profileGroupMapper.Update(newProfileGroup, newProfileGroup.CreatedBy);

                    result.success = success > 0;
                    if (!result.success) ErrorMsgHelper.UpdateNotAvailable(result.messages);
                    // return output ID
                    result._id = success;
                    result.total = 1;
                }
                else
                {
                    return PopulateValidationError<ProfileGroupDto>(validationResults, null);
                }
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Get ProfileGroup by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<ProfileGroupDto>> Read(ProfileGroupFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(ProfileGroup), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<ProfileGroupDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(ProfileGroup), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(ProfileGroup), filterDto, sortColl);

                // get data
                var data = _profileGroupMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<ProfileGroup>().ToList();
                    var listData = list.Select(item => _mapper.Map<ProfileGroupDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(ProfileGroup), filterDto);
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

        /// <summary>
        /// Delete profile group by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<ProfileGroupDto> Delete(int id)
        {
            var result = new ResponseBase<ProfileGroupDto>();

            try
            {
                var profileGroup = (City)_profileGroupMapper.Retrieve(id);
                if (profileGroup != null)
                {
                    profileGroup.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _profileGroupMapper.Update(profileGroup, DNetUserName);
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
        /// Get profile group by its code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ResponseBase<ProfileGroupDto> GetByCode(string code)
        {
            var profileGroup = new ProfileGroup();
            var profileGroupDto = new ProfileGroupDto();
            var result = new ResponseBase<ProfileGroupDto>();

            try
            {
                CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(ProfileGroup), ROW_STATUS, MatchType.Exact, ((short)(DBRowStatus.Active))));
                criterias.opAnd(new Criteria(typeof(ProfileGroup), CODE, MatchType.Exact, code));
                ArrayList profileGroupColl = _profileGroupMapper.RetrieveByCriteria(criterias);

                // get profile group if any otherwise set to null
                profileGroup = profileGroupColl.Count > 0 ? ((ProfileGroup)(profileGroupColl[0])) : null;

                if (profileGroup != null)
                {
                    profileGroupDto = _mapper.Map<ProfileGroupDto>(profileGroup);
                    profileGroupDto.ProfileHeaderToGroups = new List<ProfileHeaderToGroupDto>();

                    var group = profileGroup.ProfileHeaderToGroups.Cast<ProfileHeaderToGroup>().ToList();
                    profileGroupDto.ProfileHeaderToGroups = _mapper.Map<IList<ProfileHeaderToGroup>, IList<ProfileHeaderToGroupDto>>(group).ToList();

                    result.lst = profileGroupDto;
                    result.messages = null;
                    result.total = 1;
                    result.success = true;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(ProfileGroup), null, "Code", code);
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
        /// Check if Profile Group is exist
        /// </summary>
        /// <param name="profileGroupCode"></param>
        /// <returns></returns>
        public ResponseBase<bool> IsProfileGroupFound(string profileGroupCode)
        {
            var result = new ResponseBase<bool>();

            // get the object
            var resultObject = GetByCode(profileGroupCode);

            // update the properties
            result._id = resultObject._id;
            result.messages = resultObject.messages;
            result.lst = resultObject.success;

            return result;
        }

        /// <summary>
        /// Validate a code
        /// </summary>
        /// <param name="organizationID"></param>
        /// <param name="profileGroupName"></param>
        /// <returns></returns>
        public ResponseBase<int> ValidateCode(int organizationID, string profileGroupName)
        {
            var result = new ResponseBase<int>();

            // setup criteria
            CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(ProfileGroup), PROFILE_GROUP_NAME, MatchType.Exact, profileGroupName));
            crit.opAnd(new Criteria(typeof(ProfileGroup), DEALER_ID, MatchType.Exact, organizationID));

            // setup aggregate
            Aggregate agg = new Aggregate(typeof(ProfileGroup), PROFILE_GROUP_NAME, AggregateType.Count);

            try
            {
                result.lst = ((int)(_profileGroupMapper.RetrieveScalar(agg, crit)));
                result.success = true;
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
        #endregion

        #region Private Method
        /// <summary>
        /// Validate profile group
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        /// <param name="profileGroup"></param>
        private void ValidateProfileGroup(ProfileGroupParameterDto objUpdate, List<DNetValidationResult> validationResults, ref bool isValid, ref ProfileGroup profileGroup)
        {
            // get profile group master
            CriteriaComposite criteriaProfileGroup = new CriteriaComposite(new Criteria(typeof(ProfileGroup), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriaProfileGroup.opAnd(new Criteria(typeof(ProfileGroup), "ID", MatchType.Exact, objUpdate.ID));
            var profGroups = _profileGroupMapper.RetrieveByCriteria(criteriaProfileGroup);
            if (profGroups.Count > 0)
            {
                // cast the object
                profileGroup = profGroups[0] as ProfileGroup;
            }
            else
            {
                isValid = false;
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.ProfileGroup, objUpdate.ID)));
            }
        }
        #endregion
    }
}
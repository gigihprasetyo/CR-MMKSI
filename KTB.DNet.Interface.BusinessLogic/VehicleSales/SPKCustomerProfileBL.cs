#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKCustomerProfile business logic class
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
using SPKCustomerProfile = KTB.DNet.Domain.SPKCustomerProfile;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class SPKCustomerProfileBL : AbstractBusinessLogic, ISPKCustomerProfileBL
    {
        #region Variables
        private readonly IMapper _spkcustomerprofileMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public SPKCustomerProfileBL()
        {
            _spkcustomerprofileMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKCustomerProfile).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }

        public SPKCustomerProfileBL(AutoMapper.IMapper mapper)
        {
            _spkcustomerprofileMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKCustomerProfile).ToString());
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create a new SPKCustomerProfile
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<SPKCustomerProfileDto> Create(SPKCustomerProfileParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update SPKCustomerProfile
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<SPKCustomerProfileDto> Update(SPKCustomerProfileParameterDto objUpdate)
        {
            return null;
        }

        /// <summary>
        /// Delete SPKCustomerProfile by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<SPKCustomerProfileDto> Delete(int id)
        {
            var result = new ResponseBase<SPKCustomerProfileDto>();

            try
            {
                var spkcustomerprofile = (SPKCustomerProfile)_spkcustomerprofileMapper.Retrieve(id);
                if (spkcustomerprofile != null)
                {
                    spkcustomerprofile.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _spkcustomerprofileMapper.Update(spkcustomerprofile, DNetUserName);
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
        /// <param name="spkCustomerId"></param>
        /// <param name="profGroupId"></param>
        /// <param name="profileHeaderId"></param>
        /// <returns></returns>
        public ResponseBase<SPKCustomerProfileDto> GetSPKCustomerProfiles(int spkCustomerId, int profGroupId, int profileHeaderId)
        {
            var result = new ResponseBase<SPKCustomerProfileDto>();

            try
            {
                CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(SPKCustomerProfile), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                criterias.opAnd(new Criteria(typeof(SPKCustomerProfile), "SPKCustomer.ID", MatchType.Exact, spkCustomerId));
                criterias.opAnd(new Criteria(typeof(SPKCustomerProfile), "ProfileGroup.ID", MatchType.Exact, profGroupId));
                criterias.opAnd(new Criteria(typeof(SPKCustomerProfile), "ProfileHeader.ID", MatchType.Exact, profileHeaderId));

                var data = _spkcustomerprofileMapper.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    var list = data.Cast<SPKCustomerProfile>().ToList();
                    var listData = new List<SPKCustomerProfileDto>();
                    foreach (var item in list)
                    {
                        var spkcustomerprofileDto = _mapper.Map<SPKCustomerProfileDto>(item);

                        if (item.ProfileHeader != null)
                        {
                            spkcustomerprofileDto.ProfileHeader = _mapper.Map<ProfileHeaderDto>(item.ProfileHeader);
                        }
                        if (item.ProfileGroup != null)
                        {
                            spkcustomerprofileDto.ProfileGroup = _mapper.Map<ProfileGroupDto>(item.ProfileGroup);
                        }
                        if (item.SPKCustomer != null)
                        {
                            spkcustomerprofileDto.SPKCustomer = _mapper.Map<SPKCustomerDto>(item.SPKCustomer);
                        }

                        listData.Add(spkcustomerprofileDto);
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
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SPKCustomerProfile), null);
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
        /// Get SPKCustomerProfile by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SPKCustomerProfileDto>> Read(SPKCustomerProfileFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(SPKCustomerProfile), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<SPKCustomerProfileDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(SPKCustomerProfile), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(SPKCustomerProfile), filterDto, sortColl);

                // get data
                var data = _spkcustomerprofileMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<SPKCustomerProfile>().ToList();
                    var listData = new List<SPKCustomerProfileDto>();
                    foreach (var item in list)
                    {
                        // map it
                        var spkcustomerprofileDto = _mapper.Map<SPKCustomerProfileDto>(item);

                        if (item.ProfileHeader != null)
                        {
                            spkcustomerprofileDto.ProfileHeader = _mapper.Map<ProfileHeaderDto>(item.ProfileHeader);
                        }
                        if (item.ProfileGroup != null)
                        {
                            spkcustomerprofileDto.ProfileGroup = _mapper.Map<ProfileGroupDto>(item.ProfileGroup);
                        }
                        if (item.SPKCustomer != null)
                        {
                            spkcustomerprofileDto.SPKCustomer = _mapper.Map<SPKCustomerDto>(item.SPKCustomer);
                        }

                        // add to list
                        listData.Add(spkcustomerprofileDto);
                    }

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SPKCustomerProfile), filterDto);
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


#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_VehicleInformation business logic class
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
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_VehicleInformationBL : AbstractBusinessLogic, IVWI_VehicleInformationBL
    {
        #region Variables
        private readonly IMapper _vehicleInformationMapper;
        private IVehicleInformationRepository<VWI_VehicleInformation_IF, int> _vehicleInformationRepo;
        #endregion

        #region Constructor
        public VWI_VehicleInformationBL(IVehicleInformationRepository<VWI_VehicleInformation_IF, int> vehicleInformationRepo)
        {
            _vehicleInformationRepo = vehicleInformationRepo;
            _vehicleInformationMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_VehicleInformation).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_VehicleInformation by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_VehicleInformationDto>> Read(VWI_VehicleInformationFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<VWI_VehicleInformationDto>>();
            //var sortColl = new SortCollection();
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                string fltr1 = string.Empty; 
                if (filterDto.pages == 0)
                {
                    filterDto.pages = 1;
                }
                var innerQueryCriteria = GetInnerQueryParameter(filterDto, pageSize, ref fltr1);
                
                // get criteria
                //var criterias = Helper.BuildCriteria(typeof(VWI_VehicleInformation), filterDto, GetInnerQueryParams(), out innerQueryCriteria, typeof(ChassisMaster));

                var criterias = Helper.InitialStrCriteria(typeof(VWI_VehicleInformation_IF), filterDto);
                string sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_VehicleInformation_IF), filterDto);
                criterias = criterias + "|" + fltr1;

                List<VWI_VehicleInformation_IF> data = _vehicleInformationRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, innerQueryCriteria == string.Empty ? filterDto.pages : 1, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_VehicleInformation_IF, VWI_VehicleInformationDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_VehicleInformation_IF), filterDto);
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

        /// <summary>
        /// Get VWI_VehicleInformation by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_VehicleInformationDto>> ReadWithView(VWI_VehicleInformationFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<VWI_VehicleInformationDto>>();
            var data = new List<VWI_VehicleInformationDto>();
            var sortColl = new SortCollection();
            int totalRow = 0;
            string rawSql = string.Empty;

            try
            {
                // validate criterias
                var criterias = Helper.BuildCriteria(typeof(VWI_VehicleInformation), filterDto);

                // define the raw sql query
                if (IsSearchByChassisMasterCriteria(filterDto))
                    rawSql = Helper.GetDapperQueryView(RepositoryResource.ViewVehicleInformation, filterDto, criterias, sortColl, typeof(VWI_VehicleInformation), typeof(VWI_VehicleInformation).Name);
                else
                    rawSql = Helper.GetDapperQuery(RepositoryResource.SqlSelectVehicleInformation, filterDto, criterias, pageSize, sortColl, typeof(VWI_VehicleInformation));

                data = _vehicleInformationRepo.GetByQuery(rawSql, criterias, filterDto, pageSize, out totalRow);

                if (data.Count > 0)
                {
                    result.success = true;
                    result.lst = data;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_VehicleInformation), filterDto);
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

        private bool IsSearchByChassisMasterCriteria(FilterDtoBase filterDto)
        {
            if (filterDto.find != null && filterDto.find.Count > 0)
            {
                foreach (var filter in filterDto.find)
                {
                    if (filter.PropertyName != null)
                    {
                        if (filter.PropertyName.Equals("EndCustomerId") ||
                            filter.PropertyName.Equals("ChassisNumber") ||
                            filter.PropertyName.Equals("CategoryID") ||
                            filter.PropertyName.Equals("VechileColorID") ||
                            filter.PropertyName.Equals("VehicleKindID") ||
                            filter.PropertyName.Equals("SoldDealerID") ||
                            filter.PropertyName.Equals("EngineNumber") ||
                            filter.PropertyName.Equals("SerialNumber") ||
                            filter.PropertyName.Equals("ProductionYear") ||
                            filter.PropertyName.Equals("LastUpdateTime"))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Check if it is has chassis master criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <returns></returns>
        private List<string> GetInnerQueryParams()
        {
            return new List<string>(){
                "EndCustomer.ID"
                //"ChassisNumber", #trigger by update vehicle information query by pak anna 20200823
                //"Category.ID",
                //"VechileColor.ID",
                //"VehicleKind.ID",
                //"Dealer.ID",
                //"EngineNumber",
                //"SerialNumber",
                //"ProductionYear",
                //"LastUpdateTime"
            };
        }

        private string GetInnerQueryParameter(VWI_VehicleInformationFilterDto filterDto, int pageSize, ref string filter1)
        {
            string valueFilter = string.Empty;
            string innersortColumn = string.Empty;
            string sortPagesInner = string.Empty;
            var innerQueryCriteria = string.Empty;
            string x = "ID|ChassisNumber|IsBB|VehicleKindID|SoldDealerID|SerialNumber|ProductionYear|FSProgram|LastUpdateTime";
            string[] columnInner = x.Split('|');
            bool isWithSort = true;
            bool isHaveFilterLUT = false;

            for (int i = 0; i < columnInner.Length; i++)
            {
                if (filterDto.find != null)
                {
                    foreach (var filter in filterDto.find)
                    {
                        if (filter.PropertyName == columnInner[i] && filter.PropertyName != "LastUpdateTime")
                        {
                            valueFilter = valueFilter + Helper.UpdateStrCriteria(typeof(ChassisMaster), new MatchTypeFilter { SqlOperation = filter.SqlOperation }, filter.MatchType.GetHashCode(), filter.PropertyName, filter.PropertyValue, true, valueFilter);
                            innerQueryCriteria = valueFilter;
                            isWithSort = true;
                        }
                        else if (!x.Contains(filter.PropertyName.Trim()))
                        {
                            isWithSort = false;
                        }

                        if (filter.PropertyName == "LastUpdateTime")
                        {
                            var aa = filter.MatchType.GetHashCode();
                            string matchType = string.Empty;
                            string fltrValue = filter.PropertyValue;
                            #region MatchType
                            switch (aa)
                            {
                                case 0: // equals
                                    matchType +=  " = '" + fltrValue + "'";
                                    break;
                                case 1: // no
                                    matchType +=  " <> '" + fltrValue + "'";
                                    break;
                                case 2: // partial
                                    matchType +=  " like '%" + fltrValue + "%'";
                                    break;
                                case 3: // startswith
                                    matchType +=  " like '" + fltrValue + "%'";
                                    break;
                                case 4: // endswith
                                    matchType +=  " like '%" + fltrValue + "'";
                                    break;
                                case 5: // greater
                                    matchType +=  " > '" + fltrValue + "'";
                                    break;
                                case 6: // lesser
                                    matchType +=  " <'" + fltrValue + "'";
                                    break;
                                case 7: // isnull
                                    matchType +=  " IS NULL";
                                    break;
                                case 8: // isnotnull
                                    matchType +=  " IS NOT NULL";
                                    break;
                                case 9: // greaterorequal
                                    matchType +=  " >='" + fltrValue + "'";
                                    break;
                                case 10: // lesserorequal 
                                    matchType +=  " <='" + fltrValue + "'";
                                    break;
                                case 11: // inset
                                    matchType +=  " IN('" + fltrValue + "')";
                                    break;
                                case 12: // notinset
                                    matchType +=  " NOT IN('" + fltrValue + "')";
                                    break;
                                case 13: // notlike
                                    matchType +=  " NOT LIKE '" + fltrValue + "'";
                                    break;
                                default:
                                    matchType += string.Empty;
                                    break;
                            }
                            #endregion

                            isHaveFilterLUT = true;
                            filter1 = "and LastUpdateTime" + matchType;
                            filter1 = filter1 + "|" + "and LastUpdatedTime" + matchType;
                            filter1 = filter1 + "||" + "and ID in (select distinct ChassisMasterID from #temp_ChassisMasterID_VI with(nolock))";
                        }
                    }
                }
                if (filterDto.sort != null)
                {
                    foreach (var sort in filterDto.sort)
                    {
                        if (sort.SortColumn == columnInner[i])
                        {
                            innersortColumn = innersortColumn != string.Empty ? ", " + columnInner[i] : columnInner[i];
                            isWithSort = true;
                        }
                        else
                        {
                            isWithSort = false;
                        }
                    }
                }
            }

            if (isWithSort) { sortPagesInner = string.Format("# ORDER BY {2} OFFSET {1} * ({0}-1) ROWS FETCH NEXT {1} ROWS ONLY", filterDto.pages, pageSize, innersortColumn != string.Empty ? innersortColumn : "ID"); }
            if (!string.IsNullOrEmpty(innerQueryCriteria) || !string.IsNullOrWhiteSpace(sortPagesInner))
                innerQueryCriteria = innerQueryCriteria + sortPagesInner;
            if (!isHaveFilterLUT) { filter1 = "||top 0|"; }

            return innerQueryCriteria;
        }
    }
}


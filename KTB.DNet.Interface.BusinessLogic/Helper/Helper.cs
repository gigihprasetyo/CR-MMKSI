#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Helper class
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
using KTB.DNet.Interface.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public static class Helper
    {
        #region Constant
        private const string DEALER_CODE = "DealerCode";
        #endregion

        #region Public Methods
        /// <summary>
        /// Get data based on its code for both active and non active data
        /// </summary>
        /// <param name="type"></param>
        /// <param name="columnName"></param>
        /// <param name="columnValue"></param>
        /// <returns></returns>
        public static CriteriaComposite GenerateCriteriaAllStatus(Type type, string columnName, object columnValue)
        {
            var criterias = new CriteriaComposite(new Criteria(type, columnName, MatchType.Exact, columnValue));

            return criterias;
        }

        /// <summary>
        /// Get data based on its code
        /// </summary>
        /// <param name="type"></param>
        /// <param name="columnName"></param>
        /// <param name="columnValue"></param>
        /// <returns></returns>
        public static CriteriaComposite GenerateCriteria(Type type)
        {
            var criterias = new CriteriaComposite(new Criteria(type, "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));

            return criterias;
        }

        /// <summary>
        /// Get data based on its code
        /// </summary>
        /// <param name="type"></param>
        /// <param name="columnName"></param>
        /// <param name="columnValue"></param>
        /// <returns></returns>
        public static CriteriaComposite GenerateCriteria(Type type, string columnName, object columnValue)
        {
            var criterias = new CriteriaComposite(new Criteria(type, "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(type, columnName, MatchType.Exact, columnValue));

            return criterias;
        }

        /// <summary>
        /// Generate criteria
        /// </summary>
        /// <param name="type"></param>
        /// <param name="columnName"></param>
        /// <param name="columnName2"></param>
        /// <param name="columnValue"></param>
        /// <param name="columnValue2"></param>
        /// <returns></returns>
        public static CriteriaComposite GenerateCriteria(Type type, string columnName, string columnName2, object columnValue, object columnValue2)
        {
            var criterias = new CriteriaComposite(new Criteria(type, "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(type, columnName, MatchType.Exact, columnValue));
            criterias.opAnd(new Criteria(type, columnName2, MatchType.Exact, columnValue2));

            return criterias;
        }

        /// <summary>
        /// Get data based on its code
        /// </summary>
        /// <param name="type"></param>
        /// <param name="columnCodeName"></param>
        /// <param name="codeValue"></param>
        /// <returns></returns>
        public static CriteriaComposite GetCodeCriteria(Type type, string columnCodeName, string codeValue)
        {
            var criterias = new CriteriaComposite(new Criteria(type, "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(type, columnCodeName, MatchType.Exact, codeValue));

            return criterias;
        }

        /// <summary>
        /// Get data based on its code
        /// </summary>
        /// <param name="type"></param>
        /// <param name="columnCodeNames"></param>
        /// <param name="codeValues"></param>
        /// <returns></returns>
        public static CriteriaComposite GetCodeCriteria(Type type, List<string> columnCodeNames, List<object> codeValues)
        {
            var criterias = new CriteriaComposite(new Criteria(type, "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            int index = 0;
            foreach (var columnName in columnCodeNames)
            {
                criterias.opAnd(new Criteria(type, columnName, MatchType.Exact, codeValues[index++]));
            }

            return criterias;
        }

        /// <summary>
        /// Update the sort column object
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filterDto"></param>
        /// <param name="sortColl"></param>
        /// <returns></returns>
        public static SortCollection UpdateSortColumn(Type type, FilterDtoBase filterDto, SortCollection sortColl)
        {
            if (filterDto.sort != null && filterDto.sort.Count > 0)
            {
                foreach (var sort in filterDto.sort)
                {
                    // skip if the sortcolumn is empty or null
                    if (string.IsNullOrEmpty(sort.SortColumn) || sort.SortColumn.Equals("*"))
                        continue;

                    // remap just in case it has vechile magic naming standard
                    string propName = sort.SortColumn
                        .Replace("VehicleTypeCode", "VechileTypeCode")
                        .Replace("VehicleModelCode", "VechileModelCode")
                        .Replace("ChassisNumber", "ChassisMaster.ChassisNumber");

                    // get the prop info to prevent an error caused by case sensitive name
                    PropertyInfo propInfo = type.GetProperty(propName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (propInfo != null)
                    {
                        propName = propInfo.Name;
                    }

                    // add sort to list
                    sortColl.Add(new Sort(type, propName, sort.SortDirection));
                }
            }

            // validate sort column
            if (filterDto.sort == null || sortColl.Count == 0)
                sortColl = null;

            return sortColl;
        }

        /// <summary>
        /// Update the criterias by its parameters
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filterDto"></param>
        /// <param name="criterias"></param>
        /// <returns></returns>
        public static CriteriaComposite BuildCriteria(Type type, FilterDtoBase filterDto)
        {
            CriteriaComposite innerCriteria = null;
            return BuildCriteria(type, filterDto, null, out innerCriteria, null);
        }

        /// <summary>
        /// BuildCriteria
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filterDto"></param>
        /// <param name="innerQueryParams"></param>
        /// <param name="innerQueryCriteria"></param>
        /// <returns></returns>
        public static CriteriaComposite BuildCriteria(Type type, FilterDtoBase filterDto, List<string> innerQueryParams, out CriteriaComposite innerQueryCriteria, Type innerQueryType)
        {
            CriteriaComposite criterias = null;
            innerQueryCriteria = null;
            if (filterDto.find != null && filterDto.find.Count > 0)
            {
                foreach (var filter in filterDto.find)
                {
                    // skip if the propertyName is null
                    if (filter.PropertyName == null || filter.PropertyName.Equals("*"))
                        continue;
                    // get the prop info to prevent an error caused by case sensitive name
                    PropertyInfo propInfo = null;
                    try
                    {
                        propInfo = type.GetProperty(filter.PropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    }
                    catch (Exception)
                    {
                        // do nothing
                    }

                    // just in case the property is not exist
                    var propName = propInfo == null ? filter.PropertyName : propInfo.Name;

                    if (innerQueryParams != null && innerQueryParams.Contains(propName))
                    {
                        try
                        {
                            propInfo = innerQueryType.GetProperty(filter.PropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        }
                        catch (Exception)
                        {
                            // do nothing
                        }

                        propName = propInfo == null ? filter.PropertyName : propInfo.Name;
                        innerQueryCriteria = AddCriteria(innerQueryType, propInfo, propName, innerQueryCriteria, filter);
                    }
                    else
                    {
                        criterias = AddCriteria(type, propInfo, propName, criterias, filter);
                    }

                }
            }

            return criterias;
        }

        /// <summary>
        /// Build Criteria
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filterDto"></param>
        /// <param name="listOfInnerCriteria"></param>
        /// <returns></returns>
        public static CriteriaComposite BuildCriteria(Type type, FilterDtoBase filterDto, Dictionary<string, InnerQueryCriteria> listOfInnerCriteria)
        {
            CriteriaComposite criterias = null;

            if (filterDto.find != null && filterDto.find.Count > 0)
            {
                foreach (var filter in filterDto.find)
                {
                    // skip if the propertyName is null
                    if (filter.PropertyName == null || filter.PropertyName.Equals("*"))
                        continue;
                    // get the prop info to prevent an error caused by case sensitive name
                    PropertyInfo propInfo = null;
                    try
                    {
                        propInfo = type.GetProperty(filter.PropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    }
                    catch (Exception)
                    {
                        // do nothing
                    }

                    // just in case the property is not exist
                    var propName = propInfo == null ? filter.PropertyName : propInfo.Name;
                    bool isInnerCriteria = false;
                    List<string> innerQueryParams;
                    foreach (var innerCriteria in listOfInnerCriteria)
                    {
                        innerQueryParams = innerCriteria.Value.InnerQueryParams;
                        if (innerQueryParams != null && innerQueryParams.Contains(propName))
                        {
                            isInnerCriteria = true;
                            try
                            {
                                propInfo = innerCriteria.Value.InnerQueryType.GetProperty(filter.PropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                            }
                            catch (Exception)
                            {
                                // do nothing
                            }

                            propName = propInfo == null ? filter.PropertyName : propInfo.Name;
                            innerCriteria.Value.Criteria = AddCriteria(innerCriteria.Value.InnerQueryType, propInfo, propName, innerCriteria.Value.Criteria, filter);
                        }
                    }

                    if (!isInnerCriteria)
                    {
                        criterias = AddCriteria(type, propInfo, propName, criterias, filter);
                    }

                }
            }

            return criterias;
        }

        /// <summary>
        /// AddCriteria
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propInfo"></param>
        /// <param name="propName"></param>
        /// <param name="criterias"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static CriteriaComposite AddCriteria(Type type, PropertyInfo propInfo, string propName, CriteriaComposite criterias, MatchTypeFilter filter)
        {
            switch (filter.SqlOperation)
            {
                case SQLOperation.And:
                    {
                        if (criterias == null)
                        {
                            criterias = new CriteriaComposite(new Criteria(type, propName, filter.MatchType, Helper.GetPropertyValue(propInfo, filter.PropertyValue)));
                        }
                        else
                        {
                            criterias.opAnd(new Criteria(type, propName, filter.MatchType, Helper.GetPropertyValue(propInfo, filter.PropertyValue)));
                        }
                        break;
                    }
                case SQLOperation.Or:
                    {
                        if (criterias == null)
                        {
                            criterias = new CriteriaComposite(new Criteria(type, propName, filter.MatchType, Helper.GetPropertyValue(propInfo, filter.PropertyValue)));
                        }
                        else
                        {
                            criterias.opOr(new Criteria(type, propName, filter.MatchType, Helper.GetPropertyValue(propInfo, filter.PropertyValue)));
                        }
                        break;
                    }
            }

            return criterias;
        }

        /// <summary>
        /// Update the criterias by its parameters
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filterDto"></param>
        /// <param name="criterias"></param>
        /// <returns></returns>
        public static CriteriaComposite UpdateCriteria(Type type, FilterDtoBase filterDto, CriteriaComposite criterias, bool isFromChassisMaster = false)
        {
            return UpdateCriteria(type, filterDto, criterias, null, isFromChassisMaster);
        }

        /// <summary>
        /// Update Criteria
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filterDto"></param>
        /// <param name="criterias"></param>
        /// <param name="innerQueryParams"></param>
        /// <param name="isFromChassisMaster"></param>
        /// <returns></returns>
        public static CriteriaComposite UpdateCriteria(Type type, FilterDtoBase filterDto, CriteriaComposite criterias, List<string> innerQueryParams, bool isFromChassisMaster = false)
        {
            bool isInnerCriteria = innerQueryParams != null && innerQueryParams.Count > 0;

            if (filterDto.find != null && filterDto.find.Count > 0)
            {
                // check if it is contains OR operator
                int index = 0;
                bool isContainsOR = filterDto.find.Count > 1 && filterDto.find.Any(x => x.SqlOperation == SQLOperation.Or);
                int totalFilter = filterDto.find.Count;

                foreach (var filter in filterDto.find)
                {
                    // skip if the propertyName is null
                    if (string.IsNullOrEmpty(filter.PropertyName))
                    {
                        continue;
                    }

                    // remap just in case vechile naming
                    string propName = filter.PropertyName;
                    string propValue = filter.PropertyValue;

                    // get the prop info to prevent an error caused by case sensitive name
                    PropertyInfo propInfo = type.GetProperty(propName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    // if contains vehicle
                    if (propInfo == null && filter.PropertyName.Contains("Vehicle"))
                    {
                        // replace with DNET naming
                        propName = filter.PropertyName.Replace("VehicleTypeCode", "VechileTypeCode").Replace("VehicleModelCode", "VechileModelCode");

                        propInfo = type.GetProperty(propName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    }

                    // just in case does not found matched property name check if its reference object
                    if (propInfo == null)
                    {
                        if (propName.ToUpper().Contains("CODE") || propName.ToUpper().Equals("CHASSISNUMBER"))
                        {
                            // get prop name and value based on its ref object
                            GetReferenceProperty(type, filter, ref propName, ref propValue, isFromChassisMaster);
                        }
                        else
                        {
                            propName = filter.PropertyName;
                        }
                    }
                    else
                    {
                        propName = propInfo.Name;
                    }

                    bool doAddCriteria = true;

                    if (isInnerCriteria)
                    {
                        doAddCriteria = innerQueryParams.Contains(propName);
                    }

                    if (doAddCriteria)
                    {
                        criterias = AddCriteria(type, propInfo, propName, criterias, filter, isContainsOR, index, propValue, totalFilter);

                        index++;
                    }

                }
            }

            return criterias;
        }
        /// <summary>
        /// Add Criteria
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propInfo"></param>
        /// <param name="propName"></param>
        /// <param name="criterias"></param>
        /// <param name="filter"></param>
        /// <param name="isContainsOR"></param>
        /// <param name="index"></param>
        /// <param name="propValue"></param>
        /// <param name="totalFilter"></param>
        /// <returns></returns>
        private static CriteriaComposite AddCriteria(Type type, PropertyInfo propInfo, string propName, CriteriaComposite criterias, MatchTypeFilter filter, bool isContainsOR, int index, string propValue, int totalFilter)
        {
            switch (filter.SqlOperation)
            {
                case SQLOperation.And:
                    if (isContainsOR && index == 0)
                    {
                        if (criterias == null)
                        {
                            criterias = new CriteriaComposite(new Criteria(type, propName, filter.MatchType, Helper.GetPropertyValue(propInfo, propValue)));
                        }
                        else
                        {
                            criterias.opAnd(new Criteria(type, propName, filter.MatchType, Helper.GetPropertyValue(propInfo, propValue)), "(", true);
                        }
                    }
                    else
                    {
                        if (criterias == null)
                        {
                            criterias = new CriteriaComposite(new Criteria(type, propName, filter.MatchType, Helper.GetPropertyValue(propInfo, propValue)));
                        }
                        else
                        {
                            criterias.opAnd(new Criteria(type, propName, filter.MatchType, Helper.GetPropertyValue(propInfo, propValue)));
                        }
                    }

                    break;
                case SQLOperation.Or:
                    if (criterias == null)
                    {
                        criterias = new CriteriaComposite(new Criteria(type, propName, filter.MatchType, Helper.GetPropertyValue(propInfo, propValue)));
                    }
                    else
                    {
                        if (isContainsOR && index == totalFilter - 1)
                            criterias.opOr(new Criteria(type, propName, filter.MatchType, Helper.GetPropertyValue(propInfo, propValue)), ")", false);
                        else
                            criterias.opOr(new Criteria(type, propName, filter.MatchType, Helper.GetPropertyValue(propInfo, propValue)));

                    }
                    break;
            }

            return criterias;
        }

        /// <summary>
        /// Get the property value
        /// </summary>
        /// <param name="p"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object GetPropertyValue(PropertyInfo p, string value)
        {
            if (p == null || string.IsNullOrEmpty(value))
                return value;

            Type propType = p.PropertyType;
            if (propType == typeof(int))
            {
                return Convert.ToInt32(value);
            }
            else if (propType == typeof(Int16))
            {
                return Convert.ToInt16(value);
            }
            else if (propType == typeof(short))
            {
                return short.Parse(value);
            }
            else if (propType == typeof(Boolean))
            {
                if (value.Equals("true", StringComparison.OrdinalIgnoreCase) || value.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    return value.Equals("true", StringComparison.OrdinalIgnoreCase) ? 1 : 0;
                }
                else
                {
                    return Convert.ToInt16(value);
                }
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Get query for dapper 
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="criterias"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortColl"></param>
        /// <returns></returns>
        public static string GetDapperQuerySparepartDO(string sqlTemplate, FilterDtoBase filterDto, string dealerCode, int pageSize, SortCollection sortColl)
        {
            // calculate start and end date
            int startPage = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;
            int endPage = startPage + pageSize;

            var rawSql = string.Empty;

            // populate the sort info
            sortColl = UpdateSortColumn(typeof(VWI_SparePartPODOHaveBilling), filterDto, sortColl);

            if (sortColl != null)
                rawSql += " ORDER BY " + sortColl.ToString();

            rawSql = string.Format(sqlTemplate, dealerCode, rawSql);

            return rawSql;
        }

        /// <summary>
        /// Get query for dapper 
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="criterias"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortColl"></param>
        /// <returns></returns>
        public static string GetDapperQuery(string sqlTemplate, FilterDtoBase filterDto, CriteriaComposite criterias, int pageSize, SortCollection sortColl, Type type)
        {
            // construct sql
            string sql = criterias == null ? string.Empty : criterias.ToString();
            string rawSql = sql;

            // calculate start and end date
            int startPage = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;
            int endPage = startPage + pageSize;

            if (string.IsNullOrEmpty(sql))
            {
                rawSql += " WHERE ID > " + startPage + " AND ID <= " + endPage;
            }
            else if (type == typeof(VWI_SparePartPODOHaveBilling) || type == typeof(VWI_ServiceHistory))
            {
                rawSql += " AND ID > " + startPage + " AND ID <= " + endPage;
            }

            // populate the sort info
            sortColl = UpdateSortColumn(type, filterDto, sortColl);

            if (sortColl != null)
                rawSql += " ORDER BY " + sortColl.ToString();

            rawSql = string.Format(sqlTemplate, sql, rawSql);

            return rawSql;
        }

        /// <summary>
        /// Get query for dapper 
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="criterias"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortColl"></param>
        /// <returns></returns>
        public static string GetDapperQueryView(string sqlTemplate, FilterDtoBase filterDto, CriteriaComposite criterias, SortCollection sortColl, Type type, string typeName, string dealerCode = null, bool isReplaceWhere = false)
        {
            // construct sql            
            string rawSql = isReplaceWhere ? criterias.ToString().Replace(typeName + ".", "a.").Replace("WHERE", "AND") : criterias.ToString().Replace(typeName + ".", "");

            // only if user sent TermOfPaymentValue as param
            if (rawSql.Contains("a.TermOfPaymentValue"))
            {
                rawSql = Regex.Replace(rawSql, @"\ba.TermOfPaymentValue\b", "m.TermOfPaymentValue", RegexOptions.IgnoreCase);
            }

            // populate the sort info
            sortColl = UpdateSortColumn(type, filterDto, sortColl);
            if (sortColl != null)
            {
                if (string.IsNullOrEmpty(dealerCode))
                    rawSql = string.Format(sqlTemplate, rawSql) + " ORDER BY " + sortColl.ToString().Replace(typeName + ".", "");
                else
                    rawSql = string.Format(sqlTemplate, rawSql) + " WHERE DealerCode='" + dealerCode + "'" + " ORDER BY " + sortColl.ToString().Replace(typeName + ".", "");
            }
            else
            {
                if (string.IsNullOrEmpty(dealerCode))
                    rawSql = string.Format(sqlTemplate, rawSql);
                else
                    rawSql = string.Format(sqlTemplate, rawSql) + " WHERE DealerCode='" + dealerCode + "'";
            }

            return rawSql;
        }

        /// <summary>
        /// Generate sql from criterias and sort
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filterDto"></param>
        /// <param name="sortColl"></param>
        /// <param name="criterias"></param>
        /// <returns></returns>
        public static string GenerateSQLFromCriteriasAndSort(Type type, FilterDtoBase filterDto, SortCollection sortColl, CriteriaComposite criterias = null)
        {
            // populate criterias
            if (criterias == null)
            {
                criterias = Helper.BuildCriteria(type, filterDto);
            }
            else
            {
                criterias = UpdateCriteria(type, filterDto, criterias);
            }

            // populate the sort info
            sortColl = UpdateSortColumn(type, filterDto, sortColl);

            // construct sql
            string sql = criterias == null ? string.Empty : criterias.ToString();
            if (sortColl != null && sortColl.Count > 0)
            {
                foreach (object obj in sortColl)
                {
                    StringCollection joinClauses = ((Sort)(obj)).GetJoinClause();
                    foreach (string joinClause in joinClauses)
                    {
                        if (sql.IndexOf(joinClause) == -1)
                        {
                            sql = sql.Insert(sql.IndexOf(" WHERE "), joinClause);
                        }
                    }
                }

                sql = sql + (" ORDER BY " + sortColl.ToString());
                if (sql.EndsWith(" ORDER BY "))
                {
                    sql = sql.Replace(" ORDER BY ", "");
                }
            }

            return sql;
        }

        /// <summary>
        /// Get dealer code information from parameters
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static string GetDealerCodeFromFilter(FilterDtoBase filters)
        {
            var dealer = filters.find.Where(x => x.PropertyName.Equals(DEALER_CODE, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            return dealer == null ? null : dealer.PropertyValue;
        }

        /// <summary>
        /// Populate the error message
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filterDto"></param>
        /// <param name="col"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string GetCriteriasMessageFormat(Type type, FilterDtoBase filterDto, string col = null, string val = null)
        {
            string result = "";
            if (filterDto != null && filterDto.find != null && filterDto.find.Count > 0)
            {
                var last = filterDto.find.Last();
                foreach (var filter in filterDto.find)
                {
                    // skip if the propertyName is null
                    if (string.IsNullOrEmpty(filter.PropertyName))
                    {
                        continue;
                    }

                    // get the prop info to prevent an error caused by case sensitive name
                    PropertyInfo propInfo = type.GetProperty(filter.PropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    string propName = filter.PropertyName;
                    string propValue = filter.PropertyValue;

                    if (filter.MatchType.ToString() == "IsNotNull" || filter.MatchType.ToString() == "IsNull")
                        result += "'" + propName + "' " + ParseMatchType(filter.MatchType);
                    else
                        result += "'" + propName + "' " + ParseMatchType(filter.MatchType) + " '" + Helper.GetPropertyValue(propInfo, propValue) + "'";

                    if (!filter.Equals(last))
                    {
                        result += ", ";
                    }
                }
            }
            if (!string.IsNullOrEmpty(col) && !string.IsNullOrEmpty(val))
            {
                result = "'" + col + "' = '" + val + "'";
            }

            if (string.IsNullOrEmpty(result))
            {
                result = "kriteria tersebut";
            }

            return result;
        }

        /// <summary>
        /// Get user name
        /// </summary>
        /// <param name="dealerCode"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetUserName(string dealerCode, string user)
        {
            var usernamestr = string.Concat(string.Concat(dealerCode, "_IF_"), user);

            string username =
                !String.IsNullOrWhiteSpace(usernamestr) && usernamestr.Length >= 20
                ? usernamestr.Substring(0, 20)
                : usernamestr;

            return username;
        }

        /// <summary>
        /// get auto send to SAP flag
        /// </summary>
        /// <returns></returns>
        public static bool IsAutoSendToSAP(AutoMapper.IMapper mapper)
        {
            // get auto send to SAP flag
            IAppConfigBL appConfigBL = new AppConfigBL(mapper);

            try
            {
                AppConfig appConfig = appConfigBL.GetConfigByName("SP_POAutoSendToSAP");
                if (appConfig != null)
                {
                    return (appConfig.Value.Trim().Equals("1"));
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// get sparepart non top id
        /// </summary>
        /// <returns></returns>
        public static int GetSparePartNonTOPID(AutoMapper.IMapper mapper)
        {
            // get auto send to SAP flag
            IAppConfigBL appConfigBL = new AppConfigBL(mapper);

            try
            {
                AppConfig appConfig = appConfigBL.GetConfigByName("SparePartNonTopID");
                if (appConfig != null)
                {
                    return int.Parse(appConfig.Value.Trim());
                }

                return 1;
            }
            catch
            {
                return 1;
            }
        }

        /// <summary>
        /// create string criterias by its parameters
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filterDto"></param>
        /// <param name="criterias"></param>
        /// <returns></returns>
        public static string InitialStrCriteria(Type type, FilterDtoBase filterDto)
        {
            var strSQL = " 1=1 ";

            if (filterDto.find != null && filterDto.find.Count > 0)
            {
                foreach (var filter in filterDto.find)
                {
                    if (strSQL == " 1=1 ")
                    {
                        strSQL = UpdateStrCriteria(type, new MatchTypeFilter { SqlOperation = filter.SqlOperation }, filter.MatchType.GetHashCode(), filter.PropertyName, filter.PropertyValue, true);
                    }
                    else
                    {
                        strSQL = UpdateStrCriteria(type, new MatchTypeFilter { SqlOperation = filter.SqlOperation }, filter.MatchType.GetHashCode(), filter.PropertyName, filter.PropertyValue, false, strSQL);
                    }

                }

                return strSQL;
            }
            else
            {
                strSQL = " WHERE" + strSQL;
            }

            return strSQL;
        }

        public static string UpdateStrCriteria(Type type, MatchTypeFilter filter, int matchType, string columnName, string Value, bool firstFilter = false, string existingCriteria = "")
        {
            var strSQL = string.Empty;
            var strSQLOperation = string.Empty;
            var strSQLMatchType = string.Empty;

            #region MatchType
            switch (matchType)
            {
                case 0: // equals
                    strSQLMatchType += type.Name + "." + columnName + " = '" + Value + "'";
                    break;
                case 1: // no
                    strSQLMatchType += type.Name + "." + columnName + " <> '" + Value + "'";
                    break;
                case 2: // partial
                    strSQLMatchType += type.Name + "." + columnName + " like '%" + Value + "%'";
                    break;
                case 3: // startswith
                    strSQLMatchType += type.Name + "." + columnName + " like '" + Value + "%'";
                    break;
                case 4: // endswith
                    strSQLMatchType += type.Name + "." + columnName + " like '%" + Value + "'";
                    break;
                case 5: // greater
                    strSQLMatchType += type.Name + "." + columnName + " > '" + Value + "'";
                    break;
                case 6: // lesser
                    strSQLMatchType += type.Name + "." + columnName + " <'" + Value + "'";
                    break;
                case 7: // isnull
                    strSQLMatchType += type.Name + "." + columnName + " IS NULL";
                    break;
                case 8: // isnotnull
                    strSQLMatchType += type.Name + "." + columnName + " IS NOT NULL";
                    break;
                case 9: // greaterorequal
                    strSQLMatchType += type.Name + "." + columnName + " >='" + Value + "'";
                    break;
                case 10: // lesserorequal 
                    strSQLMatchType += type.Name + "." + columnName + " <='" + Value + "'";
                    break;
                case 11: // inset
                    strSQLMatchType += type.Name + "." + columnName + " IN('" + Value + "')";
                    break;
                case 12: // notinset
                    strSQLMatchType += type.Name + "." + columnName + " NOT IN('" + Value + "')";
                    break;
                case 13: // notlike
                    strSQLMatchType += type.Name + "." + columnName + " NOT LIKE '" + Value + "'";
                    break;
                default:
                    strSQLMatchType += string.Empty;
                    break;
            }
            #endregion

            if (filter.SqlOperation == SQLOperation.And)
            {
                strSQLOperation = " AND ";
            }
            else if (filter.SqlOperation == SQLOperation.Or)
            {
                if (firstFilter)
                {
                    strSQLOperation = " AND ";
                }
                else
                {
                    strSQLOperation = " OR ";
                }
            }

            if (columnName == "xts_company" || columnName == "dataAreaId" || columnName == "Company" || columnName == "msdyn_companycode" || columnName == "DealerCode")
            {
                if (existingCriteria.Contains("WHERE"))
                {
                    strSQL = "WHERE " + strSQLMatchType + " AND 1=1 " + existingCriteria.Replace("WHERE 1=1", string.Empty).Replace("WHERE", " AND ");
                }
                else
                {
                    strSQL = "WHERE " + strSQLMatchType + " AND 1=1 ";
                }
            }
            else
            {
                if (existingCriteria.Contains("WHERE") && existingCriteria.Trim().Substring(existingCriteria.Trim().Length - 1) == ")")
                {
                    strSQL = existingCriteria.Remove(existingCriteria.Length - 1, 1) + " " + strSQLOperation + strSQLMatchType + ")";
                    //strSQL = existingCriteria.Replace(")", " " + strSQLOperation + strSQLMatchType + ")");
                }
                else if (existingCriteria.Contains("WHERE") && existingCriteria.Trim().Substring(existingCriteria.Trim().Length - 1) != ")")
                {
                    strSQL = existingCriteria + strSQLOperation + strSQLMatchType;
                }
                else
                {
                    strSQL = " WHERE 1=1 " + strSQLOperation + "(" + strSQLMatchType + ")";
                }

                //if (existingCriteria.Contains("WHERE"))
                //{
                //    strSQL = existingCriteria.Replace(")", " " + strSQLOperation + strSQLMatchType + ")");
                //}
                //else
                //{
                //    strSQL = " WHERE 1=1 " + strSQLOperation + "(" + strSQLMatchType + ")";
                //}

            }

            return strSQL;
        }

        public static string UpdateSortColumnDapper(Type type, FilterDtoBase filterDto)
        {
            var strSort = string.Empty;
            if (filterDto.sort != null && filterDto.sort.Count > 0)
            {
                foreach (var sort in filterDto.sort)
                {
                    // skip if the sortcolumn is empty or null
                    if (string.IsNullOrEmpty(sort.SortColumn) || sort.SortColumn.Equals("*"))
                        continue;

                    // remap just in case it has vechile magic naming standard
                    string propName = sort.SortColumn.Replace("VehicleTypeCode", "VechileTypeCode").Replace("VehicleModelCode", "VechileModelCode");

                    // get Sort Direction
                    var sortDirection = (sort.SortDirection == 0) ? "ASC" : "DESC";

                    // add sort to list
                    strSort += " o." + propName + " " + sortDirection + ",";
                }
                // remove "," on end of string
                strSort = strSort.Substring(0, strSort.Length - 1);
            }

            // validate sort column
            if (filterDto.sort == null)
                strSort = string.Empty;

            return strSort;
        }

        public static string UpdateSortColumnDapper(Type type, FilterDtoBase filterDto, String aliasName = "")
        {
            var strSort = string.Empty;
            if (filterDto.sort != null && filterDto.sort.Count > 0)
            {
                foreach (var sort in filterDto.sort)
                {
                    // skip if the sortcolumn is empty or null
                    if (string.IsNullOrEmpty(sort.SortColumn) || sort.SortColumn.Equals("*"))
                        continue;

                    // remap just in case it has vechile magic naming standard
                    string propName = sort.SortColumn.Replace("VehicleTypeCode", "VechileTypeCode").Replace("VehicleModelCode", "VechileModelCode");

                    // get Sort Direction
                    var sortDirection = (sort.SortDirection == 0) ? "ASC" : "DESC";

                    // add sort to list
                    if (aliasName == "")
                    { strSort += " o." + propName + " " + sortDirection + ","; }
                    else
                    { strSort += " " + aliasName + "." + propName + " " + sortDirection + ","; }

                }
                // remove "," on end of string
                strSort = strSort.Substring(0, strSort.Length - 1);
            }

            // validate sort column
            if (filterDto.sort == null)
                strSort = string.Empty;

            return strSort;
        }
        #endregion

        #region PrivatMethods
        /// <summary>
        /// Parse the matchtype into error message
        /// </summary>
        /// <param name="matchType"></param>
        /// <returns></returns>
        private static string ParseMatchType(MatchType matchType)
        {
            string result = string.Empty;
            switch (matchType)
            {
                case MatchType.Exact:
                    result = "=";
                    break;
                case MatchType.No:
                    result = "<>";
                    break;
                case MatchType.StartsWith:
                    result = "dimulai dengan";
                    break;
                case MatchType.EndsWith:
                    result = "diakhiri dengan";
                    break;
                case MatchType.Greater:
                    result = ">";
                    break;
                case MatchType.GreaterOrEqual:
                    result = ">=";
                    break;
                case MatchType.Partial:
                    result = "partial";
                    break;
                case MatchType.Lesser:
                    result = "<";
                    break;
                case MatchType.LesserOrEqual:
                    result = "<=";
                    break;
                case MatchType.IsNotNull:
                    result = "!= Null";
                    break;
                case MatchType.IsNull:
                    result = "= Null";
                    break;
                case MatchType.InSet:
                    result = "in";
                    break;
                case MatchType.NotInSet:
                    result = "not in";
                    break;
                case MatchType.NotLike:
                    result = "not like";
                    break;
            }

            return result;
        }

        /// <summary>
        /// Get the proper property name and property value from ref object
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        private static void GetReferenceProperty(Type type, MatchTypeFilter filter, ref string propName, ref string propValue, bool isFromChassisMaster = false)
        {
            // special handler for ChassisMaster since its vechiletype is not vechileType object
            if (propName.Equals("VehicleTypeCode", StringComparison.OrdinalIgnoreCase) && isFromChassisMaster)
            {
                propName = "VechileColor.VechileType.VechileTypeCode";
            }
            else if (propName.Equals("ChassisNumber", StringComparison.OrdinalIgnoreCase))
            {
                propName = "ChassisMaster.ChassisNumber";
            }
            else if (propName.Equals("VehicleColorCode", StringComparison.OrdinalIgnoreCase))
            {
                // we doesn't support the vehicle color in this stage since vehicleColor code is not unique and 
                // we need another criteria to get the valid vehicle color id
                return;
            }
            else
            {
                // replace some keywords due to improper naming in DNET
                string tempPropName = ReplaceImproperNaming(filter);

                // get the property list
                var propList = type.GetProperties().Where(x => tempPropName.Contains(x.Name) && !x.Name.Equals("ID"));

                // check each item
                foreach (var item in propList)
                {
                    if (item.CustomAttributes.Any(y => y.ConstructorArguments.Any(z => z.Value.Equals(tempPropName))))
                    {
                        if (item.Name.Equals("Dealer"))
                        {
                            // get the dealer ID
                            var dealerMapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());
                            var dealers = dealerMapper.RetrieveByCriteria(Helper.GetCodeCriteria(typeof(Dealer), "DealerCode", propValue));
                            if (dealers.Count > 0)
                            {
                                propValue = (dealers[0] as Dealer).ID.ToString();
                                propName = "Dealer.ID";
                                break;
                            }
                        }
                        else if (item.Name.Equals("DealerBranch"))
                        {
                            // get the dealer branch ID
                            var dealerMapper = MapperFactory.GetInstance().GetMapper(typeof(DealerBranch).ToString());
                            var dealers = dealerMapper.RetrieveByCriteria(Helper.GetCodeCriteria(typeof(DealerBranch), "DealerBranchCode", propValue));
                            if (dealers.Count > 0)
                            {
                                propValue = (dealers[0] as DealerBranch).ID.ToString();
                                propName = "DealerBranch.ID";
                                break;
                            }
                        }
                        else if (item.Name.Equals("VehicleKind"))
                        {
                            // get the vehicle kind ID
                            var vehKindMapper = MapperFactory.GetInstance().GetMapper(typeof(VehicleKind).ToString());
                            var vehKinds = vehKindMapper.RetrieveByCriteria(Helper.GetCodeCriteria(typeof(VehicleKind), "Code", propValue));
                            if (vehKinds.Count > 0)
                            {
                                propValue = (vehKinds[0] as VehicleKind).ID.ToString();
                                propName = "VehicleKind.ID";
                                break;
                            }
                        }
                        else if (item.Name.Equals("VechileType"))
                        {
                            // get the vehicle type ID
                            var vehTypeMapper = MapperFactory.GetInstance().GetMapper(typeof(VechileType).ToString());
                            var vehTypes = vehTypeMapper.RetrieveByCriteria(Helper.GetCodeCriteria(typeof(VechileType), "Code", propValue));
                            if (vehTypes.Count > 0)
                            {
                                propValue = (vehTypes[0] as VechileType).ID.ToString();
                                propName = "VechileType.ID";
                                break;
                            }
                        }
                        else if (item.Name.Equals("TermOfPayment"))
                        {
                            // get the term of payment ID
                            var termOfPaymentMapper = MapperFactory.GetInstance().GetMapper(typeof(TermOfPayment).ToString());
                            var tmPayments = termOfPaymentMapper.RetrieveByCriteria(Helper.GetCodeCriteria(typeof(TermOfPayment), "TermOfPaymentCode", propValue));
                            if (tmPayments.Count > 0)
                            {
                                propValue = (tmPayments[0] as TermOfPayment).ID.ToString();
                                propName = "TermOfPayment.ID";
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Replace some improper naming
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static string ReplaceImproperNaming(MatchTypeFilter filter)
        {
            string tempPropName = filter.PropertyName.Replace("CODE", "ID").Replace("Code", "ID").Replace("VehicleColor", "VechileColor").Replace("VehicleModel", "VechileModel").Replace("VehicleType", "VechileType");
            return tempPropName;
        }
        #endregion
    }

    public class InnerQueryCriteria
    {
        public List<string> InnerQueryParams { get; set; }
        public CriteriaComposite Criteria { get; set; }
        public Type InnerQueryType { get; set; }
    }
}

using KTB.DNet.Domain.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace KTB.DNet.Interface.Repository.Dapper
{
    public static partial class ObjectQueryExtentions
    {
        #region SqlBuilder Has Been Removed
        //        public static string BuildWhereQuery(this object instance, string search, List<string> lookupColumns = null)
        //        {
        //            lookupColumns = lookupColumns ?? new List<string>();
        //            string result = string.Empty;

        //            try
        //            {
        //                if (!string.IsNullOrEmpty(search))
        //                {
        //                    Type type = instance.GetType();

        //                    IList<PropertyInfo> properties = type.GetProperties().ToList();
        //                    var scriptBuilder = new SqlBuilder();
        //                    foreach (var p in properties)
        //                    {
        //                        var cusAttr = (SearchAble)p.GetCustomAttribute(typeof(SearchAble), false);

        //                        if (cusAttr != null)
        //                        {
        //                            bool val = false;
        //                            val = cusAttr.Value;
        //                            if (val)
        //                            {
        //                                scriptBuilder.OrWhere(p.Name + " like '%" + search + "%'");
        //                            }
        //                        }
        //                    }

        //                    if (lookupColumns.Count > 0)
        //                    {
        //                        foreach (string i in lookupColumns)
        //                        {
        //                            scriptBuilder.OrWhere(i + " like '%" + search + "%'");
        //                        }
        //                    }

        //                    var sql = scriptBuilder.AddTemplate("/**where**/");
        //                    if (sql.RawSql.Length > 5)
        //                    {
        //                        result = sql.RawSql;
        //                    }
        //                }

        //            }
        //            catch (Exception e)
        //            {
        //                throw (e);
        //            }


        //            return result;
        //        }

        //        public static string BuildPagingQuery(this object instance, string defaultOrder)
        //        {
        //            string result = string.Empty;
        //            try
        //            {
        //                Type type = instance.GetType();

        //                var attribute = (System.ComponentModel.DataAnnotations.Schema.TableAttribute)type.GetCustomAttribute(typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute), false);

        //                if (attribute != null)
        //                {
        //                    string table = attribute.Name;
        //                    if (!string.IsNullOrEmpty(table))
        //                    {
        //                        result = string.Format(@"( SELECT * FROM (
        //                                SELECT ROW_NUMBER() OVER ( order by {0} ) AS RowNum, * 
        //                                FROM {1} ) AS RowConstrainedResult
        //                                WHERE RowNum >= ( (@PageIndex/@PageSize) * @PageSize + 1 )
        //                                AND RowNum <= ((@PageIndex/@PageSize) + 1) * @PageSize ) AS {1}", defaultOrder, table);
        //                    }
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                throw (e);
        //            }

        //            return result;
        //        }

        //        public static string BuildPagingQuery<T>(SqlBuilder builder)
        //        {
        //            string result = string.Empty;
        //            try
        //            {
        //                object o = (T)Activator.CreateInstance(typeof(T));
        //                Type type = o.GetType();

        //                var attribute = (System.ComponentModel.DataAnnotations.Schema.TableAttribute)type.GetCustomAttribute(typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute), false);

        //                if (attribute != null)
        //                {
        //                    string table = attribute.Name;
        //                    if (!string.IsNullOrEmpty(table))
        //                    {
        //                        string template = string.Format(@"SELECT * FROM (
        //                                SELECT ROW_NUMBER() OVER ( /**orderby**/ ) AS RowNum, {0}.*                                 
        //                                FROM {0} /**leftjoin**/ /**where**/ ) AS {0}
        //                                /**leftjoin**/
        //                                WHERE RowNum >= ( (@PageIndex/@PageSize) * @PageSize + 1 )
        //                                AND RowNum <= ((@PageIndex/@PageSize) + 1) * @PageSize", table);

        //                        var query = builder.AddTemplate(template);
        //                        result = query.RawSql;
        //                    }
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                throw (e);
        //            }

        //            return result;
        //        }

        //        public static void BuildWhereQuery(this object instance, ref SqlBuilder scriptBuilder, string search, List<string> lookupColumns = null)
        //        {
        //            scriptBuilder = scriptBuilder ?? new SqlBuilder();
        //            lookupColumns = lookupColumns ?? new List<string>();

        //            if (!string.IsNullOrEmpty(search))
        //            {
        //                Type type = instance.GetType();
        //                var attribute = (System.ComponentModel.DataAnnotations.Schema.TableAttribute)type.GetCustomAttribute(typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute), false);

        //                IList<PropertyInfo> properties = type.GetProperties().ToList();
        //                foreach (var p in properties)
        //                {
        //                    var cusAttr = (SearchAble)p.GetCustomAttribute(typeof(SearchAble), false);

        //                    if (cusAttr != null)
        //                    {
        //                        bool val = false;
        //                        val = cusAttr.Value;
        //                        if (val)
        //                        {
        //                            scriptBuilder.OrWhere(attribute.Name + "." + p.Name + " like '%" + search + "%'");
        //                        }
        //                    }
        //                }

        //                if (lookupColumns.Count > 0)
        //                {
        //                    foreach (string i in lookupColumns)
        //                    {
        //                        scriptBuilder.OrWhere(i + " like '%" + search + "%'");
        //                    }
        //                }
        //            }
        //        }

        //        public static string GetTableName(this object instance)
        //        {
        //            string result = string.Empty;
        //            try
        //            {
        //                Type type = instance.GetType();
        //                var attribute = (System.ComponentModel.DataAnnotations.Schema.TableAttribute)type.GetCustomAttribute(typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute), false);

        //                if (attribute != null)
        //                {
        //                    result = attribute.Name;
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                throw (e);
        //            }

        //            return result;
        //        }

        //        public static string BuildBulkInsertCommand(this object instance, string tableName)
        //        {
        //            string result = string.Empty;

        //            try
        //            {
        //                Type type = instance.GetType();
        //                IList<PropertyInfo> properties = type.GetProperties().ToList();
        //                result = "INSERT " + tableName + " (";
        //                List<string> propNames = new List<string>();

        //                foreach (var p in properties)
        //                {
        //                    var notMappedAttr = (System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute)p.GetCustomAttribute(typeof(System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute), false);
        //                    var keyAttr = (System.ComponentModel.DataAnnotations.KeyAttribute)p.GetCustomAttribute(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), false);
        //                    var ignoreAttr = (Ignore)p.GetCustomAttribute(typeof(Ignore), false);

        //                    if (notMappedAttr == null && keyAttr == null && ignoreAttr == null && p.GetSetMethod().IsPublic)
        //                    {
        //                        propNames.Add(p.Name);
        //                    }
        //                }

        //                result += string.Join(", ", propNames);
        //                result += ") ";

        //                propNames = propNames.Select(e => "@" + e).ToList();
        //                string valueBuilder = "VALUES (";
        //                valueBuilder += string.Join(", ", propNames);
        //                valueBuilder += ") ";

        //                result += valueBuilder;
        //            }
        //            catch (Exception e)
        //            {
        //                throw (e);
        //            }

        //            return result;
        //        }

        //        public static int SearchCount<T>(this IDbConnection cn, SqlBuilder queryBuilder = null, object parameters = null)
        //        {
        //            var name = string.Empty;

        //            object o = (T)Activator.CreateInstance(typeof(T));
        //            Type type = o.GetType();
        //            var attribute = (System.ComponentModel.DataAnnotations.Schema.TableAttribute)type.GetCustomAttribute(typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute), false);
        //            if (attribute != null)
        //            {
        //                if (!string.IsNullOrEmpty(attribute.Name))
        //                {
        //                    name = attribute.Name;
        //                }
        //            }

        //            var selectorSC = queryBuilder.AddTemplate(string.Format(CommonQuery.BuildSearchCount, name));

        //            return cn.ExecuteScalar<int>(selectorSC.RawSql, parameters);
        //        } 
        #endregion

        public static DataTable ToDataTableForCreate<T>(this IList<T> data, List<string> nullableIntColumns = null)
        {

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();

            // Set Row Header
            foreach (PropertyDescriptor prop in properties)
            {

                bool isHaveRelationInfo = prop.Attributes.OfType<RelationInfoAttribute>().Any();
                // skip all relation property
                if (!isHaveRelationInfo)
                {
                    // skip custom property column
                    if (prop.Name.ToLower() != "isloaded" && prop.Name.ToLower() != "errormessage" && prop.Name.ToLower() != "isnotchange")
                    {
                        table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                    }
                }
            }

            // set Row value
            foreach (T item in data)
            {

                DataRow row = table.NewRow();
                List<string> propListSetToNull = new List<string>();

                foreach (PropertyDescriptor prop in properties)
                {
                    bool isHaverelationInfo = prop.Attributes.OfType<RelationInfoAttribute>().Any();
                    // skip all relation property
                    if (!isHaverelationInfo)
                    {
                        // skip custom property column
                        if (prop.Name.ToLower() != "isloaded" && prop.Name.ToLower() != "errormessage" && prop.Name.ToLower() != "isnotchange")
                        {
                            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                        }
                    }
                    else if (isHaverelationInfo)
                    {
                        ColumnInfoAttribute attr = prop.Attributes.OfType<ColumnInfoAttribute>().FirstOrDefault();
                        if (attr != null)
                        {
                            string colName = attr.ColumnName;
                            PropertyDescriptor relationProp = properties.Find(colName, false);
                            // add nullable int on the relationship property
                            var propvalue = relationProp.GetValue(item);
                            if ((int)propvalue == 0)
                            {
                                propListSetToNull.Add(colName);
                            }
                        }

                    }
                }

                // add nulable int for property that  didn't have relation info
                if (nullableIntColumns != null)
                {
                    foreach (string nullablePropName in nullableIntColumns)
                    {
                        int val;
                        if (int.TryParse(row[nullablePropName].ToString(), out val))
                        {
                            if (val == 0)
                            {
                                propListSetToNull.Add(nullablePropName);
                            }
                        }
                    }
                }

                // set null value for nullable int
                foreach (string propName in propListSetToNull)
                {
                    row[propName] = DBNull.Value;
                }

                table.Rows.Add(row);
            }

            return table;

        }
    }
}

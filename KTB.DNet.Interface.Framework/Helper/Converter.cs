#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Converter  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $ Muhamad Ridwan - Initial
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace KTB.DNet.Interface.Framework
{
    public static class Converter
    {
        public static TD ConvertObject<TD>(this object obj, TD existingObj = null) where TD : class, new()
        {
            if (obj == null)
            {
                return null;
            }

            TD result = new TD();

            if (existingObj != null)
            {
                result = existingObj;
            }

            PropertyInfo[] sourceProperties = obj.GetType().GetProperties();
            PropertyInfo[] destProperties = typeof(TD).GetProperties();
            foreach (PropertyInfo property in sourceProperties)
            {
                try
                {
                    object sourceVal = property.GetValue(obj, null);
                    if (sourceVal != null)
                    {
                        PropertyInfo destProp = result.GetType().GetProperty(property.Name);
                        if (destProp != null)
                        {
                            object destVal = destProp.GetValue(result, null);

                            if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                            {
                                MethodInfo mi = typeof(Converter).GetMethod("ConvertObject");
                                MethodInfo miConstructed = mi.MakeGenericMethod(destProp.PropertyType);
                                object[] args = { sourceVal, destVal };

                                if (property.PropertyType.IsArray || property.PropertyType.Name.ToUpper().Contains("LIST"))
                                {
                                    mi = typeof(Converter).GetMethod("ConvertList");
                                    miConstructed = mi.MakeGenericMethod(property.PropertyType.GenericTypeArguments[0], destProp.PropertyType.GenericTypeArguments[0]);
                                    args = new[] { sourceVal };
                                }

                                object target = miConstructed.Invoke(null, args);
                                sourceVal = target;

                                destProp.SetValue(result, sourceVal);
                            }
                            else if (IsConvertible(sourceVal, destProp.PropertyType))
                            {
                                var validType = destProp.PropertyType;

                                // get the  underlying type if its nullable type
                                if (validType.IsGenericType && validType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                                {
                                    validType = Nullable.GetUnderlyingType(validType);
                                }

                                destProp.SetValue(result, Convert.ChangeType(sourceVal, validType));
                            }
                        }
                    }
                }
                catch
                {
                    //string errorMessage = string.Format("Error to find DBColumnAttribute on {0}. ", property.Name);
                    //this._appLog.WriteMessage(DateTime.Now, errorMessage + GetInnerMostException(e).Message);
                }
            }

            return result;
        }

        private static bool IsConvertible(object value, Type conversionType)
        {
            if (conversionType == value.GetType()) { return true; }

            if (conversionType == null || value == null || !(value is IConvertible))
            {
                return false;
            }

            return true;
        }

        public static List<TD> ConvertList<TS, TD>(this List<TS> listObj)
            where TD : class, new()
            where TS : class, new()
        {
            List<TD> result = new List<TD>();

            if (listObj != null)
            {
                if (listObj.Count > 0)
                {
                    try
                    {
                        bool isClass = listObj[0].GetType().IsClass;
                        foreach (var item in listObj)
                        {
                            if (isClass)
                            {
                                MethodInfo mi = typeof(Converter).GetMethod("ConvertObject");
                                MethodInfo miConstructed = mi.MakeGenericMethod(typeof(TD));

                                object[] args = { item, null };
                                TD target = (TD)miConstructed.Invoke(null, args);

                                result.Add(target);
                            }
                            else
                            {
                                result.Add((TD)(object)item);
                            }
                        }
                    }
                    catch { }
                }
            }
            return result;
        }

        public static List<TD> ImportListFrom<TD>(this List<TD> listObj, DataTable data)
            where TD : class, new()
        {
            listObj = new List<TD>();
            if (data != null)
            {
                if (data.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow row in data.Rows)
                        {
                            TD obj = new TD();
                            foreach (DataColumn col in data.Columns)
                            {
                                PropertyInfo destProp = obj.GetType().GetProperty(col.ColumnName);
                                if (destProp != null)
                                {
                                    try
                                    {
                                        destProp.SetValue(obj, row[col]);
                                    }
                                    catch { }

                                }
                            }
                            listObj.Add(obj);
                        }

                    }
                    catch { }
                }
            }
            return listObj;
        }

        public static int ToInt(this string str)
        {
            int result = 0;
            try
            {
                int.TryParse(str, out result);
            }
            catch
            {
                result = 0;
            }

            return result;
        }

        public static bool ToBool(this string str)
        {
            bool result = false;
            try
            {
                str = str == "1" ? "true" : str;

                bool.TryParse(str, out result);
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public static DateTime ToDate(this string str, string format)
        {
            DateTime date = new DateTime();
            try
            {
                DateTime.TryParseExact(str, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
            }
            catch
            {
                return new DateTime(1, 1, 1);
            }

            return date;
        }

        public static bool IsSimpleType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // nullable type, check if the nested type is simple.
                return IsSimpleType(type.GetGenericArguments()[0]);
            }
            return type.IsPrimitive
              || type.IsEnum
              || type.Equals(typeof(string))
              || type.Equals(typeof(String));
        }

        public static string UnescapeCodes(this string src)
        {
            var rx = new Regex("\\\\([0-9A-Fa-f]+)");
            var res = new StringBuilder();
            var pos = 0;
            foreach (Match m in rx.Matches(src))
            {
                res.Append(src.Substring(pos, m.Index - pos));
                pos = m.Index + m.Length;
                res.Append((char)Convert.ToInt32(m.Groups[1].ToString(), 16));
            }
            res.Append(src.Substring(pos));
            return res.ToString();
        }
    }
}

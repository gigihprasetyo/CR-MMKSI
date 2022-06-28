using KTB.DNet.WebAPI.SMSGetway.Helpers.Attributes;
using KTB.DNet.WebAPI.SMSGetway.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace KTB.DNet.WebAPI.SMSGetway.Helpers
{

    public class JsonSerialize<T> : IJsonSerialize where T : class
    {
        public T NewT;
        public JsonSerialize(T obj)
        {
            this.NewT = obj;
        }

        public string Serialize()
        {
            string result = string.Empty;
            string headName = string.Empty;
            StringBuilder sb = new StringBuilder();
            Dictionary<int, string> dicObj = new Dictionary<int, string>();

            ChannelNamesAttribute chAtt = NewT.GetType().GetCustomAttribute<ChannelNamesAttribute>();
            if (chAtt != null) { headName = chAtt.Name; }

            Type objType = NewT.GetType();
            PropertyInfo[] objProp = objType.GetProperties();
            FieldInfo[] objField = objType.GetFields();

            foreach (FieldInfo iField in objField)
            {
                ChannelFieldAttribute att = iField.GetCustomAttribute<ChannelFieldAttribute>();
                if (att != null)
                {
                    if (iField.FieldType == typeof(Dictionary<int, IJsonSerialize>))
                    {
                        Dictionary<int, IJsonSerialize> dicChannel = (Dictionary<int, IJsonSerialize>)iField.GetValue(NewT);
                        if (dicChannel.Count == 0)
                        {
                            dicObj.Add(att.Order, "\"" + att.FieldName + "\":{" + string.Empty + "}" + ", ");
                            continue;
                        }

                        string rest = string.Empty;
                        try
                        {
                            foreach (KeyValuePair<int, IJsonSerialize> str in dicChannel.OrderBy(x => x.Key))
                            {
                                rest = rest + str.Value.Serialize() + ", ";
                            }
                        }
                        catch { }
                        rest = rest.Remove(rest.Length - 2, 2);

                        dicObj.Add(att.Order, "\"" + att.FieldName + "\":{" + rest + "}" + ", ");
                    }
                }
            }

            foreach (PropertyInfo iField in objProp)
            {
                ChannelFieldAttribute att = iField.GetCustomAttribute<ChannelFieldAttribute>();
                if (att != null)
                {
                    if (att.AlwaysAdd)
                    {
                        if (iField.GetValue(NewT).GetType() == typeof(string))
                        {
                            dicObj.Add(att.Order, "\"" + att.FieldName + "\":\"" + iField.GetValue(NewT).ToString() + "\"" + ", ");
                        }
                        else
                        {
                            dicObj.Add(att.Order, "\"" + att.FieldName + "\":" + iField.GetValue(NewT).ToString() + ", ");
                        }
                        
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(iField.GetValue(NewT).ToString()))
                        {
                            if (iField.GetValue(NewT).GetType() == typeof(string))
                            {
                                dicObj.Add(att.Order, "\"" + att.FieldName + "\":\"" + iField.GetValue(NewT).ToString() + "\"" + ", ");
                            }
                            else
                            {
                                dicObj.Add(att.Order, "\"" + att.FieldName + "\":" + iField.GetValue(NewT).ToString() + ", ");
                            }
                        }
                    }
                }
            }
            foreach (KeyValuePair<int, string> item in dicObj.OrderBy(x => x.Key))
            {
                sb.Append(item.Value);
            }
            string strValue = sb.ToString();

            if (strValue.Length > 2)
            {
                if (strValue.EndsWith(", "))
                {
                    strValue = strValue.Remove(strValue.Length - 2, 2);
                }
            }

            if (string.IsNullOrEmpty(headName))
            {
                result = "{" + strValue + "}";
            }
            else
            {
                result = " \"" + headName + "\":{" + strValue + "}";
            }

            return result;
        }
    }
}
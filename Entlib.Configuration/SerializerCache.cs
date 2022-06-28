//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Configuration Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Collections;
using System.Text;
using System.Xml.Serialization;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration
{
	internal class SerializerCache
	{
		private static Hashtable serializers = new Hashtable();
		private static object lockObj = new object();

		private SerializerCache()
		{
		}

		public static XmlSerializer GetSerializer(Type baseType, Type[] types)
		{
			
			StringBuilder sb = new StringBuilder();
		
			sb.Append(baseType.AssemblyQualifiedName);
			foreach (Type t in types)
			{
				sb.Append(t.AssemblyQualifiedName);
			}

			string key = sb.ToString();
			lock (lockObj)
			{
				if (serializers.Contains(key))
				{
					return (XmlSerializer)serializers[key];
				}
				XmlSerializer xmlSerializer = CreateXmlSerializer(baseType, types);
				serializers.Add(key, xmlSerializer);
				return xmlSerializer;
			}
		}

		private static XmlSerializer CreateXmlSerializer(Type valueType, Type[] types)
		{
			return new XmlSerializer(valueType, types);
		}
	}
}

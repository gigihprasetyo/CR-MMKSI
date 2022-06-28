﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KTB.DNet.Interface.Repository.Dapper.DNet.VehicleSales.SqlQuery.SAPCustomer {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SAPCustomerQuery {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SAPCustomerQuery() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("KTB.DNet.Interface.Repository.Dapper.DNet.VehicleSales.SqlQuery.SAPCustomer.SAPCu" +
                            "stomerQuery", typeof(SAPCustomerQuery).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT  
        ///	COUNT(1)
        ///FROM
        ///(
        ///    SELECT
        ///            c.ID AS DNetID,
        ///           c.InformationSource AS &apos;SumberData&apos;,
        ///		   c.CreatedBy AS &apos;CreatedBy&apos;,
        ///           CONVERT(VARCHAR(10), c.CreatedTime, 111) [CreateDate],
        ///           CONVERT(VARCHAR(10), c.CreatedTime, 112) [CreateDate_YYYYMMDD],
        ///           d.DealerCode,
        ///           d.DealerName,
        ///           c.Sequence,
        ///           CASE c.CustomerType
        ///               WHEN 0 THEN
        ///                   &apos;Perorangan&apos;
        ///               WHEN 1 THEN
        ///                   [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetTotalQuery {
            get {
                return ResourceManager.GetString("GetTotalQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT  
        ///	/**PagingIndexQuery**/  
        ///    SumberData,
        ///	CreatedBy,
        ///    CreateDate,
        ///    CreateDate_YYYYMMDD,
        ///    DealerCode,
        ///    DealerName,
        ///    CustomerTypeID,
        ///    CustomerType,
        ///    SalesmanCode,
        ///    CASE 
        ///	WHEN LEFT(IndModel, CHARINDEX(&apos; &apos;, IndModel,CHARINDEX(&apos; &apos;, IndModel)+1))=&apos;&apos; THEN
        ///		CASE 
        ///			WHEN (CreatedBy=&apos;SYS-CDP&apos;) THEN
        ///				CASE 
        ///					WHEN (LEN(CONVERT(VARCHAR, COALESCE(Sequence, 0)))&gt;4) THEN
        ///						CONCAT(
        ///							CampaignName,
        ///							&apos;-&apos;,
        ///							LEFT(CustomerName, 50 - LEN(CONCAT(Ca [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SelectQuery {
            get {
                return ResourceManager.GetString("SelectQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE SAPCustomer
        ///	SET 
        ///		SalesforceID									= ISNULL(@SalesforceID,SalesforceID),
        ///		DealerID  										= ISNULL(@DealerID,DealerID),
        ///		SalesmanHeaderID   								= ISNULL(@SalesmanHeaderID,SalesmanHeaderID),
        ///		VechileTypeID   								= ISNULL(@VechileTypeID,VechileTypeID),
        ///		CustomerCode   									= ISNULL(@CustomerCode,CustomerCode),
        ///		CustomerName   									= ISNULL(@CustomerName,CustomerName),
        ///		CustomerType   									= ISNULL(@CustomerType,CustomerType),
        ///		CustomerAddress   								= IS [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string UpdateQueue {
            get {
                return ResourceManager.GetString("UpdateQueue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE SAPCustomer
        ///	SET 
        ///		SalesforceID									= ISNULL(@SalesforceID,SalesforceID),
        ///		DealerID  										= ISNULL(@DealerID,DealerID),
        ///		SalesmanHeaderID   								= ISNULL(@SalesmanHeaderID,SalesmanHeaderID),
        ///		VechileTypeID   								= ISNULL(@VechileTypeID,VechileTypeID),
        ///		CustomerCode   									= ISNULL(@CustomerCode,CustomerCode),
        ///		CustomerName   									= ISNULL(@CustomerName,CustomerName),
        ///		CustomerType   									= ISNULL(@CustomerType,CustomerType),
        ///		CustomerAddress   								= IS [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string UpdateSAPCustomer {
            get {
                return ResourceManager.GetString("UpdateSAPCustomer", resourceCulture);
            }
        }
    }
}

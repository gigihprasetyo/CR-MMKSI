//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KTB.DNet.Interface.Repository.Dapper.NonDMS.SqlQuery.CRM_xts_businessunitinquiry {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class CRM_xts_businessunitinquiryQuery {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CRM_xts_businessunitinquiryQuery() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("KTB.DNet.Interface.Repository.Dapper.NonDMS.SqlQuery.CRM_xts_businessunitinquiry.CRM_xts_businessunitinquiryQuery" +
                            "", typeof(CRM_xts_businessunitinquiryQuery).Assembly);
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
        
        		internal static string SelectQuery {
            get {
                return ResourceManager.GetString("SelectQuery", resourceCulture);
            }
        }

		internal static string GetTotalQuery {
            get {
                return ResourceManager.GetString("GetTotalQuery", resourceCulture);
            }
        }

		internal static string GetCRM_xts_businessunitinquiryByID {
            get {
                return ResourceManager.GetString("GetCRM_xts_businessunitinquiryByID", resourceCulture);
            }
        }

        internal static string UpdateCRM_xts_businessunitinquiry
        {
            get
            {
                return ResourceManager.GetString("UpdateCRM_xts_businessunitinquiry", resourceCulture);
            }
        }

        internal static string InsertCRM_xts_businessunitinquiry
        {
            get
            {
                return ResourceManager.GetString("InsertCRM_xts_businessunitinquiry", resourceCulture);
            }
        }

    }
}

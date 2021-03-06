//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.VWI_EmployeeParts {
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
    internal class VWI_EmployeePartsQuery {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal VWI_EmployeePartsQuery() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.VWI_EmployeeParts.VWI_E" +
                            "mployeePartsQuery", typeof(VWI_EmployeePartsQuery).Assembly);
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
        ///   Looks up a localized string similar to SELECT COUNT(*) FROM
        ///	(SELECT SalesmanHeader.ID, SalesmanHeader.SalesmanCode, SalesmanHeader.Name, SalesmanHeader.PlaceOfBirth, SalesmanHeader.DateOfBirth, 
        ///		SalesmanHeader.Gender, SalesmanHeader.MarriedStatus, SalesmanHeader.Address, SalesmanHeader.City, SalesmanHeader.HireDate, 
        ///		SalesmanHeader.ResignDate, SalesmanHeader.ResignReason, SalesmanHeader.DealerId, Dealer.DealerCode,	SalesmanHeader.DealerBranchID, 
        ///		DealerBranch.DealerBranchCode,                
        ///		Status = CASE WHEN SalesmanHeader.RowStatus [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetTotalQuery {
            get {
                return ResourceManager.GetString("GetTotalQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 
        ///count(*)
        ///FROM
        ///	(SELECT DISTINCT SalesmanHeader.ID, SalesmanHeader.SalesmanCode, SalesmanHeader.Name, SalesmanHeader.PlaceOfBirth, SalesmanHeader.DateOfBirth, 
        ///		KTPProfile.ProfileValue AS NoKTP,
        ///		EmailProfile.ProfileValue AS Email,
        ///		NoHPProfile.ProfileValue AS NoHP,
        ///		SalesmanHeader.LastUpdateTime
        ///	FROM SalesmanHeader WITH ( NOLOCK )
        ///		JOIN Dealer WITH ( NOLOCK ) ON SalesmanHeader.DealerId = Dealer.ID AND Dealer.RowStatus = 0
        ///		LEFT JOIN DealerBranch WITH ( NOLOCK ) ON SalesmanHeader.Dea [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetTotalResignWithSpecificData {
            get {
                return ResourceManager.GetString("GetTotalResignWithSpecificData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT COUNT(*) FROM
        ///	(SELECT SalesmanHeader.ID, SalesmanHeader.SalesmanCode, SalesmanHeader.Name, SalesmanHeader.PlaceOfBirth, SalesmanHeader.DateOfBirth, 
        ///		SalesmanHeader.Gender, SalesmanHeader.MarriedStatus, SalesmanHeader.Address, SalesmanHeader.City, SalesmanHeader.HireDate, 
        ///		SalesmanHeader.ResignDate, SalesmanHeader.ResignReason, SalesmanHeader.DealerId, Dealer.DealerCode,	SalesmanHeader.DealerBranchID, 
        ///		DealerBranch.DealerBranchCode,                
        ///		Status = CASE WHEN SalesmanHeader.RowStatus [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetTotalWithProfile {
            get {
                return ResourceManager.GetString("GetTotalWithProfile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 
        ////**PagingIndexQuery**/ 
        ///	ID, SalesmanCode, Name, PlaceOfBirth, DateOfBirth, Gender, MarriedStatus, Address, City,	
        ///	HireDate, ResignDate, ResignReason, DealerId, DealerCode, 
        ///	DealerBranchID , DealerBranchCode, Status, LastUpdateTime
        ////**EndPagingIndexQuery**/
        ///FROM
        ///	(SELECT SalesmanHeader.ID, SalesmanHeader.SalesmanCode, SalesmanHeader.Name, SalesmanHeader.PlaceOfBirth, SalesmanHeader.DateOfBirth, 
        ///		SalesmanHeader.Gender, SalesmanHeader.MarriedStatus, SalesmanHeader.Address, SalesmanHeader.Ci [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SelectQuery {
            get {
                return ResourceManager.GetString("SelectQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 
        ////**PagingIndexQuery**/ 
        ///	ID as SalesmanID, SalesmanCode, Name, PlaceOfBirth, DateOfBirth, NoKTP, LastUpdateTime
        ////**EndPagingIndexQuery**/
        ///FROM
        ///	(SELECT DISTINCT SalesmanHeader.ID, SalesmanHeader.SalesmanCode, SalesmanHeader.Name, SalesmanHeader.PlaceOfBirth, SalesmanHeader.DateOfBirth, 
        ///		KTPProfile.ProfileValue AS NoKTP,
        ///		EmailProfile.ProfileValue AS Email,
        ///		NoHPProfile.ProfileValue AS NoHP,
        ///		SalesmanHeader.LastUpdateTime
        ///	FROM SalesmanHeader WITH ( NOLOCK )
        ///		JOIN Dealer WITH ( NOLOC [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SelectTotalResignSpecificFields {
            get {
                return ResourceManager.GetString("SelectTotalResignSpecificFields", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 
        ////**PagingIndexQuery**/ 
        ///	ID, SalesmanCode, Name, PlaceOfBirth, DateOfBirth, Gender, MarriedStatus, Address, City, 
        ///	SalesmanLevelID, JobPositionId, LeaderId, LeaderSalesmanCode, LeaderSalesmanName, 
        ///	HireDate, ResignDate, ResignReason, DealerId, DealerCode, 
        ///	DealerBranchID , DealerBranchCode, Status, SalesmanAreaDesc, SalesmanLevelDesc, JobPositionDesc,
        ///    NoKTP, Email, NoHP, Kategori, Pendidikan, LastUpdateTime, StatusDNET
        ////**EndPagingIndexQuery**/
        ///FROM
        ///	(SELECT DISTINCT SalesmanHeader.I [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SelectTotalResignWithProfile {
            get {
                return ResourceManager.GetString("SelectTotalResignWithProfile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 
        ////**PagingIndexQuery**/ 
        ///	ID, SalesmanCode, Name, PlaceOfBirth, DateOfBirth, Gender, MarriedStatus, Address, City,	
        ///	HireDate, ResignDate, ResignReason, DealerId, DealerCode, 
        ///	DealerBranchID , DealerBranchCode, Status, 
        ///	SalesmanCategoryLevelId,
        ///    PositionCode,
        ///    PositionName,
        ///    ParentSalesmanCategoryLevelId,
        ///    ParentPositionCode,
        ///    ParentPositionName,
        ///	NoKTP, Email, NoHP, Kategori, Pendidikan, LastUpdateTime, StatusDNET
        ////**EndPagingIndexQuery**/
        ///FROM
        ///	(SELECT SalesmanHeader. [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SelectWithProfile {
            get {
                return ResourceManager.GetString("SelectWithProfile", resourceCulture);
            }
        }
    }
}
